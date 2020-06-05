using System;
using System.Text.Json.Serialization;

namespace AutoTestRunner.Core.Models.Response
{
    public class CreateTestReportResponseDto
    {
        [JsonPropertyName("report_id")]
        public Guid ReportId { get; set; }
    }
}
