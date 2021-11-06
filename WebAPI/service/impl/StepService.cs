using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class StepService : IStepService {

		private readonly IStepSQL stepSQL;

		public StepService(IStepSQL stepSQL) {
			this.stepSQL = stepSQL;
		}

		public List<Step> GetAll() {
			return stepSQL.GetAll();
		}

		public Step GetById(int id) {
			return stepSQL.GetById(id);
		}

		public List<StepDTO> GetStepDatas() {
			int[] roles = new[] { 1, 2, 3 };
			List<dynamic> rows = stepSQL.GetStepDatas(roles);

			List<StepDTO> datas = new List<StepDTO>();

			StepDTO curr = null;
			rows.ForEach((row) => {
				if (curr == null || curr.Id != row.s_id) {
					curr = new StepDTO(row.s_id, row.step_id, row.step_name, new List<Module>());
				}
				if (row.m_id != null) {
					Module module = new Module(row.m_id, row.step_id, row.module_id, row.module_name);
					curr.Modules.Add(module);
				}
				if (!datas.Any() || datas.Last().Id != curr.Id) {
					datas.Add(curr);
				}
			});
			return datas;
		}

		public List<Step> GetSteps() {
			int[] roles = new[] { 1, 2, 3 };
			return stepSQL.GetSteps(roles);
		}

		public long Save(Step step) {
			return stepSQL.Save(step);
		}

		public bool Update(Step step) {
			return stepSQL.Update(step);
		}

		public int Delete(int id) {
			return stepSQL.Delete(id);
		}
	}
}
