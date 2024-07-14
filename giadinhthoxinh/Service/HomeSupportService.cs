using giadinhthoxinh.Entities;
using giadinhthoxinh.IService;
using giadinhthoxinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
namespace giadinhthoxinh.Service
{
    public class HomeSupportService
    {
        private readonly GiadinhthoxinhContext _context;
        public HomeSupportService(GiadinhthoxinhContext context)
        {
            this._context = context;
        }

        public Microsoft.AspNetCore.Mvc.FileStreamResult CreateWord()
        {
            Document doc = new Document();
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

            #region title
            // Row1
            TableRow row1 = table.Rows[0];
            Paragraph firstCellInRow = row1.Cells[0].AddParagraph();
            firstCellInRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tradeMarkAbove = firstCellInRow.AppendText("SỞ Y TẾ THÀNH PHỐ HÀ NỘI\r\n");
            tradeMarkAbove.CharacterFormat.FontSize = 9.5f;
            TextRange tradeMarkBelow = firstCellInRow.AppendText("BỆNH viện ĐẠI HỌC PHENIKAA\r\n");
            tradeMarkBelow.CharacterFormat.FontSize = 9;
            tradeMarkBelow.CharacterFormat.AllCaps = true;
            tradeMarkBelow.CharacterFormat.Bold = true;
            AddRow(table, 0, 1, 0, "PHIẾU KHÁM BỆNH\r\nY HỌC CỔ TRUYỀN", 12.5f, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 0, 2, 0, "Số phiếu: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            // Row2
            AddRow(table, 1, 2, 0, "Mã NB: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            // Row3
            TableRow row3 = table.Rows[2];
            AddRow(table, 2, 0, 0, "Hotline: (024) 222 6699", 9.5f, null, null, Color.Red, HorizontalAlignment.Center);
            Paragraph type = row3.Cells[1].AddParagraph();
            TextRange normalTypeTR = type.AppendText("Thường  ");
            normalTypeTR.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxNormalTR = type.AppendText("\u2610"); // Add checkbox empty (Unicode)
            CheckboxNormalTR.CharacterFormat.FontSize = 20;
            TextRange emergencyType = type.AppendText("     Cấp cứu   ");
            emergencyType.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxEmergencyTR = type.AppendText("\u2610"); // Add checkbox empty (Unicode)
            CheckboxEmergencyTR.CharacterFormat.FontSize = 20;
            type.Format.HorizontalAlignment = HorizontalAlignment.Center;
            AddRow(table, 2, 2, 0, "Barcode ", 9.5f, true, null, Color.Red, HorizontalAlignment.Center);
            #endregion

            #region administrative 
            // Row1
            AddRow(table, 3, 0, 10, "I. HÀNH CHÍNH: ", 11, true);
            // Row2
            AddRow(table, 4, 0, 10, "1. Họ và tên (in hoa): ", 11);
            AddRow(table, 4, 1, 40, "2. Ngày sinh:      /     /    ", 11);
            AddRow(table, 4, 2, 0, "Tuổi: ", 11, null, null, null, HorizontalAlignment.Center);
            // Row3
            TableRow row6 = table.Rows[5];
            Paragraph genderAndJob = row6.Cells[0].AddParagraph();
            genderAndJob.Format.LeftIndent = 10;
            TextRange genderTR = genderAndJob.AppendText("3. Giới tính: ");
            genderTR.CharacterFormat.FontSize = 11;
            TextRange jobTR = genderAndJob.AppendText("4. Nghề nghiệp: ");
            jobTR.CharacterFormat.FontSize = 11;
            AddRow(table, 5, 1, 50, "5. Dân tộc:", 11);
            AddRow(table, 5, 2, 0, "6. Quốc tịch:", 11, null, null, null, HorizontalAlignment.Center);
            // Row4
            AddRow(table, 6, 0, 10, "7. Địa chỉ:", 11);
            // Row5
            AddRow(table, 7, 0, 10, "8. Nơi làm việc: ", 11);
            AddRow(table, 7, 1, 10, "SĐT: ", 11);
            // Row6
            TableRow row9 = table.Rows[8];
            row9.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row9.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;

            Paragraph objectPatient = row9.Cells[0].AddParagraph();
            objectPatient.Format.LeftIndent = 10;
            TextRange objectPatientTR = objectPatient.AppendText("9.Đối tượng:");
            objectPatientTR.CharacterFormat.FontSize = 11;

            TextRange healthInsuranceTR = objectPatient.AppendText("1.BHYT  ");
            healthInsuranceTR.CharacterFormat.FontSize = 11f;
            healthInsuranceTR.CharacterFormat.Italic = true;
            TextRange checkboxHealthInsurance = objectPatient.AppendText("\u2610");
            checkboxHealthInsurance.CharacterFormat.FontSize = 20;

            TextRange feeTR = objectPatient.AppendText("2.Thu phí  ");
            feeTR.CharacterFormat.FontSize = 11f;
            feeTR.CharacterFormat.Italic = true;
            TextRange CheckboxFee = objectPatient.AppendText("\u2610");
            CheckboxFee.CharacterFormat.FontSize = 20;

            TextRange freeTR = objectPatient.AppendText("3.Miễn  ");
            freeTR.CharacterFormat.FontSize = 11f;
            freeTR.CharacterFormat.Italic = true;
            TextRange checkboxFree = objectPatient.AppendText("\u2610");
            checkboxFree.CharacterFormat.FontSize = 20;

            TextRange otherTR = objectPatient.AppendText("4.Khác  ");
            otherTR.CharacterFormat.FontSize = 11f;
            otherTR.CharacterFormat.Italic = true;
            TextRange checkboxOther = objectPatient.AppendText("\u2610");
            checkboxOther.CharacterFormat.FontSize = 20;
            // Row7
            table.Rows[9].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[9].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 9, 0, 10, "10. BHYT giá trị đến ngày .......tháng...... năm......Số thẻ BHYT: ", 11);
            // Row8
            table.Rows[10].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[10].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 10, 0, 10, "11. Họ tên, địa chỉ người nhà khi cần báo tin: ", 11);
            AddRow(table, 10, 2, 0, "Số điện thoại: ", 11);
            // Row9
            AddRow(table, 11, 0, 10, "12. (a) Đến khám bệnh lúc: ", 11);
            AddRow(table, 11, 1, 0, "12. (b) Bắt đầu khám bệnh lúc: ", 11, null, null, null, HorizontalAlignment.Center);
            // Row10
            TableRow row13 = table.Rows[12];
            row13.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row13.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            Paragraph DagnosisReferralSite = row13.Cells[0].AddParagraph();
            DagnosisReferralSite.Format.LeftIndent = 10;
            TextRange DagnosisReferralSiteTR1 = DagnosisReferralSite.AppendText("13. Chẩn đoán của nơi giới thiệu ");
            DagnosisReferralSiteTR1.CharacterFormat.FontSize = 11;
            TextRange DagnosisReferralSiteTR2 = DagnosisReferralSite.AppendText("(nếu có):");
            DagnosisReferralSiteTR2.CharacterFormat.FontSize = 11;
            DagnosisReferralSiteTR2.CharacterFormat.Italic = true;
            #endregion

            #region medical examination information
            AddRow(table, 13, 0, 0, "II. THÔNG TIN KHÁM BỆNH", 12, true);
            AddRow(table, 14, 0, 0, "1. Y HỌC HIỆN ĐẠI\r\n  1.1. Lý do đến khám:\r\n", 11, true);
            AddRow(table, 15, 0, 10, "1.2 Bệnh sử:", 11, true);
            AddRow(table, 15, 2, 0, "Mạch:\t\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 16, 2, 0, "Nhiệt độ:\t\t°C", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 17, 2, 0, "Huyết áp:\t\tmmHg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 18, 2, 0, "Nhịp thở:\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 19, 0, 10, "1.3 Tiền sử bệnh:", 11, true);
            AddRow(table, 19, 2, 0, "Cân nặng:\t\tkg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 20, 0, 10, "-", 11);
            AddRow(table, 20, 2, 0, "Chiều cao:\t\tcm", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 21, 2, 0, "BMI:", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 22, 0, 10, "1.4 Khám lâm sàng:", 11, true);
            AddRow(table, 22, 2, 0, "SPO2:\t\t\t%", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 23, 0, 10, "-Toàn thân", 11);
            AddRow(table, 24, 0, 10, "-Các bộ phận", 11);
            AddRow(table, 25, 0, 10, "1.5 Chuẩn đoán sơ bộ:", 11, true);
            AddRow(table, 26, 0, 10, "-", 11);
            AddRow(table, 27, 0, 10, "-", 11);
            AddRow(table, 28, 0, 10, "1.6 Chỉ định cận lâm sàng:", 11, true);
            AddRow(table, 29, 0, 10, "-Xét nghiệm:", 11);
            AddRow(table, 30, 0, 10, "- Chẩn đoán hình ảnh, TDCN:", 11);
            AddRow(table, 31, 0, 10, "1.7 Tóm tắt kết quả cận lâm sàng:", 11, true);
            AddRow(table, 32, 0, 10, "-", 11);
            AddRow(table, 33, 0, 10, "1.8 Chẩn đoán xác định:", 11, true);
            AddRow(table, 34, 0, 10, "-Bệnh chính: \r\n-Bệnh kèm theo:", 11);
            AddRow(table, 34, 2, 0, "Mã ICD: \r\nMã ICD:\r\n", 11, true, null, null, HorizontalAlignment.Center);
            #endregion

            #region traditional medicines
            AddRow(table, 35, 0, 0, "2. Y HỌC CỔ TRUYỀN", 12, true);
            AddRow(table, 36, 0, 10, "2.1 Vọng chẩn:", 11, true);
            AddRow(table, 37, 0, 10, "-", 11);
            AddRow(table, 38, 0, 10, "2.2. Văn chẩn:", 11, true);
            AddRow(table, 39, 0, 10, "-", 11);
            AddRow(table, 40, 0, 10, "2.3. Vấn chẩn:", 11, true);
            AddRow(table, 41, 0, 10, "-", 11);
            AddRow(table, 42, 0, 10, "-", 11);
            AddRow(table, 43, 0, 10, "2.4 Thiết chẩn:", 11, true);
            AddRow(table, 44, 0, 10, "-", 11);
            AddRow(table, 45, 0, 10, "2.5 Chẩn đoán:", 11, true);
            AddRow(table, 46, 0, 10, "Bệnh chính:\r\nBệnh kèm theo:\r\n", 11);
            AddRow(table, 46, 2, 0, "Mã ICD: \r\nMã ICD:\r\n", 11, true, null, null, HorizontalAlignment.Center);

            #endregion

            #region to solve
            AddRow(table, 47, 0, 0, "III. XỬ TRÍ", 12, true);
            AddRow(table, 48, 0, 10, "1. Y học hiện đại:", 11, true);
            AddRow(table, 49, 0, 10, "-", 11);
            AddRow(table, 50, 0, 10, "2. Y học cổ truyền:", 11, true);
            AddRow(table, 51, 0, 10, "-", 11);
            AddRow(table, 52, 2, 0, "Hà Nội, ngày\ttháng\tnăm 20", 10, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 53, 2, 0, "BÁC SỸ KHÁM BỆNH", 11, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 54, 2, 0, "(Ký và ghi rõ họ tên)", 11, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 55, 0, 0, "*Ghi chú:", 12, true);
            table.Rows[56].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[56].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[56].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 56, 0, 0, "- Uống thuốc theo đơn. Có gì bất thường đến viện kiểm tra lại.\r\n- Người bệnh nhận đơn thuốc tại phòng khám bác sỹ, lĩnh thuốc tại quầy thuốc BHYT hoặc mua thuốc tại các nhà thuốc bệnh viện.\r\n", 11, null, true, null, HorizontalAlignment.Left);
            #endregion
            // Save to stream
            MemoryStream stream = new MemoryStream();
            doc.SaveToStream(stream, FileFormat.Docx2013);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "WordDocument.docx"
            };
        }


