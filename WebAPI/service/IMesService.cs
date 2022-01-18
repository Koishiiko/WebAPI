using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.enums;

namespace WebAPI.service {
    public interface IMesService {

        IEnumerable<TestState> GetAll(MesType type);

        int Update(MesStateDTO mesState);
    }
}
