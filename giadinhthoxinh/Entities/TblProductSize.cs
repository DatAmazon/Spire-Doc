using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblProductSize")]
public partial class TblProductSize
{
    [Key]
    [Column("PK_iProductSizeID")]
    public int PkIProductSizeId { get; set; }

    [Column("FK_iProductID")]
    public int FkIProductId { get; set; }

    [Column("sSizeName")]
    [StringLength(20)]
    public string? SSizeName { get; set; }

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblProductSizes")]
    public virtual TblProduct FkIProduct { get; set; } = null!;
}
