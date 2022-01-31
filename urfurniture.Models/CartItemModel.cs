using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
  public  class CartItemModel
    {
       public long CartId { get; set; }
       public string ProductName { get; set; }
       public int Quantity { get; set; }
       public string ProductImage { get; set; }
       public float Price { get; set;}
       public long ProductId { get; set; }
       public int OptionId { get; set; }
    }
}
