using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
   public class OrderModel
    {
        
        public long UserRefId { get; set; } 
        public string Token { get; set; }
        public int DeliveryCharges { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public class OrderDetail
        {
            public int Quantity { get; set; }
            public long ProductRefId { get; set; }
            public int ProductOptionId { get; set; }
        }  
    }
}
