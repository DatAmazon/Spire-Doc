using giadinhthoxinh.Entities;
using giadinhthoxinh.IService;
using giadinhthoxinh.Models;
using giadinhthoxinh.Util;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Spire.Barcode;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;
namespace giadinhthoxinh.Service
{
    public class HomeService : IHomeService
    {
        private readonly GiadinhthoxinhContext _context;
        public HomeService(GiadinhthoxinhContext context)
        {
            this._context = context;
        }

        public async Task<string> CreateMedicalBillWord(MedicalBillInfor medicalBillInfor)
        {
            Spire.Doc.Document doc = new Spire.Doc.Document();
            Section section = doc.AddSection();
            Table table = section.AddTable(true);

            table.ResetCells(70, 3);
            table.TableFormat.Borders.BorderType = BorderStyle.None;
            float pageWidth = section.PageSetup.PageSize.Width - section.PageSetup.Margins.Left - section.PageSetup.Margins.Right;
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                table.Rows[rowIndex].Cells[0].SetCellWidth(pageWidth * 0.4f, CellWidthType.Point);
                table.Rows[rowIndex].Cells[1].SetCellWidth(pageWidth * 0.3f, CellWidthType.Point);
                table.Rows[rowIndex].Cells[2].SetCellWidth(pageWidth * 0.3f, CellWidthType.Point);
            }
            DrawBorderAround(table, 15, 22, 2);
            PatientInfor patientInfor = await _context.PatientInfors.FirstOrDefaultAsync(p => p.PatientId == medicalBillInfor.PatientId);
            #region title
            AddRow(table, 0, 0, 0, ConstantManager.HEALTH_DEPARTMENT_HANOI, 9.5f, null, null, null, HorizontalAlignment.Center);
            AddRow(table, 0, 1, 0, ConstantManager.MEDICAL_EXAMINATION_FORM, 12.5f, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 0, 2, 0, ConstantManager.TICKET_NUMBER, 9.5f, null, null, null, HorizontalAlignment.Center, patientInfor?.OrderNumber?.ToString());
            AddRow(table, 1, 0, 0, ConstantManager.HOSPITAL_NAME, 9f, true, null, null, HorizontalAlignment.Center, "\r\nPHENIKAA HOÀNG NGÂN", UnderlineStyle.Single, true);
            AddRow(table, 1, 2, 0, ConstantManager.PATIENT_CODE, 9.5f, null, null, null, HorizontalAlignment.Center, medicalBillInfor.PatientId.ToString().Substring(0, 8).ToString());
            AddRow(table, 2, 0, 0, ConstantManager.HOTLINE, 9.5f, null, null, Color.Red, HorizontalAlignment.Center);

            Paragraph type = table.Rows[2].Cells[1].AddParagraph();
            TextRange normalTypeTR = type.AppendText(ConstantManager.TYPE_NORMAL);
            normalTypeTR.CharacterFormat.FontSize = 9.5f;
            AddPicture(type, Path.Combine("Assets", "Image", patientInfor?.Type == 1 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));

            TextRange emergencyType = type.AppendText(ConstantManager.EMERGENCY_TYPE);
            emergencyType.CharacterFormat.FontSize = 9.5f;
            AddPicture(type, Path.Combine("Assets", "Image", patientInfor?.Type == 2 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));

            //AddRow(table, 2, 2, 0, "Barcode ", 9.5f, true, null, Color.Red, HorizontalAlignment.Center);
            #endregion

