using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblOrderStatusCode
    {
        [Key]
        public int StatusCodeId { get; set; }
        [StringLength(50)]
        public string OrderStatusDesc { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdateTime { get; set; }
        public IEnumerable<TblOrderItemDetails> TblOrder { get; set; }

    }
}
