using System;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Api.Models
{
    public class TestReport
    {
        public Guid TestReportId { get; set; }
        public DateTimeOffset RunDateTime { get; set; }
        public Guid ProjectWatcherId { get; set; }
        public TestSummary TestSummary { get; set; }

        //public IReadOnlyList<TestDetail> TestDetails { get; set; }
    }
}
