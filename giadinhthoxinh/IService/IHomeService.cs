using giadinhthoxinh.Entities;
using giadinhthoxinh.Models;

namespace giadinhthoxinh.IService
{
    public interface IHomeService
    {
        Task<string> CreateMedicalBillWord(MedicalBillInfor medicalBillInfor);
        Task<string> CreateMedicalBillToPdf(MedicalBillInfor medicalBillInfor);
    }
}
