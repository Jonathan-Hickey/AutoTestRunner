using System.Collections.Generic;

namespace AutoTestRunner.Api.Dtos.RequestModels
{
    public class CreateTestReportDto
    {
        public string ProjectName { get; set; }
        public int? NumberOfPassedTests { get; set; }
        public int? NumberOfFailedTests { get; set; }
        public int? NumberOfIgnoredTests { get; set; }
        public int? TotalNumberOfTests { get; set; }
        public decimal TimeTakenInSecond { get; set; }
        public IReadOnlyList<string> FailedTests { get; set; }
        public IReadOnlyList<string> IgnoredTests { get; set; }
    }
}