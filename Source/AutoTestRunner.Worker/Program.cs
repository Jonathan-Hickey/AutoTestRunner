using AutoTestRunner.Core.Extensions;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
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
                    services.AddCore();
                    
                    services.AddSingleton<IFileRepository<ProjectWatcher>, FileRepository<ProjectWatcher>>(f =>
                        new FileRepository<ProjectWatcher>(f.GetService<IAppDataService>().GetProjectWatcherFilePath()));

                    services.AddServices();
                    
                    services.AddHostedService<Worker>();
                });
    }
}
