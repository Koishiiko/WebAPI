using WebAPI.entity;
using WebAPI.enums;

namespace WebAPI.sql {
    public interface IMesStateSQL {

        public int Update(MesState mesState, MesType mesType = MesType.Switch);
    }
}
