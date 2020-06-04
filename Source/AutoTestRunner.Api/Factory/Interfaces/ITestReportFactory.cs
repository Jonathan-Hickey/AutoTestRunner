using AutoTestRunner.Api.Dtos.RequestModels;
using AutoTestRunner.Api.Models;
using System;

namespace AutoTestRunner.Api.Factory.Interfaces
{
    public interface ITestReportFactory
    {
        TestReport CreateTestReport(Guid projectWatcherId, CreateTestReportDto request);
    }
}