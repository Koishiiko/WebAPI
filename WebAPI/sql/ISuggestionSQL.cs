using System.Collections.Generic;
using WebAPI.entity;

namespace WebAPI.sql {
    public interface ISuggestionSQL {

		Suggestion getById(int id);

		List<Suggestion> getByReportId(string reportId);

		int Update(Suggestion suggestion);

		int Save(Suggestion suggestion);

		int Delete(int id);

		int DeleteByStepId(int id);

		int DeleteByModuleId(string id);

		int DeleteByReportId(string reportId);
	}
}
