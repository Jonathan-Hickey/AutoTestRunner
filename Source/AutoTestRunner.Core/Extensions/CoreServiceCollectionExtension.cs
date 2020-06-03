using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Core.Extensions
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAppDataService, AppDataService>();
            serviceCollection.AddSingleton<IProjectWatcherService, ProjectWatcherService>();
            return serviceCollection;
        }
    }
}
