using System.Threading.Tasks;
using AutoTestRunner.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class ProjectWatcherController : ControllerBase
    {
        private readonly ILogger<ProjectWatcherController> _logger;
        private readonly IProjectWatcherService _projectWatcherService;

        public ProjectWatcherController(ILogger<ProjectWatcherController> logger, IProjectWatcherService projectWatcherService )
        {
            _projectWatcherService = projectWatcherService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddTesProjectToWatcher(CreateProjectWatcherDto createProjectWatcherDto)
        {
            var projectWatcher = await _projectWatcherService.AddProjectToWatcherAsync(createProjectWatcherDto.FullProjectPath);
            return Created("", "");
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectWatcher()
        {
            var watchedProjects = await _projectWatcherService.GetWatchedProjectsAsync();
            return Ok(watchedProjects);
        }
    }
}
