using System;
using System.Collections.Generic;
using System.Text;
using urfurniture.Models;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.DAL.ViewModel
{
  public  class AdminProductVM
    {
       public List<ProductDto> Products { get; set; }
       public List<ProductOptionDto> Options { get; set; }
       public List<ProductImageModel> Images { get; set; }
    }
}
