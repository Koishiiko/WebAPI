﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.dto;

namespace WebAPI.sql {
    public interface IStepSQL {

        List<Step> GetAll();

        List<Step> GetSteps(int[] roles);

        List<StepData> GetStepDatas(int[] roles);

        Step GetById(int id);

        bool Update(Step step);

        long Save(Step step);

        int Delete(int id);
    }
}
