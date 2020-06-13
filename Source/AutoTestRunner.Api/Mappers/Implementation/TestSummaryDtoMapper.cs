using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
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