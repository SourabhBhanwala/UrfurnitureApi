using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
   public class AuthenticationResultModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Message { get; set; }
        public string Roles { get; set; }
    }
}
