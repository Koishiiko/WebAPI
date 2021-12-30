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

        public List<DetailDTO> GetPageByGuid(DetailPagination pagination, out int total) {
            total = 0;
            return DataSource.DB.Queryable<Detail, Module, Item>((d, m, i) => new JoinQueryInfos(
                        JoinType.Left, d.ModuleKey == m.ModuleId,
                        JoinType.Left, d.ModuleKey == i.ModuleId && d.ItemKey == i.ItemId
                    )
                )
                .Where((d, m, i) => d.TestGuid == pagination.Guid &&
                    (d.ModuleKey == pagination.ModuleId || string.IsNullOrEmpty(pagination.ModuleId))
                )
                .Select((d, m, i) => new DetailDTO {
                    Id = d.Id,
                    TestGuid = d.TestGuid,
                    ModuleKey = d.ModuleKey,
                    ModuleName = m.Name,
                    ItemKey = d.ItemKey,
                    ItemName = i.Name,
                    RecordKey = d.RecordKey,
                    RecordName = i.RecordName,
                    RecordValue = d.RecordValue
                })
                .ToPageList(pagination.Page, pagination.Size, ref total);
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
