using System.Collections.Generic;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface ITestDetailParser
    {
        IReadOnlyList<TestDetail> CreateTestDetails(string testResultText);
    }
}