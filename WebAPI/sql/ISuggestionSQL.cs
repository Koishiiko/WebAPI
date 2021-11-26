using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.dto;

namespace WebAPI.sql {
	public interface ISuggestionSQL {

		Suggestion getById(int id);

		List<Suggestion> getByItemId(string moduleId, string itemId);

		bool Update(Suggestion suggestion);

		long Save(Suggestion suggestion);

		int Delete(int id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByItemId(string moduleId, string itemId);
	}
}
