using System;
using System.Collections.Generic;
using System.Text;
using urfurniture.DAL.Entities;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.DAL.model
{
  public class ProductsVM
    {
        public List<ProductDto> Products { get; set; }
        public int Count { get; set; }
        public List<string> ImageUrl { get; set; }
    }
}
