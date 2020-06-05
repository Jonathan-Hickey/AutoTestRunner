using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IFileRepository<ProjectWatcher> _fileRepository;
        private readonly IFileWatcherService _fileWatcherService;

        public Worker(IFileRepository<ProjectWatcher> fileRepository,
            IFileWatcherService fileWatcherService)
        {
            _fileWatcherService = fileWatcherService;
            _fileRepository = fileRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var projectsToWatch = await _fileRepository.GetAllAsync();

            foreach (var project in projectsToWatch)
            {
                _fileWatcherService.WatchTestProject(project);
            }

            _fileWatcherService.WatchForChangesInWatchedProjectFile();
        }
    }
}