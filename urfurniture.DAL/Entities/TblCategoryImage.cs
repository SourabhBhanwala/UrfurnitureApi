using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblCategoryImage 
    {
        [Key]
        public long ImageId { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblProductCategory TblProductCategory { get; set; }
        [ForeignKey("TblProductCategory")]
        public int CategoryRefId { get; set; }
    }
}
