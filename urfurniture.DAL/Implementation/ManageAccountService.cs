using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Data;
using urfurniture.DAL.Entities;
using urfurniture.DAL.Repository;
using urfurniture.Models;

namespace urfurniture.DAL.Implementation
{
    public class ManageAccountService : IManageAccount
    {
        private readonly urfurnitureContext _dbcontext;
        private readonly JwtModel _jwtSettings;
        private readonly IMail _mailService; 
        private readonly IMapper _mapper;
        private  readonly ITokenService _tokenService;
        public ManageAccountService(IOptions<JwtModel> jwtSettings,
        urfurnitureContext dbcontext, IMail mailService, IMapper mapper,ITokenService tokenService)
        {
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
            _dbcontext = dbcontext;
            _mailService = mailService;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResultModel> RegisterAsync(TblUser entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FirstName))
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "First name is required" },
                    Success = false
                };
            }
            if (string.IsNullOrWhiteSpace(entity.Password))
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "Password is required" },
                    Success = false
                };
            }
            if (string.IsNullOrWhiteSpace(entity.Email))
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "E-mail is required" },
                    Success = false
                };
            }
            if (string.IsNullOrWhiteSpace(entity.MobileNo))
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "MobileNo is required" },
                    Success = false
                };
            }
            var isEmailExist = await _dbcontext.TblUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == entity.Email.ToLower());
            if (isEmailExist != null)
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "E-mail already register" },
                    Success = false
                };
            }
            if (string.IsNullOrWhiteSpace(entity.MobileNo))
            {
                return new AuthenticationResultModel
                {
                    Message = new string[] { "Mobile no is required" },
                    Success = false
                };
            }

            
            entity.AddedDate = DateTime.Now;
            object userId=new object();
            //add user to database
          
            
                 userId = await _dbcontext.TblUsers.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
            

            //creating shoppingcart for user
            TblUserShoppingCart shoppingCart = new TblUserShoppingCart
            {
                UserRefId = entity.UserId,
                CreatedDate = DateTime.Now,
                IsActive=true
            };
            await _dbcontext.TblUserShoppingCarts.AddAsync(shoppingCart);
            await _dbcontext.SaveChangesAsync();

            //sending email confirmation link
            var emailstatus=await _mailService.SendConfirmationEmailAsync(entity);             
            
            if (userId == null)
            {
                return new AuthenticationResultModel
                {
                    Message = new[] { "User Not Created" }
                };
            }
            return new AuthenticationResultModel
            {   
                Message=new [] {emailstatus.Item2},
                UserId = entity.UserId.ToString(),
                Success = true
            };
        }
      
        public async Task<AuthenticationModel> LoginAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var isemailvaild = await _dbcontext.TblUsers.FirstOrDefaultAsync(e => e.Email == model.Email);
            if (isemailvaild is null)
            {
                return new AuthenticationModel
                {
                    Message = $"No Accounts Registered with {model.Email}. ",
                    Success = false

                };
            }
            var user = await _dbcontext.TblUsers.FirstOrDefaultAsync(e => e.Email == model.Email && e.Password == model.Password);
            if (user is null)
            {                
                return new AuthenticationModel
                {
                    Message = "Email Password Not Matched ",
                    Success = false

                };
            }
            if (!user.IsActive)
            {
                return new AuthenticationModel
                {
                    Message = $"Activate your accounts from {model.Email}",
                    Success = false
                };
            }
         
                var roles = await _dbcontext.TblRoles.SingleOrDefaultAsync(x => x.RoleId == user.RoleId);
                var roleClaims = roles.Role;
                var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role",roleClaims),
                new Claim("uid", user.UserId.ToString())                
            };
                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _dbcontext.Update(user);
                _dbcontext.SaveChanges();
                // authenticationModel.IsAuthenticated = true;
                authenticationModel.Token = accessToken;
                authenticationModel.RefreshToken = refreshToken;
                authenticationModel.Email = user.Email;
                authenticationModel.Success = true;
                authenticationModel.UserName = user.FirstName;
                authenticationModel.Roles = roles.Role;
                var userCart = _dbcontext.TblUserShoppingCarts.FirstOrDefault(x => x.UserRefId == user.UserId);
                authenticationModel.ShoppingCartID =userCart is null?0:userCart.ShoppingCartID;             
                return authenticationModel;         
        }       

        public async Task<Tuple<bool,string>> ForgetPassword(string Email)
        {
            var email = await _dbcontext.TblUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == Email.ToLower());
            if (email == null)
            {  
                return new Tuple<bool, string>(false, $"No Account Registered With This {email}");
            }

            var emailstatus = await _mailService.SendForgetPasswordEmail(email);
            return new Tuple<bool, string>(emailstatus.Item1,emailstatus.Item2);
        }
        public async Task<Tuple<bool>> ChangePassword(string email, string otp, string password)
        {
            //otp vaildation
            var vailduser = await _dbcontext.TblOtpConfirmations.FirstOrDefaultAsync(x => x.IsActive == true && x.Email.ToLower() == email.ToLower() && x.Otp == otp);
            if (vailduser != null)
            {
                vailduser.IsActive = false;
                _dbcontext.Entry(vailduser).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                

                var user = await _dbcontext.TblUsers.FirstOrDefaultAsync(x => x.UserId == vailduser.UserId && x.IsActive == true);
                user.Password = password;
                _dbcontext.Entry(user).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                return new Tuple<bool>(true);
            }
            else
            {
                return new Tuple<bool>(false);
            }
        }
       
        public async Task<Tuple<bool>> ConfirmEmail(int userid, string otp)
        {
            //otp vaildation
            var user = await _dbcontext.TblOtpConfirmations.FirstOrDefaultAsync(x => x.IsActive == true && x.UserId == userid && x.Otp == otp);
            if (user != null)
            {
                //Update user status
                var updateuser = await _dbcontext.TblUsers.FirstOrDefaultAsync(x => x.UserId == userid);
                updateuser.IsActive = true;
                _dbcontext.Entry(updateuser).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                //update otp status
                user.IsActive = false;
                _dbcontext.Entry(user).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                
                //send welcome email
               await _mailService.SendWelcomeEmailAsync(updateuser.Email,updateuser.FirstName);
                
                return new Tuple<bool>(true);
            }
            else
            {
                return new Tuple<bool>(false);
            }
        }

        public RefreshTokenReqDTOs GenerateRefreshToken(RefreshTokenReqDTOs item)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(item.Token);
            var userid = int.Parse(principal.FindFirst("uid").Value);
            var user = _dbcontext.TblUsers.FirstOrDefault(x => x.UserId == userid);
            if (user == null || user.RefreshToken != item.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null;
            }
            var roles = _dbcontext.TblRoles.FirstOrDefault(x => x.RoleId == user.RoleId);
            var claims = new[]
         {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role",roles.Role),
                new Claim("uid", user.UserId.ToString())
            };
            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _dbcontext.Update(user);
            _dbcontext.SaveChanges();
            return new RefreshTokenReqDTOs()
            {
                Token = newAccessToken,
                RefreshToken=newRefreshToken
            };
        }
    }
}
