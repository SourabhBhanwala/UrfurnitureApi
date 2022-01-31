using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using urfurniture.DAL.Repository;
using urfurniture.Helper;
using urfurniture.Models;
using Path = urfurniture.Models.Path;

namespace urfurniture.Controllers
{

    [Route("api/[controller]")] 
    [ApiController]
    public class ManageProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IManageProduct _productService;
        private readonly Path _path;

        public ManageProductController(IMapper mapper, IManageProduct productService, IOptions<Path> path) 
        {
            _productService = productService;
            _mapper = mapper;
            _path = path.Value;
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct()
        {
           string newPath =System.IO.Path.Combine(Directory.GetCurrentDirectory(),_path.ProductImagePath);
           var productForm = HttpContext.Request.Form["data"];
           var productModel = JsonConvert.DeserializeObject<ProductModel>(productForm);
           List <string> Images=new List<string>();
            if (productModel.ImageUrl != null)
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (Request.Form.Files.Count() > 0)
                {

                    if (Request.Form.Files["primaryImage"] != null)
                    {
                        var file = HttpContext.Request.Form.Files["primaryImage"];
                            if (file.Length > 0)
                            {
                                string fileName = file.FileName;
                                string fullpath =System.IO.Path.Combine(newPath, fileName);
                                using var stream = new FileStream(fullpath, FileMode.Create);
                                file.CopyTo(stream);
                                productModel.ImageUrl = fileName;
                            }
                        
                    }
                }

            }
            if (productModel.Image != null)
            {
                
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (Request.Form.Files.Count() > 0 )
                {

                    if (Request.Form.Files["file"] != null)
                    {
                        var files =HttpContext.Request.Form.Files;
                        foreach (var file in files)
                        {
                            if (file.Length > 0 && file.Name!= "primaryImage")
                            {
                                string fileName = file.FileName;
                                string fullpath = newPath + fileName;
                                using var stream = new FileStream(fullpath, FileMode.Create);
                                file.CopyTo(stream);
                                Images.Add(fileName);
                            }
                        }
                    }
                    
                }
            }
            var  productResponse = await _productService.AddProduct(productModel);
            var imagestatus = await _productService.SaveProductImages(productResponse.Item1, Images);
            var optionstatus = await _productService.AddProductOption(productModel.ProductOptions,productResponse.Item1);
            return Ok(new
            {
                success= productResponse.Item2 && imagestatus.Item1 && optionstatus.Item1,
                Id= productResponse.Item1
    });

        }

