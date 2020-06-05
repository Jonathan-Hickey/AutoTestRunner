using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IFileWatcherService
    {
        void WatchTestProject(ProjectWatcher project);
        void WatchForChangesInWatchedProjectFile();
    }
}