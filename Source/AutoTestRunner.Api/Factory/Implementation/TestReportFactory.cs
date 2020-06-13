using AutoTestRunner.Api.Factory.Interfaces;
using AutoTestRunner.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Core.Models;
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
                TestSummary = CreateTestSummary(request),
                TestDetails = CreateTestDetails(request.TestDetails)
            };
        }

        private TestSummary CreateTestSummary(CreateTestReportDto request)
        {
            return new TestSummary
            {
                TestSummaryReportId = Guid.NewGuid(),
                ProjectName = request.ProjectName,
                TimeTakenInSecond = request.TimeTakenInSecond,
                TotalNumberOfTests = request.TotalNumberOfTests,
                NumberOfPassedTests = request.NumberOfPassedTests,
                NumberOfFailedTests = request.NumberOfFailedTests,
                NumberOfIgnoredTests = request.NumberOfIgnoredTests,
            };
        }

        private IReadOnlyList<TestDetail> CreateTestDetails(IReadOnlyList<TestDetailRequestDto> testDetails)
        {
            return testDetails.Select(t => 
                new TestDetail
                {
                    TestName = t.TestName,
                    TimeTakenInMs = t.TimeTakenInMilliseconds,
                    TestStatus = t.TestStatus
                })
                .ToList();
        }
    }
}
