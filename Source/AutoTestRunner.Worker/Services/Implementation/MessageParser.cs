using AutoTestRunner.Core.Models;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class MessageParser : IMessageParser
    {

        private readonly ITestSummaryParser _testSummaryParser;
        private readonly ITestDetailParser _testDetailParser;

        public MessageParser(ITestSummaryParser testSummaryParser, ITestDetailParser  testDetailParser)
        {
            _testDetailParser = testDetailParser;
            _testSummaryParser = testSummaryParser;
        }
        

        public TestSummary GetTestResult(string testResultMessage)
        {
            var t = _testDetailParser.CreateTestDetails(testResultMessage);
            return _testSummaryParser.CreateTestSummary(testResultMessage);
        }
    }
}
