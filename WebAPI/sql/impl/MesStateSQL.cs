using WebAPI.entity;
using WebAPI.enums;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class MesStateSQL : IMesStateSQL {

        public int Update(MesState mesState, MesType mesType = MesType.Switch) {
            return DataSource.GetDB(mesType)
                .Updateable(mesState)
                .Where(ms => ms.TestStepId == mesState.TestStepId && ms.TestStationId == mesState.TestStationId)
                .IgnoreColumns(ignoreAllNullColumns: true)
                .ExecuteCommand();
        }
    }
}
