using System;
using System.Collections.Generic;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Clients.Interfaces
{
    public interface IAutoTestRunnerClient
    {
        Guid CreateTestReport(Guid projectWatcherId, TestSummary testSummary, IReadOnlyList<TestDetail> testDetails);
    }
}