using AutoTestRunner.Core.Models;
using AutoTestRunner.Services.Tests.Helpers;
using AutoTestRunner.Worker.Services.Implementation;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Worker.Tests.Services
{
    [TestFixture]
    public class MessageParser_GetTestResult
    {

        [Test]
        public void Should_Get_Test_Result_For_Passed_Failed_Ignored_Message()
        {
            var testResultMessage = FakeDataHelper.GetPassed_Failed_Ignored_Message();

            var messageParser = new MessageParser(new TestSummaryParser(), new TestDetailParser());

            var testResult = messageParser.GetTestResult(testResultMessage);

            var expectedResult = new TestSummary
            {
                TotalNumberOfTests = 3,
                ProjectName = "TestProjectUsedByAutoTestRunner.dll",
                NumberOfFailedTests = 1,
                NumberOfIgnoredTests = 1,
                NumberOfPassedTests = 1,
                TimeTakenInSecond = 1.7281m
            };

            testResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void Should_Get_Test_Result_For_Failed_Ignored_Message()
        {
            var testResultMessage = FakeDataHelper.GetFailed_Ignored_Message();

            var messageParser = new MessageParser(new TestSummaryParser(), new TestDetailParser());

            var testResult = messageParser.GetTestResult(testResultMessage);

            var expectedResult = new TestSummary
            {
                TotalNumberOfTests = 2,
                ProjectName = "TestProjectUsedByAutoTestRunner.dll",
                NumberOfFailedTests = 1,
                NumberOfIgnoredTests = 1,
                NumberOfPassedTests = null,
                TimeTakenInSecond = 1.7281m
            };

            testResult.Should().BeEquivalentTo(expectedResult);
        }

    }
}