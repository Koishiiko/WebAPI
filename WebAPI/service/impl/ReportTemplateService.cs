using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebAPI.entity;
using WebAPI.sql;
using WebAPI.utils;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Text.RegularExpressions;
using WebAPI.po;
using NPOI.HSSF.UserModel;

namespace WebAPI.service.impl {
    public class ReportTemplateService : IReportTemplateService {

        private readonly IReportTemplateSQL reportTemplateSQL;
        private readonly IReportSQL reportSQL;
        private readonly IDetailSQL detailSQL;

        private readonly string imageFilePath = Path.Combine(AppSettings.FolderPath, AppSettings.ImagePath);

        public ReportTemplateService(IReportTemplateSQL reportTemplateSQL, IReportSQL reportSQL, IDetailSQL detailSQL) {
            this.reportTemplateSQL = reportTemplateSQL;
            this.reportSQL = reportSQL;
            this.detailSQL = detailSQL;
        }

        public List<ReportTemplate> GetAll() {
            return reportTemplateSQL.GetAll();
        }

        public string Save(IFormFile file) {
            string relativePath = Path.Combine(AppSettings.ReportTemplatePath, file.FileName);

            string newName = FileUtils.SaveFile(file, Path.Combine(AppSettings.FolderPath, relativePath));
            relativePath = Path.Combine(AppSettings.ReportTemplatePath, newName);

            reportTemplateSQL.Save(new ReportTemplate { Name = newName, Path = relativePath });

            return relativePath;
        }

        public IWorkbook GetTemplate(string productId, int templateId, out string name) {
            ReportTemplate template = reportTemplateSQL.GetById(templateId);
            string templatePath = Path.Combine(AppSettings.FolderPath, template.Path);
            var valueDict = GetDetailValues(productId);
            name = template.Name;

            IWorkbook workbook;
            using (var fs = File.OpenRead(templatePath)) {
                workbook = new XSSFWorkbook(fs);
            }

            FillTemplate(workbook, valueDict);

            return workbook;
        }

        /// <summary>
        /// 获取当前产品的所有记录值字典
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>
        ///     Key: reportId
        ///     Value: recordValue
        /// </returns>
        private IDictionary<string, string> GetDetailValues(string productId) {
            List<RecordPO> records = reportSQL.GetAllByProductId(productId);

            IDictionary<string, string> dict = new Dictionary<string, string>();

            records.ForEach(record => {
                List<DetailPO> details = detailSQL.GetDataByGuid(record.TestGuid);
                details.ForEach(detail => {
                    if (!string.IsNullOrEmpty(detail.ReportId)) {
                        dict.TryAdd(detail.ReportId, detail.Value);
                    }
                });
            });

            return dict;
        }

        /// <summary>
        /// 将sheet表中的reportId替换为对应的recordValue
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="valueDict"></param>
        private void FillTemplate(IWorkbook workbook, in IDictionary<string, string> valueDict) {
            ISheet sheet = workbook.GetSheetAt(0);

            int rows = sheet.LastRowNum;
            for (int rowNum = 0; rowNum <= rows; rowNum++) {
                IRow row = sheet.GetRow(rowNum);
                if (row == null) {
                    continue;
                }

                int columns = row.LastCellNum;
                for (int columnNum = 0; columnNum <= columns; columnNum++) {
                    ICell cell = row.GetCell(columnNum);
                    if (cell == null) {
                        continue;
                    }

                    string cellValue = cell.ToString();
                    // reportId(xxx_xxx_xx) 
                    if (!Regex.IsMatch(cellValue, @"^(\d{3}_){2}\d{2}$")) {
                        continue;
                    }

                    if (!valueDict.TryGetValue(cellValue, out string value)) {
                        cell.SetCellValue(string.Empty);
                        continue;
                    }

                    if (cellValue.EndsWith("00")) {
                        cell.SetCellValue(value == "1" ? "合格" : "不合格");
                        continue;
                    }

                    if (cellValue.EndsWith("30")) {
                        string imageFileName = Path.GetFileName(value);
                        byte[] bytes = File.ReadAllBytes(Path.Combine(imageFilePath, imageFileName));
                        sheet.CreateDrawingPatriarch().CreatePicture(
                            new XSSFClientAnchor(0, 0, 0, 0, columnNum, rowNum, columnNum + 1, rowNum + 1),
                            workbook.AddPicture(bytes, PictureType.PNG)
                        );
                        cell.SetCellValue(string.Empty);
                        continue;
                    }

                    cell.SetCellValue(value);
                }
            }
        }
    }
}
