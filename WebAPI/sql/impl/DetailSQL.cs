using System.Collections.Generic;
using WebAPI.utils;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.po;
using SqlSugar;

namespace WebAPI.sql.impl {
    public class DetailSQL : IDetailSQL {

        public List<Detail> GetByGuid(string guid) {
            return DataSource.DB.Queryable<Detail>().Where(d => d.TestGuid == guid).ToList();
        }

        public List<DetailPO> GetDataByGuid(string guid) {
            return DataSource.DB
                .Queryable<Detail>()
                .LeftJoin<Item>((d, i) => d.ModuleKey == i.ModuleId && d.ItemKey == i.ItemId)
                .Where(d => d.TestGuid == guid)
                .Select((d, i) => new DetailPO {
                    ReportId = i.ReportId,
                    Value = d.RecordValue
                })
                .ToList();
        }

        public List<DetailTemplatePO> GetTemplates(string productId) {
            string sql = @"
                SELECT
	                d.module_key, d.item_key, d.record_key, d.record_value
                FROM
					test_detail d
	                JOIN (
		                SELECT test_guid, step_id FROM (
			                SELECT
				                ROW_NUMBER() OVER(PARTITION BY product_id, step_id ORDER BY end_time) row_num,
				                test_guid, step_id, product_id
			                FROM test_record
		                ) r0 WHERE row_num = 1 AND product_id = @productId
	                ) r
		            ON d.test_guid = r.test_guid
			";
            return DataSource.QueryMany<DetailTemplatePO>(sql, new { productId });
        }

        public long Save(Detail detail) {
            return DataSource.Save(detail);
        }
    }
}
