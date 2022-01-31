using System;
using System.Collections.Generic;
using System.Text;
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.DAL.ViewModel
{
  public class ProductReviewVM
    {
        public List<ProductReviewDto> ProductReviewDtos { get; set; }
        public int count;
    }
}
