﻿using System.Text.RegularExpressions;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class TestSummaryParser : ITestSummaryParser
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


        public TestSummary CreateTestSummary(string testResultText)
        {
            var totalTests = GetNullableIntValue(_totalTestsRegex, testResultText, TotalTests.Length);
            var passedTests = GetNullableIntValue(_passedTestsRegex, testResultText, PassedTests.Length);
            var failedTests = GetNullableIntValue(_failedTestsRegex, testResultText, FailedTests.Length);
            var ignoredTests = GetNullableIntValue(_ignoredTestsRegex, testResultText, IgnoredTests.Length);
            var projectName = GetStringValue(_projectNameRegex, testResultText);
            var timeTaken = GetDecimalValue(_totalTimeTakenRegex, testResultText, TotalTimeTaken.Length);


            return new TestSummary
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