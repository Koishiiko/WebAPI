using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.po;

namespace WebAPI.sql {
	public interface IModuleSQL {

		Module GetById(string id);

		List<Module> GetByStepId(int id);

		int GetCountByStepId(int id);

		int Update(Module module);

		int Save(Module module);

		int Delete(string id);

		int DeleteByStepId(int id);
	}
}
