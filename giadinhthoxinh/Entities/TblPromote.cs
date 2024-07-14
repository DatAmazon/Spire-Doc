using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblPromote")]
public partial class TblPromote
{
    [Key]
    [Column("PK_iPromoteID")]
    public int PkIPromoteId { get; set; }

    [Column("sPromoteName")]
    [StringLength(200)]
    public string? SPromoteName { get; set; }

    [Column("sPromoteRate")]
    public double? SPromoteRate { get; set; }

    [Column("dtStartDay", TypeName = "datetime")]
    public DateTime? DtStartDay { get; set; }

    [Column("dtEndDay", TypeName = "datetime")]
    public DateTime? DtEndDay { get; set; }

    [InverseProperty("FkIPromote")]
    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
