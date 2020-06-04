using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class RepositoryCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileRepository<ProjectWatcher>, FileRepository<ProjectWatcher>>(f => 
                new FileRepository<ProjectWatcher>(f.GetService<IAppDataService>().GetProjectWatcherFilePath()));

            serviceCollection.AddSingleton<IFileRepository<TestReport>, FileRepository<TestReport>>(f =>
                new FileRepository<TestReport>(f.GetService<IAppDataService>().GetTestReportFilePath()));


            return serviceCollection;
        }
    }
}