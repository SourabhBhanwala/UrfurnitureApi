using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
   public class UserDetailsModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }

    }
}
