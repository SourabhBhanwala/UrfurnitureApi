using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblUserAddress
    {
        [Key] 
        public long AddressId { get; set; }
        public string AddressType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; } 
        public string Pincode { get; set; }
        public string Landmark { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblUser TblUser { get; set; }
        [ForeignKey("TblUser")]
        public long UserRefId { get; set; }
        
        
    }
}
