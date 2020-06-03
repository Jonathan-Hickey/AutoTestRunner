using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Core.Extensions
{
    public static class CoreRepositoriesCollectionExtension
    {
        public static IServiceCollection AddCoreRepositories(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSingleton<IFileRepository, FileRepository>(f => 
                new FileRepository(f.GetService<IAppDataService>().GetAutoTestRunnerDataFolderPath()));
            return serviceCollection;
        }

       
    }
}