using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebAPI.po;
using WebAPI.sql;
using WebAPI.utils;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebAPI.service.impl {
    public class ReportTemplateService : IReportTemplateService {

        private IReportTemplateSQL reportTemplateSQL { get; }
        private IDetailSQL detailSQL { get; }

        public ReportTemplateService(IReportTemplateSQL reportTemplateSQL, IDetailSQL detailSQL) {
            this.reportTemplateSQL = reportTemplateSQL;
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
            IDictionary<string, string> valueDict = GetDetailValues(productId);
            name = template.Name;

            IWorkbook workbook;
            using (var fs = File.OpenRead(templatePath)) {
                workbook = new XSSFWorkbook(fs);
            }

            FillTemplate(workbook.GetSheetAt(0), valueDict);

            return workbook;
        }

        private IDictionary<string, string> GetDetailValues(string productId) {
            return detailSQL.GetTemplates(productId)
                 .ToDictionary(
                    value => $"{value.ModuleKey}_{value.ItemKey}_{value.RecordKey}",
                    value => value.RecordValue
                 );
        }

        private void FillTemplate(ISheet sheet, IDictionary<string, string> valueDict) {
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
                    if (Regex.IsMatch(cellValue, @"^(\d{3}_){2}\d{2}$")) {
                        cell.SetCellValue(valueDict.TryGetValue(cellValue, out string value) ? value : "");
                    }
                }
            }
        }
    }
}
