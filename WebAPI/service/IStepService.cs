using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.service {
	public interface IStepService {

		List<Step> GetAll();

		List<Step> GetSteps();

		List<StepDTO> GetStepDatas();

		Step GetById(int id);

		long Save(Step step);

		bool Update(Step step);

		int Delete(int id);
	}
}
