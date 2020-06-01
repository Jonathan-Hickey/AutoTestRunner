using System;
using System.Threading.Tasks;
using AutoTestRunner.Services;

namespace TestConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter test project file path.");
                var testProjectPath = Console.ReadLine();

                if (string.IsNullOrEmpty(testProjectPath))
                {
                    testProjectPath = "C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner";
                }

                var commandLineService = new CommandLineService();

                var testResultMessage = await commandLineService.RunTestProjectAsync(testProjectPath);

                var messageParser = new MessageParser();

                var testResult = messageParser.GetTestResult(testResultMessage);
                
                var windowsNotificationService = new WindowsNotificationService();
                windowsNotificationService.Push(testResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            Console.WriteLine("pushing notification have a nice day!");
            Console.WriteLine("Press any key to leave close the console!");
            Console.ReadKey();
        }
    }
}
