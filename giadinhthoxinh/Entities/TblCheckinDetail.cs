using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblCheckinDetail")]
public partial class TblCheckinDetail
{
    [Key]
    [Column("PK_iCheckinDetailID")]
    public int PkICheckinDetailId { get; set; }

    [Column("FK_iImportOrderID")]
    public int FkIImportOrderId { get; set; }

    [Column("FK_iProductID")]
    public int? FkIProductId { get; set; }

    [Column("iQuatity")]
    public int? IQuatity { get; set; }

    [Column("fPrice")]
    public double? FPrice { get; set; }

    [ForeignKey("FkIImportOrderId")]
    [InverseProperty("TblCheckinDetails")]
    public virtual TblImportOrder FkIImportOrder { get; set; } = null!;

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblCheckinDetails")]
    public virtual TblProduct? FkIProduct { get; set; }
}
