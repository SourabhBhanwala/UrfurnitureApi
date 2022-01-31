using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;
using urfurniture.Models;

namespace urfurniture.DAL.Repository
{
  public  interface IManageUser
    {
        List<TblUser> Users(int pageno,int pagesize, out int total);
        //user details
        Task<Tuple<bool>> UpdateUserDetails(UserDetailsModel user);
        Task<UserDetailsModel> GetUserDetails(int userid);
        //user address
        Task<Tuple<bool>> AddUserAddress(UserAddressModel userAddress);
        Task<Tuple<bool>> DeleteAddress(int userid);
        Task<Tuple<bool>> UpdateAddress(UserAddressModel userAddress);
        Task<List<UserAddressModel>> GetAddress(int userid);
        Task<TblUser> getUserInfo(int userid);
        bool checkPostCode(string postalcode);
        //User Order
        //Task<Tuple<bool>> GetUserOrders(int userid);




        //Task<Tuple<bool>> AddUserAddress(TblUserAddress userAddress);
        //Task<Tuple<bool>> DeleteAddress(int userid);
        //Task<Tuple<bool>> UpdateAddress(UserAddressModel userAddress);
        //Task<UserAddressModel> GetAddress(int userid);

    }
}
