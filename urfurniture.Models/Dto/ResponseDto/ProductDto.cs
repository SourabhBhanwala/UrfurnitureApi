using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models.Dto.ResponseDto
{
 public class ProductDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Discount { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProdctSubCategoryId { get; set; }
       
    }
}
