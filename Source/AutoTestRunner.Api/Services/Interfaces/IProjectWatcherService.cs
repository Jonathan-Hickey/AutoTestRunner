using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Api.Services.Interfaces
{
    public interface IProjectWatcherService
    {
        Task<ProjectWatcher> AddProjectToWatcherAsync(string fullPath);
        Task<IReadOnlyList<ProjectWatcher>> GetWatchedProjectsAsync();
    }
}