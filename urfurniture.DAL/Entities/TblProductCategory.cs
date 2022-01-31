using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace urfurniture.DAL.Entities
{
    public class TblProductCategory
    {
        [Key]
        public int ProdctCategoryId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] UpdatedTime { get; set; }
        public IEnumerable<TblSubCategory> TblSubCategory { get; set; }
        public IEnumerable<TblCategoryImage> TblCategoryImage { get; set; }
    }
}
