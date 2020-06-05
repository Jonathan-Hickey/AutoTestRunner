using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Requests
{
    public class CreateProjectWatcherDto
    {
        [JsonPropertyName("full_project_path")]
        public string FullProjectPath { get; set; }
    }
}