        [HttpGet("GetProductImages")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetProductImages(long Productid)
        {

            var productsimages = await _productService.GetProductImages(Productid);

            return Ok(productsimages);

        }

        [HttpDelete("DeleteProductImages")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductImages(long imageid)
        {

            var productResponse = await _productService.DeleteProductImages(imageid);

            return Ok(new
            {
                success = productResponse.Item1
            });

        }
       
        [HttpGet("getsortproducts")]
        public async Task<IActionResult> GetProducts(int pageno, int pagesize, int sortid)
        {

            string newPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";

            var products = await _productService.GetProducts(pageno, pagesize, newPath, sortid);

            return Ok(products);

        }


        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int PageNumber,int PageSize)
        {
            
            string newPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";


            PagingParameters parameters = new PagingParameters
            {
           
                PageNumber = PageNumber,
                PageSize = PageSize
            };

            var products = await _productService.GetProduct(parameters.PageNumber,parameters.PageSize,newPath);

            return Ok( products);
        }
        [HttpPost("FilterProductBySubCategory")]
        public async Task<IActionResult> FilterProductBySubCategory(ProductBySubcategoryReqDTOs item)
        {
            string newPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";
            PagingParameters parameters = new PagingParameters
            {
                PageNumber = item.PageNumber,
                PageSize = item.PageSize
            };
            var products = await _productService.FilterProductBySubCategory(parameters.PageNumber, parameters.PageSize, item.Subcategory);
            products.Products.ForEach(x => x.ImageUrl = newPath + x.ImageUrl);
            return Ok(products);
        }
        [HttpGet("GetProductbyid")]
        public async Task<IActionResult> GetProductbyid(long productid)
        {
            var path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";
            var products = await _productService.GetProductByid(productid,path);

            return Ok(products);

        }

        [HttpGet("GetProductByAdmin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetProductByAdmin(long productid)
        {
            var path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";
            var products = await _productService.GetProductByid(productid, path);

            return Ok(products);

        }
        [HttpGet("GetProductByPrice")]
        public async Task<IActionResult> GetProductByPrice(int PageNumber, int PageSize, int Initialprice, int FinalPrice)
        {

            string newPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{_path.ProductImagePath}";

            var products = await _productService.GetProductByPrice(PageNumber, PageSize, newPath, Initialprice, FinalPrice);

            return Ok(products);

        }

        [HttpPut("UpdateProduct")]
       [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct()
        {
            var productForm = Request.Form["data"];
            var productModel = JsonConvert.DeserializeObject<ProductModel>(productForm);
            string newPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), _path.ProductImagePath);
            List<string> Images = new List<string>();
            if (productModel.ImageUrl != null)
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (Request.Form.Files.Count() > 0)
                {

                    if (Request.Form.Files["primaryImage"] != null)
                    {
                        var file = HttpContext.Request.Form.Files["primaryImage"];
                        if (file.Length > 0)
                        {
                            string fileName = file.FileName;
                            string fullpath = System.IO.Path.Combine(newPath, fileName);
                            using var stream = new FileStream(fullpath, FileMode.Create);
                            file.CopyTo(stream);
                            productModel.ImageUrl = fileName;
                        }

                    }
                }

            }
            if (productModel.Image != null)
            {

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (Request.Form.Files.Count() > 0)
                {

                    if (Request.Form.Files["file"] != null)
                    {
                        var files = HttpContext.Request.Form.Files;
                        foreach (var file in files)
                        {
                            if (file.Length > 0 && file.Name != "primaryImage")
                            {
                                string fileName = file.FileName;
                                string fullpath = newPath + fileName;
                                using var stream = new FileStream(fullpath, FileMode.Create);
                                file.CopyTo(stream);
                                Images.Add(fileName);
                            }
                        }
                    }

                }
            }
            var productResponse = await _productService.UpdateProduct(productModel);
            var imagestatus = await _productService.SaveProductImages(productModel.ProductId, Images);
            var optionstatus = await _productService.AddProductOption(productModel.ProductOptions,productModel.ProductId);
            return Ok(new
            {
                success = productResponse.Item1 && imagestatus.Item1&& optionstatus.Item1,
                Id = productResponse.Item1
            });
        }
        [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(long productid)
        {

            var productResponse = await _productService.DeleteProduct(productid);

            return Ok(new
            {
                success = productResponse.Item1
            });

        }
        [HttpDelete("DisableProduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DisableProduct(long productid)
        {

            var productResponse = await _productService.DisableProduct(productid);

            return Ok(new
            {
                success = productResponse.Item1
            });

        }

        // [HttpDelete("SearchPrdouct")]
        //public async Task<IActionResult> SearchPrdouct(string Product)
        //{

        //    var productlist = await _productService.SearchPrdouct(Product);

        //    return Ok(productlist);

        //}


        //ProductOption
        [HttpGet("GetProductOptionbyid")]
        public async Task<IActionResult> GetProductOptionbyid(int optionid)
        {

            var options = await _productService.GetProductOptionbyid(optionid);

            return Ok(options);

        }
        [HttpGet("GetProductOptionbyProductId")]
        public async Task<IActionResult> GetProductOptionbyProductId(long ProducId)
        {

            var options = await _productService.GetProductOptionbyProductId(ProducId);

            return Ok(options);

        }
        
        //[HttpPut("UpdateProductOption")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> UpdateProductOption([FromBody] ProductOptionModel model)
        //{

        //    var productCategory = await _productService.UpdateProductOption(model);

        //    return Ok(new
        //    {
        //        success = productCategory.Item1
        //    });

        //}
        [HttpDelete("DeleteProductOption")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductOption(int id)
        {

            var optionstatus = await _productService.DeleteProductOption(id);

            return Ok(new
            {
                success = optionstatus.Item1
            });

        }
        [HttpDelete("DisableProductOption")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DisableProductOption(int id)
        {

            var optionstatus = await _productService.DisableProductOption(id);

            return Ok(new
            {
                success = optionstatus.Item1
            });

        }



        // product category


        [HttpGet("GetProductCategory")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetProductCategory()
        {

            var productCategory = await _productService.GetProductCategory();

            return Ok(productCategory);

        }
        [HttpGet("GetProductCategorybyid")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetProductCategorybyid(int categoryid)
        {

            var productCategory = await _productService.GetProductCategorybyid(categoryid);

            return Ok(productCategory);

        }
        [HttpPost("AddCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCategory([FromBody]string name)
        {

            var productCategory = await _productService.AddCategory(name);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }

        [HttpPost("AddCategoryImages")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCategoryImages()
        {

            string newPath = Directory.GetCurrentDirectory() + _path.CategoryImagePath;
            var productForm = HttpContext.Request.Form["data"];
            var ImageModel = JsonConvert.DeserializeObject<CategoryImageModel>(productForm);
            List<string> Images = new List<string>();

            if (ImageModel.Image != null)
            {

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (Request.Form.Files.Count() > 0)
                {

                    if (Request.Form.Files["file"] != null)
                    {
                        var files = HttpContext.Request.Form.Files;
                        foreach (var file in files)
                        {
                            if (file.Length > 0 && file.Name != "primaryImage")
                            {
                                string fileName = file.FileName;
                                string fullpath = newPath + fileName;
                                using var stream = new FileStream(fullpath, FileMode.Create);
                                file.CopyTo(stream);
                                Images.Add(fileName);
                            }
                        }
                    }

                }
            }   


           var ImageStatus = await _productService.AddCategoryImages(ImageModel.CategoryId,Images);

            return Ok(new
            {
                success = ImageStatus.Item1
            });

        }

        [HttpPut("UpdateCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory([FromBody]ProductCategoryModel Model)
        {

            var productCategory = await _productService.UpdateCategory(Model.Id,Model.Name);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }
        [HttpDelete("DeleteCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var productCategory = await _productService.DeleteCategory(id);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }






        // product sub category

        [HttpGet("GetSubCategorybycategoryid")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetSubCategorybycategoryid(int categoryid)
        {

            var productCategory = await _productService.GetSubCategorybycategoryid(categoryid);

            return Ok(productCategory);

        }
        [HttpGet("GetProductSubCategorybysubcategoryid")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetProductSubCategorybysubcategoryid(int subcategoryid)
        {

            var productCategory = await _productService.GetProductSubCategory(subcategoryid);

            return Ok(productCategory);

        }


        [HttpPost("AddSubCategory")]
       [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddSubCategory([FromBody]ProductCategoryModel model)
        {

            var productCategory = await _productService.AddSubCategory(model.Name, model.Id);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }
        [HttpDelete("DeleteSubCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {

            var productCategory = await _productService.DeleteSubCategory(id);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }
        [HttpPut("UpdateSubCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] ProductCategoryModel model)
        {

            var productCategory = await _productService.UpdateSubCategory(model.Id,model.Name);

            return Ok(new
            {
                success = productCategory.Item1
            });

        }


        //product reviews
        [HttpPost("AddProductReview")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddProductReview()
        {
            string newPath = Directory.GetCurrentDirectory() + _path.ProductReviewImagePath;

            var productForm = HttpContext.Request.Form["data"];
            var productModel = JsonConvert.DeserializeObject<ProductReviewModel>(productForm);
            List<string> Images = new List<string>();
            if (productModel.Image != null)
            {

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (Request.Form.Files.Count() > 0)
                {

                    if (Request.Form.Files["file"] != null)
                    {
                        var files = HttpContext.Request.Form.Files;
                        foreach (var file in files.Select((value, index) => new { value, index }))
                        {
                            if (file.value.Length > 0)
                            {
                                string fileName = file.value.FileName;
                                string fullpath = newPath + fileName;
                                using var stream = new FileStream(fullpath, FileMode.Create);
                                file.value.CopyTo(stream);
                                Images.Add(fileName);
                            }
                        }
                    }

                }
            }


            var productResponse = await _productService.AddProductReview(productModel);
           // var imagestatus = await _productService.SaveProductImages(productResponse.Item1, Images);
            return Ok(new
            {
                success = productResponse.Item2, //&& imagestatus.Item1,
                Id = productResponse.Item1
            });

        }




    }
}
