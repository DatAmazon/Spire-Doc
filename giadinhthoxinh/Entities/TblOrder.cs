using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblOrder")]
public partial class TblOrder
{
    [Key]
    [Column("PK_iOrderID")]
    public int PkIOrderId { get; set; }

    [Column("FK_iAccountID")]
    public int FkIAccountId { get; set; }

    [Column("sCustomerName")]
    [StringLength(50)]
    public string? SCustomerName { get; set; }

    [Column("sCustomerPhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SCustomerPhone { get; set; }

    [Column("sDeliveryAddress")]
    [StringLength(150)]
    public string? SDeliveryAddress { get; set; }

    [Column("dInvoidDate", TypeName = "datetime")]
    public DateTime DInvoidDate { get; set; }

    [Column("sBiller")]
    [StringLength(50)]
    public string? SBiller { get; set; }

    [Column("iDeliveryMethod")]
    public int? IDeliveryMethod { get; set; }

    [Column("fSurcharge")]
    public double? FSurcharge { get; set; }

    [Column("iPaid")]
    public int? IPaid { get; set; }

    [Column("sState")]
    [StringLength(50)]
    public string? SState { get; set; }

    [Column("iTotal")]
    public int? ITotal { get; set; }

    [ForeignKey("FkIAccountId")]
    [InverseProperty("TblOrders")]
    public virtual TblUser FkIAccount { get; set; } = null!;

    [InverseProperty("FkIOrder")]
    public virtual ICollection<TblCheckoutDetail> TblCheckoutDetails { get; set; } = new List<TblCheckoutDetail>();
}
