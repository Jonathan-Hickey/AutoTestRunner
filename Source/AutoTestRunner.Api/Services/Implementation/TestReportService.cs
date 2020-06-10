using AutoTestRunner.Api.Models;
using AutoTestRunner.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Api.Repositories.Interfaces;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class TestReportService : ITestReportService
    {
        private readonly ITestReportRepository _testReportRepository;

        public TestReportService(ITestReportRepository testReportRepository)
        {
            _testReportRepository = testReportRepository;
        }

        public void CreateTestReportAsync(TestReport testReport)
        {
            _testReportRepository.AddTestReport(testReport);
        }

        public TestReport GetTestReport(Guid projectWatcherId, Guid reportId)
        {
            return _testReportRepository.GetTestReport(projectWatcherId: projectWatcherId, testReportId:reportId);
        }

        public IReadOnlyList<TestReport> GetTestReports(Guid projectWatcherId)
        {
            return _testReportRepository.GetTestReports(projectWatcherId).OrderByDescending(r => r.RunDateTime).ToList();
        }
    }
}