using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

[Table("tblPermission")]
public partial class TblPermission
{
    [Key]
    [Column("PK_iPermissionID")]
    public int PkIPermissionId { get; set; }

    [Column("sPermissionName")]
    [StringLength(50)]
    public string? SPermissionName { get; set; }

    [Column("iState")]
    public int? IState { get; set; }

    [InverseProperty("FkIPermission")]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
