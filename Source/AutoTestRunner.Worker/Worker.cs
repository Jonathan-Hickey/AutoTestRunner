using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
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
        
        private readonly MemoryCache _memoryCache;
        private readonly string[] _paths;
        private readonly List<CustomFileWatcher> _fileWatchers;
        public Worker(ILogger<Worker> logger,
                      ICommandLineService commandLineService,
                      IMessageParser messageParser,
                      IWindowsNotificationService windowsNotificationService)
        {

            _memoryCache = MemoryCache.Default;
            _windowsNotificationService = windowsNotificationService;
            _messageParser = messageParser;
            _commandLineService = commandLineService;
            _logger = logger;

            _paths = new[]
            {
                "C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner\\obj\\Debug\\netcoreapp3.1",
                "C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\NUnitTestProject2\\obj\\Debug\\netcoreapp3.1"
            };

            _fileWatchers = new List<CustomFileWatcher>();
        }

        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            foreach (var path in _paths)
            {
                var fileWatcher = new CustomFileWatcher(_memoryCache, path);
                fileWatcher.OnDllChanged = RunDllTests;

                _fileWatchers.Add(fileWatcher);
            }
            
            return Task.CompletedTask;
        }

        private void RunDllTests(FileSystemEventArgs e)
        {
            var fullPathWithFileNameRemoved = e.FullPath.Substring(0,e.FullPath.Length - e.Name.Length);
            var projectPath = Path.Combine(fullPathWithFileNameRemoved, "..", "..", "..");

            var testResultMessage = _commandLineService.RunTestProject(projectPath);
            var messageResult = _messageParser.GetTestResult(testResultMessage);
            _windowsNotificationService.Push(messageResult);
        }
    }
}