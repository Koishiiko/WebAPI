using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.sql {
	public interface IItemSQL {

		List<Item> getByModuleId(string id);

		List<dynamic> getDataByModuleId(string id);

		Item getById(int id);

		List<dynamic> getByItemId(string moduleId, string itemId);

		bool Update(Item item);

		long Save(Item item);

		int Delete(string id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByItemId(string moduleId, string itemId);
	}
}
