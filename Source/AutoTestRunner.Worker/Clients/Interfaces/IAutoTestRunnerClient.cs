using System;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Clients.Interfaces
{
    public interface IAutoTestRunnerClient
    {
        Guid CreateTestReport(Guid projectWatcherId, TestSummary testSummary);
    }
}