using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblMaterColor")]
public partial class TblMaterColor
{
    [Key]
    [Column("PK_iMaterColorID")]
    public int PkIMaterColorId { get; set; }

    [Column("FK_iMaterialID")]
    public int FkIMaterialId { get; set; }

    [Column("sMaterColor")]
    [StringLength(40)]
    public string? SMaterColor { get; set; }

    [ForeignKey("FkIMaterialId")]
    [InverseProperty("TblMaterColors")]
    public virtual TblMaterial FkIMaterial { get; set; } = null!;
}
