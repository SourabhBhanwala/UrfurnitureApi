using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urfurniture.DAL.Data;
using urfurniture.DAL.Entities;
using urfurniture.DAL.model;
using urfurniture.DAL.Repository;
using urfurniture.DAL.ViewModel;     
using urfurniture.Models;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.DAL.Implementation 
{
    public class ManageProductService : IManageProduct
    {
        private readonly urfurnitureContext _dbcontext;
        private readonly IMapper _mapper;
       // private readonly List<ProductDto> _productList;
      
        public ManageProductService(urfurnitureContext dbcontext, IMapper mapper)
        {

            _dbcontext = dbcontext;
            _mapper = mapper;
           // _productList = productlist;

        }

        //product 
        public async Task<Tuple<long, bool>> AddProduct(ProductModel  productmodel)
        {
            //adding  product to database

            var model = _mapper.Map<TblProduct>(productmodel);
            model.AddedDate = DateTime.Now;
            model.IsActive = true;
            model.IsDeleted = false;
            await _dbcontext.TblProducts.AddAsync(model);
            await _dbcontext.SaveChangesAsync();

            return new Tuple<long, bool>(model.ProductId, true);



        }
        public async Task<ProductsVM> GetProduct(int PageNumber,int PageSize,string path)
        {
            var productvm = new ProductsVM();

            var products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false&&p.IsActive==true)
                                  orderby p.ProductId descending
                                  select new ProductDto
                                  {

                                 ProductId = p.ProductId,
                                 Name = p.Name,
                                 Description = p.Description,
                                 Price = p.Price,
                                 Discount = p.Discount,
                                 Stock = p.Stock,
                                 ImageUrl = path+p.ImageUrl

                                  }).Skip((PageNumber - 1) * PageSize)
                          .Take(PageSize).ToListAsync();


            productvm.Products = products;
            productvm.Count = _dbcontext.TblProducts.Where(p => p.IsDeleted == false).Count();


            return productvm;
        }
        public async Task<ProductsVM> FilterProductBySubCategory(int pageno, int pagesize, List<int> subcategory)
        {
            var productvm = new ProductsVM();
            var products = await(from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true 
                                 && subcategory.Contains(p.ProdctSubCategoryRefId))
                                 orderby p.ProductId descending
                                 select new ProductDto
                                 {
                                     ProductId = p.ProductId,
                                     Name = p.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Discount = p.Discount,
                                     Stock = p.Stock,
                                     ImageUrl = p.ImageUrl
                                 }).Skip((pageno - 1) * pagesize)
                         .Take(pagesize).ToListAsync();


            productvm.Products = products;
            productvm.Count = _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true 
            && subcategory.Contains(p.ProdctSubCategoryRefId)).Count();
            return productvm;
        }
        public async Task<ProductsVM> GetProductBySubCategoryId(int PageNumber, int PageSize, string path, int subcategoryid)
        {
            var productvm = new ProductsVM();

            var products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false&&p.IsActive==true&& p.ProdctSubCategoryRefId==subcategoryid)
                                  orderby p.ProductId descending
                                  select new ProductDto
                                  {

                                      ProductId = p.ProductId,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Discount = p.Discount,
                                      Stock = p.Stock,
                                      ImageUrl = path + p.ImageUrl

                                  }).Skip((PageNumber - 1) * PageSize)
                                    .Take(PageSize).ToListAsync();


            productvm.Products = products;
            productvm.Count = await _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true && p.ProdctSubCategoryRefId == subcategoryid).CountAsync(); ;


            return productvm;
        }
        public async Task<ProductsVM> GetProductByPrice(int PageNumber, int PageSize, string path, int Initialprice, int FinalPrice)
        {
            var productvm = new ProductsVM();

            var products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true && p.Price<=FinalPrice && p.Price>=Initialprice)
                                  orderby p.ProductId descending
                                  select new ProductDto
                                  {

                                      ProductId = p.ProductId,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Discount = p.Discount,
                                      Stock = p.Stock,
                                      ImageUrl = path + p.ImageUrl

                                  }).Skip((PageNumber - 1) * PageSize)
                                    .Take(PageSize).ToListAsync();


            productvm.Products = products;
            productvm.Count =await _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true && p.Price <= FinalPrice && p.Price >= Initialprice).CountAsync();


            return productvm;
        }
       
        public async Task<ProductsVM> GetProducts(int PageNumber, int PageSize, string path, int sortid)
        {
            var productvm = new ProductsVM();
            productvm.Count = await _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true).CountAsync();
            switch (sortid)
            {
           

         case 1:
                    productvm.Products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true)
                                  orderby p.Price descending
                                  select new ProductDto
                                  {

                                      ProductId = p.ProductId,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Discount = p.Discount,
                                      Stock = p.Stock,
                                      ImageUrl = path + p.ImageUrl

                                  }).Skip((PageNumber - 1) * PageSize)
                                    .Take(PageSize).ToListAsync();
            
                    break;
                case 2:
                    productvm.Products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true)
                                                orderby p.Price ascending
                                                select new ProductDto
                                                {

                                                    ProductId = p.ProductId,
                                                    Name = p.Name,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    Discount = p.Discount,
                                                    Stock = p.Stock,
                                                    ImageUrl = path + p.ImageUrl

                                                }).Skip((PageNumber - 1) * PageSize)
                                 .Take(PageSize).ToListAsync();

                    break;
                case 3:
                    productvm.Products = await (from p in _dbcontext.TblProducts.Where(p => p.IsDeleted == false && p.IsActive == true)
                                                orderby p.AddedDate descending
                                                select new ProductDto
                                                {

                                                    ProductId = p.ProductId,
                                                    Name = p.Name,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    Discount = p.Discount,
                                                    Stock = p.Stock,
                                                    ImageUrl = path + p.ImageUrl

                                                }).Skip((PageNumber - 1) * PageSize)
                                 .Take(PageSize).ToListAsync();

                    break;
                default: 
                    productvm.Products = new List<ProductDto>();
                    break;
        }

            return productvm;
        }
        public async Task<ProductsVM> GetProductByid(long id,string path)
        {
            var productvm = new ProductsVM();
            productvm.ImageUrl = await _dbcontext.TblProductImages.Where(x => x.ProductRefId == id && x.IsActive==true ).Select(x => path + x.Url).ToListAsync();

            productvm.Products = await _dbcontext.TblProducts.Where(x => x.ProductId == id && x.IsDeleted == false &&x.IsActive==true).Select(x => new ProductDto
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Discount = x.Discount,
                Stock = x.Stock,
                ImageUrl = path + x.ImageUrl,
                ProdctSubCategoryId = x.ProdctSubCategoryRefId,
                ProductCategoryId=_dbcontext.TblSubCategories.FirstOrDefault(s=>s.Id==x.ProdctSubCategoryRefId).ProductCategoryRefId
              

            }).ToListAsync();
            return productvm;

        }
        public async Task<AdminProductVM> GetProductByAdmin(long id, string path)
        {
            var productvm = new AdminProductVM();
            productvm.Images = await _dbcontext.TblProductImages.Where(x => x.ProductRefId == id && x.IsActive==true)
                .Select(x => new ProductImageModel
                {
                    ImageId = x.ImageId,
                    Url = path + x.Url
                })
                .ToListAsync();
            productvm.Products = await _dbcontext.TblProducts.Where(x => x.ProductId == id && x.IsDeleted == false).Select(x => new ProductDto
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Discount = x.Discount,
                Stock = x.Stock,
                ImageUrl = path + x.ImageUrl,
                ProdctSubCategoryId = x.ProdctSubCategoryRefId,
                ProductCategoryId = _dbcontext.TblSubCategories.FirstOrDefault(s => s.Id == x.ProdctSubCategoryRefId).ProductCategoryRefId


            }).ToListAsync();
            productvm.Options= await _dbcontext.TblProductOptions.Where(po => po.ProductRefId == id&&po.IsDelete==false).Select(o => new ProductOptionDto
            {
                ProductOptionId = o.ProductOptionId,
                OptionName = o.OptionName,
                OptionPriceIncrement = o.OptionPriceIncrement
            }).ToListAsync();
            return productvm;

        }

        public async Task<Tuple<bool>> UpdateProduct(ProductModel productModel)
        {
            var product = _dbcontext.TblProducts.FirstOrDefault(x => x.ProductId == productModel.ProductId);
            if(product is null) return new Tuple<bool>(false);
            product.Description = productModel.Description;
            product.Discount = productModel.Discount;
            product.ImageUrl =string.IsNullOrWhiteSpace(productModel.ImageUrl)?product.ImageUrl:productModel.Image.Contains('/')?productModel.ImageUrl
                .Substring(productModel.ImageUrl.LastIndexOf('/')+1):productModel.ImageUrl;
            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.ProdctSubCategoryRefId = productModel.ProdctSubCategoryRefId;
            product.Stock = productModel.Stock;
            _dbcontext.Update(product);
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<Tuple<bool>> DeleteProduct(long id)
        {


            var product = await _dbcontext.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
            product.IsDeleted = true;
            product.IsActive = false;
            _dbcontext.Entry(product).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            var productimage = await _dbcontext.TblProductImages.Where(x => x.ProductRefId == id).ToListAsync();
            foreach (var url in productimage)
            {
                url.IsActive = false;
                _dbcontext.Entry(url).State = EntityState.Modified;
            }
            
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);


        }
        public async Task<Tuple<bool>> DisableProduct(long id)
        {
            var product = await _dbcontext.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
            product.IsActive = false;
            _dbcontext.Entry(product).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        //public async Task<List<TblProduct>> SearchPrdouct(string Product)
        //{
        //    var product = await _dbcontext.TblProducts.Where(x => x.Name.Contains(Product)).OrderBy(x => x.Name).ToListAsync();
        //    return product;
        //}

      

      
        
        //product Option

        public async Task<Tuple<bool>> AddProductOption(List<ProductOptionModel> options,long ProductRefId)
        {
            for (int i=0;i<options.Count;i++ )
            {
                if (options[i].ProductOptionId > 0)
                {
                    var option = _dbcontext.TblProductOptions.FirstOrDefault();
                    if (option is null) continue;
                    option.OptionPriceIncrement = options[i].OptionPriceIncrement;
                    option.OptionName = options[i].OptionName;
                     _dbcontext.Update(option);
                    await _dbcontext.SaveChangesAsync();
                }
                else
                {
                    TblProductOption option = new TblProductOption()
                    {
                        OptionPriceIncrement = options[i].OptionPriceIncrement,
                        CreateDate = DateTime.Now,
                        IsActive = true,
                        IsDelete = false,
                        ProductRefId = ProductRefId,
                        OptionName = options[i].OptionName
                    };
                    await _dbcontext.TblProductOptions.AddAsync(option);
                    await _dbcontext.SaveChangesAsync();
                }
                
            }
            return new Tuple<bool>(true);
        }
        public async Task<Tuple<bool>> DeleteProductOption(int optionid)
        {
            var option =await _dbcontext.TblProductOptions.Where(o => o.ProductOptionId == optionid).FirstOrDefaultAsync();
            option.IsDelete = true;
            _dbcontext.Entry(option).State = EntityState.Modified;
             await  _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<Tuple<bool>> DisableProductOption(int optionid)
        {
            var option = await _dbcontext.TblProductOptions.Where(o => o.ProductOptionId == optionid).FirstOrDefaultAsync();
            option.IsActive = true;
            _dbcontext.Entry(option).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<Tuple<bool>> UpdateProductOption(ProductOptionModel productoption)
        {
            var option =await _dbcontext.TblProductOptions.FirstOrDefaultAsync(o => o.ProductOptionId == productoption.ProductOptionId);
            if(option is null) return new Tuple<bool>(false);
            var updatedoption = _mapper.Map<ProductOptionModel, TblProductOption>(productoption, option);
            _dbcontext.Entry(updatedoption).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true); 
        }
        public async Task<ProductOptionModel> GetProductOptionbyid(int optionid)
        {
            var option = await _dbcontext.TblProductOptions.FirstOrDefaultAsync(o => o.ProductOptionId == optionid);
            var productoption = _mapper.Map<ProductOptionModel>(option);
            return productoption;
        }
        public async Task<List<ProductOptionDto>> GetProductOptionbyProductId(long productid)
        {



            var option = await _dbcontext.TblProductOptions.Where(po => po.ProductRefId == productid).Select(o => new ProductOptionDto
            {
                ProductOptionId = o.ProductOptionId,
                OptionName = o.OptionName,
                OptionPriceIncrement = o.OptionPriceIncrement
            }).ToListAsync();
            
            return option;
        }

        //product Image
        public async Task<Tuple<bool>> SaveProductImages(long productid, List<string> images)
        {
            if (images.Count()==0) return new Tuple<bool>(true);
            var productImages = _dbcontext.TblProductImages.Where(x => x.ProductRefId == productid);
            if (productImages.Count()>0)
            {
                 _dbcontext.RemoveRange(productImages);
               await _dbcontext.SaveChangesAsync();
            }
            foreach (var imageurl in images)
            {
                TblProductImage image = new TblProductImage()
                {
                    Url = string.IsNullOrWhiteSpace(imageurl) ? "" : imageurl.Contains('/') ? imageurl
                .Substring(imageurl.LastIndexOf('/') + 1) : imageurl,
                    IsActive = true,
                    ProductRefId = productid
                };
                await _dbcontext.TblProductImages.AddAsync(image);
                await _dbcontext.SaveChangesAsync();
            }
            return new Tuple<bool>(true);
        }
        public async Task<Tuple<bool>> DeleteProductImages(long imageid)
        {
            var imagestatus = await _dbcontext.TblProductImages.Where(x => x.ImageId == imageid && x.IsActive == true).FirstOrDefaultAsync();
            imagestatus.IsActive = false;
            _dbcontext.Entry(imagestatus).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<List<ProductImageModel>> GetProductImages(long Productid)
        {
            var images = await _dbcontext.TblProductImages.Where(x => x.ProductRefId == Productid && x.IsActive == true)
                .Select(x => new ProductImageModel
                {
                    ImageId = x.ImageId,
                    Url = x.Url
                })
                .ToListAsync();

            return images;

        }
        
        //product category
        

        public async Task<Tuple<bool>> AddCategory(string name)
        {
            var categoryexist = await _dbcontext.TblProductCategories.Where(x => x.Name.ToLower() == name.ToLower()&&x.IsActive==true).CountAsync();
            if (categoryexist == 0)
            {
                TblProductCategory category = new TblProductCategory
                {
                    Name = name,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                await _dbcontext.TblProductCategories.AddAsync(category);
                await _dbcontext.SaveChangesAsync();
                return new Tuple<bool>(true);
            }
            else return new Tuple<bool>(false);
        }

        public async Task<Tuple<bool>> AddCategoryImages(int categoryid,List<string> images)
        {

            foreach (var imageurl in images)
            {
                TblCategoryImage image = new TblCategoryImage()
                {
                    Url = imageurl,
                    IsActive = true,
                    CategoryRefId = categoryid
                };
                await _dbcontext.TblCategoryImages.AddAsync(image);
                await _dbcontext.SaveChangesAsync();
            }
            return new Tuple<bool>(true);

        }
        public async  Task<Tuple<bool>> DeleteCategory(int id)
        {
            var category = await _dbcontext.TblProductCategories.FirstOrDefaultAsync(x => x.ProdctCategoryId == id);
            category.IsActive = false;
           
            _dbcontext.Entry(category).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async  Task<Tuple<bool>> UpdateCategory(int id,string name)
        {
            var category = await _dbcontext.TblProductCategories.FirstOrDefaultAsync(x=>x.ProdctCategoryId==id);
            category.Name = name;
            _dbcontext.Entry(category).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);
        }
        public async Task<List<ProductCategoryModel>> GetProductCategory()
        {
            
                var category = await _dbcontext.TblProductCategories.Where(x=>x.IsActive==true).Select(x => new ProductCategoryModel { Id= x.ProdctCategoryId,Name= x.Name }).OrderByDescending(x=>x.Id).ToListAsync();
                return category;
            
        }
        public async Task<ProductCategoryModel> GetProductCategorybyid(int categoryid)
        {

            var category = await _dbcontext.TblProductCategories.FirstOrDefaultAsync(x =>x.ProdctCategoryId==categoryid &&x.IsActive == true);
            ProductCategoryModel model = new ProductCategoryModel();
            if (category != null)
            {
                model.Id = category.ProdctCategoryId;
                model.Name = category.Name;
            }
            return model;
            }


        //product sub category
        public async Task<Tuple<bool>> AddSubCategory(string name, int categoryrefid)
        {
            var categoryexist = await _dbcontext.TblSubCategories.Where(x => x.Name.ToLower() == name.ToLower() && x.IsActive == true).CountAsync();
            if (categoryexist == 0)
            {

                TblSubCategory category = new TblSubCategory
                {
                    Name = name,
                    CreateDate = DateTime.Now,
                    IsActive = true,
                    ProductCategoryRefId = categoryrefid
                };
                await _dbcontext.TblSubCategories.AddAsync(category);
                await _dbcontext.SaveChangesAsync();
                return new Tuple<bool>(true);
            }
            else return new Tuple<bool>(false);

        }
        public async Task<Tuple<bool>> DeleteSubCategory(int id)
        {

            var category = await _dbcontext.TblSubCategories.FirstOrDefaultAsync(x => x.Id == id);
            category.IsActive = false;

            _dbcontext.Entry(category).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<Tuple<bool>> UpdateSubCategory(int id, string name)
        {
            var category = await _dbcontext.TblSubCategories.FirstOrDefaultAsync(x => x.Id == id);
            category.Name = name;
            _dbcontext.Entry(category).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }
        public async Task<List<ProductCategoryModel>> GetSubCategorybycategoryid(int categoryrefid)
        {

            var category = await _dbcontext.TblSubCategories.Where(x => x.IsActive == true && x.ProductCategoryRefId == categoryrefid).Select(x => new ProductCategoryModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return category;

        }
        public async Task<List<ProductCategoryModel>> GetProductSubCategory(int subcategoryrefid)
        {

            var category = await _dbcontext.TblSubCategories.Where(x => x.IsActive == true && x.Id == subcategoryrefid).Select(x => new ProductCategoryModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return category;

        }

        //product review


        public async  Task<ProductReviewVM> GetProductReviewByProductId(int PageNumber, int PageSize, string path,int productid)
        {

            ProductReviewVM productReview = new ProductReviewVM();
            var reviews = await (from r in _dbcontext.TblProductReviews.Where(r => r.IsActive == true && r.ProductRefId==productid)
                                            orderby r.PublishAt descending
                                           select new ProductReviewDto
                                           {
                                              Title=r.Title,
                                              RatingValue=r.RatingValue,
                                              Description=r.Description,
                                              PublishAt=r.PublishAt,
                                              UserName=_dbcontext.TblUsers.FirstOrDefault(u=>u.UserId==r.UserRefId).FirstName,
                                              City= _dbcontext.TblUserAddresses.FirstOrDefault(a => a.UserRefId == r.UserRefId).City,
                                              ImageUrl= _dbcontext.TblProductReviewImages.Where(i=>i.ReviewRefId==r.Id&&i.IsActive==true)
                                              .Select(i=>path+i.Url)
                                              .ToList()

                                           }
                                           )
                                 .Skip((PageNumber - 1) * PageSize)
                          .Take(PageSize).ToListAsync();


            productReview.ProductReviewDtos = reviews;
            productReview.count =await _dbcontext.TblProductReviews.Where(r => r.ProductRefId == productid).CountAsync();
            return productReview;

        }
        //public async Task<ProductReviewVM> GetProductReviewByUserId(int PageNumber, int PageSize, string path, int userid)
        //{
        //    ProductReviewVM productReview = new ProductReviewVM();
        //    var reviews = await (from r in _dbcontext.TblProductReviews.Where(r => r.IsActive == true && r.UserRefId == userid)
        //                         orderby r.PublishAt descending
        //                         select new ProductReviewDto
        //                         {

        //                             Title = r.Title,
        //                             RatingValue = r.RatingValue,
        //                             Description = r.Description,
        //                             PublishAt = r.PublishAt,
        //                             UserName = _dbcontext.TblUsers.FirstOrDefault(u => u.UserId == r.UserRefId).FirstName,
        //                             City = _dbcontext.TblUserAddresses.FirstOrDefault(a => a.UserRefId == r.UserRefId).City,
        //                             ImageUrl = _dbcontext.TblProductReviewImages.Where(i => i.ReviewRefId == r.Id && i.IsActive == true)
        //                            .Select(i => path + i.Url)
        //                            .ToList()

        //                         }
        //                                   )
        //                         .Skip((PageNumber - 1) * PageSize)
        //                  .Take(PageSize).ToListAsync();


        //    productReview.ProductReviewDtos = reviews;
        //    productReview.count = await _dbcontext.TblProductReviews.Where(r => r.UserRefId == userid).CountAsync();
        //    return productReview;

        //}
        public async Task<Tuple<long, bool>> AddProductReview(ProductReviewModel Reviewmodel)
        {
            var model = _mapper.Map<TblProductReview>(Reviewmodel);

            model.PublishAt = DateTime.Now;
            model.IsActive = true;

            await _dbcontext.TblProductReviews.AddAsync(model);
            await _dbcontext.SaveChangesAsync();

            return new Tuple<long, bool>(model.Id, true);

        }
        public async Task<Tuple<bool>> DisableProductReview(int Reviewid)
        {
            var productreview = await _dbcontext.TblProductReviews.FirstOrDefaultAsync(r => r.Id == Reviewid);
            productreview.IsActive = false;
            _dbcontext.Entry(productreview).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool>(true);

        }

       

        //Task<Tuple<bool>> UpdateProductReview(int id, string name);

    }
}
