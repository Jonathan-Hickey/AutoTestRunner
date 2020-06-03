using AutoTestRunner.Core.Extensions;
using AutoTestRunner.Worker.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoTestRunner.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddCoreServices();
                    services.AddCoreRepositories();
                    
                    services.AddServices();
                    
                    services.AddHostedService<Worker>();
                });
    }
}
