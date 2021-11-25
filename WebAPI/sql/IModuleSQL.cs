using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.sql {
	public interface IModuleSQL {

		Module getById(string id);

		List<Module> getByStepId(int id);

		int GetCountByStepId(int id);

		bool Update(Module module);

		long Save(Module module);

		int Delete(string id);

		int DeleteByStepId(int id);
	}
}
