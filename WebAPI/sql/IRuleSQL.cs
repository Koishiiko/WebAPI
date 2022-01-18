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

		List<ItemRule> getByItemId(string moduleId, string itemId);

		int Update(ItemRule rule);

		int Save(ItemRule rule);

		int Delete(int id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByItemId(string moduleId, string itemId);
	}
}
