using System;
using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class StepSQL : IStepSQL {

        public List<Step> GetAll() {
            return DataSource.DB.Queryable<Step>().Where(s => s.StepId > 0).OrderBy(s => s.StepId).ToList();
        }

        public List<Step> GetSteps(int[] roles) {
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
							s.step_id != 0 AND
                            rs.role_id IN (@ids) AND
                            s.step_id = rs.step_id
                    )
                ORDER BY step_id
			";
            return DataSource.QueryMany<Step>(sql, new { ids = roles });
        }

        public List<StepData> GetStepDatas(int[] roles) {
            string sql = @"
				SELECT
					s.id AS s_id, s.step_id, s.name AS step_name,
	                m.id AS m_id, m.module_id, m.name AS module_name
                FROM (
                    SELECT
                        s0.id, s0.step_id, s0.name
                    FROM
                        step s0
                    WHERE 
						EXISTS (
                            SELECT
				                rs.id
                            FROM
				                role_step rs
                            WHERE s0.step_id != 0 AND
				                rs.role_id IN (@ids) AND
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

        public Step GetById(int id) {
            return DataSource.DB.Queryable<Step>().Single(s => s.StepId == id);
        }

        public long Save(Step step) {
            return DataSource.Save(step);
        }

        public bool Update(Step step) {
            return DataSource.Update(step);
        }

        public int Delete(int id) {
            return DataSource.DB.Deleteable<Step>().Where(s => s.StepId == id).ExecuteCommand();
        }
    }
}
