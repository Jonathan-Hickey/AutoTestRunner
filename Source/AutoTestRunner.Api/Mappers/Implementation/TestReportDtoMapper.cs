using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class TestReportDtoMapper : IMapper<TestReport, TestReportDto>
    {
        private readonly IMapper<TestSummary, TestSummaryDto> _testSummaryDtoMapper;

        public TestReportDtoMapper(IMapper<TestSummary, TestSummaryDto> testSummaryDtoMapper)
        {
            _testSummaryDtoMapper = testSummaryDtoMapper;
        }

        public TestReportDto Map(TestReport testReport)
        {
            return new TestReportDto
            {
                ReportId = testReport.TestReportId,
                ProjectWatcherId = testReport.ProjectWatcherId,
                RunDateTime = testReport.RunDateTime.ToLocalTime().ToString("HH:mm:ss:fff dd/MM/yyyy"),
                TestSummary = _testSummaryDtoMapper.Map(testReport.TestSummary)
            };
        }
    }

    public class TestSummaryDtoMapper : IMapper<TestSummary, TestSummaryDto>
    {
        public TestSummaryDto Map(TestSummary testSummary)
        {
            return new TestSummaryDto
            {
                TotalNumberOfTests = testSummary.TotalNumberOfTests,
                TimeTakenInMillisecond = testSummary.TimeTakenInSecond,
                NumberOfFailedTests = testSummary.NumberOfFailedTests,
                ProjectName = testSummary.ProjectName,
                NumberOfIgnoredTests = testSummary.NumberOfIgnoredTests,
                NumberOfPassedTests = testSummary.NumberOfPassedTests
            };
        }
    }
}
