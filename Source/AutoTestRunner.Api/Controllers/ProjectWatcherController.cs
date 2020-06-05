using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("ProjectWatcher")]
    public class ProjectWatcherController : ControllerBase
    {
        private readonly ILogger<ProjectWatcherController> _logger;
        private readonly IProjectWatcherService _projectWatcherService;
        private readonly string _dataStorePath;
        public ProjectWatcherController(ILogger<ProjectWatcherController> logger, IProjectWatcherService projectWatcherService)
        {
            _projectWatcherService = projectWatcherService;
            _logger = logger;

            _dataStorePath = Path.Combine(Directory.GetCurrentDirectory(), "AppData");
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddTesProjectToWatcher(CreateProjectWatcherDto createProjectWatcherDto)
        {

            _logger.LogInformation(_dataStorePath);

            var projectWatcher = await _projectWatcherService.AddProjectToWatcherAsync(createProjectWatcherDto.FullProjectPath);
            return Created("", "");
        }

        [HttpGet]
        [Route("")]

        public async Task<IActionResult> GetProjectWatcher()
        {

            _logger.LogWarning(_dataStorePath);


            var watchedProjects = await _projectWatcherService.GetWatchedProjectsAsync();
            return Ok(watchedProjects);
        }
    }
}
