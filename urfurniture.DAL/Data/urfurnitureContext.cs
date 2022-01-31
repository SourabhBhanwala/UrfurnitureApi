using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using urfurniture.DAL.Entities;

namespace urfurniture.DAL.Data
{
    public class urfurnitureContext : DbContext
    {
        //public urfurnitureContext()
        //{
        //}

        public urfurnitureContext(DbContextOptions<urfurnitureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public DbSet<TblOtpConfirmation> TblOtpConfirmations { get; set; }
        public DbSet<TblProduct> TblProducts { get; set; }
        public DbSet<TblProductCategory> TblProductCategories { get; set; }
        public DbSet<TblCategoryImage> TblCategoryImages { get; set; }
        public DbSet<TblSubCategory> TblSubCategories { get; set; }
        public DbSet<TblProductMetadata> TblProductMetadatas { get; set; }
        public DbSet<TblProductReview> TblProductReviews { get; set; }
        public DbSet<TblProductReviewImage> TblProductReviewImages { get; set; }
        public DbSet<TblProductImage> TblProductImages { get; set; }
        public DbSet<TblProductOption> TblProductOptions { get; set; }



        public DbSet<TblUserAddress> TblUserAddresses { get; set; }
        public DbSet<TblUserShoppingCart> TblUserShoppingCarts { get; set; }
        public DbSet<TblCartItem> TblCartItems { get; set; }
      
       
    
        public DbSet<TblOrder> TblOrders { get; set; }
        public DbSet<TblOrderStatusCode> TblOrderStatusCodes { get; set; }
        public DbSet<TblOrderItemDetails> TblOrderItemDetails { get; set; }
        public DbSet<TblUserPaymentMethodsCode> TblUserPaymentMethodsCodes { get; set; }
        public DbSet<TblUserPaymentMethod> TblUserPaymentMethods { get; set; }
        public DbSet<TblDeliveryCharge> TblDeliveryCharges { get; set; }




    }
}
