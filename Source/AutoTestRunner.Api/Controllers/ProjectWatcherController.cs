﻿using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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
        public async Task<IActionResult> AddTestProjectToWatcher(CreateProjectWatcherDto createProjectWatcherDto)
        {
            if (createProjectWatcherDto == null || string.IsNullOrEmpty(createProjectWatcherDto.FullProjectPath))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Must supply {nameof(CreateProjectWatcherDto)} and the value {nameof(createProjectWatcherDto.FullProjectPath)}.");
            }
            
            if(!System.IO.File.Exists(createProjectWatcherDto.FullProjectPath))
            {
                return BadRequest("Unable to find local path");
            }

            var projectWatcher = await _projectWatcherService.AddProjectToWatcherAsync(createProjectWatcherDto.FullProjectPath);
            return Created("", "");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProjectWatcher()
        {
            var watchedProjects = await _projectWatcherService.GetWatchedProjectsAsync();

            var watchedProjectDtos = _projectWatcherDtoMapper.Map(watchedProjects);

            return Ok(watchedProjectDtos);
        }
    }
}
