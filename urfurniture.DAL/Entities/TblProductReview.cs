using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblProductReview
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public short RatingValue { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime PublishAt { get; set; }
        [Column(TypeName = "bit")]
        public Boolean IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblUser TblUser { get; set; }
        [ForeignKey("TblUser")]
        public long UserRefId { get; set; }
        public TblProduct TblProduct { get; set; }
        [ForeignKey("TblProduct")]
        public long ProductRefId { get; set; }
        public IEnumerable<TblProductReviewImage> TblProductReviewImages { get; set; }
    }
}
