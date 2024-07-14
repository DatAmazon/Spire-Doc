using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblProduct")]
public partial class TblProduct
{
    [Key]
    [Column("PK_iProductID")]
    public int PkIProductId { get; set; }

    [Column("FK_iCategoryID")]
    public int FkICategoryId { get; set; }

    [Column("FK_iPromoteID")]
    public int FkIPromoteId { get; set; }

    [Column("sProductName")]
    [StringLength(200)]
    public string? SProductName { get; set; }

    [Column("sDescribe")]
    [StringLength(1000)]
    public string? SDescribe { get; set; }

    [Column("fPrice")]
    public double? FPrice { get; set; }

    [Column("sColor")]
    [StringLength(40)]
    public string? SColor { get; set; }

    [Column("sSize")]
    [StringLength(20)]
    public string? SSize { get; set; }

    [Column("sImage")]
    [StringLength(250)]
    public string? SImage { get; set; }

    [Column("sUnit")]
    [StringLength(20)]
    public string? SUnit { get; set; }

    [Column("iQuantity")]
    public int? IQuantity { get; set; }

    [ForeignKey("FkICategoryId")]
    [InverseProperty("TblProducts")]
    public virtual TblCategory FkICategory { get; set; } = null!;

    [ForeignKey("FkIPromoteId")]
    [InverseProperty("TblProducts")]
    public virtual TblPromote FkIPromote { get; set; } = null!;

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblCheckinDetail> TblCheckinDetails { get; set; } = new List<TblCheckinDetail>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblCheckoutDetail> TblCheckoutDetails { get; set; } = new List<TblCheckoutDetail>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblImage> TblImages { get; set; } = new List<TblImage>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblProductColor> TblProductColors { get; set; } = new List<TblProductColor>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblProductPrice> TblProductPrices { get; set; } = new List<TblProductPrice>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblProductSize> TblProductSizes { get; set; } = new List<TblProductSize>();

    [InverseProperty("FkIProduct")]
    public virtual ICollection<TblReview> TblReviews { get; set; } = new List<TblReview>();
}
