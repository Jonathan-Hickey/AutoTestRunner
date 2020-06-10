using System;
using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Api.Models;
using AutoTestRunner.Api.Repositories.Interfaces;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Repositories.Interfaces;

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

                return testReports.Find(r => r.TestReportId == testReportId && r.ProjectWatcherId == projectWatcherId).Single();
            }
        }

        public IReadOnlyList<TestReport> GetTestReports(Guid projectWatcherId)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                var testReports = db.GetCollection<TestReport>();

                return testReports.Find(r => r.ProjectWatcherId == projectWatcherId).ToList();
            }
        }

        public void AddTestReport(TestReport testReport)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                var testReports = db.GetCollection<TestReport>();

                testReports.EnsureIndex(p => p.TestReportId);
                testReports.Insert(testReport);
            }
        }
    }
}
