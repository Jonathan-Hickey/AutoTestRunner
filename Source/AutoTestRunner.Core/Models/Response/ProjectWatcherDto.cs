using System;
using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Response
{
    public class ProjectWatcherDto
    {
        [JsonPropertyName("project_watcher_id")]
        public Guid ProjectWatcherId { get; set; }

        [JsonPropertyName("full_project_path")]
        public string FullProjectPath { get; set; }
    }
}
