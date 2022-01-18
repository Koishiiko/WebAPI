using System;
using WebAPI.enums;

namespace WebAPI.entity {
    public class MesState {

        public int Id { get; set; }
        public string ProductId { get; set; }
        public int TestStepId { get; set; }
        public int TestStationId { get; set; }
        public ReadyStatus ProductReady { get; set; }
        public ReadyStatus TestReady { get; set; }
        public TestStatus TestStatus { get; set; }
        public DateTime GYUpdatetime { get; set; }
    }
}
