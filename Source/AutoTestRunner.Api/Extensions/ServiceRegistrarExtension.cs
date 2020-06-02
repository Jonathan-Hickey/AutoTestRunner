using AutoTestRunner.Api.Controllers;
using AutoTestRunner.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProjectWatcherService, ProjectWatcherService>();
            return serviceCollection;
        }
    }
}
