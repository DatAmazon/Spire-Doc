using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblImage")]
public partial class TblImage
{
    [Key]
    [Column("PK_iImageID")]
    public int PkIImageId { get; set; }

    [Column("FK_iProductID")]
    public int FkIProductId { get; set; }

    [Column("sImage")]
    [StringLength(250)]
    public string? SImage { get; set; }

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblImages")]
    public virtual TblProduct FkIProduct { get; set; } = null!;
}
