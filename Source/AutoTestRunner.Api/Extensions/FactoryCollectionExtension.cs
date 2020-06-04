using AutoTestRunner.Api.Factory.Implementation;
using AutoTestRunner.Api.Factory.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class FactoryCollectionExtension
    {
        public static IServiceCollection AddFactories(this IServiceCollection service)
        {
            service.AddSingleton<ITestReportFactory, TestReportFactory>();

            return service;
        }
    }
}