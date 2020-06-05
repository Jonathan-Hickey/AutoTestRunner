using AutoTestRunner.Api.Models;
using System;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Api.Factory.Interfaces
{
    public interface ITestReportFactory
    {
        TestReport CreateTestReport(Guid projectWatcherId, CreateTestReportDto request);
    }
}