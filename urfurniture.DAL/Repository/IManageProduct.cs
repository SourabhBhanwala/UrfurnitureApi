using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using urfurniture.DAL.model;
using urfurniture.DAL.ViewModel;
using urfurniture.Models;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.DAL.Repository
{
    public interface IManageProduct   
    { 
        //product
        Task<Tuple<long, bool>> AddProduct(ProductModel model);
        Task<ProductsVM> GetProduct(int pageno,int pagesize,string path);  
        Task<ProductsVM> GetProductByid(long id,string path );
        Task<AdminProductVM> GetProductByAdmin(long id, string path);
        Task<Tuple<bool>> UpdateProduct(ProductModel model);
        Task<Tuple<bool>> DeleteProduct(long id);
        Task<Tuple<bool>> DisableProduct(long id);
        Task<ProductsVM> FilterProductBySubCategory(int pageno, int pagesize, List<int> subcategory);
        //Task<List<TblProduct>> SearchPrdouct(string Product);
        Task<ProductsVM> GetProductBySubCategoryId(int pageno, int pagesize, string path,int subcategoryid);
        Task<ProductsVM> GetProductByPrice(int PageNumber, int PageSize, string path, int Initialprice, int FinalPrice);
        Task<ProductsVM> GetProducts(int PageNumber, int PageSize, string path, int sortid);


        //Product Option

        Task<Tuple<bool>> AddProductOption(List<ProductOptionModel> options, long ProductRefId);
        Task<Tuple<bool>> DeleteProductOption(int id);
        Task<Tuple<bool>> DisableProductOption(int id);
        Task<Tuple<bool>> UpdateProductOption(ProductOptionModel productoption);
        Task<ProductOptionModel> GetProductOptionbyid(int optionid);
        Task<List< ProductOptionDto>> GetProductOptionbyProductId(long ProducId);












        //product images
        Task<Tuple<bool>> SaveProductImages(long productid, List<string> images);
        Task<Tuple<bool>> DeleteProductImages(long imageid);
        Task<List<ProductImageModel>> GetProductImages(long Productid);



        // product category
        Task<List<ProductCategoryModel>> GetProductCategory();
        Task<ProductCategoryModel> GetProductCategorybyid(int categoryid);
        Task<Tuple<bool>> AddCategory(string name);
        Task<Tuple<bool>> AddCategoryImages(int categoryid, List<string> images);
        Task<Tuple<bool>> DeleteCategory(int id);
        Task<Tuple<bool>> UpdateCategory(int id,string name);

        
        
        //subcategory
        Task<List<ProductCategoryModel>> GetSubCategorybycategoryid(int categoryid);
        Task<List<ProductCategoryModel>> GetProductSubCategory(int categoryid);
        Task<Tuple<bool>> AddSubCategory(string name,int categoryrefid);
        Task<Tuple<bool>> DeleteSubCategory(int id);
        Task<Tuple<bool>> UpdateSubCategory(int id, string name);



        //product Review


        Task<ProductReviewVM> GetProductReviewByProductId(int PageNumber, int PageSize, string path, int productid);
        //Task<ProductReviewVM> GetProductReviewByUserId(int PageNumber, int PageSize, string path, int userid);
        Task<Tuple<long,bool>> AddProductReview(ProductReviewModel model);
        Task<Tuple<bool>> DisableProductReview(int id);
        //Task<Tuple<bool>> UpdateProductReview(ProductReviewModel model);




    }
}
