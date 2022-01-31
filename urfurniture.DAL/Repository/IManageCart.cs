using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;
using urfurniture.Models;

namespace urfurniture.DAL.Repository
{
 public   interface IManageCart
    {
        Task<Tuple<bool>> BulkAddToCart(List<CartModel> cartModels, int shoppingCartID);
        Task<Tuple<bool>> AddProduct(CartModel model);
        Task<Tuple<bool>> RemoveProduct(CartModel model);
        Task<List<CartItemModel>> GetCartItem(long shoppingcartid, string filepath);
        Task<Tuple<bool>> SaveForLater(CartModel model);
        Task<Tuple<bool>> QuantityUpdate(CartModel model);


    }
}
