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
using Spire.Xls;

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

        public Workbook GetTemplate(string productId, int templateId, out string name) {
            ReportTemplate template = reportTemplateSQL.GetById(templateId);
            string templatePath = Path.Combine(AppSettings.FolderPath, template.Path);
            name = template.Name;
            return GetWorkbook(productId, templatePath);
        }

        public void PrintTemplate(string productId, int templateId) {
            ReportTemplate template = reportTemplateSQL.GetById(templateId);
            string templatePath = Path.Combine(AppSettings.FolderPath, template.Path);

            Workbook workbook = GetWorkbook(productId, templatePath);

            workbook.PrintDocument.Print();
        }

        private Workbook GetWorkbook(string productId, string path) {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path);

            var dict = GetDetailValues(productId);

            var sheet = workbook.ActiveSheet;

            int rows = sheet.LastRow;
            int cols = sheet.LastColumn;
            for (int row = 1; row <= rows; row++) {
                for (int col = 1; col <= cols; col++) {
                    var cell = sheet.Range[row, col];
                    string cellValue = cell.Text;
                    if (string.IsNullOrEmpty(cellValue)) {
                        continue;
                    }

                    // reportId(xxx_xxx_xx) 
                    if (!Regex.IsMatch(cellValue, @"^(\d{3}_){2}\d{2}$")) {
                        continue;
                    }

                    if (!dict.TryGetValue(cellValue, out string value)) {
                        cell.Text = string.Empty;
                        continue;
                    }

                    if (cellValue.EndsWith("00")) {
                        cell.Text = (value == "1" ? "合格" : "不合格");
                        continue;
                    }

                    if (cellValue.EndsWith("30")) {
                        string imageFileName = Path.GetFileName(value);
                        sheet.Pictures.Add(row, col, Path.Combine(imageFilePath, imageFileName));
                        cell.Text = string.Empty;
                        continue;
                    }

                    cell.Text = value;
                }
            }
            return workbook;
        }

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
    }
}
