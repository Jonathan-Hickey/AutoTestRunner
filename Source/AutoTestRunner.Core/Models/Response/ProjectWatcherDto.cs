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

        [JsonPropertyName("project_watch_path")]
        public string ProjectWatchPath { get; set; }

        [JsonPropertyName("rile_to_watch")]
        public string FileToWatch { get; set; }
    }
}
