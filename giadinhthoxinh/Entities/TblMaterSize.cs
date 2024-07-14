using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblMaterSize")]
public partial class TblMaterSize
{
    [Key]
    [Column("PK_iMaterSizeID")]
    public int PkIMaterSizeId { get; set; }

    [Column("FK_iMaterialID")]
    public int FkIMaterialId { get; set; }

    [Column("sMaterSize")]
    [StringLength(20)]
    public string? SMaterSize { get; set; }

    [ForeignKey("FkIMaterialId")]
    [InverseProperty("TblMaterSizes")]
    public virtual TblMaterial FkIMaterial { get; set; } = null!;
}
