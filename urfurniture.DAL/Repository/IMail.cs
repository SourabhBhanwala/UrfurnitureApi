using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;

namespace urfurniture.DAL.Repository
{
    public interface IMail
    { 
        Task<Tuple<bool,string>> SendConfirmationEmailAsync(TblUser user);
        Task SendWelcomeEmailAsync(string username,string email);
        Task<Tuple<bool, string>> SendForgetPasswordEmail( TblUser user);
    }
}
