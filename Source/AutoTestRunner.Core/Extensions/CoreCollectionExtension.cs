using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Core.Extensions
{
    public static class CoreCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection service)
        {
            service.AddSingleton<IProjectWatcherRepository, ProjectWatcherRepository>();
            service.AddSingleton<IAppDataService, AppDataService>();
            service.AddSingleton<IJsonService, JsonService>();
            return service;
        }
    }
}
