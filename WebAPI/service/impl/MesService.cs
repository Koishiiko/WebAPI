using System;
using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.enums;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class MesService : IMesService {

        private readonly IMesStateSQL mesStateSQL;
        private readonly ITestStateSQL testStateSQL;

        public MesService(IMesStateSQL mesStateSQL, ITestStateSQL testStateSQL) {
            this.mesStateSQL = mesStateSQL;
            this.testStateSQL = testStateSQL;
        }

        public IEnumerable<TestState> GetAll(MesType type) {
            return testStateSQL.GetAll(type);
        }

        public int Update(MesStateDTO state) {
            var mesState = new MesState {
                ProductId = state.ProductId,
                TestStepId = state.TestStepId,
                TestStationId = state.TestStationId,
                TestReady = state.TestReady,
                ProductReady = state.ProductReady,
                TestStatus = state.TestStatus,
                GYUpdatetime = DateTime.Now
            };

            return mesStateSQL.Update(mesState, state.Type);
        }
    }
}
