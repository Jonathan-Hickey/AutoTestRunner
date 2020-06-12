using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoTestRunner.Core.Enums;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class TestDetailParser : ITestDetailParser
    {
        private const string TimeTakenInMinutesPattern = "(\\d*m)";
        private const string TimeTakenInSecondsPattern = "(\\d*s)";
        private const string TimeTakenInMillisecondsPattern = "(\\d*ms)";

        private static readonly string TimeTakenPattern = $"\\[(< )?{TimeTakenInMinutesPattern}?\\s?{TimeTakenInSecondsPattern}?\\s?{TimeTakenInMillisecondsPattern}?\\]";
        private readonly Regex _testName = new Regex($"[V!X] \\w* {TimeTakenPattern}", RegexOptions.Compiled);
        private readonly Regex _timeTaken = new Regex(TimeTakenPattern, RegexOptions.Compiled);
        
        private readonly Regex _testTimeTakenInMinutes = new Regex(TimeTakenInMinutesPattern, RegexOptions.Compiled);
        private readonly Regex _testTimeTakenInSeconds = new Regex(TimeTakenInSecondsPattern, RegexOptions.Compiled);
        private readonly Regex _testTimeTakenInMilliseconds = new Regex(TimeTakenInMillisecondsPattern, RegexOptions.Compiled);

        public IReadOnlyList<TestDetail> CreateTestDetails(string testResultText)
        {
            var testReportDetailMatch = _testName.Matches(testResultText);
            return GetTestDetails(testReportDetailMatch);
        }

        private IReadOnlyList<TestDetail> GetTestDetails(MatchCollection matchCollection)
        {
            var testDetails = new List<TestDetail>();

            foreach (Match match in matchCollection)
            {
                if (match.Success)
                {
                    testDetails.Add(CreateTestDetail(match.Value));
                }
            }

            return testDetails;
        }

        private TestDetail CreateTestDetail(string testDetailText)
        {
            var arr = testDetailText.Split(" ");

            var testTimeTakenInMs = GetTestTimeTakenInMs(testDetailText);

            var testName = arr[1];
            return new TestDetail
            {
                TestStatus = GetTestStatus(arr[0][0]),
                TimeTakenInMs = testTimeTakenInMs,
                TestName = testName,
            };
        }

        private int GetTestTimeTakenInMs(string testDetailText)
        {
            var match = _timeTaken.Match(testDetailText);
            
            var s = match.Value;

            var milliseconds = GetMilliseconds(s);
            s = _testTimeTakenInMilliseconds.Replace(s, "");

            var seconds =GetSecondsInMilliseconds(s);
            s = _testTimeTakenInSeconds.Replace(s, "");

            var minutes = GetMinutesInMilliseconds(s);
            
            return milliseconds+seconds+minutes;
        }

        private int GetMinutesInMilliseconds(string lineInfo)
        {
            var match = _testTimeTakenInMinutes.Match(lineInfo);
            return TestTimeTaken(match.Value) * 1000 * 60;
        }
        
        private int GetSecondsInMilliseconds(string lineInfo)
        {
            var match = _testTimeTakenInSeconds.Match(lineInfo);
            return TestTimeTaken(match.Value) * 1000;
        }

        private int GetMilliseconds(string lineInfo)
        {
            var match = _testTimeTakenInMilliseconds.Match(lineInfo);
            return TestTimeTaken(match.Value);
        }

        private static int TestTimeTaken(string s)
        {
            var testTimeTakenString = string.Empty;
            foreach (var c in s)
            {
                if (char.IsDigit(c))
                {
                    testTimeTakenString += c;
                }
            }

            return string.IsNullOrEmpty(testTimeTakenString) ? 0 : int.Parse(testTimeTakenString);
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
    }
}