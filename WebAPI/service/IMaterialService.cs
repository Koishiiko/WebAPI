using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.service {
	public interface IMaterialService {

		MaterialDTO GetByProductId(string productId);

		MaterialDTO GetStepDataByProductId(int stepId, string productId);

		MaterialDTO GetByGuid(string guid);

		bool Save(MaterialDTO material, AccountJWTPayload payload);
	}
}
