using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class TestReportDtoMapper : IMapper<TestReport, TestReportDto>
    {
        public TestReportDto Map(TestReport testReport)
        {
            return new TestReportDto
            {
                TotalNumberOfTests = testReport.TotalNumberOfTests,
                TimeTakenInSecond = testReport.TimeTakenInSecond,
                NumberOfFailedTests = testReport.NumberOfFailedTests,
                ProjectName = testReport.ProjectName,
                NumberOfIgnoredTests = testReport.NumberOfIgnoredTests,
                IgnoredTests = testReport.IgnoredTests,
                FailedTests = testReport.FailedTests,
                ReportId = testReport.ReportId,
                ProjectWatcherId = testReport.ProjectWatcherId,
                NumberOfPassedTests = testReport.NumberOfPassedTests
            };
        }
    }
}
