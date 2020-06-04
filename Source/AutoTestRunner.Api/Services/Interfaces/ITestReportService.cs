using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Api.Models;

namespace AutoTestRunner.Api.Services.Interfaces
{
    public interface ITestReportService
    {
        Task CreateTestReportAsync(TestReport testReport);
        Task<TestReport> GetTestReportAsync(Guid projectWatcherId, Guid reportId);
        Task<IEnumerable<TestReport>> GetTestReportsAsync(Guid projectWatcherId);
    }
}
