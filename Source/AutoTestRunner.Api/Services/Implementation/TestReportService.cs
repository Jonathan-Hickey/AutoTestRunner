﻿using AutoTestRunner.Api.Models;
using AutoTestRunner.Api.Services.Interfaces;
using AutoTestRunner.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class TestReportService : ITestReportService
    {
        private readonly IFileRepository<TestReport> _testReportRepository;

        public TestReportService(IFileRepository<TestReport> testReportRepository)
        {
            _testReportRepository = testReportRepository;
        }

        public async Task CreateTestReportAsync(TestReport testReport)
        {
            await _testReportRepository.WriteAsync(testReport);
        }

        public Task<TestReport> GetTestReportAsync(Guid projectWatcherId, Guid reportId)
        {
            return GetTestReportsAsync(projectWatcherId).SingleAsync(r => r.ReportId == reportId);
        }

        public async Task<IEnumerable<TestReport>> GetTestReportsAsync(Guid projectWatcherId)
        {
            var testReports = await _testReportRepository.GetAllAsync();

            return testReports.Where(r => r.ProjectWatcherId == projectWatcherId);
        }
    }


}