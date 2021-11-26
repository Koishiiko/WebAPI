using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.service {
	public interface IStepService {

		List<Step> GetAll();

		List<Step> GetSteps(AccountJWTPayload payload);

		List<StepDTO> GetStepDatas(AccountJWTPayload payload);

		Step GetById(int id);

		long Save(Step step);

		int Update(Step step);

		int Delete(int id);
	}
}
