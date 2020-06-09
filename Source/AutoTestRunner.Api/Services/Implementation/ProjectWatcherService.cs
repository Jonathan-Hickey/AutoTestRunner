using AutoTestRunner.Api.Services.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class ProjectWatcherService : IProjectWatcherService
    {
        private readonly IFileRepository<ProjectWatcher> _fileRepository;
        private readonly IHashService _service;

        public ProjectWatcherService(IFileRepository<ProjectWatcher> fileRepository, IHashService service)
        {
            _service = service;
            _fileRepository = fileRepository;
        }

        public async Task<ProjectWatcher> AddProjectToWatcherAsync(string fullPath)
        {
            var fullProjectPathHash = _service.GetHash(fullPath);

            var projectWatcher = await GetWatchedProjectAsync(fullProjectPathHash);

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

            await _fileRepository.WriteAsync(newestProjectWatcher);

            return newestProjectWatcher;
        }

        public async Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync()
        {
            return await _fileRepository.GetAllAsync().ToListAsync().AsTask();
        }

        public Task<ProjectWatcher> GetWatchedProjectAsync(Guid projectWatcherId)
        {
            return _fileRepository.GetAllAsync().SingleAsync(p => p.ProjectWatcherId == projectWatcherId).AsTask();
        }

        private Task<ProjectWatcher> GetWatchedProjectAsync(string fullProjectPathHash)
        {
            return _fileRepository.GetAllAsync().SingleOrDefaultAsync(p => p.FullProjectPathHash == fullProjectPathHash).AsTask();
        }
    }
}