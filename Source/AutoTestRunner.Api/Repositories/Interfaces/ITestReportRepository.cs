using System;
using System.Collections.Generic;
using AutoTestRunner.Api.Models;

namespace AutoTestRunner.Api.Repositories.Interfaces
{
    public interface ITestReportRepository
    {
        TestReport GetTestReport(Guid projectWatcherId, Guid testReportId);

        IReadOnlyList<TestReport> GetTestReports(Guid projectWatcherId);

        void AddTestReport(TestReport testReport);
    }
}