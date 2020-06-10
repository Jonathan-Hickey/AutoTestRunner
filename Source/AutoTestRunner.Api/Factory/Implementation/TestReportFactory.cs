using AutoTestRunner.Api.Factory.Interfaces;
using AutoTestRunner.Api.Models;
using System;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Api.Factory.Implementation
{
    public class TestReportFactory : ITestReportFactory
    {
        public TestReport CreateTestReport(Guid projectWatcherId, CreateTestReportDto request)
        {
            return new TestReport
            {
                TestReportId = Guid.NewGuid(),
                ProjectWatcherId = projectWatcherId,
                RunDateTime = DateTimeOffset.Now,
                ProjectName = request.ProjectName,
                TimeTakenInSecond = request.TimeTakenInSecond,
                TotalNumberOfTests = request.TotalNumberOfTests,
                NumberOfPassedTests = request.NumberOfPassedTests,
                NumberOfFailedTests = request.NumberOfFailedTests,
                NumberOfIgnoredTests = request.NumberOfIgnoredTests,
                IgnoredTests = request.IgnoredTests,
                FailedTests = request.FailedTests
            };
        }
    }
}
