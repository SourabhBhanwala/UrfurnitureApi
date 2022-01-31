using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
  public  class ProductModel
    { 
        public long ProductId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public float Price { get; set; }
        public string Image { get; set; } 
        public int Discount { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductOptionModel> ProductOptions { get; set; }
        public int ProdctSubCategoryRefId { get; set; }
    }
}
