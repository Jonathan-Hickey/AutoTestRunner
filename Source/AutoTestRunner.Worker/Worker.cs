using System;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Services;
using AutoTestRunner.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoTestRunner.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICommandLineService _commandLineService;
        private readonly IMessageParser _messageParser;
        private readonly IWindowsNotificationService _windowsNotificationService;

        public Worker(ILogger<Worker> logger,
                      ICommandLineService commandLineService,
                      IMessageParser messageParser,
                      IWindowsNotificationService windowsNotificationService)
        {
            _windowsNotificationService = windowsNotificationService;
            _messageParser = messageParser;
            _commandLineService = commandLineService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
