using AutoTestRunner.Api.Mappers.Implementation;
using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions

{
    public static class MapperCollectionExtension
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper<ProjectWatcher, ProjectWatcherDto>, ProjectWatcherDtoMapper>();
            serviceCollection.AddSingleton<IMapper<TestReport, TestSummaryDto>, TestReportDtoMapper>();
            
            return serviceCollection;
        }
    }
}
