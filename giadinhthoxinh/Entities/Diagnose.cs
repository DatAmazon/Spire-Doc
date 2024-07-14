using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace giadinhthoxinh.Entities
{
    [Table("diagnose")]
    public class Diagnose
    {
        [Key]
        [Column("diagnose_id")]
        public Guid? DiagnoseId {  get; set; }
        public Guid? PatientId { get; set; }
        public string? MainDisease { get; set; }
        public string? IncludingDiseases {get; set; }
        public string ICDCode { get; set; }
        //public virtual PatientInfor? Patient { get; set; }
    }
}
