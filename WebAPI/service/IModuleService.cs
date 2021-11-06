using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.service {
	public interface IModuleService {

		Module GetById(string id);

		List<Module> GetByStepId(int id);

		bool Update(Module module);

		long Save(Module module);

		int Delete(int id);
	}
}
