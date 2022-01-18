using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.enums;

namespace WebAPI.dto {
    public class MesStateDTO {

        public string ProductId { get; set; }
        public int TestStepId { get; set; }
        public int TestStationId { get; set; }
        public ReadyStatus ProductReady { get; set; }
        public ReadyStatus TestReady { get; set; }
        public TestStatus TestStatus { get; set; }
        public MesType Type { get; set; }
    }
}
