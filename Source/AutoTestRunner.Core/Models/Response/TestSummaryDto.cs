using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Response
{
    public class TestSummaryDto
    {
        [JsonPropertyName("project_watcher_id")]
        public Guid ProjectWatcherId { get; set; }

        [JsonPropertyName("run_date_time")]
        public string RunDateTime { get; set; }

        [JsonPropertyName("report_id")]
        public Guid ReportId { get; set; }
        
        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; }

        [JsonPropertyName("number_of_passed_tests")]
        public int? NumberOfPassedTests { get; set; }

        [JsonPropertyName("number_of_failed_tests")]
        public int? NumberOfFailedTests { get; set; }

        [JsonPropertyName("number_of_ignored_tests")]
        public int? NumberOfIgnoredTests { get; set; }

        [JsonPropertyName("total_number_of_tests")]
        public int? TotalNumberOfTests { get; set; }
        
        [JsonPropertyName("time_taken_in_second")]
        public decimal TimeTakenInSecond { get; set; }

        [JsonPropertyName("failed_tests")]
        public IReadOnlyList<string> FailedTests { get; set; }

        [JsonPropertyName("ignored_tests")] 
        public IReadOnlyList<string> IgnoredTests { get; set; }

    }
}
