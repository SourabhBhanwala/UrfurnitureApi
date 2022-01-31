using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using urfurniture.DAL.Entities;
using urfurniture.DAL.Repository;
using urfurniture.Helper;
using urfurniture.Models;

namespace urfurniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IManageAccount _accountService;
        private readonly ITokenService _tokenService;
        public ManageAccountController(IMapper mapper, IManageAccount accountService,ITokenService tokenService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] UserModel request)
        { 
            var user = _mapper.Map<TblUser>(request);
            user.RoleId = UserRoles.User;
            string FilePath = Environment.CurrentDirectory;
            var authResponse = await _accountService.RegisterAsync(user);
            if (!authResponse.Success)
            {
                return Ok(new AuthenticationResultModel
                {
                    Success=false,
                    Message = authResponse.Message
                });
            }
            else
            {
                return Ok(new AuthenticationResultModel
                {
                    UserId = authResponse.UserId,
                    Success = authResponse.Success
                });
            }
        }
       
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] TokenRequestModel request)
       {
            var authResponse = await _accountService.LoginAsync(request);
            if (!authResponse.Success)
            {
                return Ok(new AuthenticationResultModel
                {
                    Success=false,
                    Message = new [] { authResponse.Message }
                });
            }


            else
            {
                return Ok(authResponse);
            }
        }
        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenReqDTOs request)
        {
            if (request is null)
            {
                return BadRequest("Invalid client request");
            }
           
           /*request.Token= JsonSerializer.Deserialize<string>(request.Token);
            request.RefreshToken = JsonSerializer.Deserialize<string>(request.RefreshToken);*/
            var response = _accountService.GenerateRefreshToken(request);
            return Ok(response);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail( int userid ,string otp)
        {
            var otpresponce = await _accountService.ConfirmEmail(userid,otp);
            
                return Ok("Email is confirmed ");
        }


        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            string FilePath = Directory.GetCurrentDirectory();
            var emailstatus = await _accountService.ForgetPassword(email);
            if (!emailstatus.Item1)
            {
                return BadRequest();
            }
            else
            {

                return Ok(new
                {
                    Success = emailstatus.Item1,
                    Message=emailstatus.Item2
                });
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel Model)
        {
            var otpresponce = await _accountService.ChangePassword(Model.Email,Model.Otp,Model.Password);
            if (!otpresponce.Item1)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return Ok(new { Success=otpresponce.Item1});
            }
        }


       


    }
}
