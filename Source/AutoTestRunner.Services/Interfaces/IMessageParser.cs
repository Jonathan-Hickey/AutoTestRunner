using AutoTestRunner.Services.Models;

namespace AutoTestRunner.Services.Interfaces
{
    public interface IMessageParser
    {
        TestResult GetTestResult(string testResultMessage);
    }
}