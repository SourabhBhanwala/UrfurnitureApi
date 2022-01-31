using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using urfurniture.DAL.Repository;
using urfurniture.Models;
using Path = urfurniture.Models.Path;

namespace urfurniture.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManageCartController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly IManageCart _cartService;
        private readonly Path _path;
        public ManageCartController(/*IMapper mapper*/IManageCart cartService, IOptions<Path> path)
        {
            _cartService = cartService;
            _path = path.Value;
            //_mapper = mapper;
        }

        [HttpPost("AddProductToCart")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddProduct([FromBody]CartModel model)
        {
            var productstatus = await _cartService.AddProduct(model);
            return Ok(new
            {
                success = productstatus.Item1
            });

        }
        [HttpPost("bulkaddtocart")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> BulkAddProductToCart([FromBody] List<CartModel> cartModels, int shoppingCartID)
        {
            var productstatus = await _cartService.BulkAddToCart(cartModels, shoppingCartID);
            return Ok(new
            {
                success = productstatus.Item1
            });
        }


        [HttpDelete("RemoveProduct")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> RemoveProduct(long shoppingCartID,long productId)
        {
            CartModel model = new CartModel
            {
                ShoppingCartID = shoppingCartID,
                ProductId = productId
            };
            var productstatus = await _cartService.RemoveProduct(model);
            return Ok(new
            {
                success = productstatus.Item1
            });

        }
        [HttpGet("GetCartItem")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetCartItem(long shoppingcartid)
        {
            string FilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";


            var productstatus = await _cartService.GetCartItem(shoppingcartid, FilePath);
            return Ok(productstatus);

        }
        [HttpPost("SaveForLater")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> SaveForLater([FromBody]CartModel model)
        {
            var productstatus = await _cartService.SaveForLater(model);
            return Ok(new
            {
                success = productstatus.Item1
            });

        }
        [HttpPut("QuantityUpdate")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> QuantityUpdate([FromBody] CartModel model)
        {
            var productstatus = await _cartService.QuantityUpdate(model);
            return Ok(new
            {
                success = productstatus.Item1
            });

        }



    }
}
