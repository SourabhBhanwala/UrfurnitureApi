using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;
using urfurniture.Models;

namespace urfurniture.DAL.Repository
{
  public  interface IManageAccount
    { 
        
        Task<AuthenticationResultModel> RegisterAsync(TblUser entity);
        Task<AuthenticationModel> LoginAsync(TokenRequestModel user);
        Task<Tuple<bool,string>> ForgetPassword(string email);
        Task<Tuple<bool>> ChangePassword(string email, string otp, string password);
        Task<Tuple<bool>> ConfirmEmail(int userid, string otp);

        RefreshTokenReqDTOs GenerateRefreshToken(RefreshTokenReqDTOs item);
        //Task<Tuple<bool>> DeleteCategory(int id);
        //Task<Tuple<bool>> UpdateCateogory();

        //Task<JwtSecurityToken> CreateJwtToken(TblUser user);
    }
}
