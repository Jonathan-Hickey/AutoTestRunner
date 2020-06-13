using System;
using System.IO;
using AutoTestRunner.Worker.Clients.Interfaces;
using AutoTestRunner.Worker.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class TestRunnerService : ITestRunnerService
    {
        private readonly ILogger<TestRunnerService> _logger;
        private readonly ICommandLineService _commandLineService;
        private readonly IAutoTestRunnerClient _autoTestRunnerClient;
        private readonly IWindowsNotificationService _windowsNotificationService;
        private readonly ITestSummaryParser _testSummaryParser;
        private readonly ITestDetailParser _testDetailParser;

        public TestRunnerService(ILogger<TestRunnerService> logger,
            ICommandLineService commandLineService,
            ITestSummaryParser testSummaryParser,
            IAutoTestRunnerClient autoTestRunnerClient,
            ITestDetailParser testDetailParser,
            IWindowsNotificationService windowsNotificationService)
        {
            _testDetailParser = testDetailParser;
            _testSummaryParser = testSummaryParser;
            _windowsNotificationService = windowsNotificationService;
            _autoTestRunnerClient = autoTestRunnerClient;
            _commandLineService = commandLineService;
            _logger = logger;
        }

        public void RunTests(Guid projectWatcherId, FileSystemEventArgs e)
        {
            _logger.LogInformation($"Running Dll {e.Name}");
            var fullPathWithFileNameRemoved = e.FullPath.Substring(0, e.FullPath.Length - e.Name.Length);
            var projectPath = Path.Combine(fullPathWithFileNameRemoved, "..", "..", "..");

            var testResultMessage = _commandLineService.RunTestProject(projectPath);
            
            var testSummary = _testSummaryParser.CreateTestSummary(testResultMessage);
            var testDetails =  _testDetailParser.CreateTestDetails(testResultMessage);

            var reportId = _autoTestRunnerClient.CreateTestReport(projectWatcherId, testSummary, testDetails);

            _windowsNotificationService.Push(projectWatcherId: projectWatcherId, reportId: reportId, testSummary);
        }
    }
}