            #region administrative 
            AddRow(table, 3, 0, 0, ConstantManager.MEDICAL_INFO_TITLE, 11, true);
            AddRow(table, 4, 0, 10, ConstantManager.NAME_IN_UPPER_CASE, 11, null, null, null, null, patientInfor?.Username?.ToUpper());
            AddRow(table, 4, 1, 20, $"{ConstantManager.BIRTHDAY + patientInfor?.Birthday?.Day.ToString()}/{patientInfor.Birthday?.Month.ToString()}/{patientInfor.Birthday?.Year.ToString()}", 11, null, null, null, null);
            AddRow(table, 4, 2, 10, ConstantManager.AGE, 11, null, null, null, null, patientInfor.Age?.ToString());
            Paragraph genderAndJob = table.Rows[5].Cells[0].AddParagraph();
            genderAndJob.Format.LeftIndent = 10;
            TextRange genderTR = genderAndJob.AppendText($"{ConstantManager.GENDER + patientInfor.Gender?.ToString()}   ");
            genderTR.CharacterFormat.FontSize = 11;
            TextRange jobTR = genderAndJob.AppendText($"{ConstantManager.JOB + patientInfor.Job?.ToString()}");
            jobTR.CharacterFormat.FontSize = 11;
            AddRow(table, 5, 1, 20, ConstantManager.ETHNIC, 11, null, null, null, null, patientInfor.Ethnic?.ToString());
            AddRow(table, 5, 2, 10, ConstantManager.NATIONALITY, 11, null, null, null, null, patientInfor.Nationality?.ToString());
            AddRow(table, 6, 0, 10, ConstantManager.ADDRESS, 11, null, null, null, null, patientInfor.Address?.ToString());
            AddRow(table, 7, 0, 10, ConstantManager.WORKPLACE, 11, null, null, null, null, patientInfor.Workplace?.ToString());
            AddRow(table, 7, 1, 20, ConstantManager.PHONE, 11, null, null, null, null, patientInfor.Phone?.ToString());
            #region subject
            table.Rows[8].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[8].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            Paragraph objectPatient = table.Rows[8].Cells[0].AddParagraph();
            objectPatient.Format.LeftIndent = 10;
            TextRange objectPatientTR = objectPatient.AppendText(ConstantManager.SUBJECT_TITLE);
            objectPatientTR.CharacterFormat.FontSize = 11;

            TextRange healthInsuranceTR = objectPatient.AppendText(ConstantManager.HEALTH_INSURANCE);
            healthInsuranceTR.CharacterFormat.FontSize = 11f;
            healthInsuranceTR.CharacterFormat.Italic = true;
            AddPicture(objectPatient, Path.Combine("Assets", "Image", patientInfor?.Subject == 1 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));

            TextRange feeTR = objectPatient.AppendText(ConstantManager.FEE);
            feeTR.CharacterFormat.FontSize = 11f;
            feeTR.CharacterFormat.Italic = true;
            AddPicture(objectPatient, Path.Combine("Assets", "Image", patientInfor?.Subject == 2 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));

            TextRange freeTR = objectPatient.AppendText(ConstantManager.FREE);
            freeTR.CharacterFormat.FontSize = 11f;
            freeTR.CharacterFormat.Italic = true;
            AddPicture(objectPatient, Path.Combine("Assets", "Image", patientInfor?.Subject == 3 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));

