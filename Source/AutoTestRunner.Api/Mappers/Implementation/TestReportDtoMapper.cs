using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Enums;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class TestReportDtoMapper : IMapper<TestReport, TestReportDto>
    {
        private readonly IMapper<TestSummary, TestSummaryDto> _testSummaryDtoMapper;
        private readonly IMapper<TestDetail, TestDetailResponseDto> _testDetailResponseDtoMapper;

        public TestReportDtoMapper(IMapper<TestSummary, TestSummaryDto> testSummaryDtoMapper, IMapper<TestDetail, TestDetailResponseDto> testDetailResponseDtoMapper)
        {
            _testDetailResponseDtoMapper = testDetailResponseDtoMapper;
            _testSummaryDtoMapper = testSummaryDtoMapper;
        }

        public TestReportDto Map(TestReport testReport)
        {
            var passedTests = testReport.TestDetails.Where(t => t.TestStatus == TestStatus.Passed).ToList();
            var ignoredTests = testReport.TestDetails.Where(t => t.TestStatus == TestStatus.Ignored).ToList();
            var failedTests = testReport.TestDetails.Where(t => t.TestStatus == TestStatus.Failed).ToList();

            return new TestReportDto
            {
                ReportId = testReport.TestReportId,
                ProjectWatcherId = testReport.ProjectWatcherId,
                RunDateTime = testReport.RunDateTime.ToLocalTime().ToString("HH:mm:ss:fff dd/MM/yyyy"),
                TestSummary = _testSummaryDtoMapper.Map(testReport.TestSummary),
                PassedTests = _testDetailResponseDtoMapper.Map(passedTests),
                IgnoredTests = _testDetailResponseDtoMapper.Map(ignoredTests),
                FailedTests = _testDetailResponseDtoMapper.Map(failedTests),
            };
        }
    }
}
