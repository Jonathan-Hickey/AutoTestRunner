using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class FileWatcherService : IFileWatcherService
    {
        private readonly IDictionary<Guid, CustomFileWatcher> _fileWatcherLookUp;
        private readonly IFileRepository<ProjectWatcher> _fileRepository;
        private readonly ITestRunnerService _testRunnerService;
        private readonly IAppDataService _appDataService;

        private static readonly string _filter = "*.dll";
        private readonly MemoryCache _memoryCache;
        

        public FileWatcherService(IFileRepository<ProjectWatcher> fileRepository, ITestRunnerService testRunnerService, IAppDataService appDataService)
        {
            _appDataService = appDataService;
            _testRunnerService = testRunnerService;
            _fileRepository = fileRepository;
            _fileWatcherLookUp = new Dictionary<Guid, CustomFileWatcher>();

            _memoryCache = MemoryCache.Default;
        }

        public void WatchTestProject(ProjectWatcher project)
        {
            var fileWatcher = new CustomFileWatcher(_memoryCache, project.ProjectWatcherId, project.FullProjectPath, _filter);
            fileWatcher.OnChange = _testRunnerService.RunTests;
            _fileWatcherLookUp.Add(project.ProjectWatcherId, fileWatcher);
        }

        public void WatchForChangesInWatchedProjectFile()
        {
            var myApplicationId = Guid.NewGuid();

            var projectWatcherFileWatcher = new CustomFileWatcher(_memoryCache, myApplicationId, _appDataService.GetAutoTestRunnerDataFolderPath(), _appDataService.GetProjectWatcherFileName());
            projectWatcherFileWatcher.OnChange = OnWatchProjectFileChanged;
            _fileWatcherLookUp.Add(myApplicationId, projectWatcherFileWatcher);
        }

        private void OnWatchProjectFileChanged(Guid myApplicationId, FileSystemEventArgs e)
        {
            var projectsToWatch = _fileRepository.GetAll();

            foreach (var project in projectsToWatch)
            {
                if (!_fileWatcherLookUp.ContainsKey(project.ProjectWatcherId))
                {
                    WatchTestProject(project);
                }
            }

            var projectsToStopWatching = GetProjectsToStopWatching(myApplicationId, projectsToWatch);

            foreach (var projectId in projectsToStopWatching)
            {
                RemoveProjectFromWatcher(projectId);
            }
        }

        private void RemoveProjectFromWatcher(Guid projectId)
        {
            _fileWatcherLookUp[projectId].Dispose();
            _fileWatcherLookUp.Remove(projectId);
        }

        private List<Guid> GetProjectsToStopWatching(Guid myApplicationId, IReadOnlyList<ProjectWatcher> projectsToWatch)
        {
            var projectsToStopWatching = new List<Guid>();
     
            foreach (var customFileWatcher in _fileWatcherLookUp)
            {
                if (projectsToWatch.All(p => p.ProjectWatcherId != customFileWatcher.Key)
                    && myApplicationId != customFileWatcher.Key)
                {
                    projectsToStopWatching.Add(customFileWatcher.Key);
                }
            }

            return projectsToStopWatching;
        }
    }
}