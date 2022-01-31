using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using urfurniture.DAL.Repository;
using urfurniture.Models;
using urfurniture.Utility;
using Path = urfurniture.Models.Path;

namespace urfurniture.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ManageOrderController : Controller
    {
        private IConverter _converter;
        private readonly IMapper _mapper;
        private readonly IManageOrder _orderService;
        private readonly Path _path;
        public ManageOrderController(IMapper mapper, IManageOrder orderService, IOptions<Path> path, IConverter converter)
        {
            _orderService = orderService;
            _mapper = mapper;
            _path = path.Value;
            _converter = converter; 
        }

        [HttpPost("AddPaymentMethodCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPaymentMethodCode(string name)
        {

            var PaymentCodeStatus = await _orderService.AddPaymentMethodCode(name);

            return Ok(new
            {
                success = PaymentCodeStatus.Item1
            });

        }
        [HttpPut("UpdatePaymentMethodCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePaymentMethodCode(int id,string name)
        {

            var PaymentCodeStatus = await _orderService.UpdatePaymentMethodCode(id,name);

            return Ok(new
            {
                success = PaymentCodeStatus.Item1
            });

        }
         
        [HttpDelete("DeletePaymentMethodCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePaymentMethodCode(int id)
        {

            var PaymentCodeStatus = await _orderService.DeletePaymentMethodCode(id);

            return Ok(new
            {
                success = PaymentCodeStatus.Item1
            });

        }
        [HttpGet("GetPaymentMethodCode")]
        public async Task<IActionResult> GetPaymentMethodCode()
        {

            var PaymentCodeStatus = await _orderService.GetPaymentMethodCode();

            return Ok(PaymentCodeStatus);

        }

        [HttpPost("AddOrderStatusCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddOrderStatusCode(string name)
        {

            var OrderCodeStatus = await _orderService.AddOrderStatusCode(name);

            return Ok(new
            {
                success = OrderCodeStatus.Item1
            });

        }
        [HttpPut("UpdateOrderStatusCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatusCode(int id, string name)
        {

            var OrderCodeStatus = await _orderService.UpdateOrderStatusCode(id, name);

            return Ok(new
            {
                success = OrderCodeStatus.Item1
            });

        }

        [HttpDelete("DeleteOrderStatusCode")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrderStatusCode(int id)
        {

            var OrderCodeStatus = await _orderService.DeletePaymentMethodCode(id);

            return Ok(new
            {
                success = OrderCodeStatus.Item1
            });

        }

        [HttpPost("CreateOrder")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderModel model)
        {
            var order = await _orderService.CreateOrder(model);
            return Ok(new
            {
                OrderId=order.Item1,
                Status=order.Item2
            });
        }
        [HttpGet("GetUserOrder")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetUserOrder(int userid)
        {
            string newPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";
            var order = await _orderService.GetUserOrder(userid, newPath);
            return Ok(order);
        }
        [HttpGet("GetAllOrder")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllOrder(int pageno, int pagesize, int orderstatus=0)
        {
            string filepath = HttpContext.Request.Host.Value + _path.ProductImagePath;
            var order = _orderService.GetAllOrder(pageno, pagesize,orderstatus);
            order.Orders.ForEach(x => x.Image = filepath+ x.Image);
            return Ok(order);
        }
       
        [HttpGet("CancelOrder")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CancelOrder(int orderid)
        {
            var orderstatus = await _orderService.CancelOrder(orderid);
            return Ok(new
            {
                Status = orderstatus.Item1
            });
        }
        [HttpGet("GetInvoice")]
        //[Authorize(Roles = "user,admin")]
        public async Task<FileResult> GetInvoice(long orderid)
        {
            string filepath = HttpContext.Request.Host.Value + _path.ProductImagePath;
            var invoice = await _orderService.GetInvoice(orderid, filepath);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Invoice"
                //used for downloading the pdf
               //Out = @"D:\PDFCreator\Employee_Report.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(invoice,filepath),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "Invoice.pdf");
        }
        [HttpPut("UpdateOrderStatus")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody]OrderStatusModel model)
        {

            var OrderStatus = await _orderService.UpdateOrderStatus(model.Orderid, model.Statuscode);

            return Ok(new
            {
                success = OrderStatus.Item1
            });

        }
        [HttpGet("OrderStatusCode")]
        [Authorize(Roles = "admin")]
        public  IActionResult GetOrderStatusCode()
        {
            var statusCode = _orderService.GetOrderStatusCode();
            return Ok(new
            {
                status = statusCode.Count() > 0 ? true:false,
                data=statusCode
            }); 
        }


    }
}
