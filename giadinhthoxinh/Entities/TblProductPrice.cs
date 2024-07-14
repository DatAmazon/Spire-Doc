using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblProductPrice")]
public partial class TblProductPrice
{
    [Key]
    [Column("PK_iProductPriceID")]
    public int PkIProductPriceId { get; set; }

    [Column("FK_iProductID")]
    public int FkIProductId { get; set; }

    [Column("fPrice")]
    public double? FPrice { get; set; }

    [Column("dtStartDay", TypeName = "datetime")]
    public DateTime? DtStartDay { get; set; }

    [Column("dtEndDay", TypeName = "datetime")]
    public DateTime? DtEndDay { get; set; }

    [ForeignKey("FkIProductId")]
    [InverseProperty("TblProductPrices")]
    public virtual TblProduct FkIProduct { get; set; } = null!;
}
