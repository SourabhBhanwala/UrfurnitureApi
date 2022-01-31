using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{ 
   public class TblProductOption
    {
        [Key]
        public int ProductOptionId { get; set; }
        public string OptionName { get; set; }
        public int OptionPriceIncrement { get; set; }
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblProduct TblProduct { get; set; } 
        [ForeignKey("TblProduct")]
        public long ProductRefId { get; set; }

    }
}
