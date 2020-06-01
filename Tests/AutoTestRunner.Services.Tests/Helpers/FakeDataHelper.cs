using System.IO;

namespace AutoTestRunner.Services.Tests.Helpers
{
    public static class FakeDataHelper
    {
        public static string GetPassed_Failed_Ignored_Message()
        {
            var path = @$"{Directory.GetCurrentDirectory()}\TestData\Passed_Failed_Ignored_Message.txt";
            return File.ReadAllText(path);
        }       
    }
}