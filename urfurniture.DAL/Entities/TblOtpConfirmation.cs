using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace urfurniture.DAL.Entities
{
    public class TblOtpConfirmation
    {
        [Key]
        public int OtpId { get; set; }
        public long UserId { get; set; }
        public string Otp { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdateTime { get; set; }

    }
}
