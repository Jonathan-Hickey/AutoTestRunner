using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Worker.Mappers.Implementation
{
    public class CreateTestReportDtoMapper : IMapper<TestSummary, CreateTestReportDto>
    {
        public CreateTestReportDto Map(TestSummary testSummary)
        {
            return new CreateTestReportDto
            {
                TotalNumberOfTests = testSummary.TotalNumberOfTests,
                TimeTakenInSecond = testSummary.TimeTakenInSecond,
                ProjectName = testSummary.ProjectName,
                NumberOfIgnoredTests = testSummary.NumberOfIgnoredTests,
                NumberOfPassedTests = testSummary.NumberOfPassedTests,
                NumberOfFailedTests = testSummary.NumberOfFailedTests,
            };
        }
    }
}