            TextRange otherTR = objectPatient.AppendText(ConstantManager.OTHER);
            otherTR.CharacterFormat.FontSize = 11f;
            otherTR.CharacterFormat.Italic = true;
            AddPicture(objectPatient, Path.Combine("Assets", "Image", patientInfor?.Subject == 4 ? ConstantManager.CHECKED : ConstantManager.UNCHECK));
            #endregion
            table.Rows[9].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[9].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[9].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 9, 0, 10, $"10. BHYT giá trị đến ngày {patientInfor.SocialInsurancePeriod?.Day.ToString()} tháng {patientInfor.SocialInsurancePeriod?.Month.ToString()} năm {patientInfor.SocialInsurancePeriod?.Year.ToString()},  Số thẻ BHYT: {patientInfor.InsuranceCardNumber}", 11);
            table.Rows[10].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[10].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 10, 0, 10, ConstantManager.FAMILY_INFO, 11, null, null, null, null, patientInfor.FamilyInformation?.ToString());
            AddRow(table, 10, 2, 0, ConstantManager.PHONE, 11, null, null, null, null, patientInfor.FamilyInformationPhone?.ToString());
            AddRow(table, 11, 0, 10, ConstantManager.EXAMINATION_TIME, 11, null, null, null, null, patientInfor.TimeComeExamination?.ToString("HH:mm"));
            table.Rows[11].Cells[1].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[11].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 11, 1, 0, ConstantManager.START_EXAMINATION_TIME, 11, null, null, null, null, patientInfor.TimeStartExamination?.ToString("HH:mm"));
            TableRow row13 = table.Rows[12];
            row13.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row13.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddTextToCellWithManyDifferentStyle(table, 12, 0, 10, HorizontalAlignment.Left, new string[] { ConstantManager.REFERRAL_DIAGNOSIS, ConstantManager.REFERRAL_DIAGNOSIS_NOTE, $"{patientInfor.DiagnosisOfReferralSite}" }, new float[] { 11, 11, 11 }, new bool[] { false, false, false }, new bool[] { false, true, false }); ;
            #endregion

            #region medical examination information
            AddRow(table, 13, 0, 0, ConstantManager.MEDICAL_EXAMINATION_INFO, 12, true);
            AddRow(table, 14, 0, 10, ConstantManager.MODERN_MEDICINE, 11, true);
            AddRow(table, 15, 0, 10, ConstantManager.MEDICAL_HISTORY, 11, true);
            AddRow(table, 15, 2, 0, ConstantManager.PULSE, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 16, 0, 10, ConstantManager.BLANK_TEXT, 11, null, true, null, null, medicalBillInfor?.ReportParamData?.ReportParameterUserPersonalMedicalHistory);
            AddRow(table, 16, 2, 0, ConstantManager.TEMPERATURE, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 17, 2, 0, ConstantManager.BLOOD_PRESSURE, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 18, 2, 0, ConstantManager.RESPIRATORY_RATE, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 19, 0, 10, ConstantManager.PERSONAL_MEDICAL_HISTORY, 11, true);
            AddRow(table, 19, 2, 0, ConstantManager.WEIGHT, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 20, 0, 10, ConstantManager.OWN, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterUserPersonalMedicalHistory);
            AddRow(table, 20, 2, 0, ConstantManager.HEIGHT, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 21, 0, 10, ConstantManager.FAMILY, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterUserFamilyMedicalHistory);
            AddRow(table, 21, 2, 0, ConstantManager.BMI, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 22, 0, 10, ConstantManager.CLINICAL_EXAMINATION, 11, true);
            AddRow(table, 22, 2, 0, ConstantManager.SPO2, 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 23, 0, 10, ConstantManager.BODY, 11);
            AddRow(table, 24, 0, 10, ConstantManager.BODY_PARTS, 11);
            AddRow(table, 25, 0, 10, ConstantManager.PRELIMINARY_DIAGNOSIS, 11, true);
            AddRow(table, 26, 0, 10, ConstantManager.BLANK_TEXT, 11);
            AddRow(table, 27, 0, 10, ConstantManager.PARACLINICAL_INDICATIONS, 11, true);
            table.Rows[28].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[28].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[28].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 28, 0, 10, ConstantManager.TEST, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterIndicationsParaclinicalTest);
            table.Rows[29].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[29].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[29].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 29, 0, 10, ConstantManager.IMAGE_ANALYSATION, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterIndicationsParaclinicalCdha);
            AddRow(table, 30, 0, 10, ConstantManager.SUMMARY_OF_PARACLINICAL_RESULTS, 11, true);
            table.Rows[31].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[31].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[31].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 31, 0, 10, ConstantManager.BLANK_TEXT, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterSubTest);
            table.Rows[32].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[32].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[32].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 32, 0, 10, ConstantManager.BLANK_TEXT, 11, null, null, null, null, medicalBillInfor?.ReportParamData?.ReportParameterSubTest);
            AddRow(table, 33, 0, 10, ConstantManager.FINAL_DIAGNOSIS, 11, true);
            List<Diagnose> diagnoses = await _context.Diagnoses.Where(x => x.PatientId == patientInfor.PatientId).ToListAsync();
            int diagnosesCount = diagnoses.Count;
            int indexIsAddedIntermediately = 0;
            for (int i = 0; i < diagnosesCount; i++)
            {
                if (i == 0) AddRow(table, 34, 0, 10, ConstantManager.MAIN_DISEASE, 11, null, null, null, null, null);
                table.Rows[34 + i + 1].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
                table.Rows[34 + i + 1].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
                AddRow(table, 34 + i + 1, 0, 10, "", 9.5f, null, null, null, null, diagnoses[i].MainDisease);
                AddTextToCellWithManyDifferentStyle(table, 34 + i + 1, 2, 0, null, new string[] { ConstantManager.ICD_CODE, diagnoses[i].ICDCode }, new float[] { 9.5f, 9.5f }, new bool[] { true, false }, new bool[] { false, false }); ;

            }
            for (int i = 0; i < diagnosesCount; i++)
            {
                if (i == 0) AddRow(table, 35 + diagnosesCount, 0, 10, "- Kèm theo: ", 11, null, null, null, null, null);
                indexIsAddedIntermediately = i + diagnosesCount + 1;
                table.Rows[35 + indexIsAddedIntermediately].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
                table.Rows[35 + indexIsAddedIntermediately].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
                AddRow(table, 35 + indexIsAddedIntermediately, 0, 10, "", 9.5f, null, null, null, null, diagnoses[i].IncludingDiseases);
                AddTextToCellWithManyDifferentStyle(table, 35 + indexIsAddedIntermediately, 2, 0, null, new string[] { ConstantManager.ICD_CODE, diagnoses[i].ICDCode }, new float[] { 9.5f, 9.5f }, new bool[] { true, false }, new bool[] { false, false }); ;
            }
            #endregion

            #region to solve
            AddRow(table, 36 + indexIsAddedIntermediately, 0, 0, ConstantManager.SOLVE, 12, true);
            AddRow(table, 37 + indexIsAddedIntermediately, 2, 0, $"Hà Nội, ngày {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Day.ToString()} tháng {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Month.ToString()} năm {medicalBillInfor?.ReportParamData?.ReportParameterUserStartExaminationDateTime?.Year.ToString()}", 9.5f, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 38 + indexIsAddedIntermediately, 2, 0, ConstantManager.DOCTOR_EXAMINATION, 11, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 39 + indexIsAddedIntermediately, 2, 0, ConstantManager.SIGNATURE_NOTE, 9.5f, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 42 + indexIsAddedIntermediately, 2, 0, "", 11, null, true, null, HorizontalAlignment.Center, medicalBillInfor?.ReportParamData?.Doctor?.Name);
            AddRow(table, 43 + indexIsAddedIntermediately, 0, 0, ConstantManager.NOTE, 12, true);
            table.Rows[44 + indexIsAddedIntermediately].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[44 + indexIsAddedIntermediately].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 44 + indexIsAddedIntermediately, 0, 0, ConstantManager.PRESCRIPTION_NOTE, 11, null, true);
            GenerateBarcode(table, 2, 2, BarCodeType.Code39, medicalBillInfor.PatientId.ToString(), ConstantManager.BARCODE_IMAGE_PATH, 140);

            string documentPath = ConstantManager.WORD_PATH;
            doc.SaveToFile(documentPath, FileFormat.Docx2013);
            doc.Close();

            //ProcessStartInfo docStartInfo = new ProcessStartInfo
            //{
            //    FileName = documentPath,
            //    UseShellExecute = true
            //};
            #endregion

            return "success!";
        }

        public void GenerateBarcode(Table table, int rowIndex, int colIndex, BarCodeType barcodeType, string barcodeData, string imagePath, float imageWidth)
        {
            // create barcode
            BarcodeSettings bs = new BarcodeSettings();
            bs.Type = barcodeType;
            bs.Data = barcodeData;
            BarCodeGenerator bg = new BarCodeGenerator(bs);

            // save barcode in an image
            Image barcodeImage = bg.GenerateImage();
            barcodeImage.Save(imagePath);
            DocPicture picture = table.Rows[rowIndex].Cells[colIndex].AddParagraph().AppendPicture(barcodeImage) as DocPicture;
            picture.Width = imageWidth;
        }
        public async Task<string> CreateMedicalBillToPdf(MedicalBillInfor medicalBillInfor)
        {
            Spire.Doc.Document doc = new Spire.Doc.Document();
            await CreateMedicalBillWord(medicalBillInfor);
            doc.LoadFromFile(ConstantManager.WORD_PATH);
            doc.SaveToFile(ConstantManager.PDF_PATH, FileFormat.PDF);
            doc.Close();
            return "success!";
        }

        public void AddRow(Table table, int rowIndex, int cellIndex, float leftIndent, string text, float fontSize, bool? bold = null, bool? italic = null, Color? textColor = null, HorizontalAlignment? alignment = null, string? binding = null, UnderlineStyle? underlineStyle = null, bool? boldBiding = null)
        {
            TableRow row = table.Rows[rowIndex];
            Paragraph paragraph = row.Cells[cellIndex].AddParagraph();
            if (leftIndent > 0)
            {
                paragraph.Format.LeftIndent = leftIndent;
            }
            TextRange textRange = paragraph.AppendText(text);
            textRange.CharacterFormat.FontSize = fontSize;
            textRange.CharacterFormat.Bold = bold ?? false;
            textRange.CharacterFormat.Italic = italic ?? false;
            if (textColor.HasValue)
                textRange.CharacterFormat.TextColor = textColor.Value;
            if (!string.IsNullOrEmpty(binding))
            {
                TextRange bindingTextRange = paragraph.AppendText(binding);
                bindingTextRange.CharacterFormat.FontSize = underlineStyle.HasValue ? 9 : 11;
                bindingTextRange.CharacterFormat.UnderlineStyle = underlineStyle ?? UnderlineStyle.None;
                bindingTextRange.CharacterFormat.Bold = boldBiding ?? false;
            }
            paragraph.Format.HorizontalAlignment = alignment ?? HorizontalAlignment.Left;
        }

        public void AddTextToCellWithManyDifferentStyle(Table table, int rowIndex, int cellIndex, int? LeftIndent = null, HorizontalAlignment? alignment = null, string[]? texts = null, float[]? fontSizes = null, bool[]? bold = null, bool[]? italic = null)
        {
            if (texts?.Length != fontSizes?.Length
                || (bold != null && texts?.Length != bold.Length)
                || (italic != null && texts?.Length != italic.Length))
            {
                throw new ArgumentException("Array lengths must match.");
            }

            TableRow row = table.Rows[rowIndex];
            Paragraph paragraph = row.Cells[cellIndex].AddParagraph();
            paragraph.Format.HorizontalAlignment = alignment ?? HorizontalAlignment.Left;
            paragraph.Format.LeftIndent = LeftIndent ?? 0;

            for (int i = 0; i < texts?.Length; i++)
            {
                TextRange textRange = paragraph.AppendText(texts[i]);
                textRange.CharacterFormat.FontSize = fontSizes[i];

                if (bold != null)
                    textRange.CharacterFormat.Bold = bold[i];

                if (italic != null)
                    textRange.CharacterFormat.Italic = italic[i];
            }
        }

        void DrawBorderAround(Table table, int startRow, int endRow, int colIndex)
        {
            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                TableCell cell = table.Rows[rowIndex].Cells[colIndex];
                if (rowIndex == startRow)
                    cell.CellFormat.Borders.Top.BorderType = BorderStyle.Single;

                if (rowIndex == endRow)
                    cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Single;

                cell.CellFormat.Borders.Left.BorderType = BorderStyle.Single;
                cell.CellFormat.Borders.Right.BorderType = BorderStyle.Single;
            }
        }

        private DocPicture AddPicture(Paragraph paragraph, string imagePath)
        {
            DocPicture pic = paragraph.AppendPicture(Image.FromFile(imagePath));
            pic.Width = 10;
            pic.Height = 10;
            return pic;
        }

        public async Task<string> MedicalBillSaveChange(PatientInfor medicalBillInfor)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    medicalBillInfor.PatientId = Guid.NewGuid();
                    _context.PatientInfors.Add(medicalBillInfor);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return "Added medical bill successfully";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"add  medical bill failed: {ex.Message}";
                }
            }
        }


    }
}
