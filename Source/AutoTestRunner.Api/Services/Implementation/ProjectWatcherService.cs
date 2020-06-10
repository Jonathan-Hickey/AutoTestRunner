using AutoTestRunner.Api.Services.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class ProjectWatcherService : IProjectWatcherService
    {
        private readonly IHashService _service;
        private readonly IProjectWatcherRepository _projectWatcherRepository;

        public ProjectWatcherService(IHashService service, IProjectWatcherRepository projectWatcherRepository)
        {
            _projectWatcherRepository = projectWatcherRepository;
            _service = service;
        }

        public ProjectWatcher AddProjectToWatcher(string fullPath)
        {
            var fullProjectPathHash = _service.GetHash(fullPath);

            var projectWatcher = GetWatchedProject(fullProjectPathHash);

            if (projectWatcher != null)
            {
                return projectWatcher;
            }

            var indexOfLastBackSlash = fullPath.LastIndexOf('\\');
            var projectWatchPath = fullPath.Substring(0, indexOfLastBackSlash);
            var fileToWatch = fullPath.Substring(indexOfLastBackSlash + 1);
            
            var newestProjectWatcher = new ProjectWatcher
            {
                ProjectWatcherId = Guid.NewGuid(),
                FullProjectPathHash = fullProjectPathHash,
                ProjectWatchPath = projectWatchPath,
                FileToWatch =  fileToWatch
            };

            _projectWatcherRepository.AddProjectWatcher(newestProjectWatcher);

            return newestProjectWatcher;
        }

        public IReadOnlyList<ProjectWatcher> GetWatchedProjects()
        {
            return _projectWatcherRepository.GetProjectWatchers();
        }

        public ProjectWatcher GetWatchedProject(Guid projectWatcherId)
        {
            return _projectWatcherRepository.GetProjectWatcher(projectWatcherId);
        }

        private ProjectWatcher GetWatchedProject(string fullProjectPathHash)
        { 
            return _projectWatcherRepository.GetProjectWatcher(fullProjectPathHash);
        }
    }
}