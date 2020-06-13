using System;
using AutoTestRunner.Core.Models;
using System.Collections.Generic;

namespace AutoTestRunner.Api.Services.Interfaces
{
    public interface IProjectWatcherService
    {
        ProjectWatcher AddProjectToWatcher(string fullPath); 
        IReadOnlyList<ProjectWatcher> GetWatchedProjects();
        ProjectWatcher GetWatchedProject(Guid projectWatcherId);
        void DeleteWatchedProject(Guid projectWatcherId);
    }
}