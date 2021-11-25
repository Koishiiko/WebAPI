﻿using System;
using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class StepSQL : IStepSQL {

		private IDataSource dataSource { get; }

		public StepSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		List<Step> IStepSQL.GetAll() {
			string sql = @"
				SELECT id, step_id, name FROM step WHERE step_id > 0 ORDER BY step_id;
			";
			return dataSource.QueryMany<Step>(sql);
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
			return dataSource.QueryMany<Step>(sql, new { ids = roles });
		}

		List<dynamic> IStepSQL.GetStepDatas(int[] roles) {
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

			return dataSource.QueryMany<dynamic>(sql, new { ids = roles });
		}

		Step IStepSQL.GetById(int id) {
			string sql = @"
				SELECT id, step_id, name FROM step WHERE step_id = @step_id
			";
			return dataSource.QueryOne<Step>(sql, new { step_id = id });
		}

		long IStepSQL.Save(Step step) {
			return dataSource.Save(step);
		}

		bool IStepSQL.Update(Step step) {
			return dataSource.Update(step);
		}

		int IStepSQL.Delete(int id) {
			string sql = @"
				DELETE FROM step WHERE step_id = @step_id
			";
			return dataSource.Delete(sql, new { step_id = id });
		}
	}
}
