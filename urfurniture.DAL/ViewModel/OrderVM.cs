using System;
using System.Collections.Generic;
using System.Text;
using urfurniture.Models;

namespace urfurniture.DAL.ViewModel
{
   public class OrderVM
    {
        public List<OrderDto> Orders { get; set; }
        public long count { get; set; }
    }
}
