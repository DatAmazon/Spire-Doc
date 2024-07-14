using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblSupplier")]
public partial class TblSupplier
{
    [Key]
    [Column("PK_iSupplierID")]
    public int PkISupplierId { get; set; }

    [Column("sSupplierName")]
    [StringLength(200)]
    public string? SSupplierName { get; set; }

    [Column("sPhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SPhone { get; set; }

    [Column("sEmail")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SEmail { get; set; }

    [Column("sAddress")]
    [StringLength(250)]
    public string? SAddress { get; set; }

    [InverseProperty("FkISupplier")]
    public virtual ICollection<TblImportOrder> TblImportOrders { get; set; } = new List<TblImportOrder>();
}
