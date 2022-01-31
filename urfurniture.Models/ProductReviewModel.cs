using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{ 
  public  class ProductReviewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public short RatingValue { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long UserRefId { get; set; }
        public long ProductRefId { get; set; }
    }
}
