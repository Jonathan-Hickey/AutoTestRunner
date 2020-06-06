using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Core.Extensions
{
    public static class CoreCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAppDataService, AppDataService>();
            serviceCollection.AddSingleton<IJsonService, JsonService>();
            return serviceCollection;
        }
    }
}
