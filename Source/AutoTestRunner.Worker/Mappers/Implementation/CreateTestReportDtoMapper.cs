using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Worker.Models;

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
                //TODO: add list of test data {testName : "", TimeTakenInMilliSeconds}
                //IgnoredTests = new List<string>(),
                //FailedTests = new List<string>(),
            };
        }
    }
}