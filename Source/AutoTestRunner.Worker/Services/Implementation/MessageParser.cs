using AutoTestRunner.Worker.Models;
using AutoTestRunner.Worker.Services.Interfaces;
using System.Text.RegularExpressions;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class MessageParser : IMessageParser
    {
        private const string TotalTests = "Total tests: ";
        private const string PassedTests = "Passed: ";
        private const string FailedTests = "Failed: ";
        private const string IgnoredTests = "Skipped: ";
        private const string TotalTimeTaken = "Total time: ";

        private readonly Regex _totalTestsRegex = new Regex($"{TotalTests}\\d*", RegexOptions.Compiled);
        private readonly Regex _passedTestsRegex = new Regex($"{PassedTests}\\d*", RegexOptions.Compiled);
        private readonly Regex _failedTestsRegex = new Regex($"{FailedTests}\\d*", RegexOptions.Compiled);

        private readonly Regex _ignoredTestsRegex = new Regex($"{IgnoredTests}\\d*", RegexOptions.Compiled);
        private readonly Regex _totalTimeTakenRegex = new Regex($"{TotalTimeTaken}\\d*.\\d*", RegexOptions.Compiled);
        private readonly Regex _projectNameRegex = new Regex("\\w*.dll", RegexOptions.Compiled);


        public TestResult GetTestResult(string testResultMessage)
        {

            var totalTests = GetNullableIntValue(_totalTestsRegex, testResultMessage, TotalTests.Length);
            var passedTests = GetNullableIntValue(_passedTestsRegex, testResultMessage, PassedTests.Length);
            var failedTests = GetNullableIntValue(_failedTestsRegex, testResultMessage, FailedTests.Length);
            var ignoredTests = GetNullableIntValue(_ignoredTestsRegex, testResultMessage, IgnoredTests.Length);
            var projectName = GetStringValue(_projectNameRegex, testResultMessage);
            var timeTaken = GetDecimalValue(_totalTimeTakenRegex, testResultMessage, TotalTimeTaken.Length);

            return new TestResult
            {
                ProjectName = projectName,
                TotalNumberOfTests = totalTests,
                NumberOfPassedTests = passedTests,
                NumberOfFailedTests = failedTests,
                NumberOfIgnoredTests = ignoredTests,
                TimeTakenInSecond = timeTaken
            };
        }

        private int? GetNullableIntValue(Regex regex, string testResultMessage, int substringStartIndex)
        {
            var intValue = regex.Match(testResultMessage);
            if (intValue.Success)
            {
                return int.Parse(intValue.Value.Substring(substringStartIndex));
            }

            return default(int?);
        }

        private string GetStringValue(Regex regex, string testResultMessage)
        {
            var totalTests = regex.Match(testResultMessage);
            return totalTests.Value;
        }
        
        private decimal GetDecimalValue(Regex regex, string testResultMessage, int substringStartIndex)
        {
            var totalTests = regex.Match(testResultMessage);
            return decimal.Parse(totalTests.Value.Substring(substringStartIndex));
        }
    }
}
