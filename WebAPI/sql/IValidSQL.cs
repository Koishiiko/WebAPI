using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.sql {
    public interface IValidSQL {

        List<Valid> GetByGuid(string guid);

        long Save(Valid valid);
    }
}
