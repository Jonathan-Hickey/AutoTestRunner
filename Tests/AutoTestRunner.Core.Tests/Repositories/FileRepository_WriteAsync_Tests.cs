using System.Collections.Generic;
using System.IO;
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
    public class FileRepository_WriteAsync_Tests
    {
        [Test]
        public async Task When_NonNullObject_Passed_Should_Then_Add_ToFile()
        {
            var moqFileHelper = new Mock<IFileHelper>();

            var list = new List<string>();
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");

            moqFileHelper.Setup(f => f.ReadFileAsync()).ReturnsAsync(list);


            var expectedInput = new List<string>();
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeId\":1,\"FakeData\":\"This is some fake input\"}");

            moqFileHelper.Setup(f => f.WriteToFileAsync(It.Is<List<string>>(l => string.Equals(l[3], "item4"))))
                .Returns(Task.CompletedTask);


            var fileRepository = new FileRepository<FakeClass>(new JsonService(), moqFileHelper.Object);

            var input = new FakeClass()
            {
                FakeData = "This is some fake input",
                FakeId = 1
            };

            await fileRepository.WriteAsync(input);

            moqFileHelper.Verify(f => f.WriteToFileAsync(It.IsAny<List<string>>()), Times.Once(),
                "Write was not called");
        }


        [Test]
        public void Test()
        {
            var fullPath =
                "C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\NUnitTestProject2\\bin\\Debug\\netcoreapp3.1\\NUnitTestProject2.dll";
            var f = fullPath.LastIndexOf('\\');
            var projectWatchPath = fullPath.Substring(0, f);
            var fileToWatch = fullPath.Substring(f+1);

            Directory.Exists(projectWatchPath).Should().BeTrue();

            projectWatchPath.Should()
                .BeEquivalentTo(
                    "C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\NUnitTestProject2\\bin\\Debug\\netcoreapp3.1\\");

            fileToWatch.Should().BeEquivalentTo("NUnitTestProject2.dll");
        }
    }
}
