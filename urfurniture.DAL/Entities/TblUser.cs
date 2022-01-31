using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace urfurniture.DAL.Entities
{
    public class TblUser 
    {
       [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public string MobileNo { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblRole TblRole { get; set; }
        [ForeignKey("TblRole")]
        public int RoleId { get; set; }
        public IEnumerable<TblUserAddress> TblUserAddresses { get; set; }
        public IEnumerable<TblUserShoppingCart> TblUserShoppingCarts { get; set; }
        public IEnumerable<TblProductReview> TblProductReviews { get; set; }
        public IEnumerable<TblOrder> TblOrders { get; set; }
        

    }
}
