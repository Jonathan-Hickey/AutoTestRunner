using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<ProjectWatcher> AddProjectWatcherAsync(string fullPath);
        Task<IReadOnlyList<ProjectWatcher>> GetProjectWatchersAsync();
        IReadOnlyList<ProjectWatcher> GetProjectWatchers();
    }
}