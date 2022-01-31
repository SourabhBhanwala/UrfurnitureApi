using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
   public class OrderDto
    {
        public long OrderId { get; set; }
        public long ProductRefId { get; set; }
        public long OrderItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public int ProductOptionRefId { get; set; }
        public int Quantity { get; set; }
        public int DeliveryCharges { get; set; }
        public float TotalAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public long Total { get; set; }
    }
}
