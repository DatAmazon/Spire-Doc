using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblMaterPriceImport")]
public partial class TblMaterPriceImport
{
    [Key]
    [Column("PK_iMaterPriceImportID")]
    public int PkIMaterPriceImportId { get; set; }

    [Column("FK_iMaterialID")]
    public int FkIMaterialId { get; set; }

    [Column("fMaterPriceImport")]
    public double? FMaterPriceImport { get; set; }

    [Column("dtStartDay", TypeName = "datetime")]
    public DateTime? DtStartDay { get; set; }

    [Column("dtEndDay", TypeName = "datetime")]
    public DateTime? DtEndDay { get; set; }

    [ForeignKey("FkIMaterialId")]
    [InverseProperty("TblMaterPriceImports")]
    public virtual TblMaterial FkIMaterial { get; set; } = null!;
}
