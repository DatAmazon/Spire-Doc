using giadinhthoxinh.Entities;
using giadinhthoxinh.IService;
using giadinhthoxinh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace giadinhthoxinh.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly GiadinhthoxinhContext context;
        private readonly IHomeService homeService;
        public HomeController(GiadinhthoxinhContext context, IHomeService homeService)
        {
            this.context = context;
            this.homeService = homeService;
        }

        [HttpPost("print-medical-bill-to-word")]
        public async Task<ActionResult> PrintMedicalBillToWord(List<MedicalInforDataPost>? dataPost)
        {
            if (dataPost == null || dataPost.Count == 0 || string.IsNullOrEmpty(dataPost[0].ReportParamData))
            {
                return BadRequest("Invalid data");
            }

            ReportParamData? reportParamData = JsonConvert.DeserializeObject<ReportParamData>(dataPost[0].ReportParamData);
            if (reportParamData == null)
                return BadRequest("Invalid ReportParamData JSON");

            MedicalBillInfor medicalBillDataPost = new MedicalBillInfor
            {
                ReportTypeCode = dataPost[0].ReportTypeCode,
                ReportParamData = reportParamData,
                PatientReceptionId = dataPost[0].PatientReceptionId,
                PatientDesignateServiceId = dataPost[0].PatientDesignateServiceId,
                BasicInformationPatientId = dataPost[0].BasicInformationPatientId,
                PatientId = dataPost[0].PatientId
            };
            return Ok(await homeService.CreateMedicalBillWord(medicalBillDataPost));
        }

        [HttpPost("print-medical-bill-to-pdf")]
        public async Task<ActionResult> PrintMedicalBillToPdf(List<MedicalInforDataPost>? dataPost)
        {
            if (dataPost == null || dataPost.Count == 0 || string.IsNullOrEmpty(dataPost[0].ReportParamData))
            {
                return BadRequest("Invalid data");
            }

            ReportParamData? reportParamData = JsonConvert.DeserializeObject<ReportParamData>(dataPost[0].ReportParamData);
            if (reportParamData == null)
                return BadRequest("Invalid ReportParamData JSON");

            MedicalBillInfor medicalBillDataPost = new MedicalBillInfor
            {
                ReportTypeCode = dataPost[0].ReportTypeCode,
                ReportParamData = reportParamData,
                PatientReceptionId = dataPost[0].PatientReceptionId,
                PatientDesignateServiceId = dataPost[0].PatientDesignateServiceId,
                BasicInformationPatientId = dataPost[0].BasicInformationPatientId,
                PatientId = dataPost[0].PatientId
            };
            return Ok(await homeService.CreateMedicalBillToPdf(medicalBillDataPost));
        }

    }
}
