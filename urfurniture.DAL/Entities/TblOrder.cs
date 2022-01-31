using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblOrder
    {
        [Key] 
        public long OrderId { get; set; }
        public TblUser TblUser { get; set; }  
        [ForeignKey("TblUser")]
        public long UserRefId { get; set; }
        public bool IsActive { get; set; }
        public string Chargeid { get; set; }
        public int DeliveryCharges { get; set; }
        public IEnumerable<TblOrderItemDetails> TblOrderItemDetails { get; set; }
    }
}
