using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class DetailSQL : IDetailSQL {

        public List<Detail> GetByGuid(string guid) {
            return DataSource.Switch.Queryable<Detail>().Where(d => d.TestGuid == guid).ToList();
        }

        public List<DetailPO> GetDataByGuid(string guid) {
            return DataSource.Switch
                .Queryable<Detail>()
                .LeftJoin<Item>((d, i) => d.ModuleKey == i.ModuleId && d.ItemKey == i.ItemId)
                .Where(d => d.TestGuid == guid)
                .Select((d, i) => new DetailPO {
                    ReportId = i.ReportId,
                    Value = d.RecordValue
                })
                .ToList();
        }

        public long Save(Detail detail) {
            return DataSource.Save(detail);
        }
    }
}
