using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblImportOrder")]
public partial class TblImportOrder
{
    [Key]
    [Column("PK_iImportOrderID")]
    public int PkIImportOrderId { get; set; }

    [Column("FK_iAccountID")]
    public int FkIAccountId { get; set; }

    [Column("FK_iSupplierID")]
    public int FkISupplierId { get; set; }

    [Column("dtDateAdded", TypeName = "datetime")]
    public DateTime? DtDateAdded { get; set; }

    [Column("sDeliver")]
    [StringLength(80)]
    public string? SDeliver { get; set; }

    [Column("iState")]
    public int? IState { get; set; }

    [ForeignKey("FkIAccountId")]
    [InverseProperty("TblImportOrders")]
    public virtual TblUser FkIAccount { get; set; } = null!;

    [ForeignKey("FkISupplierId")]
    [InverseProperty("TblImportOrders")]
    public virtual TblSupplier FkISupplier { get; set; } = null!;

    [InverseProperty("FkIImportOrder")]
    public virtual ICollection<TblCheckinDetail> TblCheckinDetails { get; set; } = new List<TblCheckinDetail>();

    [InverseProperty("FkIImportOrder")]
    public virtual ICollection<TblImportMaterial> TblImportMaterials { get; set; } = new List<TblImportMaterial>();
}
