using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblReview")]
public partial class TblReview
{
    [Key]
    [Column("PK_iReviewID")]
    public int PkIReviewId { get; set; }

    [Column("FK_iProductID")]
    public int FkIProductId { get; set; }

    [Column("FK_iAccountID")]
    public int FkIAccountId { get; set; }

    [Column("iStarRating")]
    public int? IStarRating { get; set; }

    [Column("dtReviewTime", TypeName = "datetime")]
    public DateTime? DtReviewTime { get; set; }

    [ForeignKey("FkIAccountId")]
    [InverseProperty("TblReviews")]
    public virtual TblUser FkIAccount { get; set; } = null!;

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblReviews")]
    public virtual TblProduct FkIProduct { get; set; } = null!;
}
