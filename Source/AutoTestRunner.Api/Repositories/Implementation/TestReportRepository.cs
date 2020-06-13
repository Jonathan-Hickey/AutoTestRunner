using System;
using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Api.Models;
using AutoTestRunner.Api.Repositories.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using LiteDB;

namespace AutoTestRunner.Api.Repositories.Implementation
{
    public class TestReportRepository : ITestReportRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TestReportRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public TestReport GetTestReport(Guid projectWatcherId, Guid testReportId)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                var testReports = db.GetCollection<TestReport>();

                return testReports.Include(r => r.TestSummary)
                                  .Include(r => r.TestDetails)
                                  .Find(r => r.TestReportId == testReportId && r.ProjectWatcherId == projectWatcherId)
                                  .Single();
            }
        }

        public IReadOnlyList<TestReport> GetTestReports(Guid projectWatcherId)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                var testReports = db.GetCollection<TestReport>();

                return testReports.Include(r => r.TestSummary)
                                  .Include(r => r.TestDetails)
                                  .Find(r => r.ProjectWatcherId == projectWatcherId)
                                  .ToList();
            }
        }

        public void AddTestReport(TestReport testReport)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                BsonMapper.Global.Entity<TestSummary>()
                    .Id(c => c.TestSummaryReportId);

                BsonMapper.Global.Entity<TestDetail>()
                    .Id(t => t.TestDetailId);

                BsonMapper.Global.Entity<TestReport>()
                    .Id(c => c.TestReportId)
                    .DbRef(e => e.TestSummary, "TestSummary")
                    .DbRef(e => e.TestDetails, "TestDetail");

                var testSummaries = db.GetCollection<TestSummary>();
                testSummaries.EnsureIndex(s => s.TestSummaryReportId);
                testSummaries.Insert(testReport.TestSummary);

                var testDetails = db.GetCollection<TestDetail>();
                testDetails.EnsureIndex(s => s.TestDetailId);
                testDetails.Insert(testReport.TestDetails);

                var testReports = db.GetCollection<TestReport>();

                testReports.EnsureIndex(p => p.TestReportId);
                testReports.Insert(testReport);
            }
        }
    }
}