using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ReportSQL : IReportSQL {

        public List<Report> GetAll() {
            return DataSource.DB.Queryable<Report>().OrderBy(r => r.EndTime, SqlSugar.OrderByType.Desc).ToList();
        }

        public List<ReportDTO> GetByPage(ReportPagination pagination) {
            string sql = @"
                SELECT
	                r.id, r.product_id, r.product_type, r.begin_time, r.end_time
                FROM
	                test_record r JOIN (
		                SELECT
			                TOP (@end) ROW_NUMBER() OVER(ORDER BY end_time DESC) n,
			                r1.id, end_time
		                FROM (
				                SELECT id, end_time FROM (
				                    SELECT
					                    ROW_NUMBER() OVER(PARTITION BY product_id ORDER BY end_time DESC) row_num,
					                    id, end_time
				                    FROM test_record
			                            WHERE
                                            step_id != 0
                                        AND
                                            end_time BETWEEN
                                                ISNULL(@beginTime, '1970.01.01')
                                            AND
                                                ISNULL(@endTime, '2100.01.01')
                                        AND (
				                            @productId IS NULL OR
                                            @productId = '' OR
                                            product_id = @productId
                                        )
				                ) r0
                                WHERE row_num = 1
			                ) r1
		                ) r2 ON r.id = r2.id
                WHERE r2.n > @start
                ORDER BY r2.n ASC
			";

            return DataSource.QueryMany<ReportDTO>(sql, new {
                start = pagination.Page * pagination.Size,
                end = (pagination.Page + 1) * pagination.Size,
                beginTime = pagination.BeginTime,
                endTime = pagination.EndTime,
                productId = pagination.ProductId
            });
        }

        public int GetCount(ReportPagination pagination) {
            string sql = @"
                SELECT COUNT(id) AS rows FROM (
                    SELECT
                        ROW_NUMBER() OVER(PARTITION BY product_id ORDER BY end_time DESC) row_num,
                        id
                    FROM test_record
                        WHERE
                            step_id != 0
                        AND
                            end_time BETWEEN
                                ISNULL(@beginTime, '1970.01.01')
                            AND
                                ISNULL(@endTime, '2100.01.01')
                        AND (
                            product_id = @productId OR
                            @productId IS NULL OR
                            @productId = ''
                        )
                    )  r0
                WHERE row_num = 1
			";

            return DataSource.QueryOne<int>(sql, new {
                beginTime = pagination.BeginTime,
                endTime = pagination.EndTime,
                productId = pagination.ProductId
            });
        }

        public List<ReportDTO> GetAllByProductId(string productId) {
            string sql = @"
                SELECT
	                r.step_id, r.test_guid
                FROM test_record AS r
	                JOIN (
    		            SELECT
                            ROW_NUMBER() OVER(PARTITION BY product_id, step_id ORDER BY end_time DESC) row_num,
    		                id
    		            FROM test_record
		                WHERE product_id = @productId
	                ) r1 ON r.id = r1.id
                WHERE r1.row_num = 1
                ORDER BY step_id
			";

            return DataSource.QueryMany<ReportDTO>(sql, new { productId });
        }

        public List<ReportDTO> GetByProductId(string productId) {
            string sql = @"
                SELECT
                    r.id, r.test_guid, r.product_id, r.product_type,
                    r.step_id, s.name AS step_name, r.station_id,
                    r.begin_time, r.end_time,
                    r.testor, r.test_result, r.upload_flag
                FROM
	                test_record r
	                JOIN step s
	                ON r.step_id = s.step_id
                WHERE
                    r.step_id != 0 AND product_id = @productId
                ORDER BY
                    r.step_id, station_id, end_time DESC
			";

            return DataSource.QueryMany<ReportDTO>(sql, new { productId });
        }

        public Report GetLastByProductId(int stepId, string productId) {
            return DataSource.DB.Queryable<Report>().Single(r => r.ProductId == productId && r.StepId == stepId);
        }

        public Report GetByGuid(string guid) {
            return DataSource.DB.Queryable<Report>().Single(r => r.TestGuid == guid);
        }


        public long Save(Report report) {
            return DataSource.Save(report);
        }
    }
}
