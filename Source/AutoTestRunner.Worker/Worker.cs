using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Interfaces;
using AutoTestRunner.Worker.Services.Implementation;
using AutoTestRunner.Worker.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoTestRunner.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ICommandLineService _commandLineService;
        private readonly IMessageParser _messageParser;
        private readonly IWindowsNotificationService _windowsNotificationService;
        
        private readonly MemoryCache _memoryCache;
        private readonly IDictionary<Guid,CustomFileWatcher> _fileWatcherLookUp;
        private readonly IFileRepository<ProjectWatcher> _fileRepository;
        private readonly IAppDataService _appDataService;
        private readonly ILogger<Worker> _logger;
        private readonly Guid _myApplicationId; 

        private static readonly string _filter = "*.dll";

        public Worker(ICommandLineService commandLineService,
                      IMessageParser messageParser,
                      IWindowsNotificationService windowsNotificationService,
                      IFileRepository<ProjectWatcher> fileRepository,
                      IAppDataService appDataService,
                      ILogger<Worker> logger)
        {
            _logger = logger;
            _appDataService = appDataService;
            _fileRepository = fileRepository;

            _memoryCache = MemoryCache.Default;
            _windowsNotificationService = windowsNotificationService;
            _messageParser = messageParser;
            _commandLineService = commandLineService;
            _myApplicationId = Guid.NewGuid();
            _fileWatcherLookUp = new Dictionary<Guid, CustomFileWatcher>();
        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var projectsToWatch = await _fileRepository.GetAllAsync();

            foreach (var project in projectsToWatch)
            {
                WatchProject(project);
            }

            var projectWatcherFileWatcher = new CustomFileWatcher(_memoryCache, _appDataService.GetAutoTestRunnerDataFolderPath(), _appDataService.GetProjectWatcherFileName());
            projectWatcherFileWatcher.OnChange = WatchNewProject;
            _fileWatcherLookUp.Add(_myApplicationId, projectWatcherFileWatcher);
        }

        private void WatchProject(ProjectWatcher project)
        {
            var fileWatcher = new CustomFileWatcher(_memoryCache, project.FullProjectPath, _filter);
            fileWatcher.OnChange = RunDllTests;
            _fileWatcherLookUp.Add(project.ProjectWatcherId, fileWatcher);
        }

        private void RunDllTests(FileSystemEventArgs e)
        {
            _logger.LogInformation($"Running Dll {e.Name}");
            var fullPathWithFileNameRemoved = e.FullPath.Substring(0,e.FullPath.Length - e.Name.Length);
            var projectPath = Path.Combine(fullPathWithFileNameRemoved, "..", "..", "..");

            var testResultMessage = _commandLineService.RunTestProject(projectPath);
            var messageResult = _messageParser.GetTestResult(testResultMessage);
            _windowsNotificationService.Push(messageResult);
        }

        private void WatchNewProject(FileSystemEventArgs e)
        {
            var projectsToWatch = _fileRepository.GetAll();

            foreach (var project in projectsToWatch)
            {
                if (!_fileWatcherLookUp.ContainsKey(project.ProjectWatcherId))
                {
                    WatchProject(project);
                }
            }

            var projectsToStopWatching = new List<Guid>();

            foreach (var customFileWatcher in _fileWatcherLookUp)
            {
                if (projectsToWatch.All(p => p.ProjectWatcherId != customFileWatcher.Key)
                    && _myApplicationId != customFileWatcher.Key)
                {
                    projectsToStopWatching.Add(customFileWatcher.Key);
                }
            }

            foreach (var projectId in projectsToStopWatching)
            {
                _fileWatcherLookUp[projectId].Dispose();
                _fileWatcherLookUp.Remove(projectId);
            }
        }
    }
}