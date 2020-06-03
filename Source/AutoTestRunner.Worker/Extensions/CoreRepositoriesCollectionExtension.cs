using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Worker.Extensions
{
    public static class RepositoriesCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddSingleton<IFileRepository, FileRepository>(f => 
                new FileRepository(f.GetService<IAppDataService>().GetAutoTestRunnerDataFolderPath()));
            return serviceCollection;
        }

       
    }
}