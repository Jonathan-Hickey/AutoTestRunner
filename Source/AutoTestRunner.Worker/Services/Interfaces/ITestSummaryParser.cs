using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface ITestSummaryParser
    {
        TestSummary CreateTestSummary(string testResultText);
    }
}