        public void AddTextAndCheckboxToCell(Table table, int rowIndex, int cellIndex, string text1, float fontSize1, string text2, bool isChecked)
        {
            TableRow row = table.Rows[rowIndex];
            Paragraph paragraph = row.Cells[cellIndex].AddParagraph();
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

            // Add first text
            TextRange textRange1 = paragraph.AppendText(text1 + "\r\n");
            textRange1.CharacterFormat.FontSize = fontSize1;

            // Add second text with checkbox
            TextRange textRange2 = paragraph.AppendText(text2 + (isChecked ? "\u2611" : "\u2610"));
            textRange2.CharacterFormat.FontSize = 20;
        }

        public string CreateMedicalBill(PatientInfor medicalBillInfor)
        {
            Document doc = new Document();
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

            #region title
            AddTextToCell(table, 0, 0, HorizontalAlignment.Center, new string[] { "SỞ Y TẾ THÀNH PHỐ HÀ NỘI\r\n", "BỆNH VIỆN ĐẠI HỌC PHENIKAA\r\n" }, new float[] { 9.5f, 9f }, new bool[] { false, true });
            AddRow(table, 0, 1, 0, "PHIẾU KHÁM BỆNH", 12.5f, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 0, 2, 0, "Số phiếu: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            AddRow(table, 1, 2, 0, "Mã NB: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            AddRow(table, 2, 0, 0, "Hotline: (024) 222 6699", 9.5f, null, null, Color.Red, HorizontalAlignment.Center);

            Paragraph type = table.Rows[2].Cells[1].AddParagraph();
            TextRange normalTypeTR = type.AppendText("Thường  ");
            normalTypeTR.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxNormalTR = type.AppendText(medicalBillInfor.Type == 1 ? "\u2611" : "\u2610"); // Add checkbox empty (Unicode)
            CheckboxNormalTR.CharacterFormat.FontSize = 20;
            TextRange emergencyType = type.AppendText("     Cấp cứu   ");
            emergencyType.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxEmergencyTR = type.AppendText(medicalBillInfor.Type == 2 ? "\u2611" : "\u2610");
            CheckboxEmergencyTR.CharacterFormat.FontSize = 20;
            type.Format.HorizontalAlignment = HorizontalAlignment.Center;
            AddRow(table, 2, 2, 0, "Barcode ", 9.5f, true, null, Color.Red, HorizontalAlignment.Center);
            #endregion

            #region administrative 
            AddRow(table, 3, 0, 10, "I. HÀNH CHÍNH: ", 11, true);
            AddRow(table, 4, 0, 10, "1. Họ và tên (in hoa): ", 11, null, null, null, null, medicalBillInfor.Username?.ToUpper());
            AddRow(table, 4, 1, 20, $"2. Ngày sinh: {medicalBillInfor.Birthday?.Day.ToString()}/{medicalBillInfor.Birthday?.Month.ToString()}/{medicalBillInfor.Birthday?.Year.ToString()}", 11, null, null, null, null);
            AddRow(table, 4, 2, 10, "Tuổi: ", 11, null, null, null, null, medicalBillInfor.Age?.ToString());
            Paragraph genderAndJob = table.Rows[5].Cells[0].AddParagraph();
            genderAndJob.Format.LeftIndent = 10;
            TextRange genderTR = genderAndJob.AppendText($"3. Giới tính: {medicalBillInfor.Gender?.ToString()}   ");
            genderTR.CharacterFormat.FontSize = 11;
            TextRange jobTR = genderAndJob.AppendText($"4. Nghề nghiệp: {medicalBillInfor.Job?.ToString()}");
            jobTR.CharacterFormat.FontSize = 11;
            AddRow(table, 5, 1, 20, "5. Dân tộc:", 11, null, null, null, null, medicalBillInfor.Ethnic?.ToString());
            AddRow(table, 5, 2, 10, "6. Quốc tịch:", 11, null, null, null, null, medicalBillInfor.Nationality?.ToString());
            AddRow(table, 6, 0, 10, "7. Địa chỉ: ", 11, null, null, null, null, medicalBillInfor.Address?.ToString());
            AddRow(table, 7, 0, 10, "8. Nơi làm việc: ", 11, null, null, null, null, medicalBillInfor.Workplace?.ToString());
            AddRow(table, 7, 1, 20, "SĐT: ", 11, null, null, null, null, medicalBillInfor.Phone?.ToString());
            #region subject
            table.Rows[8].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[8].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            Paragraph objectPatient = table.Rows[8].Cells[0].AddParagraph();
            objectPatient.Format.LeftIndent = 10;
            TextRange objectPatientTR = objectPatient.AppendText("9.Đối tượng:");
            objectPatientTR.CharacterFormat.FontSize = 11;

            TextRange healthInsuranceTR = objectPatient.AppendText(" 1.BHYT ");
            healthInsuranceTR.CharacterFormat.FontSize = 11f;
            healthInsuranceTR.CharacterFormat.Italic = true;
            TextRange checkboxHealthInsurance = objectPatient.AppendText(medicalBillInfor.Subject == 1 ? "\u2611  " : "\u2610  ");
            checkboxHealthInsurance.CharacterFormat.FontSize = 20;

            TextRange feeTR = objectPatient.AppendText("2.Thu phí ");
            feeTR.CharacterFormat.FontSize = 11f;
            feeTR.CharacterFormat.Italic = true;
            TextRange CheckboxFee = objectPatient.AppendText(medicalBillInfor.Subject == 2 ? "\u2611  " : "\u2610  ");
            CheckboxFee.CharacterFormat.FontSize = 20;

            TextRange freeTR = objectPatient.AppendText("3.Miễn ");
            freeTR.CharacterFormat.FontSize = 11f;
            freeTR.CharacterFormat.Italic = true;
            TextRange checkboxFree = objectPatient.AppendText(medicalBillInfor.Subject == 3 ? "\u2611  " : "\u2610  ");
            checkboxFree.CharacterFormat.FontSize = 20;

            TextRange otherTR = objectPatient.AppendText("4.Khác ");
            otherTR.CharacterFormat.FontSize = 11f;
            otherTR.CharacterFormat.Italic = true;
            TextRange checkboxOther = objectPatient.AppendText(medicalBillInfor.Subject == 4 ? "\u2611  " : "\u2610  ");
            checkboxOther.CharacterFormat.FontSize = 20;
            #endregion
            // Row7
            table.Rows[9].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[9].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[9].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 9, 0, 10, $"10. BHYT giá trị đến ngày {medicalBillInfor.SocialInsurancePeriod?.Day.ToString()} tháng {medicalBillInfor.SocialInsurancePeriod?.Month.ToString()} năm {medicalBillInfor.SocialInsurancePeriod?.Month.ToString()} Số thẻ BHYT: {medicalBillInfor.InsuranceCardNumber}", 11);
            // Row8
            table.Rows[10].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[10].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 10, 0, 10, "11. Họ tên, địa chỉ người nhà khi cần báo tin: ", 11, null, null, null, null, medicalBillInfor.FamilyInformation?.ToString());
            AddRow(table, 10, 2, 0, "Số điện thoại: ", 11, null, null, null, null, medicalBillInfor.FamilyInformationPhone?.ToString());

