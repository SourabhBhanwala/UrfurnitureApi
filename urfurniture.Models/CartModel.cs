using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
   public class CartModel
    {
        public long ShoppingCartID { get; set; }
        public long ProductId { get; set; }
        public short Quantity { get; set; } = 1;
        public int OptionId { get; set; }
    }
}
