using AutoTestRunner.Worker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AutoTestRunner.Worker.Clients.Implementation
{
    public interface IAutoTestRunnerClient
    {
        void CreateTestReport(Guid projectWatcherId, TestResult testResult);
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

        public void CreateTestReport(Guid projectWatcherId, TestResult testResult)
        {
            var responseMessage = _httpClient.Post($"https://localhost:5001/ProjectWatcher/{projectWatcherId}/TestReports", testResult);

            if (responseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation("Api accepted the request");
                return;
            }

            _logger.LogCritical("Api failed to accept the request. Is the api running?");
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
