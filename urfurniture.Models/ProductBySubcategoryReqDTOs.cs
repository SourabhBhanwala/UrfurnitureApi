using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
  public  class ProductBySubcategoryReqDTOs
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       public List<int> Subcategory { get; set; }
    }
}
