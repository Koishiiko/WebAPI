using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.service {
	public interface IStepService {

		List<Step> GetAll();

		List<Step> GetSteps(AccountPayload payload);

		List<StepDTO> GetStepDatas(AccountPayload payload);

		Step GetById(int id);

		long Save(AccountPayload payload, Step step);

		int Update(Step step);

		int Delete(int id);
	}
}
