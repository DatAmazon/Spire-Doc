using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblCheckoutDetail")]
public partial class TblCheckoutDetail
{
    [Key]
    [Column("PK_iCheckoutDetailID")]
    public int PkICheckoutDetailId { get; set; }

    [Column("FK_iOrderID")]
    public int FkIOrderId { get; set; }

    [Column("FK_iProductID")]
    public int? FkIProductId { get; set; }

    [Column("iQuantity")]
    public int? IQuantity { get; set; }

    [Column("fPrice")]
    public double? FPrice { get; set; }

    [ForeignKey("FkIOrderId")]
    [InverseProperty("TblCheckoutDetails")]
    public virtual TblOrder FkIOrder { get; set; } = null!;

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblCheckoutDetails")]
    public virtual TblProduct? FkIProduct { get; set; }
}
