using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using urfurniture.DAL.Data;
using urfurniture.DAL.Entities;
using urfurniture.DAL.Repository;
using urfurniture.DAL.ViewModel;
using urfurniture.Models;
using static urfurniture.Models.InvoiceModel;

namespace urfurniture.DAL.Implementation
{    
    public class ManageOrderService : IManageOrder 
    {
        private readonly urfurnitureContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ManageOrderService(urfurnitureContext dbcontext,IMapper mapper, IConfiguration configuration)
        {           
            _dbcontext = dbcontext; 
            _mapper = mapper;
            _configuration = configuration;

        }
        
        public async Task<Tuple<bool>> AddPaymentMethodCode(string paymentmethodname)
        {
            TblUserPaymentMethodsCode paymentcode = new TblUserPaymentMethodsCode
            {
                PaymentMethodCodeDesc = paymentmethodname,
                IsActive = true
            };

            await _dbcontext.TblUserPaymentMethodsCodes.AddAsync(paymentcode);
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<Tuple<bool>> UpdatePaymentMethodCode(int id, string paymentmethodname)
        {
            var paymentcode = await _dbcontext.TblUserPaymentMethodsCodes.FirstOrDefaultAsync(x => x.IsActive == true && x.PaymentMethodCode == id);
            paymentcode.PaymentMethodCodeDesc = paymentmethodname;
            _dbcontext.Entry(paymentcode).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<Tuple<bool>> DeletePaymentMethodCode(int id)
        {
            var paymentcode = await _dbcontext.TblUserPaymentMethodsCodes.FirstOrDefaultAsync(x => x.IsActive == true && x.PaymentMethodCode == id);
            paymentcode.IsActive = false;
            _dbcontext.Entry(paymentcode).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<object> GetPaymentMethodCode()
        {
            var paymentcode = await _dbcontext.TblUserPaymentMethodsCodes.Select(x => new { x.PaymentMethodCode, x.PaymentMethodCodeDesc }).ToListAsync();
            return paymentcode;
        }
        //orderstatus
        public async Task<Tuple<bool>> AddOrderStatusCode(string orderstatusname)
        {
            TblOrderStatusCode orderStatuscode = new TblOrderStatusCode
            {
                OrderStatusDesc = orderstatusname,
                IsActive = true
            };

            await _dbcontext.TblOrderStatusCodes.AddAsync(orderStatuscode);
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<Tuple<bool>> UpdateOrderStatusCode(int id, string orderstatusname)
        {
            var orderStatuscode = await _dbcontext.TblOrderStatusCodes.FirstOrDefaultAsync(x => x.IsActive == true && x.StatusCodeId == id);
            orderStatuscode.OrderStatusDesc = orderstatusname;
            _dbcontext.Entry(orderStatuscode).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<Tuple<bool>> DeleteOrderStatusCode(int id)
        {
            var orderStatuscode = await _dbcontext.TblOrderStatusCodes.FirstOrDefaultAsync(x => x.IsActive == true && x.StatusCodeId == id);
            orderStatuscode.IsActive = false;
            _dbcontext.Entry(orderStatuscode).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }       
        public async Task<Tuple<long,string>> CreateOrder(OrderModel model)
        {
            

            long orderid=0;
            float total_amount = 0;
            foreach(var item in model.OrderDetails)
            {
                var price = _dbcontext.TblProducts.FirstOrDefault(p => p.ProductId == item.ProductRefId).Price;
                price += _dbcontext.TblProductOptions.FirstOrDefault(po => po.ProductOptionId == item.ProductOptionId).OptionPriceIncrement;
               total_amount+=price * item.Quantity;
            }
            if (model.Token != null)
            {
                total_amount += model.DeliveryCharges;
                var finalAmount = Math.Round(total_amount,2);
                Charge user = await ChargeUser(model.Token, finalAmount);
                if (user.Status == "succeeded")
                {
                    TblOrder order = new TblOrder
                    {
                        UserRefId = model.UserRefId,
                        IsActive = true,
                        DeliveryCharges = model.DeliveryCharges,
                        Chargeid=user.Id
                    };
                    await _dbcontext.TblOrders.AddAsync(order);
                    await _dbcontext.SaveChangesAsync();
                    orderid = order.OrderId;
                    foreach(var orderDetail in model.OrderDetails)
                    {
                        var price = _dbcontext.TblProducts.FirstOrDefault(p => p.ProductId == orderDetail.ProductRefId).Price;
                        price += _dbcontext.TblProductOptions.FirstOrDefault(po => po.ProductOptionId == orderDetail.ProductOptionId).OptionPriceIncrement;
                        price = price * orderDetail.Quantity;
                        TblOrderItemDetails orderdetails = new TblOrderItemDetails
                        {
                            ProductRefId = orderDetail.ProductRefId,
                            TotalAmount =price,
                            CreateDate = DateTime.Now,
                            PaymentStatus = true,
                            IsCancel = false,
                            Quantity = orderDetail.Quantity,
                            Isactive = true,
                            ProductOptionId = orderDetail.ProductOptionId,
                            OrderRefId = orderid,
                            OrderStatusRefId = 5
                        };
                        await _dbcontext.TblOrderItemDetails.AddAsync(orderdetails);
                        await _dbcontext.SaveChangesAsync();
                        await Sku(orderDetail.ProductRefId, -1);
                    }
                        
                    var shoppincarid = _dbcontext.TblUserShoppingCarts.FirstOrDefault(sc => sc.UserRefId == model.UserRefId).ShoppingCartID;
                    var cartitems = await _dbcontext.TblCartItems.Where(c => c.UserShoppingCartRefId == shoppincarid && c.IsActive == true).ToListAsync();
                    foreach (var product in cartitems)
                    {
                        product.IsActive = false;
                        _dbcontext.Update(product);
                    }
                    if (cartitems.Count() > 0)
                        _dbcontext.SaveChanges();
                    //end
                }

                return new Tuple<long, string>(orderid, user.Status);
            }
                return new Tuple<long, string>(orderid, "false");
            
            }
        private async Task<Charge> ChargeUser(string token, double amount)
            {
            StripeConfiguration.ApiKey = _configuration["StripeSecretKey"];
            var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt64(amount),
                    Currency = "gbp",
                    Source = token,
                    Description = "My First Test Charge (created for API docs)",
                };
                var service = new ChargeService();
                var charges = await service.CreateAsync(options);
                return charges;
             

        }
        private async Task<bool> Sku(long productid,int quantity)
        {
            var product = await _dbcontext.TblProducts.FirstOrDefaultAsync(p => p.ProductId == productid);
            
            product.Stock += quantity;
            if (product.Stock <= 0)
            {
                product.IsActive = false;
            }
            _dbcontext.Entry(product).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<List<OrderDto>> GetUserOrder(int userid, string filepath)
        {
            var order = await _dbcontext.TblOrders.Where(x => x.UserRefId == userid)
                .Join(_dbcontext.TblOrderItemDetails, order => order.OrderId, item => item.OrderRefId,
                (order, item) => new { order, item })
                .Join(_dbcontext.TblProducts, orderItem => orderItem.item.ProductRefId, product => product.ProductId,
                (orderItem, Product) => new { orderItem, Product }).Join(_dbcontext.TblOrderStatusCodes,
                productOrderItem => productOrderItem.orderItem.item.OrderStatusRefId, status => status.StatusCodeId,
                (productOrderItem, status) => new OrderDto
                {
                    OrderId=productOrderItem.orderItem.order.OrderId,
                    ProductRefId = productOrderItem.orderItem.item.ProductRefId,
                    Name = productOrderItem.Product.Name,
                    Description = productOrderItem.Product.Description,
                    Price = productOrderItem.Product.Price,
                    Image=filepath + productOrderItem.Product.ImageUrl,
                    CreateDate = productOrderItem.orderItem.item.CreateDate,
                    ProductOptionRefId = productOrderItem.orderItem.item.ProductOptionId, 
                    Quantity = productOrderItem.orderItem.item.Quantity,
                    DeliveryCharges=productOrderItem.orderItem.order.DeliveryCharges,
                    TotalAmount = productOrderItem.orderItem.item.TotalAmount,
                    PaymentStatus = productOrderItem.orderItem.item.PaymentStatus,
                    OrderStatus = status.OrderStatusDesc
                }).ToListAsync();

            return order;
            
        }
        public OrderVM GetAllOrder(int PageNumber, int PageSize, int orderstatus)
        {
            OrderVM Orders = new OrderVM();

           using(SqlConnection _conn=new SqlConnection(_dbcontext.Database.GetConnectionString()))
            {
                using(var cmd=new SqlCommand("sp_getallorders", _conn))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Page", PageNumber));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Size", PageSize));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@orderstatus", orderstatus));
                    adapter.Fill(ds);
                    List<OrderDto> orders = new List<OrderDto>();
                    var data = ds.Tables[0].Rows;
                    for(int i=0;i<data.Count;i++ )
                    {
                        orders.Add(new OrderDto()
                        {
                            OrderId = long.Parse(data[i]["OrderId"].ToString()),
                            ProductRefId = int.Parse(data[i]["ProductRefId"].ToString()),
                            OrderItemId = int.Parse(data[i]["OrderItemId"].ToString()),
                            Name = data[i]["Name"].ToString(),
                            Description = data[i]["Description"].ToString(),
                            Price = float.Parse(data[i]["Price"].ToString()),
                            Image = data[i]["Image"].ToString(),
                            CreateDate = DateTime.Parse(data[i]["CreateDate"].ToString()),
                            ProductOptionRefId = int.Parse(data[i]["ProductOptionRefId"].ToString()),
                            Quantity = int.Parse(data[i]["Quantity"].ToString()),
                            DeliveryCharges= int.Parse(data[i]["DeliveryCharges"].ToString()),
                            TotalAmount = float.Parse(data[i]["TotalAmount"].ToString()),
                            PaymentStatus = bool.Parse(data[i]["PaymentStatus"].ToString()),
                            OrderStatus = data[i]["OrderStatus"].ToString(),
                            OrderStatusId = int.Parse(data[i]["OrderStatusId"].ToString()),
                            UserId = int.Parse(data[i]["UserId"].ToString()),
                            FirstName = data[i]["FirstName"].ToString(),
                            Email = data[i]["Email"].ToString(),
                            Total= long.Parse(data[i]["Total"].ToString())
                        });
                    }
                    ds.Dispose();
                    Orders.Orders = orders;
                    Orders.count =orders.Count()>0? orders[0].Total:0;
                }
            }   
            return Orders;
        }
        public async Task<Tuple<bool>> CancelOrder(int orderid)
        {
            var order = await _dbcontext.TblOrders.FirstOrDefaultAsync(o => o.OrderId == orderid);
            order.IsActive = false;
            var products = await _dbcontext.TblOrderItemDetails.Where(o => o.OrderRefId == orderid).ToListAsync();
            foreach (var item in products)
            {
                await Sku(item.ProductRefId, +1);
            }
            _dbcontext.Entry(order).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
            }
        public async Task<Tuple<bool>> UpdateOrderStatus(long orderitemid,int statuscode)
        {
            var orderstatus = await _dbcontext.TblOrderItemDetails.FirstOrDefaultAsync(o => o.OrderItemDetailId == orderitemid);
            orderstatus.OrderStatusRefId = statuscode;
            _dbcontext.Entry(orderstatus).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<InvoiceModel> GetInvoice(long orderid, string filepath)
        {          
            var orderProduct = await _dbcontext.TblOrderItemDetails.Where(op => op.OrderRefId == orderid).Join(
                _dbcontext.TblProducts, item => item.ProductRefId, Product => Product.ProductId, (item, Product) => new ProductOrderDetails
                {
                    Name = Product.Name + _dbcontext.TblProductOptions.FirstOrDefault(op => op.ProductOptionId == item.ProductOptionId).OptionName.ToString(),
                    Quantity = item.Quantity,
                    Price = Product.Price + _dbcontext.TblProductOptions.FirstOrDefault(op => op.ProductOptionId == item.ProductOptionId).OptionPriceIncrement,
                    Discount = Product.Discount,
                    Tax = 3,
                    LineTotal = Product.Price + (Product.Price * 3) / 100 - (Product.Price * Product.Discount) / 100                                     
                }
                ).ToListAsync();
              var userdetails = _dbcontext.TblOrders.Where(o => o.OrderId == orderid).Join
                (_dbcontext.TblUserAddresses, order => order.UserRefId, useraddress => useraddress.UserRefId, (order, useraddress) => new { order, useraddress }
                ).Join(_dbcontext.TblUsers, orderaddress => orderaddress.order.UserRefId, user => user.UserId, (orderaddress, user) => new Userdetails
                {
                    City = orderaddress.useraddress.City,
                    State = orderaddress.useraddress.State,
                    Address1 = orderaddress.useraddress.Address1,
                    Address2 = orderaddress.useraddress.Address2,
                    Zipcode = orderaddress.useraddress.Pincode,
                    Landmark = orderaddress.useraddress.Landmark,
                    Name = user.FirstName,
                    Email = user.Email,
                    PhoneNo = user.MobileNo
                }
                ).FirstOrDefault();
            InvoiceModel invoice = new InvoiceModel(orderProduct, userdetails);
            invoice.LogoImage = System.IO.Path.Combine(filepath, "Upload/logo.png");
            invoice.OrderDate = _dbcontext.TblOrderItemDetails.FirstOrDefault(o => o.OrderRefId == orderid).CreateDate.ToString("dd/MM/yyyy");
            invoice.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
            invoice.InvoiceNo = $"URF{_dbcontext.TblOrderItemDetails.FirstOrDefault(o => o.OrderRefId == orderid).OrderItemDetailId}";
            invoice.ShippingCharge = _dbcontext.TblOrders.FirstOrDefault(o => o.OrderId == orderid).DeliveryCharges;
            invoice.Subtotal += invoice.ShippingCharge;
            invoice.AmountDue = (_dbcontext.TblOrderItemDetails.FirstOrDefault(o => o.OrderRefId == orderid).PaymentStatus) ? 0 : invoice.Subtotal;
            return invoice;
        }
        public List<TblOrderStatusCode> GetOrderStatusCode()
        {
            var status = _dbcontext.TblOrderStatusCodes.Where(x => x.IsActive == true).ToList();
            status.ForEach(x => x.UpdateTime = null);
            return status;
        }
    }
}
