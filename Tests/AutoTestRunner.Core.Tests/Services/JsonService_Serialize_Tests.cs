using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Tests.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Core.Tests.Services
{
    [TestFixture]
    public class JsonService_Serialize_Tests
    {
        [Test]
        public void When_Object_With_Null_Values_Should_Then_Return_Json_WithOut_Those_Values_Contained_In_The_String()
        {
            var input = new FakeNullableData
            {
                FakeData = "This is some fake input",
                FakeNullableId = null
            };

            var jsonService = new JsonService();

            var result = jsonService.Serialize(input);
            var expectedResult = "{\"FakeData\":\"This is some fake input\"}";

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void When_Object_With_NonNull_Values_Should_Then_Return_Json_WithOut_Those_Values_Contained_In_The_String()
        {
            var input = new FakeNullableData
            {
                FakeData = "This is some fake input",
                FakeNullableId = 1
            };

            var jsonService = new JsonService();

            var result = jsonService.Serialize(input);
            var expectedResult = "{\"FakeNullableId\":1,\"FakeData\":\"This is some fake input\"}";

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void When_Null_Object_Should_Then_null_in_String()
        {
            FakeNullableData input = null;
            var jsonService = new JsonService();

            var result = jsonService.Serialize(input);
            var expectedResult = "null";

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}