using System;
using System.Collections.Generic;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IProjectWatcherRepository
    {
        ProjectWatcher GetProjectWatcher(Guid projectWatcherId);
        
        void AddProjectWatcher(ProjectWatcher projectWatcher);

        IReadOnlyList<ProjectWatcher> GetProjectWatchers();
        
        ProjectWatcher GetProjectWatcher(string fullPathHash);


        void DeleteProjectWatcher(Guid projectWatcherId);
    }
}