            // Row9
            AddRow(table, 11, 0, 10, "12. (a) Đến khám bệnh lúc: ", 11, null, null, null, null, medicalBillInfor.TimeComeExamination?.ToString("HH:mm"));
            table.Rows[11].Cells[1].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[11].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 11, 1, 0, "12. (b) Bắt đầu khám lúc: ", 11, null, null, null, null, medicalBillInfor.TimeStartExamination?.ToString("HH:mm"));
            // Row10
            TableRow row13 = table.Rows[12];
            row13.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row13.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;

            Paragraph DagnosisReferralSite = row13.Cells[0].AddParagraph();
            DagnosisReferralSite.Format.LeftIndent = 10;
            TextRange DagnosisReferralSiteTR1 = DagnosisReferralSite.AppendText("13. Chẩn đoán của nơi giới thiệu ");
            DagnosisReferralSiteTR1.CharacterFormat.FontSize = 11;
            TextRange DagnosisReferralSiteTR2 = DagnosisReferralSite.AppendText($"(nếu có): {medicalBillInfor.DiagnosisOfReferralSite}");
            DagnosisReferralSiteTR2.CharacterFormat.FontSize = 11;
            DagnosisReferralSiteTR2.CharacterFormat.Italic = true;
            AddTextToCell(table, 12, 0, HorizontalAlignment.Left, new string[] { "Chẩn đoán của nơi giới thiệu ", "(nếu có): " }, new float[] { 11, 11 }, new bool[] { false, false }, new bool[] { false, true });
            #endregion

