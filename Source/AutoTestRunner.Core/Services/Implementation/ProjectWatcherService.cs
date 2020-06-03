using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;

namespace AutoTestRunner.Core.Services.Implementation
{
    public class ProjectWatcherService : IProjectWatcherService
    {
        private readonly IFileRepository _fileRepository;

        public ProjectWatcherService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Task<ProjectWatcher> AddProjectToWatcherAsync(string path)
        {
            return _fileRepository.AddProjectWatcherAsync(path);
        }

        public Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync()
        {
            return _fileRepository.GetProjectWatchersAsync();
        }
    }
}