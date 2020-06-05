using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Worker.Mappers.Interfaces;
using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Mappers.Implementation
{
    public class CreateTestReportDtoMapper : IMapper<TestResult, CreateTestReportDto>
    {
        public CreateTestReportDto Map(TestResult testResult)
        {
            return new CreateTestReportDto
            {
                TotalNumberOfTests = testResult.TotalNumberOfTests,
                TimeTakenInSecond = testResult.TimeTakenInSecond,
                ProjectName = testResult.ProjectName,
                NumberOfIgnoredTests = testResult.NumberOfIgnoredTests,
                NumberOfPassedTests = testResult.NumberOfPassedTests,
                NumberOfFailedTests = testResult.NumberOfFailedTests,
                //TODO: add list of test data {testName : "", TimeTakenInMilliSeconds}
                //IgnoredTests = new List<string>(),
                //FailedTests = new List<string>(),
            };
        }
    }
}