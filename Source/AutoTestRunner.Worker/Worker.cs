using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Services.Implementation;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker
{
    public class Worker : BackgroundService
    {

        private readonly MemoryCache _memoryCache;
        private readonly IDictionary<Guid, CustomFileWatcher> _fileWatcherLookUp;
        private readonly IFileRepository<ProjectWatcher> _fileRepository;
        private readonly IAppDataService _appDataService;


        private static readonly string _filter = "*.dll";
        private readonly ITestRunnerService _testRunnerService;

        public Worker(IFileRepository<ProjectWatcher> fileRepository,
                      IAppDataService appDataService,
                      ITestRunnerService testRunnerService)
        {
            _testRunnerService = testRunnerService;
            _appDataService = appDataService;
            _fileRepository = fileRepository;

            _memoryCache = MemoryCache.Default;
            _fileWatcherLookUp = new Dictionary<Guid, CustomFileWatcher>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var projectsToWatch = await _fileRepository.GetAllAsync();

            foreach (var project in projectsToWatch)
            {
                WatchProject(project);
            }

            var myApplicationId = Guid.NewGuid();

            var projectWatcherFileWatcher = new CustomFileWatcher(_memoryCache, myApplicationId, _appDataService.GetAutoTestRunnerDataFolderPath(), _appDataService.GetProjectWatcherFileName());
            projectWatcherFileWatcher.OnChange = WatchNewProject;
            _fileWatcherLookUp.Add(myApplicationId, projectWatcherFileWatcher);
        }

        private void WatchProject(ProjectWatcher project)
        {
            var fileWatcher = new CustomFileWatcher(_memoryCache, project.ProjectWatcherId, project.FullProjectPath, _filter);
            fileWatcher.OnChange = _testRunnerService.RunTests;
            _fileWatcherLookUp.Add(project.ProjectWatcherId, fileWatcher);
        }

        private void WatchNewProject(Guid myApplicationId, FileSystemEventArgs e)
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
                    && myApplicationId != customFileWatcher.Key)
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