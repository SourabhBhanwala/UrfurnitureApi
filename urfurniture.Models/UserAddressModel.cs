using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
 public class UserAddressModel
    {
        public long AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pincode { get; set; }
        public string Landmark { get; set; }
        public string AddressType { get; set; }
        public long UserRefId { get; set; }
    }
}
