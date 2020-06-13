using System.Text.Json.Serialization;
using AutoTestRunner.Core.Enums;

namespace AutoTestRunner.Core.Models.Requests
{
    public class TestDetailRequestDto
    {
        [JsonPropertyName("test_name")]
        public string TestName { get; set; }
        
        [JsonPropertyName("time_taken_in_milliseconds")]
        public int TimeTakenInMilliseconds { get; set; }
        
        [JsonPropertyName("test_status")]
        public TestStatus TestStatus { get; set; }
    }
}