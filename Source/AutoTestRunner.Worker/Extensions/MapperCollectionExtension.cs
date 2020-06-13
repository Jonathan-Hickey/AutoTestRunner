using System.Collections.Generic;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Worker.Mappers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Worker.Extensions
{
    public static class MapperCollectionExtension
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<IMapper<TestDetail, TestDetailRequestDto>, TestDetailRequestDtoMapper>();
            services.AddSingleton<IMapper<TestSummary, IReadOnlyList<TestDetail>, CreateTestReportDto>, CreateTestReportDtoMapper>();

            return services;
        }
    }
}
