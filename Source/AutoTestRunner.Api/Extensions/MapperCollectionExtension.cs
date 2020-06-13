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
        public static IServiceCollection AddMappers(this IServiceCollection service)
        {
            service.AddSingleton<IMapper<ProjectWatcher, ProjectWatcherDto>, ProjectWatcherDtoMapper>();
            service.AddSingleton<IMapper<TestReport, TestReportDto>, TestReportDtoMapper>();
            service.AddSingleton<IMapper<TestSummary, TestSummaryDto>, TestSummaryDtoMapper>();
            service.AddSingleton<IMapper<TestDetail, TestDetailResponseDto>, TestDetailResponseDtoMapper>();
            return service;
        }
    }
}
