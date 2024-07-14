using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblCategory")]
public partial class TblCategory
{
    [Key]
    [Column("PK_iCategoryID")]
    public int PkICategoryId { get; set; }

    [Column("sCategoryName")]
    [StringLength(60)]
    public string? SCategoryName { get; set; }

    [InverseProperty("FkICategory")]
    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
