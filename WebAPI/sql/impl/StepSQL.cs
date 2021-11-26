using System;
using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class StepSQL : IStepSQL {

		List<Step> IStepSQL.GetAll() {
			string sql = @"
				SELECT id, step_id, name FROM step WHERE step_id > 0 ORDER BY step_id;
			";
			return DataSource.QueryMany<Step>(sql);
		}

		List<Step> IStepSQL.GetSteps(int[] roles) {
			string sql = @"
                SELECT
                    s.id, s.step_id, s.name
                FROM
                    step s
                WHERE
					EXISTS (
                        SELECT
                            rs.id
                        FROM
                            role_step rs
                        WHERE
							s.step_id != 0
						AND
                            rs.role_id IN (@ids)
                        AND
                            s.step_id = rs.step_id
                    )
                ORDER BY step_id
			";
			return DataSource.QueryMany<Step>(sql, new { ids = roles });
		}

		List<StepData> IStepSQL.GetStepDatas(int[] roles) {
			string sql = @"
               SELECT
	               s.id AS s_id, s.step_id, s.name AS step_name,
	               m.id AS m_id, m.module_id, m.name AS module_name
               FROM (
                   SELECT
                       s0.id, s0.step_id, s0.name
                   FROM
                       step s0
                   WHERE EXISTS (
                           SELECT
								rs.id
                           FROM
								role_step rs
                           WHERE
								s0.step_id != 0
							AND
								rs.role_id IN (@ids)
							AND
								s0.step_id = rs.step_id
                       )
                   ) s
	               LEFT JOIN
                       module m
		           ON s.step_id = m.step_id
               ORDER BY
                   step_id, module_id
			";

			return DataSource.QueryMany<StepData>(sql, new { ids = roles });
		}

		Step IStepSQL.GetById(int id) {
			string sql = @"
				SELECT id, step_id, name FROM step WHERE step_id = @step_id
			";
			return DataSource.QueryOne<Step>(sql, new { step_id = id });
		}

		long IStepSQL.Save(Step step) {
			return DataSource.Save(step);
		}

		bool IStepSQL.Update(Step step) {
			return DataSource.Update(step);
		}

		int IStepSQL.Delete(int id) {
			string sql = @"
				DELETE FROM step WHERE step_id = @step_id
			";
			return DataSource.Delete(sql, new { step_id = id });
		}
	}
}
