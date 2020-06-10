using System;
using System.Linq;
using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Core.Models.Response;
using Microsoft.AspNetCore.Http;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("api/ProjectWatcher")]
    public class ProjectWatcherController : ControllerBase
    {
        private readonly ILogger<ProjectWatcherController> _logger;
        private readonly IProjectWatcherService _projectWatcherService;
        private readonly IMapper<ProjectWatcher, ProjectWatcherDto> _projectWatcherDtoMapper;

        public ProjectWatcherController(ILogger<ProjectWatcherController> logger,
                                        IProjectWatcherService projectWatcherService,
                                        IMapper<ProjectWatcher, ProjectWatcherDto> projectWatcherDtoMapper)
        {
            _projectWatcherDtoMapper = projectWatcherDtoMapper;
            _projectWatcherService = projectWatcherService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddTestProjectToWatcher(CreateProjectWatcherDto createProjectWatcherDto)
        {
            if (createProjectWatcherDto == null || string.IsNullOrEmpty(createProjectWatcherDto.FullProjectPath))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Must supply {nameof(CreateProjectWatcherDto)} and the value {nameof(createProjectWatcherDto.FullProjectPath)}.");
            }
            
            if(!System.IO.File.Exists(createProjectWatcherDto.FullProjectPath))
            {
                return BadRequest("Unable to find local path");
            }

            var projectWatcher = _projectWatcherService.AddProjectToWatcher(createProjectWatcherDto.FullProjectPath);
            
            return CreatedAtRoute("GetProjectWatcher", new { projectWatcherId = projectWatcher.ProjectWatcherId }, new { project_watcher_id = projectWatcher.ProjectWatcherId });
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProjectWatchers()
        {
            var watchedProjects = _projectWatcherService.GetWatchedProjects();

            var watchedProjectDtos = _projectWatcherDtoMapper.Map(watchedProjects).OrderBy(p => p.ProjectWatchPath);

            return Ok(watchedProjectDtos);
        }

        [HttpGet]
        [Route("{projectWatcherId}", Name = "GetProjectWatcher")]
        public IActionResult GetProjectWatcher(Guid projectWatcherId)
        {
            var watchedProject =_projectWatcherService.GetWatchedProject(projectWatcherId);

            var watchedProjectDto = _projectWatcherDtoMapper.Map(watchedProject);

            return Ok(watchedProjectDto);
        }
    }
}
