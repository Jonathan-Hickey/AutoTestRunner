using AutoTestRunner.Worker.Services.Implementation;
using AutoTestRunner.Worker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWindowsNotificationService, WindowsNotificationService>();
            serviceCollection.AddSingleton<IMessageParser, MessageParser>();
            serviceCollection.AddSingleton<ICommandLineService, CommandLineService>();
            serviceCollection.AddSingleton<ITestRunnerService, TestRunnerService>();

            return serviceCollection;
        }
    }
}
