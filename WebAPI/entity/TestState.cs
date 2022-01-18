using System;
using WebAPI.enums;

namespace WebAPI.entity {
    public class TestState {

        public int Id { get; set; }
        public string ProductType { get; set; }
        public string ProductId { get; set; }
        public string TestStepName { get; set; }
        public int TestStepId { get; set; }
        public int TestStationId { get; set; }
        public TestStatus TestStatus { get; set; }
        public string Testor { get; set; }
        public DateTime UpdateTime { get; set; }
        public string FailDesc { get; set; }
    }
}
