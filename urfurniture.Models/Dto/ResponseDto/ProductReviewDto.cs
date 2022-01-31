using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models.Dto.ResponseDto
{
 public  class ProductReviewDto
    {
        public string Title { get; set; }
        public short RatingValue { get; set; }
        public string Description { get; set; }
        public DateTime PublishAt { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public List<string> ImageUrl { get; set; }
    }
}
