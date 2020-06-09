using AutoTestRunner.Api.Controllers;
using AutoTestRunner.Api.Services.Implementation;
using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddSingleton<ITestReportService, TestReportService>();
            service.AddSingleton<IProjectWatcherService, ProjectWatcherService>();
            service.AddSingleton<IHashService, HashService>();
            service.AddSingleton<IValidator, Validator>();
            return service;
        }
    }
}
