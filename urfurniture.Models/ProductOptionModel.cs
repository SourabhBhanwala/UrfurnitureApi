using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{ 
   public class ProductOptionModel
    {
        public int OptionPriceIncrement { get; set; }
        public string OptionName { get; set; }
        public long ProductRefId { get; set; }
        public int ProductOptionId { get; set; }

    }
}
