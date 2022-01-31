using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
    public class TblOrderItemDetails
    {
       
        [Key]
        public long OrderItemDetailId { get; set; } 

        public float TotalAmount { get; set; }  
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "bit")]
        public bool PaymentStatus { get; set; }
        public int Quantity { get; set; }
        public int ProductOptionId { get; set; }


        [Column(TypeName = "bit")]
        public Boolean IsCancel { get; set; }

        public bool Isactive { get; set; }
        [Timestamp]
        public byte[] UpdateTime { get; set; } 
        public TblOrder TblOrder { get; set; }
        [ForeignKey("TblOrder")]
        public long OrderRefId { get; set; }
        public TblOrderStatusCode TblOrderStatusCodes { get; set; }
        [ForeignKey("TblOrderStatusCodes")]
        public int OrderStatusRefId { get; set; }
        public TblProduct TblProduct { get; set; }
        [ForeignKey("TblProduct")]
        public long ProductRefId { get; set; }
    }
}
