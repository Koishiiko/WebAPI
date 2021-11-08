using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;

namespace WebAPI.sql.impl {
	public class DetailSQL : IDetailSQL {

		private IDataSource dataSource { get; }

		public DetailSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<Detail> GetByGuid(string guid) {
			string sql = @"
				SELECT
					id, test_guid, module_key, item_key, record_key, record_value
				FROM test_detail WHERE test_guid = @guid
			";

			return dataSource.QueryMany<Detail>(sql, new { guid });
		}

		public List<DetailDTO> GetPageByGuid(DetailPagination pagination) {
			string sql = @"
                SELECT
	                d1.id, d1.test_guid,
                    d1.module_key, m.name AS module_name,
                    d1.item_key, i.name AS item_name,
                    d1.record_key, r.name AS record_name, d1.record_value
                FROM
	                test_detail d1
	                LEFT JOIN module m
						ON d1.module_key = m.module_id
	                LEFT JOIN item i 
						ON d1.module_key = i.module_id AND d1.item_key = i.item_id
	                LEFT JOIN record r 
						ON d1.record_key = r.record_id,
                    (
                        SELECT TOP @end ROW_NUMBER() OVER (ORDER BY id ASC) n, id FROM test_detail
	                    WHERE
                            test_guid = @guid
                        AND (
                            module_key = @moduleId OR
                            @moduleId IS NULL OR
                            @moduleId = ''
                        )
                    ) d2
                WHERE d1.id = d2.id AND d2.n > %(start)s
			";
			return dataSource.QueryMany<DetailDTO>(sql, new {
				start = pagination.Page * pagination.Size,
				end = (pagination.Page + 1) * pagination.Size,
				guid = pagination.Guid,
				moduleId = pagination.ModuleId,
			});
		}

		public int GetCount(DetailPagination pagination) {
			string sql = @"
                SELECT COUNT(id) as rows FROM test_detail
                WHERE
                    test_guid = %(test_guid)s
                AND (
                    module_key = %(module_key)s OR
                    %(module_key)s IS NULL OR
                    %(module_key)s = ''
                )
			";
			return dataSource.QueryOne<int>(sql, new {
				guid = pagination.Guid,
				moduleId = pagination.ModuleId,
			});
		}

		public List<DetailTemplateDTO> GetTemplates(string productId) {
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

			return dataSource.QueryMany<DetailTemplateDTO>(sql, new { productId });
		}

		public long Save(Detail detail) {
			return dataSource.Save(detail);
		}
	}
}
