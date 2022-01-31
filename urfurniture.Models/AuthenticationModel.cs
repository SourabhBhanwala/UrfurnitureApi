using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace urfurniture.Models
{
    public class AuthenticationModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public  string Roles { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public long ShoppingCartID { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
