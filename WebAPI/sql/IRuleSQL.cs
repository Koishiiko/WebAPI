using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.sql {
	public interface IRuleSQL {

		List<ItemRule> getAll();

		ItemRule getById(int id);

		List<ItemRule> getByReportId(string reportId);

		int Update(ItemRule rule);

		int Save(ItemRule rule);

		int Delete(int id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByReportId(string reportId);
	}
}
