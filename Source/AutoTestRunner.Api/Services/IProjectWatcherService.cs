using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Api.Models;

namespace AutoTestRunner.Api.Services
{
    public interface IProjectWatcherService
    {
        Task<ProjectWatcher> AddProjectToWatcherAsync(string path);

        Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync();
    }
}