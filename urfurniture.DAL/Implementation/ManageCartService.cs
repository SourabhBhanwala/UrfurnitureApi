using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Data;
using urfurniture.DAL.Entities;
using urfurniture.DAL.Repository;
using urfurniture.Models;

namespace urfurniture.DAL.Implementation
{ 
  public  class ManageCartService: IManageCart
    {
        private readonly urfurnitureContext _dbcontext;
        //private readonly IMapper _mapper;
        public ManageCartService(urfurnitureContext dbcontext)/*, IMapper mapper)*/
        {
             
            _dbcontext = dbcontext;
            //_mapper = mapper;

        }

       public async Task<Tuple<bool>> AddProduct(CartModel model)
            {
             
            var checkproductexist = await _dbcontext.TblCartItems.FirstOrDefaultAsync(x => x.IsActive == true && 
            x.UserShoppingCartRefId == model.ShoppingCartID && x.ProductRefId == model.ProductId&&x.ProductOptionRefId==model.OptionId);
            if (checkproductexist != null)
            {
                checkproductexist.Quantity += 1;
                _dbcontext.Entry(checkproductexist).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

            }
            else
            {
                TblCartItem cartItem = new TblCartItem
                {
                    UserShoppingCartRefId = model.ShoppingCartID,
                    ProductRefId = model.ProductId,
                    TimeAdded = DateTime.Now,
                    IsActive = true,
                    Quantity=model.Quantity,
                    ProductOptionRefId=model.OptionId
                    

                };
                await _dbcontext.TblCartItems.AddAsync(cartItem);
                await _dbcontext.SaveChangesAsync();
            }
                return new Tuple<bool>(true);

            
            }
        public async Task<Tuple<bool>> BulkAddToCart(List<CartModel> cartModels, int shoppingCartID)
        {
            if (cartModels.Count == 0)
                return new Tuple<bool>(false);
            foreach (var cartItem in cartModels)
            {
                var productExistInCart = await _dbcontext.TblCartItems.FirstOrDefaultAsync(x => x.IsActive == true &&
              x.UserShoppingCartRefId == shoppingCartID && x.ProductRefId == cartItem.ProductId&&x.ProductOptionRefId==cartItem.OptionId);
                if (productExistInCart != null)
                {
                    productExistInCart.Quantity += 1;
                    _dbcontext.Entry(productExistInCart).State = EntityState.Modified;
                    await _dbcontext.SaveChangesAsync();
                }
                else
                {
                    TblCartItem cart = new TblCartItem
                    {
                        UserShoppingCartRefId = shoppingCartID,
                        ProductRefId = cartItem.ProductId,
                        TimeAdded = DateTime.Now,
                        IsActive = true,
                        Quantity = cartItem.Quantity,
                        ProductOptionRefId = cartItem.OptionId
                    };
                    await _dbcontext.TblCartItems.AddAsync(cart);
                    await _dbcontext.SaveChangesAsync();
                }
            }
            return new Tuple<bool>(true);
        }


        public async Task<Tuple<bool>> RemoveProduct(CartModel model)
        {
            var product = await _dbcontext.TblCartItems.FirstOrDefaultAsync(x => x.ProductRefId == model.ProductId && x.UserShoppingCartRefId == model.ShoppingCartID
            &&x.IsActive==true);
                product.IsActive = false;
                _dbcontext.Entry(product).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                return new Tuple<bool>(true);
            
            }
        public async Task<List<CartItemModel>> GetCartItem(long shoppingcartid, string filepath)
        {

            var cartitems =await _dbcontext.TblCartItems.Where(c=>c.UserShoppingCartRefId==shoppingcartid&&c.IsActive==true).Join(_dbcontext.TblProducts, o => o.ProductRefId, i => i.ProductId,
                (o, i) => new CartItemModel
                {
                    ProductId=o.ProductRefId,
                    CartId=o.CartId,
                    ProductName=i.Name, 
                    Quantity=o.Quantity,
                    ProductImage= System.IO.Path.Combine(filepath ,i.ImageUrl),
                    Price=i.Price+_dbcontext.TblProductOptions.FirstOrDefault(op=>op.ProductOptionId==o.ProductOptionRefId).OptionPriceIncrement,
                    OptionId=o.ProductOptionRefId
                    
                }).ToListAsync();
          
            if (cartitems != null)
                return cartitems;
            else return new List<CartItemModel> { };
        }
        public async Task<Tuple<bool>> SaveForLater(CartModel model)
        {
          
                var product = await _dbcontext.TblCartItems.FirstOrDefaultAsync(x => x.ProductRefId == model.ProductId && x.UserShoppingCartRefId == model.ShoppingCartID);
                product.SaveForLater = true;
                _dbcontext.Entry(product).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                return new Tuple<bool>(true);
            
        }

        public async Task<Tuple<bool>> QuantityUpdate(CartModel model)
        {
            var product = await _dbcontext.TblCartItems.FirstOrDefaultAsync
                (x => x.ProductRefId == model.ProductId && x.UserShoppingCartRefId == model.ShoppingCartID
            && x.IsActive == true);
            product.Quantity = model.Quantity;
            _dbcontext.Entry(product).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }

    }
}
