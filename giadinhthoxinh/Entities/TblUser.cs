using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblUser")]
public partial class TblUser
{
    [Key]
    [Column("PK_iAccountID")]
    public int PkIAccountId { get; set; }

    [Column("FK_iPermissionID")]
    public int FkIPermissionId { get; set; }

    [Column("sEmail")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SEmail { get; set; }

    [Column("sPass")]
    [StringLength(50)]
    public string? SPass { get; set; }

    [Column("sUserName")]
    [StringLength(50)]
    public string? SUserName { get; set; }

    [Column("sPhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SPhone { get; set; }

    [Column("sAddress")]
    [StringLength(150)]
    public string? SAddress { get; set; }

    [Column("iState")]
    public int? IState { get; set; }

    [ForeignKey("FkIPermissionId")]
    [InverseProperty("TblUsers")]
    public virtual TblPermission FkIPermission { get; set; } = null!;

    [InverseProperty("FkIAccount")]
    public virtual ICollection<TblImportOrder> TblImportOrders { get; set; } = new List<TblImportOrder>();

    [InverseProperty("FkIAccount")]
    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();

    [InverseProperty("FkIAccount")]
    public virtual ICollection<TblReview> TblReviews { get; set; } = new List<TblReview>();
}
