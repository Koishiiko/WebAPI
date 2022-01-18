using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
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

        public IEnumerable<DetailDTO> GetByProductId(string productId) {
            List<RecordPO> records = reportSQL.GetAllByProductId(productId);
            var result = new List<DetailDTO>();

            records.ForEach(record => {
                GetByGuid(record.StepId, record.TestGuid, result);
            });
            return result;
        }

        public IEnumerable<DetailDTO> GetByGuid(string guid) {
            var result = new List<DetailDTO>();
            Record record = reportSQL.GetByGuid(guid);
            GetByGuid(record.StepId, guid, result);
            return result;
        }

        private void GetByGuid(int stepId, string guid, in List<DetailDTO> list) {
            IEnumerable<ItemDetailPO> items = itemSQL.GetDataByStepId(stepId);

            IEnumerable<DetailPO> details = detailSQL.GetDataByGuid(guid);
            IDictionary<string, string> detailsDict = new Dictionary<string, string>();
            foreach (var detail in details) {
                if (!string.IsNullOrEmpty(detail.ReportId)) {
                    detailsDict.TryAdd(detail.ReportId, detail.Value);
                }
            }

            foreach (var item in items) {
                if (!list.Any() || list.Last().ModuleId != item.ModuleId) {
                    list.Add(new DetailDTO {
                        ModuleId = item.ModuleId,
                        ModuleName = item.ModuleName,
                        Items = new List<DetailItem>()
                    });
                }

                list.Last().Items.Add(new DetailItem {
                    ReportId = item.ReportId,
                    ItemName = item.ItemName,
                    Type = item.Type,
                    Value = detailsDict.TryGetValue(item.ReportId, out string val) ? val : string.Empty
                });
            }
        }

    }
}
