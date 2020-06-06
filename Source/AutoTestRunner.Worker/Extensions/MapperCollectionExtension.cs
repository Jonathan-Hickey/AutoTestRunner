using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Worker.Mappers.Implementation;
using AutoTestRunner.Worker.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Worker.Extensions
{
    public static class MapperCollectionExtension
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper<TestResult, CreateTestReportDto>, CreateTestReportDtoMapper>();
            return serviceCollection;
        }
    }
}
