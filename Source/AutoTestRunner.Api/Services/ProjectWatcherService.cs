using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Api.Models;

namespace AutoTestRunner.Api.Services
{
    public class ProjectWatcherService : IProjectWatcherService
    {
        public ProjectWatcherService()
        {
        }

        public Task<ProjectWatcher> AddProjectToWatcherAsync(string path)
        {
            var projectWatcher = new ProjectWatcher
            {
                FullProjectPath = path
            };

            return Task.FromResult(projectWatcher);
        }

        public Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync()
        {
            return Task.FromResult((IReadOnlyList<ProjectWatcher>)new List<ProjectWatcher>());
        }
    }
}