using AutoTestRunner.Api.Services.Implementation;
using AutoTestRunner.Api.Services.Interfaces;
using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddSingleton<ITestReportService, TestReportService>();
            service.AddSingleton<IProjectWatcherService, ProjectWatcherService>();
            return service;
        }
    }
}
