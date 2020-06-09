using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class ProjectWatcherDtoMapper : IMapper<ProjectWatcher, ProjectWatcherDto>
    {
        public ProjectWatcherDto Map(ProjectWatcher projectWatcher)
        {
            return new ProjectWatcherDto
            {
                ProjectWatcherId = projectWatcher.ProjectWatcherId,
                FullProjectPath = projectWatcher.FullProjectPathHash,
                FileToWatch = projectWatcher.FileToWatch,
                ProjectWatchPath = projectWatcher.ProjectWatchPath
            };
        }
    }
}
