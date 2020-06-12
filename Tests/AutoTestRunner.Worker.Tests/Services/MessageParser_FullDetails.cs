using AutoTestRunner.Core.Enums;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Services.Tests.Helpers;
using AutoTestRunner.Worker.Services.Implementation;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Worker.Tests.Services
{
    [TestFixture]
    public class MessageParser_FullDetails
    {
        [Test]
        public void Test()
        {
            var testResultMessage = FakeDataHelper.GetPassed_Failed_Ignored_Message();

            var parser = new TestDetailParser();

            var list = parser.CreateTestDetails(testResultMessage);

            Assert.Pass();
        }

        [Test]
        public void When_Test_Fails_Then_Parser_Can_Parse_To_TestDetail()
        {
            var testResultMessage = "X Test_That_Fails [67ms]";

            var parser = new TestDetailParser();

            var list = parser.CreateTestDetails(testResultMessage);

            var expectedResult = new TestDetail
            {
                TestName = "Test_That_Fails",
                TestStatus = TestStatus.Failed,
                TimeTakenInMs = 67
            };

            list.Should().NotBeNullOrEmpty();
            list[0].Should().BeEquivalentTo(expectedResult);
        }


        [Test]
        public void When_Test_Passed_Then_Parser_Can_Parse_To_TestDetail()
        {
            var testResultMessage = "V Test_That_Passes_3 [1ms]";

            var parser = new TestDetailParser();

            var list = parser.CreateTestDetails(testResultMessage);

            var expectedResult = new TestDetail
            {
                TestName = "Test_That_Passes_3",
                TestStatus = TestStatus.Passed,
                TimeTakenInMs = 1
            };

            list.Should().NotBeNullOrEmpty();
            list[0].Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void When_Test_Is_Ignored_Then_Parser_Can_Parse_To_TestDetail()
        {
            var testResultMessage = "! Test_That_Ignore [< 1ms]";

            var parser = new TestDetailParser();

            var list = parser.CreateTestDetails(testResultMessage);

            var expectedResult = new TestDetail
            {
                TestName = "Test_That_Ignore",
                TestStatus = TestStatus.Ignored,
                TimeTakenInMs = 1
            };

            list.Should().NotBeNullOrEmpty();
            list[0].Should().BeEquivalentTo(expectedResult);
        }



        [Test]
        public void When_Test_Passed_And_Took_Seconds_To_Execute_Then_Parser_Can_Parse_To_TestDetail()
        {
            var testResultMessage = "V Test_That_Passes_3 [2s 38ms]";

            var parser = new TestDetailParser();

            var list = parser.CreateTestDetails(testResultMessage);

            var expectedResult = new TestDetail
            {
                TestName = "Test_That_Passes_3",
                TestStatus = TestStatus.Passed,
                TimeTakenInMs = 2038
            };

            list.Should().NotBeNullOrEmpty();
            list[0].Should().BeEquivalentTo(expectedResult);
        }

    }
}
