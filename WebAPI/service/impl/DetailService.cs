using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.po;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class DetailService : IDetailService {

        private readonly IReportSQL reportSQL;
        private readonly IDetailSQL detailSQL;
        private readonly IItemSQL itemSQL;

        public DetailService(IReportSQL reportSQL, IDetailSQL detailSQL, IItemSQL itemSQL) {
            this.reportSQL = reportSQL;
            this.detailSQL = detailSQL;
            this.itemSQL = itemSQL;
        }

        public IEnumerable<DetailDTO> GetRecordDetail(string guid) {
            Report report = reportSQL.GetByGuid(guid);
            List<ItemDetailPO> items = itemSQL.GetDataByStepId(report.StepId);

            List<DetailPO> details = detailSQL.GetDataByGuid(guid);
            IDictionary<string, string> detailsDict = new Dictionary<string, string>();
            foreach (var detail in details) {
                if (string.IsNullOrEmpty(detail.ReportId)) {
                    continue;
                }
                detailsDict.Add(detail.ReportId, detail.Value);
            }

            var result = new List<DetailDTO>();
            items.ForEach(item => {
                if (!result.Any() || result.Last().ModuleId != item.ModuleId) {
                    result.Add(new DetailDTO {
                        ModuleId = item.ModuleId,
                        ModuleName = item.ModuleName,
                        Items = new List<DetailItem>()
                    });
                }

                string value = detailsDict.TryGetValue(item.ReportId, out string val) ? val : string.Empty;
                result.Last().Items.Add(new DetailItem {
                    ReportId = item.ReportId,
                    ItemName = item.ItemName,
                    Value = value
                });
            });

            return result;
        }
    }
}