            #region medical examination information
            AddRow(table, 13, 0, 0, "II. THÔNG TIN KHÁM BỆNH", 12, true);
            AddRow(table, 14, 0, 0, "1. Y HỌC HIỆN ĐẠI\r\n  1.1. Lý do đến khám:\r\n", 11, true);
            AddRow(table, 15, 0, 10, "1.2 Bệnh sử: \r\n- ", 11, true);
            AddRow(table, 15, 2, 0, "Mạch:\t\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 16, 2, 0, "Nhiệt độ:\t\t°C", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 17, 2, 0, "Huyết áp:\t\tmmHg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 18, 2, 0, "Nhịp thở:\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 19, 0, 10, "1.3 Tiền sử bệnh:", 11, true);
            AddRow(table, 19, 2, 0, "Cân nặng:\t\tkg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 20, 0, 10, "-", 11);
            AddRow(table, 20, 2, 0, "Chiều cao:\t\tcm", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 21, 2, 0, "BMI:", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 22, 0, 10, "1.4 Khám lâm sàng:", 11, true);
            AddRow(table, 22, 2, 0, "SPO2:\t\t\t%", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 23, 0, 10, "- Toàn thân", 11);
            AddRow(table, 24, 0, 10, "- Các bộ phận", 11);
            AddRow(table, 25, 0, 10, "1.5 Chuẩn đoán sơ bộ:", 11, true);
            AddRow(table, 26, 0, 10, "-", 11);
            AddRow(table, 27, 0, 10, "-", 11);
            AddRow(table, 28, 0, 10, "1.6 Chỉ định cận lâm sàng:", 11, true);
            AddRow(table, 29, 0, 10, "- Xét nghiệm:", 11);
            AddRow(table, 30, 0, 10, "- Chẩn đoán hình ảnh, TDCN:", 11);
            AddRow(table, 31, 0, 10, "1.7 Tóm tắt kết quả cận lâm sàng:", 11, true);
            AddRow(table, 32, 0, 10, "-", 11);
            AddRow(table, 33, 0, 10, "1.8 Chẩn đoán xác định:", 11, true);
            AddRow(table, 34, 0, 10, "- Bệnh chính: \r\n-Bệnh kèm theo:", 11);
            AddRow(table, 34, 2, 0, "Mã ICD: \r\nMã ICD:\r\n", 11, true, null, null, HorizontalAlignment.Center);
            #endregion

            #region to solve
            AddRow(table, 35, 0, 0, "III. XỬ TRÍ", 12, true);
            AddRow(table, 36, 2, 0, "Hà Nội, ngày\ttháng\tnăm 20", 10, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 37, 2, 0, "BÁC SỸ KHÁM BỆNH", 11, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 38, 2, 0, "(Ký và ghi rõ họ tên)", 11, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 39, 0, 0, "*Ghi chú:", 12, true);
            table.Rows[40].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[40].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 40, 0, 0, "- Uống thuốc theo đơn. Có gì bất thường đến viện kiểm tra lại.\r\n- Người bệnh nhận đơn thuốc tại phòng khám bác sỹ, lĩnh thuốc tại quầy thuốc BHYT hoặc mua thuốc tại các nhà thuốc bệnh viện.\r\n", 11, null, true);
            #endregion
            // Save to stream
            MemoryStream stream = new MemoryStream();
            doc.SaveToStream(stream, FileFormat.Docx2013);
            stream.Position = 0;

            string filePath = @"C:\Users\DELL\OneDrive\Desktop\test_export\WordDocument.docx";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
            return "success!";
        }

