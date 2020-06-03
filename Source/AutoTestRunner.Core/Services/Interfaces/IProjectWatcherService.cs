using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Core.Services.Interfaces
{
    public interface IProjectWatcherService
    {
        Task<ProjectWatcher> AddProjectToWatcherAsync(string path);

        Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync();
    }
}