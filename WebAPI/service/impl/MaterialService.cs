using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.po;
using WebAPI.sql;
using WebAPI.utils;

namespace WebAPI.service {
    public class MaterialService : IMaterialService {

        private readonly IReportSQL reportSQL;
        private readonly IDetailSQL detailSQL;
        private readonly IValidSQL validSQL;
        private readonly IModuleSQL moduleSQL;

        private static readonly int baseStepId = 0;
        private static readonly string baseModuleId = "000";
        private static readonly string pIdReportId = "000_001_31";
        private static readonly string typeReportId = "000_002_31";

        private static readonly int stationId = 1;
        private static readonly string uploadFlag = "1";
        private enum TestResult { Valid = 1, NoValid = 2 }

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
                result.Data.Add(stepId, GetModuleData(guid));
                if (stepId != baseStepId) {
                    result.Valids.Add(stepId, GetModuleValid(guid));
                }
            });

            return result;
        }

        /// <summary>
        /// 将detail表数据转换为字典形式
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>
        ///     {
        ///         模块id: {
        ///             报告id: 记录值,
        ///             报告id: 记录值,
        ///             ...
        ///         },
        ///         模块id: {...},
        ///         ...
        ///     }
        /// </returns>
        private IDictionary<string, IDictionary<string, string>> GetModuleData(string guid) {
            List<Detail> details = detailSQL.GetByGuid(guid);

            var result = new Dictionary<string, IDictionary<string, string>>();
            details.ForEach((detail) => {
                if (!result.ContainsKey(detail.ModuleKey)) {
                    result.Add(detail.ModuleKey, new Dictionary<string, string>());
                }
                string reportId = $"{detail.ModuleKey}_{detail.ItemKey}_{detail.RecordKey}";
                result[detail.ModuleKey].Add(reportId, detail.RecordValue);
            });

            return result;
        }

        /// <summary>
        /// 将valid表数据转换为字典形式
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>
        ///     {
        ///         模块1: 是否验证,
        ///         模块2: 是否验证,
        ///         ...
        ///     }
        /// </returns>
        private IDictionary<string, bool> GetModuleValid(string guid) {
            List<Valid> valids = validSQL.GetByGuid(guid);

            var result = new Dictionary<string, bool>();
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

            result.Data.Add(baseStepId, GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, GetModuleData(guid));
            result.Valids.Add(report.StepId, GetModuleValid(guid));

            return result;
        }

        public MaterialDTO GetStepDataByProductId(int stepId, string productId) {
            MaterialDTO result = new MaterialDTO {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            Report baseReport = reportSQL.GetLastByProductId(baseStepId, productId);
            Report report = reportSQL.GetLastByProductId(stepId, productId);

            result.Data.Add(baseStepId, GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, GetModuleData(report.TestGuid));
            result.Valids.Add(report.StepId, GetModuleValid(report.TestGuid));

            return result;
        }

        public void Save(MaterialDTO material, AccountJWTPayload payload) {
            var baseData = material.Data[baseStepId][baseModuleId];
            string productId = baseData[pIdReportId];
            string productType = baseData[typeReportId];
            string tester = payload.AccountName;

            foreach (var stepPair in material.Data) {
                int stepId = stepPair.Key;
                var modules = stepPair.Value;

                SaveReport(stepId, modules, material.Valids[stepId], productId, productType, tester);
            }
        }

        private void SaveReport(int stepId, in IDictionary<string, IDictionary<string, string>> modules, in IDictionary<string, bool> modulesValid,
           string productId, string productType, string tester) {
            string guid = Guid.NewGuid().ToString();

            Report report = new Report {
                TestGuid = guid,
                ProductId = productId,
                ProductType = productType,
                StepId = stepId,
                Testor = tester,
                StationId = stationId,
                UploadFlag = uploadFlag,
                BeginTime = DateTime.Now
            };

            // 是否所有模块都通过验证
            int moduleCount = moduleSQL.GetCountByStepId(stepId);
            report.TestResult = (int)(modulesValid.Count != moduleCount || modulesValid.Select(valid => !valid.Value).Any() ? TestResult.NoValid : TestResult.Valid);

            foreach (var modulePair in modules) {
                string moduleId = modulePair.Key;
                var items = modulePair.Value;

                SaveDetails(guid, stepId, moduleId, items, modulesValid[moduleId]);
            }

            report.EndTime = DateTime.Now;
            reportSQL.Save(report);
        }

        private void SaveDetails(string guid, int stepId, string moduleId, in IDictionary<string, string> items, bool isValid) {
            if (items == null || !items.Any()) {
                return;
            }

            if (stepId != baseStepId) {
                Valid valid = new Valid() {
                    Guid = guid,
                    ModuleId = moduleId,
                    Value = isValid
                };
                validSQL.Save(valid);
            }

            foreach (var itemPair in items) {
                string reportId = itemPair.Key;
                string recordValue = itemPair.Value;

                var arr = reportId.Split('_');
                Detail detail = new Detail {
                    TestGuid = guid,
                    ModuleKey = arr[0],
                    ItemKey = arr[1],
                    RecordKey = arr[2],
                    RecordValue = recordValue
                };
                detailSQL.Save(detail);
            }
        }
    }
}
