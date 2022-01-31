using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace urfurniture.DAL.Entities
{
  public  class TblUserPaymentMethodsCode
    {
        [Key]
        public int PaymentMethodCode { get; set; }
        [StringLength(20)]
        public string PaymentMethodCodeDesc { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public IEnumerable<TblUserPaymentMethod> TblUserPaymentMethods { get; set; }
    }
}
