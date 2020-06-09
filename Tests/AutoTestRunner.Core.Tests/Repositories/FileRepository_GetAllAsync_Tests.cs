using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Tests.Fakes;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AutoTestRunner.Core.Tests.Repositories
{
    [TestFixture]
    public class FileRepository_GetAllAsync_Tests
    {
        [Test]
        public async Task When_NonNullObject_Passed_Should_Then_Add_ToFile()
        {
            var moqFileHelper = new Mock<IFileHelper>();

            static async IAsyncEnumerable<string> GetTestValues()
            {
                yield return "{\"FakeData\":\"This is some fake input\"}";
                yield return "{\"FakeData\":\"This is some fake input\"}";
                yield return "{\"FakeData\":\"This is some fake input\"}";

                await Task.CompletedTask; // to make the compiler warning go away
            }
            
            moqFileHelper.Setup(f => f.ReadFileAsync()).Returns(GetTestValues());

            var fileRepository = new FileRepository<FakeNullableData>(new JsonService(), moqFileHelper.Object);

            var results = await fileRepository.GetAllAsync(CancellationToken.None).ToListAsync().AsTask();

            var expectedResults = new List<FakeNullableData>
            {
                new FakeNullableData {FakeData = "This is some fake input"},
                new FakeNullableData {FakeData = "This is some fake input"},
                new FakeNullableData {FakeData = "This is some fake input"},
            };

            moqFileHelper.Verify(f => f.ReadFileAsync(), Times.Once(), "Write was not called");

            results.Should().BeEquivalentTo(expectedResults);
        }
    }
}
