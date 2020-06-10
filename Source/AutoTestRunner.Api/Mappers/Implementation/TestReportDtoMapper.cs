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
                ReportId = testReport.TestReportId,
                ProjectWatcherId = testReport.ProjectWatcherId,
                RunDateTime = testReport.RunDateTime,
                TotalNumberOfTests = testReport.TotalNumberOfTests,
                TimeTakenInSecond = testReport.TimeTakenInSecond,
                NumberOfFailedTests = testReport.NumberOfFailedTests,
                ProjectName = testReport.ProjectName,
                NumberOfIgnoredTests = testReport.NumberOfIgnoredTests,
                IgnoredTests = testReport.IgnoredTests,
                FailedTests = testReport.FailedTests,
                NumberOfPassedTests = testReport.NumberOfPassedTests
            };
        }
    }
}
