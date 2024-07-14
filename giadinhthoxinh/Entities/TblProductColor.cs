using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblProductColor")]
public partial class TblProductColor
{
    [Key]
    [Column("PK_iProductColorID")]
    public int PkIProductColorId { get; set; }

    [Column("FK_iProductID")]
    public int FkIProductId { get; set; }

    [Column("sProductColor")]
    [StringLength(40)]
    public string? SProductColor { get; set; }

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblProductColors")]
    public virtual TblProduct FkIProduct { get; set; } = null!;
}
