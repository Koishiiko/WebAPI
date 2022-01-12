using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.po;

namespace WebAPI.sql {
    public interface IItemSQL {

		List<Item> getByModuleId(string id);

		List<ItemDataPO> getDataByModuleId(string id);

		Item getById(int id);

		List<ItemDetailPO> GetDataByStepId(int id);

		List<ItemDataPO> getByItemId(string moduleId, string itemId);

		bool Update(Item item);

		long Save(Item item);

		int Delete(string id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByItemId(string moduleId, string itemId);
	}
}