        public void AddRow(Table table, int rowIndex, int cellIndex, float leftIndent, string text, float fontSize, bool? bold = null, bool? italic = null, Color? textColor = null, HorizontalAlignment? alignment = null, string? binding = null)
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
            if (!String.IsNullOrEmpty(binding))
            {
                TextRange bindingTextRange = paragraph.AppendText(binding);
                bindingTextRange.CharacterFormat.FontSize = 11;
            }
            paragraph.Format.HorizontalAlignment = alignment ?? HorizontalAlignment.Left;
        }

        public void AddTextToCell(Table table, int rowIndex, int cellIndex, HorizontalAlignment? alignment = null, string[]? texts = null, float[]? fontSizes = null, bool[]? bold = null, bool[]? italic = null)
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

        public async Task<string> PrintMedicalBillToPdf(PatientInfor medicalBillInfor)
        {
            Document doc = new Document();
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

            #region title
            // Row1
            TableRow row1 = table.Rows[0];
            Paragraph firstCellInRow = row1.Cells[0].AddParagraph();
            firstCellInRow.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tradeMarkAbove = firstCellInRow.AppendText("SỞ Y TẾ THÀNH PHỐ HÀ NỘI\r\n");
            tradeMarkAbove.CharacterFormat.FontSize = 9.5f;
            TextRange tradeMarkBelow = firstCellInRow.AppendText("BỆNH viện ĐẠI HỌC PHENIKAA\r\n");
            tradeMarkBelow.CharacterFormat.FontSize = 9;
            tradeMarkBelow.CharacterFormat.AllCaps = true;
            tradeMarkBelow.CharacterFormat.Bold = true;
            AddRow(table, 0, 1, 0, "PHIẾU KHÁM BỆNH\r\nY HỌC CỔ TRUYỀN", 12.5f, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 0, 2, 0, "Số phiếu: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            // Row2
            AddRow(table, 1, 2, 0, "Mã NB: ", 9.5f, null, null, null, HorizontalAlignment.Center);
            // Row3
            TableRow row3 = table.Rows[2];
            AddRow(table, 2, 0, 0, "Hotline: (024) 222 6699", 9.5f, null, null, Color.Red, HorizontalAlignment.Center);
            Paragraph type = row3.Cells[1].AddParagraph();
            TextRange normalTypeTR = type.AppendText("Thường  ");
            normalTypeTR.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxNormalTR = type.AppendText(medicalBillInfor.Type == 1 ? "\u2611" : "\u2610"); // Add checkbox empty (Unicode)
            CheckboxNormalTR.CharacterFormat.FontSize = 20;
            TextRange emergencyType = type.AppendText("     Cấp cứu   ");
            emergencyType.CharacterFormat.FontSize = 9.5f;
            TextRange CheckboxEmergencyTR = type.AppendText(medicalBillInfor.Type == 2 ? "\u2611" : "\u2610");
            CheckboxEmergencyTR.CharacterFormat.FontSize = 20;
            type.Format.HorizontalAlignment = HorizontalAlignment.Center;
            AddRow(table, 2, 2, 0, "Barcode ", 9.5f, true, null, Color.Red, HorizontalAlignment.Center);
            #endregion

            #region administrative 
            // Row1
            AddRow(table, 3, 0, 10, "I. HÀNH CHÍNH: ", 11, true);
            // Row2
            AddRow(table, 4, 0, 10, "1. Họ và tên (in hoa): ", 11, null, null, null, null, medicalBillInfor.Username?.ToUpper());
            AddRow(table, 4, 1, 20, $"2. Ngày sinh: {medicalBillInfor.Birthday?.Day.ToString()}/{medicalBillInfor.Birthday?.Month.ToString()}/{medicalBillInfor.Birthday?.Year.ToString()}", 11, null, null, null, null);
            AddRow(table, 4, 2, 10, "Tuổi: ", 11, null, null, null, null, medicalBillInfor.Age?.ToString());
            // Row3
            TableRow row6 = table.Rows[5];
            Paragraph genderAndJob = row6.Cells[0].AddParagraph();
            genderAndJob.Format.LeftIndent = 10;
            TextRange genderTR = genderAndJob.AppendText($"3. Giới tính: {medicalBillInfor.Gender?.ToString()}   ");
            genderTR.CharacterFormat.FontSize = 11;
            TextRange jobTR = genderAndJob.AppendText($"4. Nghề nghiệp: {medicalBillInfor.Job?.ToString()}");
            jobTR.CharacterFormat.FontSize = 11;
            AddRow(table, 5, 1, 20, "5. Dân tộc:", 11, null, null, null, null, medicalBillInfor.Ethnic?.ToString());
            AddRow(table, 5, 2, 10, "6. Quốc tịch:", 11, null, null, null, null, medicalBillInfor.Nationality?.ToString());
            AddRow(table, 6, 0, 10, "7. Địa chỉ: ", 11, null, null, null, null, medicalBillInfor.Address?.ToString());
            AddRow(table, 7, 0, 10, "8. Nơi làm việc: ", 11, null, null, null, null, medicalBillInfor.Workplace?.ToString());
            AddRow(table, 7, 1, 20, "SĐT: ", 11, null, null, null, null, medicalBillInfor.Phone?.ToString());
            // Row6
            TableRow row9 = table.Rows[8];
            row9.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row9.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;

            Paragraph objectPatient = row9.Cells[0].AddParagraph();
            objectPatient.Format.LeftIndent = 10;
            TextRange objectPatientTR = objectPatient.AppendText("9.Đối tượng:");
            objectPatientTR.CharacterFormat.FontSize = 11;

            TextRange healthInsuranceTR = objectPatient.AppendText(" 1.BHYT ");
            healthInsuranceTR.CharacterFormat.FontSize = 11f;
            healthInsuranceTR.CharacterFormat.Italic = true;
            TextRange checkboxHealthInsurance = objectPatient.AppendText(medicalBillInfor.Subject == 1 ? "\u2611  " : "\u2610  ");
            checkboxHealthInsurance.CharacterFormat.FontSize = 20;

            TextRange feeTR = objectPatient.AppendText("2.Thu phí ");
            feeTR.CharacterFormat.FontSize = 11f;
            feeTR.CharacterFormat.Italic = true;
            TextRange CheckboxFee = objectPatient.AppendText(medicalBillInfor.Subject == 2 ? "\u2611  " : "\u2610  ");
            CheckboxFee.CharacterFormat.FontSize = 20;

            TextRange freeTR = objectPatient.AppendText("3.Miễn ");
            freeTR.CharacterFormat.FontSize = 11f;
            freeTR.CharacterFormat.Italic = true;
            TextRange checkboxFree = objectPatient.AppendText(medicalBillInfor.Subject == 3 ? "\u2611  " : "\u2610  ");
            checkboxFree.CharacterFormat.FontSize = 20;

            TextRange otherTR = objectPatient.AppendText("4.Khác ");
            otherTR.CharacterFormat.FontSize = 11f;
            otherTR.CharacterFormat.Italic = true;
            TextRange checkboxOther = objectPatient.AppendText(medicalBillInfor.Subject == 4 ? "\u2611  " : "\u2610  ");
            checkboxOther.CharacterFormat.FontSize = 20;
            // Row7
            table.Rows[9].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[9].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[9].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 9, 0, 10, $"10. BHYT giá trị đến ngày {medicalBillInfor.SocialInsurancePeriod?.Day.ToString()} tháng {medicalBillInfor.SocialInsurancePeriod?.Month.ToString()} năm {medicalBillInfor.SocialInsurancePeriod?.Year.ToString()}, Số thẻ BHYT: {medicalBillInfor.InsuranceCardNumber}", 11);
            // Row8
            table.Rows[10].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[10].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 10, 0, 10, "11. Họ tên, địa chỉ người nhà khi cần báo tin: ", 11, null, null, null, null, medicalBillInfor.FamilyInformation?.ToString());
            AddRow(table, 10, 2, 0, "Số điện thoại: ", 11, null, null, null, null, medicalBillInfor.FamilyInformationPhone?.ToString());
            // Row9
            AddRow(table, 11, 0, 10, "12. (a) Đến khám bệnh lúc: ", 11, null, null, null, null, medicalBillInfor.TimeComeExamination?.ToString("HH:mm"));
            table.Rows[11].Cells[1].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[11].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 11, 1, 0, "12. (b) Bắt đầu khám lúc: ", 11, null, null, null, null, medicalBillInfor.TimeStartExamination?.ToString("HH:mm"));
            // Row10
            TableRow row13 = table.Rows[12];
            row13.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            row13.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            Paragraph DagnosisReferralSite = row13.Cells[0].AddParagraph();
            DagnosisReferralSite.Format.LeftIndent = 10;
            TextRange DagnosisReferralSiteTR1 = DagnosisReferralSite.AppendText("13. Chẩn đoán của nơi giới thiệu ");
            DagnosisReferralSiteTR1.CharacterFormat.FontSize = 11;
            TextRange DagnosisReferralSiteTR2 = DagnosisReferralSite.AppendText($"(nếu có): {medicalBillInfor.DiagnosisOfReferralSite}");
            DagnosisReferralSiteTR2.CharacterFormat.FontSize = 11;
            DagnosisReferralSiteTR2.CharacterFormat.Italic = true;
            #endregion

            #region medical examination information
            AddRow(table, 13, 0, 0, "II. THÔNG TIN KHÁM BỆNH", 12, true);
            AddRow(table, 14, 0, 0, "1. Y HỌC HIỆN ĐẠI\r\n  1.1. Lý do đến khám:\r\n", 11, true);
            AddRow(table, 15, 0, 10, "1.2 Bệnh sử:", 11, true);
            AddRow(table, 15, 2, 0, "Mạch:\t\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 16, 2, 0, "Nhiệt độ:\t\t°C", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 17, 2, 0, "Huyết áp:\t\tmmHg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 18, 2, 0, "Nhịp thở:\t\tL/phút", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 19, 0, 10, "1.3 Tiền sử bệnh:", 11, true);
            AddRow(table, 19, 2, 0, "Cân nặng:\t\tkg", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 20, 0, 10, "-", 11);
            AddRow(table, 20, 2, 0, "Chiều cao:\t\tcm", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 21, 2, 0, "BMI:", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 22, 0, 10, "1.4 Khám lâm sàng:", 11, true);
            AddRow(table, 22, 2, 0, "SPO2:\t\t\t%", 11, null, true, null, HorizontalAlignment.Left);
            AddRow(table, 23, 0, 10, "-Toàn thân", 11);
            AddRow(table, 24, 0, 10, "-Các bộ phận", 11);
            AddRow(table, 25, 0, 10, "1.5 Chuẩn đoán sơ bộ:", 11, true);
            AddRow(table, 26, 0, 10, "-", 11);
            AddRow(table, 27, 0, 10, "-", 11);
            AddRow(table, 28, 0, 10, "1.6 Chỉ định cận lâm sàng:", 11, true);
            AddRow(table, 29, 0, 10, "-Xét nghiệm:", 11);
            AddRow(table, 30, 0, 10, "- Chẩn đoán hình ảnh, TDCN:", 11);
            AddRow(table, 31, 0, 10, "1.7 Tóm tắt kết quả cận lâm sàng:", 11, true);
            AddRow(table, 32, 0, 10, "-", 11);
            AddRow(table, 33, 0, 10, "1.8 Chẩn đoán xác định:", 11, true);
            AddRow(table, 34, 0, 10, "-Bệnh chính: \r\n-Bệnh kèm theo:", 11);
            AddRow(table, 34, 2, 0, "Mã ICD: \r\nMã ICD:\r\n", 11, true, null, null, HorizontalAlignment.Center);
            #endregion

            #region traditional medicines
            AddRow(table, 35, 0, 0, "2. Y HỌC CỔ TRUYỀN", 12, true);
            AddRow(table, 36, 0, 10, "2.1 Vọng chẩn:", 11, true);
            AddRow(table, 37, 0, 10, "-", 11);
            AddRow(table, 38, 0, 10, "2.2. Văn chẩn:", 11, true);
            AddRow(table, 39, 0, 10, "-", 11);
            AddRow(table, 40, 0, 10, "2.3. Vấn chẩn:", 11, true);
            AddRow(table, 41, 0, 10, "-", 11);
            AddRow(table, 42, 0, 10, "-", 11);
            AddRow(table, 43, 0, 10, "2.4 Thiết chẩn:", 11, true);
            AddRow(table, 44, 0, 10, "-", 11);
            AddRow(table, 45, 0, 10, "2.5 Chẩn đoán:", 11, true);
            AddRow(table, 46, 0, 10, "Bệnh chính:\r\nBệnh kèm theo:\r\n", 11);
            AddRow(table, 46, 2, 0, "Mã ICD: \r\nMã ICD:\r\n", 11, true, null, null, HorizontalAlignment.Center);
            #endregion

            #region to solve
            AddRow(table, 47, 0, 0, "III. XỬ TRÍ", 12, true);
            AddRow(table, 48, 0, 10, "1. Y học hiện đại:", 11, true);
            AddRow(table, 49, 0, 10, "-", 11);
            AddRow(table, 50, 0, 10, "2. Y học cổ truyền:", 11, true);
            AddRow(table, 51, 0, 10, "-", 11);
            AddRow(table, 52, 2, 0, "Hà Nội, ngày\ttháng\tnăm 20", 10, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 53, 2, 0, "BÁC SỸ KHÁM BỆNH", 11, true, null, null, HorizontalAlignment.Center);
            AddRow(table, 54, 2, 0, "(Ký và ghi rõ họ tên)", 11, null, true, null, HorizontalAlignment.Center);
            AddRow(table, 55, 0, 0, "*Ghi chú:", 12, true);
            table.Rows[56].Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
            table.Rows[56].Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;
            table.Rows[56].Cells[2].CellFormat.HorizontalMerge = CellMerge.Continue;
            AddRow(table, 56, 0, 0, "- Uống thuốc theo đơn. Có gì bất thường đến viện kiểm tra lại.\r\n- Người bệnh nhận đơn thuốc tại phòng khám bác sỹ, lĩnh thuốc tại quầy thuốc BHYT hoặc mua thuốc tại các nhà thuốc bệnh viện.\r\n", 11, null, true, null, HorizontalAlignment.Left);
            #endregion
            // Save to stream
            MemoryStream stream = new MemoryStream();
            doc.SaveToStream(stream, FileFormat.PDF);
            stream.Position = 0;

            string filePath = @"C:\Users\DELL\OneDrive\Desktop\test_export\WordDocument.pdf";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }

            return await MedicalBillSaveChange(medicalBillInfor);
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

        public async Task<string> CreateMedicalBill1(MedicalBillInfor medicalBillInfor)
        {
            return "ok";
        }


        //public async Task<List<TblProduct>> GetData()
        //{
        //    return await _context.TblProducts.ToListAsync();
        //}
    }
}
