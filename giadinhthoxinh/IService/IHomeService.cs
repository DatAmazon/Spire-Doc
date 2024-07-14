using giadinhthoxinh.Entities;
using giadinhthoxinh.Models;

namespace giadinhthoxinh.IService
{
    public interface IHomeService
    {
        Microsoft.AspNetCore.Mvc.FileStreamResult CreateWord();
        string CreateMedicalBill(PatientInfor medicalBillInfor);
        Task<string> PrintMedicalBillToPdf(PatientInfor medicalBillInfor);
        Task<string> CreateMedicalBill1(MedicalBillInfor medicalBillInfor);
    }
}
