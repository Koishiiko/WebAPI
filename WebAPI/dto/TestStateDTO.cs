using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
    public class TestStateDTO {

        public IEnumerable<TestState> SwitchStatus { get; set; }
        public IEnumerable<TestState> ViewStatus { get; set; }
    }
}
