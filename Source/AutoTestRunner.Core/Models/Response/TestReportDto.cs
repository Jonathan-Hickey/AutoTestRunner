using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Response
{
    public class TestReportDto
    {
        [JsonPropertyName("report_id")]
        public Guid ReportId { get; set; }

        [JsonPropertyName("project_watcher_id")]
        public Guid ProjectWatcherId { get; set; }
        
        [JsonPropertyName("run_date_time")]
        public string RunDateTime { get; set; }

        [JsonPropertyName("test_summary")]
        public TestSummaryDto TestSummary { get; set; }

        [JsonPropertyName("passed_tests")]
        public IReadOnlyList<TestDetailDto> PassedTests { get; set; }
        
        [JsonPropertyName("failed_tests")] 
        public IReadOnlyList<TestDetailDto> FailedTests { get; set; }
        
        [JsonPropertyName("ignored_tests")]
        public IReadOnlyList<TestDetailDto> IgnoredTests { get; set; }
    }
}