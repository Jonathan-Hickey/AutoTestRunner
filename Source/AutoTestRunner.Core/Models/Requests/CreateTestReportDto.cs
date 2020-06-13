using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Requests
{
    public class CreateTestReportDto
    {
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
        public IReadOnlyList<TestDetailRequestDto> TestDetails { get; set; }
    }
}