using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
  public  class TblCartItem
    {
        
        [Key]
        public long CartId { get; set; }
        [Column(TypeName = "bit")]
        public Boolean SaveForLater { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeAdded { get; set; }
        [Column(TypeName = "bit")]
        public Boolean IsActive { get; set; }
        [Timestamp]
        public byte[] UpdateTime { get; set; }
        public TblProduct TblProduct { get; set; }
        [ForeignKey("TblProduct")]
        public long ProductRefId { get; set; }
        public TblUserShoppingCart TblUserShoppingCart { get; set; }
        [ForeignKey("TblUserShoppingCart")]
        public long UserShoppingCartRefId { get; set; }
        public int ProductOptionRefId { get; set; }
    }
}
