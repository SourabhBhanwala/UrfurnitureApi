using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblProductMetadata
    {
        [Key]
        public long ProductMetaId { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        public TblProduct TblProduct { get; set; }
        [ForeignKey("TblProduct")]
        public long ProductRefId { get; set; }
    }
}
