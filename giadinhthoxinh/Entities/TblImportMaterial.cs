using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblImportMaterial")]
public partial class TblImportMaterial
{
    [Key]
    [Column("PK_iImportMaterialID")]
    public int PkIImportMaterialId { get; set; }

    [Column("FK_iImportOrderID")]
    public int FkIImportOrderId { get; set; }

    [Column("FK_iMaterialID")]
    public int FkIMaterialId { get; set; }

    [Column("iQuatity")]
    public int? IQuatity { get; set; }

    [Column("fPrice")]
    public double? FPrice { get; set; }

    [ForeignKey("FkIImportOrderId")]
    [InverseProperty("TblImportMaterials")]
    public virtual TblImportOrder FkIImportOrder { get; set; } = null!;

    [ForeignKey("FkIMaterialId")]
    [InverseProperty("TblImportMaterials")]
    public virtual TblMaterial FkIMaterial { get; set; } = null!;
}
