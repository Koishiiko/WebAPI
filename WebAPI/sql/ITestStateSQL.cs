using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.enums;

namespace WebAPI.sql {
    public interface ITestStateSQL {

        public IEnumerable<TestState> GetAll(MesType mesType);

        public int Update(TestState state, MesType type);
    }
}
