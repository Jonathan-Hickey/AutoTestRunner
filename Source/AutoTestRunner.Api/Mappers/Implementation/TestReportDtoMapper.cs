using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class TestReportDtoMapper : IMapper<TestReport, TestSummaryDto>
    {
        public TestSummaryDto Map(TestReport testReport)
        {
            return new TestSummaryDto
            {
                ReportId = testReport.TestReportId,
                ProjectWatcherId = testReport.ProjectWatcherId,
                RunDateTime = testReport.RunDateTime.ToLocalTime().ToString("HH:mm:ss:fff dd/MM/yyyy"),
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
