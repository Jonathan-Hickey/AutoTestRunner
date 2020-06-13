using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Models;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestRunner.Api.Extensions
{
    public static class BsonMapperExtension
    {
        public static IServiceCollection InitBsonMappers(this IServiceCollection service)
        {
            BsonMapper.Global.Entity<ProjectWatcher>()
                .Id(p => p.ProjectWatcherId);

            BsonMapper.Global.Entity<TestSummary>()
                .Id(c => c.TestSummaryReportId);

            BsonMapper.Global.Entity<TestDetail>()
                .Id(t => t.TestDetailId);

            BsonMapper.Global.Entity<TestReport>()
                .Id(c => c.TestReportId)
                .DbRef(e => e.TestSummary, nameof(TestSummary))
                .DbRef(e => e.TestDetails, nameof(TestDetail));

            return service;
        }
    }
}
