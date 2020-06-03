using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IMessageParser
    {
        TestResult GetTestResult(string testResultMessage);
    }
}