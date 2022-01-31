using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;
using urfurniture.DAL.ViewModel;
using urfurniture.Models;

namespace urfurniture.DAL.Repository
{
    public interface IManageOrder
    {

        List<TblOrderStatusCode> GetOrderStatusCode();
        Task<Tuple<bool>> AddPaymentMethodCode(string paymentmethodcode);
        Task<Tuple<bool>> UpdatePaymentMethodCode(int id, string paymentmethodcode);
        Task<Tuple<bool>> DeletePaymentMethodCode(int id);
        Task<dynamic> GetPaymentMethodCode();

        Task<Tuple<bool>> AddOrderStatusCode(string orderstatuscode);
        Task<Tuple<bool>> UpdateOrderStatusCode(int id, string orderstatuscode);
        Task<Tuple<bool>> DeleteOrderStatusCode(int id);



        Task<Tuple<long, string>> CreateOrder(OrderModel model);
        Task<Tuple<bool>> CancelOrder(int orderid);
        Task<List<OrderDto>> GetUserOrder(int userid, string filepath);
        OrderVM GetAllOrder(int pageno, int pagesize, int orderstatus);
        Task<Tuple<bool>> UpdateOrderStatus(long orderid, int statuscode);
        //Generate Invoice

        Task<InvoiceModel> GetInvoice(long orderid, string filepath);
        //Task<int> GetDeliveryCharge(string Destination);


        // Task<Tuple<int,bool>> CreateOrder(OrderModel model);
        //Task<Tuple<bool>> UpdateCateogory();
        //Task<dynamic> GetOrderStatuCcode();

        //Task<Tuple<bool>> DeleteCategory(int id);
        //Task<Tuple<bool>> UpdateCateogory();

    }
}
