using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Core.Models.Response;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Clients.Interfaces;
using AutoTestRunner.Worker.Extensions;
using AutoTestRunner.Worker.Helpers;
using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Clients.Implementation
{
    public class AutoTestRunnerClient : IAutoTestRunnerClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AutoTestRunnerClient> _logger;
        private readonly IMapper<TestSummary, CreateTestReportDto> _createTestReportDtoMapper;
        private readonly IJsonService _jsonService;

        public AutoTestRunnerClient(HttpClient httpClient,
                                    ILogger<AutoTestRunnerClient> logger,
                                    IJsonService jsonService,
                                    IMapper<TestSummary, CreateTestReportDto> createTestReportDtoMapper)
        {
            _jsonService = jsonService;
            _createTestReportDtoMapper = createTestReportDtoMapper;
            _logger = logger;
            _httpClient = httpClient;
        }

        public Guid CreateTestReport(Guid projectWatcherId, TestSummary testSummary)
        {
            var request = _createTestReportDtoMapper.Map(testSummary);

            var responseMessage = _httpClient.Post(_jsonService, ApiUrlHelper.GetCreateTestReportUrl(projectWatcherId), request);

            if (responseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation("Api accepted the request");

                var responseText = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = _jsonService.Deserialize<CreateTestReportResponseDto>(responseText);

                return result.ReportId;
            }

            throw new Exception("Api failed to accept the request.");
        }
    }
}