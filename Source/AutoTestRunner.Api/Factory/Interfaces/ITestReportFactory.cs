using System;
using AutoTestRunner.Api.Dtos.RequestModels;
using AutoTestRunner.Api.Models;

namespace AutoTestRunner.Api.Factory.Interfaces
{
    public interface ITestReportFactory
    {
        TestReport CreateTestReport(Guid projectWatcherId, CreateTestReportDto request);
    }
}