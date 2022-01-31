using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models.Dto.ResponseDto
{
   public class UserResDTOs
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
