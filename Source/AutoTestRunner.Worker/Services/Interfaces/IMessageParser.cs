using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IMessageParser
    {
        TestSummary GetTestResult(string testResultMessage);
    }
}