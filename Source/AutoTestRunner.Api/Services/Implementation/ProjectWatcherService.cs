using AutoTestRunner.Api.Services.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class ProjectWatcherService : IProjectWatcherService
    {
        private readonly IFileRepository<ProjectWatcher> _fileRepository;

        public ProjectWatcherService(IFileRepository<ProjectWatcher> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<ProjectWatcher> AddProjectToWatcherAsync(string fullPath)
        {
            var newestProjectWatcher = new ProjectWatcher
            {
                ProjectWatcherId = Guid.NewGuid(),
                FullProjectPath = fullPath
            };

            await _fileRepository.WriteAsync(newestProjectWatcher);

            return newestProjectWatcher;
        }

        public Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync()
        {
            return _fileRepository.GetAllAsync();
        }
    }
}