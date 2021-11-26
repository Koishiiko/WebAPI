using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.po;
using WebAPI.sql;
using WebAPI.utils;

namespace WebAPI.service {
    public class MaterialService : IMaterialService {

        private IReportSQL reportSQL { get; }
        private IDetailSQL detailSQL { get; }
        private IValidSQL validSQL { get; }
        private IModuleSQL moduleSQL { get; }

        private readonly int baseStepId = 0;
        private readonly string baseModuleId = "000";
        private readonly string pIdReportId = "000_001_31";
        private readonly string typeReportId = "000_002_31";

        public MaterialService(IReportSQL reportSQL, IDetailSQL detailSQL, IValidSQL validSQL, IModuleSQL moduleSQL) {
            this.reportSQL = reportSQL;
            this.detailSQL = detailSQL;
            this.validSQL = validSQL;
            this.moduleSQL = moduleSQL;
        }

        public MaterialDTO GetByProductId(string productId) {
            List<ReportDTO> rows = reportSQL.GetALLByProductId(productId);
            if (!rows.Any()) {
                return null;
            }

            MaterialDTO result = new MaterialDTO() {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            rows.ForEach((row) => {
                int stepId = row.StepId;
                string guid = row.TestGuid;
                result.Data.Add(stepId, this.GetModuleData(guid));
                if (stepId != baseStepId) {
                    result.Valids.Add(stepId, this.GetModuleValid(guid));
                }
            });

            return result;
        }

        private IDictionary<string, IDictionary<string, string>> GetModuleData(string guid) {
            var result = new Dictionary<string, IDictionary<string, string>>();

            List<Detail> details = detailSQL.GetByGuid(guid);
            details.ForEach((detail) => {
                if (!result.ContainsKey(detail.ModuleKey)) {
                    result.Add(detail.ModuleKey, new Dictionary<string, string>());
                }
                string reportId = $"{detail.ModuleKey}_{detail.ItemKey}_{detail.RecordKey}";
                result[detail.ModuleKey].Add(reportId, detail.RecordValue);
            });

            return result;
        }

        private IDictionary<string, bool> GetModuleValid(string guid) {
            var result = new Dictionary<string, bool>();

            List<Valid> valids = validSQL.GetByGuid(guid);
            valids.ForEach((valid) => {
                result.Add(valid.ModuleId, valid.Value);
            });

            return result;
        }

        public MaterialDTO GetByGuid(string guid) {
            Report report = reportSQL.GetByGuid(guid);

            MaterialDTO result = new MaterialDTO() {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            Report baseReport = reportSQL.GetLastByProductId(baseStepId, report.ProductId);

            result.Data.Add(baseStepId, this.GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, this.GetModuleData(guid));
            result.Valids.Add(report.StepId, this.GetModuleValid(guid));

            return result;
        }

        public MaterialDTO GetStepDataByProductId(int stepId, string productId) {
            MaterialDTO result = new MaterialDTO {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            Report baseReport = reportSQL.GetLastByProductId(baseStepId, productId);
            Report report = reportSQL.GetLastByProductId(stepId, productId);

            result.Data.Add(baseStepId, this.GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, this.GetModuleData(report.TestGuid));
            result.Valids.Add(report.StepId, this.GetModuleValid(report.TestGuid));

            return result;
        }

        public bool Save(MaterialDTO material, AccountJWTPayload payload) {
            var baseData = material.Data[baseStepId][baseModuleId];
            string productId = baseData[pIdReportId];
            string productType = baseData[typeReportId];
            string testor = payload.AccountName;

            foreach (var stepPair in material.Data) {
                int stepId = stepPair.Key;
                var modules = stepPair.Value;

                string guid = Guid.NewGuid().ToString();
                var stepValids = material.Valids[stepId];

                Report report = GetReport(guid, productId, productType, stepId, testor);

                int moduleCount = moduleSQL.GetCountByStepId(stepId);
                if (stepValids.Count == moduleCount) {
                    report.TestResult = 1;
                }

                foreach (var modulePair in modules) {
                    string moduleId = modulePair.Key;
                    var items = modulePair.Value;

                    if (!items.Any()) {
                        continue;
                    }

                    if (stepId != baseStepId) {
                        Valid valid = new Valid() {
                            Guid = guid,
                            ModuleId = moduleId,
                            Value = stepValids[moduleId]
                        };
                        if (!valid.Value) {
                            report.TestResult = 2;
                        }
                        validSQL.Save(valid);
                    }

                    foreach (var itemPair in items) {
                        string reportId = itemPair.Key;

                        var arr = reportId.Split('_');
                        Detail detail = new Detail {
                            TestGuid = guid,
                            ModuleKey = arr[0],
                            ItemKey = arr[1],
                            RecordKey = arr[2],
                            RecordValue = itemPair.Value
                        };
                        detailSQL.Save(detail);
                    }
                }
                report.EndTime = DateTime.Now;
                reportSQL.Save(report);
            }
            return true;
        }

        private Report GetReport(string guid, string productId, string productType, int stepId, string testor) {
            return new Report {
                TestGuid = guid,
                ProductId = productId,
                ProductType = productType,
                StepId = stepId,
                Testor = testor,
                StationId = 1,
                UploadFlag = "1",
                BeginTime = DateTime.Now
            };
        }
    }
}
