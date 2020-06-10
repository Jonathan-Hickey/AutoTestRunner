using AutoTestRunner.Api.Models;
using System;
using System.Collections.Generic;

namespace AutoTestRunner.Api.Services.Interfaces
{
    public interface ITestReportService
    {
        void CreateTestReportAsync(TestReport testReport);
        TestReport GetTestReport(Guid projectWatcherId, Guid reportId);
        IReadOnlyList<TestReport> GetTestReports(Guid projectWatcherId);
    }
}
