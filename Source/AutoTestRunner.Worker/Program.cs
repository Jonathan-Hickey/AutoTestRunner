using AutoTestRunner.Core.Extensions;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Clients.Implementation;
using AutoTestRunner.Worker.Clients.Interfaces;
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
                .ConfigureServices((hostContext, service) =>
                {
                    service.AddCore();
                    
                    service.AddHttpClient<IAutoTestRunnerClient, AutoTestRunnerClient>();
                    
                    service.AddSingleton<IConnectionFactory, ConnectionFactory>(s =>
                        new ConnectionFactory(s.GetService<IAppDataService>().GetLiteDatabaseConnectionString()));
                    
                    service.AddMappers();

                    service.AddServices();

                    service.AddHostedService<Worker>();
                });
    }
}
