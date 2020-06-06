using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Tests.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Core.Tests.Services
{
    [TestFixture]
    public class JsonService_Deserialize_Tests
    {
        [Test]
        public void When_Json_Does_Not_Contain_Elements_Then_Those_Elements_Should_Be_Null()
        {
            var input =  "{\"FakeData\":\"This is some fake input\"}";

            var jsonService = new JsonService();

            var result = jsonService.Deserialize<FakeNullableData>(input);
            
            var expectedResult = new FakeNullableData
            {
                FakeData = "This is some fake input"
            };

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void When_All_Json_Elements_Contain_Data_Then_Those_Elements_Should_Not_Be_Null()
        {
            var input = "{\"FakeNullableId\":1,\"FakeData\":\"This is some fake input\"}";

            var jsonService = new JsonService();

            var result = jsonService.Deserialize<FakeNullableData>(input);

            var expectedResult = new FakeNullableData
            {
                FakeData = "This is some fake input",
                FakeNullableId = 1
            };

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void When_Json_Says_null_Then_Should_Return_Null()
        {
            string input = "null";
            var jsonService = new JsonService();

            var result = jsonService.Deserialize<FakeNullableData>(input);

            result.Should().BeNull();
        }
    }
}