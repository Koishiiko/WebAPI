using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.service {
	public interface IItemService {
		
		List<Item> getByModuleId(string moduleId);

		List<ItemDTO> getDataByModuleId(string moduleId);

		ItemDTO getByItemId(string moduleId, string itemId);

		long Save(ItemDTO item);

		bool Update(ItemDTO item);

		int Delete(string moduleId, string itemId);
	}
}
