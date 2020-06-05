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
        private readonly IMessageParser _messageParser;
        private readonly IAutoTestRunnerClient _autoTestRunnerClient;
        private readonly IWindowsNotificationService _windowsNotificationService;

        public TestRunnerService(ILogger<TestRunnerService> logger,
            ICommandLineService commandLineService,
            IMessageParser messageParser,
            IAutoTestRunnerClient autoTestRunnerClient,
            IWindowsNotificationService windowsNotificationService)
        {
            _windowsNotificationService = windowsNotificationService;
            _autoTestRunnerClient = autoTestRunnerClient;
            _messageParser = messageParser;
            _commandLineService = commandLineService;
            _logger = logger;
        }

        public void RunTests(Guid projectWatcherId, FileSystemEventArgs e)
        {
            _logger.LogInformation($"Running Dll {e.Name}");
            var fullPathWithFileNameRemoved = e.FullPath.Substring(0, e.FullPath.Length - e.Name.Length);
            var projectPath = Path.Combine(fullPathWithFileNameRemoved, "..", "..", "..");

            var testResultMessage = _commandLineService.RunTestProject(projectPath);
            var messageResult = _messageParser.GetTestResult(testResultMessage);


            var reportId = _autoTestRunnerClient.CreateTestReport(projectWatcherId, messageResult);

            _windowsNotificationService.Push(projectWatcherId: projectWatcherId, reportId: reportId, messageResult);
        }
    }
}