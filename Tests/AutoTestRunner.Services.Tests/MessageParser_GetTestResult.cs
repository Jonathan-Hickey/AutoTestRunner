using AutoTestRunner.Services.Models;
using AutoTestRunner.Services.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Services.Tests
{
    [TestFixture]
    public class MessageParser_GetTestResult
    {

        [Test]
        public void Should_Get_Test_Result()
        {
            var testResultMessage = FakeDataHelper.GetPassed_Failed_Ignored_Message();

            var messageParser = new MessageParser();

            var testResult = messageParser.GetTestResult(testResultMessage);
            
            var expectedResult = new TestResult
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
    }
}