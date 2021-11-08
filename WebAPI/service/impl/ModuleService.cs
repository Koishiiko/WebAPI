using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class ModuleService : IModuleService {

		private IModuleSQL moduleSQL { get; }

		public ModuleService(IModuleSQL moduleSQL) {
			this.moduleSQL = moduleSQL;
		}

		Module IModuleService.GetById(string id) {
			return moduleSQL.getById(id);
		}

		List<Module> IModuleService.GetByStepId(int id) {
			return moduleSQL.getByStepId(id);
		}

		long IModuleService.Save(Module module) {
			return moduleSQL.Save(module);
		}

		bool IModuleService.Update(Module module) {
			return moduleSQL.Update(module);
		}

		int IModuleService.Delete(int id) {
			return moduleSQL.Delete(id);
		}
	}
}
