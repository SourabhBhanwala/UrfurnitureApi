using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
  public  class TblUserPaymentMethod
    {
        [Key]
        public int UserPaymentId { get; set; }
        [StringLength(50)]
        public string CardNo { get; set; }
        [StringLength(50)]
        public string CardHolderName { get; set; }
        [StringLength(50)]
        public string ExpireMonth { get; set; }
        [StringLength(10)]
        public string ExpireYear { get; set; }
        [StringLength(10)]
        public string CvvNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblUser TblUser { get; set; }
        [ForeignKey("TblUser")]
        public long UserRefId { get; set; }
        public TblUserPaymentMethodsCode TblUserPaymentMethodsCode { get; set; }
        [ForeignKey("TblUserPaymentMethodsCode")]
        public int UserPaymentMethodRefCode { get; set; }

    }
}
