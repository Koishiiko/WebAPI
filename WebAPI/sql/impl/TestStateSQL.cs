using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.enums;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class TestStateSQL : ITestStateSQL {

        public IEnumerable<TestState> GetAll(MesType type) {
            return DataSource.GetDB(type).Queryable<TestState>().ToList();
        }

        public int Update(TestState state, MesType type) {
            return DataSource.GetDB(type)
                .Updateable(state)
                .Where(ts => ts.TestStepId == state.TestStepId && ts.TestStationId == state.TestStationId)
                .IgnoreColumns(ignoreAllNullColumns: true)
                .ExecuteCommand();
        }
    }
}
