using AutoTestRunner.Core.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IFileWatcherService _fileWatcherService;
        private readonly IProjectWatcherRepository _projectWatcherRepository;

        public Worker(IProjectWatcherRepository projectWatcherRepository, 
            IFileWatcherService fileWatcherService)
        {
            _projectWatcherRepository = projectWatcherRepository;
            _fileWatcherService = fileWatcherService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach(var project in _projectWatcherRepository.GetProjectWatchers())
            {
                _fileWatcherService.WatchTestProject(project);
            }

            _fileWatcherService.WatchForChangesInWatchedProjectFile();

            return Task.CompletedTask;
        }
    }
}