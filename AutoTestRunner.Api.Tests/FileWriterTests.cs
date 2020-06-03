using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace AutoTestRunner.Api.Tests
{
    [Ignore("these methods are private and they are interacting with files from the OS")]
    [TestFixture]
    public class FileWriterTests
    {

        [Test]
        public void CreateFile_Test()
        {
            var fileName = "MyTestFile.json";
            var filePathToWatch = "{\"filePath\": \"C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner\\obj\\Debug\\netcoreapp3.1\"}";
            
            //WriteToFile(fileName, new List<string> { filePathToWatch });

            File.Exists(fileName).Should().BeTrue();
        }


        [Test]
        public void ReadFile_Test()
        {
            var fileName = "MyTestFile.json";
            var filePathToWatch = "{\"filePath\": \"C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner\\obj\\Debug\\netcoreapp3.1\"}";

            //WriteToFile(fileName, new List<string> { filePathToWatch });
            //var content = ReadFile(fileName);

            //content.Should().NotBeNullOrEmpty();
            //content[0].Should().BeEquivalentTo(filePathToWatch);
        }

        [Test]
        
        public void Read_And_Write_File_Test()
        {
            var fileName = "MyTestFile.json";

            var filePathToWatch = "{\"filePath\":\"C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner\\obj\\Debug\\netcoreapp3.1\"}";
            var secondFilePath = "{\"filePath\":\"C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\NUnitTestProject2\\obj\\Debug\\netcoreapp3.1\"}";

            //WriteToFile(fileName, new List<string> { filePathToWatch });

            //var content = ReadFile(fileName);

            //WriteToFile(fileName, new List<string> { filePathToWatch, secondFilePath });

            //content.Should().NotBeNullOrEmpty();
            //content[0].Should().BeEquivalentTo(filePathToWatch);
        }
    }
}
