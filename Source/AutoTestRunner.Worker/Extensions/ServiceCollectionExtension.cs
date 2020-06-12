using AutoTestRunner.Worker.Services.Implementation;
using AutoTestRunner.Worker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddSingleton<IWindowsNotificationService, WindowsNotificationService>();
            service.AddSingleton<IMessageParser, MessageParser>();
            service.AddSingleton<ICommandLineService, CommandLineService>();
            service.AddSingleton<ITestRunnerService, TestRunnerService>();
            service.AddSingleton<IFileWatcherService, FileWatcherService>();
            service.AddSingleton<ITestSummaryParser, TestSummaryParser>();
            service.AddSingleton<ITestDetailParser, TestDetailParser>();

            return service;
        }
    }
}
