using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface ITestSummaryParser
    {
        TestSummary CreateTestSummary(string testResultText);
    }
}