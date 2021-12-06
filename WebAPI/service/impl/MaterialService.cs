using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.sql;
using WebAPI.utils;

namespace WebAPI.service {
    public class MaterialService : IMaterialService {

        private readonly IReportSQL reportSQL;
        private readonly IDetailSQL detailSQL;
        private readonly IValidSQL validSQL;
        private readonly IModuleSQL moduleSQL;

        /**
         * XXX: 一些固定的值 暂时放在这里
         */
        // 产品信息数据的工序id为0 模块id为"000"
        // 方便统一存储和查询
        private static readonly int baseStepId = 0;
        private static readonly string baseModuleId = "000";
        // 产品id对应的报告id
        private static readonly string pIdReportId = "000_001_31";
        // 产品类型对应的报告id
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
            List<ReportDTO> rows = reportSQL.GetAllByProductId(productId);
            if (rows != null && !rows.Any()) {
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
        ///     { 模块id: { 报告id: 记录值, ... }, ...}
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
        ///     { 模块id: 是否验证, ... }
        /// </returns>
        private IDictionary<string, bool> GetModuleValid(string guid) {
            List<Valid> valids = validSQL.GetByGuid(guid);
            return valids.ToDictionary(valid => valid.ModuleId, valid => valid.Value);
        }

        public MaterialDTO GetByGuid(string guid) {
            Report report = reportSQL.GetByGuid(guid);

            MaterialDTO result = new MaterialDTO() {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            // 除了获取当前guid的记录
            // 还需要获取产品信息
            Report baseReport = reportSQL.GetLastByProductId(baseStepId, report.ProductId);

            result.Data.Add(baseStepId, GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, GetModuleData(guid));
            result.Valids.Add(report.StepId, GetModuleValid(guid));

            return result;
        }

        public MaterialDTO GetStepDataByProductId(int stepId, string productId) {
            MaterialDTO result = new MaterialDTO() {
                Data = new Dictionary<int, IDictionary<string, IDictionary<string, string>>>(),
                Valids = new Dictionary<int, IDictionary<string, bool>>()
            };

            // 除了获取当前工序的记录
            // 还需要获取产品信息
            Report baseReport = reportSQL.GetLastByProductId(baseStepId, productId);
            Report report = reportSQL.GetLastByProductId(stepId, productId);

            result.Data.Add(baseStepId, GetModuleData(baseReport.TestGuid));
            result.Data.Add(report.StepId, GetModuleData(report.TestGuid));
            result.Valids.Add(report.StepId, GetModuleValid(report.TestGuid));

            return result;
        }

        public void Save(MaterialDTO material, AccountPayload payload) {
            var baseData = material.Data[baseStepId][baseModuleId];
            string productId = baseData[pIdReportId];
            string productType = baseData[typeReportId];
            string tester = payload.AccountName;

            // 每个工序分别创建一条test_record记录
            foreach (var stepPair in material.Data) {
                int stepId = stepPair.Key;
                var modules = stepPair.Value;

                SaveReport(stepId, modules, material.Valids[stepId], productId, productType, tester);
            }
        }

        private void SaveReport(int stepId, in IDictionary<string, IDictionary<string, string>> modules, in IDictionary<string, bool> modulesValid,
           string productId, string productType, string tester) {
            string guid = Guid.NewGuid().ToString();

            // XXX: 目前是在开始保存之前记录BeginTime
            // 执行完毕后记录EndTime 再将数据插入到表中
            // 大概先插入数据 执行完成后更新EndTime会比较好
            Report report = new Report() {
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
            report.TestResult = (int)(modulesValid.Count == moduleCount && modulesValid.Values.ToList().TrueForAll(valid => valid)
                                    ? TestResult.Valid : TestResult.NoValid) ;

            // 根据模块保存当前工序(test_record)下的数据
            foreach (var modulePair in modules) {
                string moduleId = modulePair.Key;
                var items = modulePair.Value;

                // 基本信息由前台验证通过
                if (stepId != baseStepId) {
                    SaveValid(guid, moduleId, modulesValid[moduleId]);
                }
                SaveDetails(guid, items);
            }

            report.EndTime = DateTime.Now;
            reportSQL.Save(report);
        }

        private void SaveValid(string guid, string moduleId, bool isValid) {
            Valid valid = new Valid() {
                Guid = guid,
                ModuleId = moduleId,
                Value = isValid
            };
            validSQL.Save(valid);
        }

        private void SaveDetails(string guid, in IDictionary<string, string> items) {
            if (items == null || !items.Any()) {
                return;
            }

            // 保存当前模块下的detail数据
            foreach (var itemPair in items) {
                string reportId = itemPair.Key;
                string recordValue = itemPair.Value;

                // reportId: moduleId_itemId_recordId
                var arr = reportId.Split('_');
                Detail detail = new Detail() {
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
