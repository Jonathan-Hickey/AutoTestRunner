using AutoTestRunner.Api.Repositories.Implementation;
using AutoTestRunner.Api.Repositories.Interfaces;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class RepositoryCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {

            service.AddSingleton<IConnectionFactory, ConnectionFactory>(s =>
                new ConnectionFactory(s.GetService<IAppDataService>().GetLiteDatabaseConnectionString()));

            service.AddSingleton<ITestReportRepository, TestReportRepository>();

            return service;
        }
    }
}