using System;
using System.Collections.Generic;
using AutoTestRunner.Worker.Models;
using AutoTestRunner.Worker.Services.Interfaces;
using System.Text.RegularExpressions;
using AutoTestRunner.Core.Enums;
using AutoTestRunner.Core.Models;

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

        private readonly Regex _testName = new Regex("[V!X] \\w*\\[\\d*ms\\]", RegexOptions.Compiled);
        private readonly Regex _testTimeTakenInMs = new Regex("\\[\\d*ms\\]", RegexOptions.Compiled);


        public TestSummary GetTestResult(string testResultMessage)
        {
            var testReportDetailMatch = _testName.Match(testResultMessage);
            var  testDetails =  GetTestDetails(testReportDetailMatch);


            return GetTestReportSummary(testResultMessage);
        }

        private TestSummary GetTestReportSummary(string testResultMessage)
        {
            var totalTests = GetNullableIntValue(_totalTestsRegex, testResultMessage, TotalTests.Length);
            var passedTests = GetNullableIntValue(_passedTestsRegex, testResultMessage, PassedTests.Length);
            var failedTests = GetNullableIntValue(_failedTestsRegex, testResultMessage, FailedTests.Length);
            var ignoredTests = GetNullableIntValue(_ignoredTestsRegex, testResultMessage, IgnoredTests.Length);
            var projectName = GetStringValue(_projectNameRegex, testResultMessage);
            var timeTaken = GetDecimalValue(_totalTimeTakenRegex, testResultMessage, TotalTimeTaken.Length);


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


        private TestStatus GetTestStatus(char testStatus)
        {
            switch (testStatus)
            {
                case '!':
                    return TestStatus.Ignored;
                case 'X':
                    return TestStatus.Failed;
                case 'V':
                    return TestStatus.Passed;
                default:
                    throw new ArgumentException($"Unknown mapping for {testStatus}");
            }
        }

        private IReadOnlyList<TestDetail> GetTestDetails(Match match)
        {

            var tests = new List<TestDetail>();

            if (match.Success)
            {
                foreach (Group line in match.Groups)
                {
                    var testTimeTakenInMs = _testTimeTakenInMs.Match(line.Value);


                    var arr = line.Value.Split(" ");

                    var testTimeTakenInMsString = string.Empty;

                    foreach (var c in testTimeTakenInMs.Value)
                    {
                        if (char.IsDigit(c))
                        {
                            testTimeTakenInMsString += c;
                        }
                    }

                    var testName = arr[1].Substring(0, arr[1].IndexOf('['));
                    tests.Add(new TestDetail
                    {
                        TestStatus = GetTestStatus(arr[0][0]),
                        TimeTakenInMs = int.Parse(testTimeTakenInMsString),
                        TestName = testName,
                    });
                }
            }

            return tests;
        }
    }
}
