using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
  public class TblProduct
    {
        [Key]
        public long ProductId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 
        public float Price { get; set; }
        public int Discount { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdateTime { get; set; } 
        public TblSubCategory TblSubCategory { get; set; }
        [ForeignKey("TblSubCategory")]
        public int ProdctSubCategoryRefId { get; set; }
        public IEnumerable<TblCartItem> TblCartItems { get; set; }
        public IEnumerable<TblProductReview> TblProductReviews { get; set; }
        public IEnumerable<TblProductMetadata> TblProductMetadatas { get; set; }
        public IEnumerable<TblOrderItemDetails> TblOrderDetails { get; set; }
        public IEnumerable<TblProductImage> TblProductImage { get; set; }
        public IEnumerable<TblProductOption> TblProductOption { get; set; }

    }
}
