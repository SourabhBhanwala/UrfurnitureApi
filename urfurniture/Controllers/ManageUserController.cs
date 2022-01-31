using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using urfurniture.DAL.Repository;
using urfurniture.Models;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ManageUserController : ControllerBase
    {

        private readonly IManageUser _userService;
        private readonly IMapper _mapper;
        public ManageUserController(IManageUser userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            
        }
        [HttpGet("users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers(int pageno, int pagesize)
        {
            var userdetails =  _userService.Users(pageno,pagesize,out int total);
          var users=  _mapper.Map<List<UserResDTOs>>(userdetails);
            return Ok(new{
                status= users.Count()>0?true:false,
                data= users,
                total = total
            });
        }
        [HttpGet("GetUserDetails")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> GetUserDetails(int userid)
        {
            var userdetails = await _userService.GetUserDetails(userid);
            return Ok(userdetails);
        }

        [HttpGet("getuserinfo")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserInfo(int userid)
        {
            var user = await _userService.getUserInfo(userid);           
            return Ok(new
            {
                status = user is null ? false : true,
                data = user
            });
        }

        [HttpPut("UpdateUserDetails")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateUserDetails([FromBody]UserDetailsModel userDeatils)
        {
            var detailStatus = await _userService.UpdateUserDetails(userDeatils);
            return Ok(new
            {
                Success = detailStatus.Item1
            });
        }
















        //address

        [HttpPost("AddUserAddress")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddUserAddress([FromBody]UserAddressModel address)
        {
            var address_status = await _userService.AddUserAddress(address);

            return Ok(new
            {
                Success = address_status.Item1
            });
        }
        [HttpDelete("DeleteAddress")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteAddress(int userid)
        {

            var address_status = await _userService.DeleteAddress(userid);

            return Ok(new
            {
                sucess = address_status.Item1
            });
        }
        [HttpGet("GetAddress")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAddress(int userid)
        {

            var address = await _userService.GetAddress(userid);

            return Ok(address);
        }
        [HttpPut("UpdateAddress")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateAddress([FromBody] UserAddressModel address)
        {

            var address_status = await _userService.UpdateAddress(address);

            return Ok(new
            {
                success = address_status.Item1
            });
        }
        [HttpGet("checkpostalcode")]
        [Authorize(Roles = "user")]
        public IActionResult CheckPostCode(string postcode)
        {
           var exist =  _userService.checkPostCode(postcode);
            return Ok(new
            {
                status = exist,
                amount = exist ? 30 : 0
            });
        }
    }
}
