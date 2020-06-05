using AutoTestRunner.Worker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Worker.Clients.Implementation
{
    public interface IAutoTestRunnerClient
    {
        Guid CreateTestReport(Guid projectWatcherId, TestResult testResult);
    }

    public class AutoTestRunnerClient : IAutoTestRunnerClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AutoTestRunnerClient> _logger;
        
        public AutoTestRunnerClient(HttpClient httpClient, ILogger<AutoTestRunnerClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public Guid CreateTestReport(Guid projectWatcherId, TestResult testResult)
        {

            var responseMessage = _httpClient.Post(ApiUrlHelper.GetCreateTestReportUrl(projectWatcherId), testResult);

            if (responseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation("Api accepted the request");

                var responseText = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<CreateTestReportResponseDto>(responseText);

                return result.ReportId;
            }

            throw new Exception("Api failed to accept the request.");
        }
    }


    public static class ApiUrlHelper
    {
        private static readonly string CreateTestReportUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports";
        private static readonly string CallBackUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports/{1}";

        public static string GetCreateTestReportUrl(Guid projectWatcherId)
        {
            return string.Format(CreateTestReportUrl, projectWatcherId);
        }

        public static string GetCallBackUrl(Guid projectWatcherId, Guid reportId)
        {
            return string.Format(CallBackUrl, projectWatcherId, reportId);
        }
    }
}

namespace System.Net.Http
{
    public static class HttpClientExtension
    {
        public static HttpResponseMessage Post<T>(this HttpClient client, string uri, T obj)
        {
            return client.PostAsync(uri, new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json"))
                         .GetAwaiter()
                         .GetResult();
        }
    }

}
