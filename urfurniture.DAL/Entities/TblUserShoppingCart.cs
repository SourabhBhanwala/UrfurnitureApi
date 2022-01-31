using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
    public class TblUserShoppingCart
    {
       
        [Key]
        public long ShoppingCartID { get; set; }
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "bit")]
        public Boolean IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public TblUser TblUser { get; set; }
        [ForeignKey("TblUser")]
        public long UserRefId { get; set; }
        public IEnumerable<TblCartItem> TblCartItems { get; set; }
    }
}
