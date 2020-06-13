using System;
using AutoTestRunner.Core.Enums;

namespace AutoTestRunner.Core.Models
{
    public class TestDetail
    {
        public Guid TestDetailId { get; set; }
        public TestStatus TestStatus { get; set; }
        public string TestName { get; set; }
        public int TimeTakenInMs { get; set; }
    }
}
