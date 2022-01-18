using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.enums {
    public enum TestStatus {

        Offline = 0,
        Online = 1,
        Testing = 2,
        Completed = 3,
        ProductFault = 4,
        EquipmentFault = 5,
        Pause = 6
    }
}
