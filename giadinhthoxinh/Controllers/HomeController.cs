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
        //[HttpGet]
        //public async Task<List<TblProduct>> GetData()
        //{
        //    List<TblProduct> lstProduct = await homeService.GetData();
        //    return lstProduct;
        //}
        //[HttpGet("{id}")]
        //public async Task< IActionResult> ExportWord(Guid id)
        //{
        //    try
        //    {
        //        var medicalBill = context.MedicalBills.FirstOrDefault(b => b.medical_bill_id == id);

        //        if (medicalBill == null)
        //            return NotFound();

        //        var document = new Document();
        //        Section section = document.AddSection();
        //        Paragraph paragraph = section.AddParagraph();

        //        // Add content to the document
        //        paragraph.AppendText($"Medical Bill ID: {medicalBill.medical_bill_id}");
        //        paragraph.AppendText($"Username: {medicalBill.username}");
        //        paragraph.AppendText($"Birthday: {medicalBill.birthday}");
        //        paragraph.AppendText($"Age: {medicalBill.age}");
        //        paragraph.AppendText($"Gender: {medicalBill.gender}");
        //        paragraph.AppendText($"Job: {medicalBill.job}");
        //        paragraph.AppendText($"Ethnic: {medicalBill.ethnic}");
        //        paragraph.AppendText($"Nationality: {medicalBill.nationality}");
        //        paragraph.AppendText($"Type: {medicalBill.type}");

        //        // Save document
        //        MemoryStream stream = new MemoryStream();
        //        document.SaveToStream(stream, FileFormat.Docx);
        //        stream.Seek(0, SeekOrigin.Begin);

        //        return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "medical_bill.docx");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        //[HttpGet]
        //public async Task<IActionResult> ExportExcel()
        //{
        //    List<TblProduct> listProduct = await homeService.GetData();
        //    MemoryStream memoryStream = homeService.ExportProductsToExcel(listProduct);
        //    string fileName = "product.xlsx";
        //    string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    return File(memoryStream, mimeType, fileName);
        //}
        //[HttpGet]
        //public async Task<IActionResult> ExportExcel2()
        //{
        //    List<TblProduct> products = await homeService.GetData();
        //    MemoryStream memoryStream = new MemoryStream();
        //    using (var package = new ExcelPackage(memoryStream))
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Products");

        //        worksheet.Cells.LoadFromCollection(products, true);
        //        package.SaveAs(memoryStream);
        //        memoryStream.Position = 0;// Rewind the memory stream to the beginning
        //    }

        //    string fileName = $"product_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
        //    string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    return File(memoryStream, mimeType, fileName);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ExportWord()
        //{
        //    List<TblProduct> listProduct = await homeService.GetData();
        //    MemoryStream memoryStream = homeService.ExportProductsToWord(listProduct);
        //    string fileName = "products.docx";
        //    string mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //    return File(memoryStream, mimeType, fileName);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ExportPdf()
        //{
        //    List<TblProduct> listProduct = await homeService.GetData();
        //    MemoryStream memoryStream = homeService.ExportProductsToWord(listProduct);
        //    string fileName = "products.docx";
        //    string mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //    return File(memoryStream, mimeType, fileName);
        //}
        [HttpPost("create-medical-bill")]
        public string CreateMedicalBill(PatientInfor medicalBillInfor)
        {
            return homeService.CreateMedicalBill(medicalBillInfor);
        }
        [HttpGet("create-word")]
        public IActionResult CreateWord()
        {
            return homeService.CreateWord();
        }
        [HttpPost("print-medical-bill-to-pdf")]
        public async Task<string> PrintMedicalBillToPdf(PatientInfor medicalBillInfor)
        {
            return await homeService.PrintMedicalBillToPdf(medicalBillInfor);
        }


        [HttpPost("print-medical-bill-to-pdf1")]
        public async Task<ActionResult> PrintMedicalBillToPdf1(List<MedicalInforDataPost>? dataPost)
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
            await homeService.CreateMedicalBill1(medicalBillDataPost);

            return Ok(medicalBillDataPost);
        }

    }
}
