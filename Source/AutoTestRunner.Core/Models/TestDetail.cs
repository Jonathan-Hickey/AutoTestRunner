using AutoTestRunner.Core.Enums;

namespace AutoTestRunner.Core.Models
{
    public class TestDetail
    {
        public TestStatus TestStatus { get; set; }
        public string TestName { get; set; }
        public int TimeTakenInMs { get; set; }
    }
}
