using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace urfurniture.DAL.Entities
{
    public class TblRole
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool? IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
