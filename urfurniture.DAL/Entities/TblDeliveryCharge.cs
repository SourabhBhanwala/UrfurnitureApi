using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace urfurniture.DAL.Entities
{
  public  class TblDeliveryCharge
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
    }
}
