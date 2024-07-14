using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblMaterial")]
public partial class TblMaterial
{
    [Key]
    [Column("PK_iMaterialID")]
    public int PkIMaterialId { get; set; }

    [Column("sMaterialName")]
    [StringLength(50)]
    public string? SMaterialName { get; set; }

    [Column("sDescribe")]
    [StringLength(200)]
    public string? SDescribe { get; set; }

    [Column("iQuatity")]
    public int? IQuatity { get; set; }

    [Column("sUnit")]
    [StringLength(20)]
    public string? SUnit { get; set; }

    [InverseProperty("FkIMaterial")]
    public virtual ICollection<TblMaterSize> TblMaterSizes { get; set; }

    [InverseProperty("FkIMaterial")]
    public virtual ICollection<TblImportMaterial> TblImportMaterials { get; set; } = new List<TblImportMaterial>();

    [InverseProperty("FkIMaterial")]
    public virtual ICollection<TblMaterColor> TblMaterColors { get; set; } = new List<TblMaterColor>();

    [InverseProperty("FkIMaterial")]
    public virtual ICollection<TblMaterPriceImport> TblMaterPriceImports { get; set; } = new List<TblMaterPriceImport>();


}
