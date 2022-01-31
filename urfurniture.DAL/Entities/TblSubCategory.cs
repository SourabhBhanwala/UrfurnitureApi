using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
   public class TblSubCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; } 
        public TblProductCategory TblProductCategory { get; set; }
        [ForeignKey("TblProductCategory")]
        public int ProductCategoryRefId { get; set; }
        public IEnumerable<TblProduct> TblProducts { get; set; }
    }
}
