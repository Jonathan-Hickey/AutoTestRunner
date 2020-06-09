using AutoTestRunner.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTestRunner.Api.Services.Interfaces
{
    public interface ITestReportService
    {
        Task CreateTestReportAsync(TestReport testReport);
        Task<TestReport> GetTestReportAsync(Guid projectWatcherId, Guid reportId);
        Task<IReadOnlyList<TestReport>> GetTestReportsAsync(Guid projectWatcherId);
    }
}
