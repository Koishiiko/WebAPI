using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.service {
	public interface IMaterialService {

		MaterialDTO GetByProductId(string productId);

		MaterialDTO GetStepDataByProductId(int stepId, string productId);

		MaterialDTO GetByGuid(string guid);

		void Save(MaterialDTO material, AccountPayload payload);
	}
}
