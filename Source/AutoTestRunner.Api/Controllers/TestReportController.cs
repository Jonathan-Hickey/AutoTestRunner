using AutoTestRunner.Api.Dtos.RequestModels;
using AutoTestRunner.Api.Factory.Interfaces;
using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("ProjectWatcher/{projectWatcherId}/TestReports")]
    public class TestReportController : ControllerBase
    {
        private readonly ILogger<TestReportController> _logger;
        private readonly ITestReportService _testReportService;
        private readonly ITestReportFactory _testReportFactory;

        public TestReportController(ILogger<TestReportController> logger,
                                    ITestReportService testReportService,
                                    ITestReportFactory testReportFactory)
        {
            _testReportFactory = testReportFactory;
            _testReportService = testReportService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddTestResultReportAsync(Guid projectWatcherId, CreateTestReportDto testReportDto)
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(AddTestResultReportAsync)}");
            var testReport = _testReportFactory.CreateTestReport(projectWatcherId, testReportDto);

            await _testReportService.CreateTestReportAsync(testReport);

            return Created("", "");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTestResultReport(Guid projectWatcherId)
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(GetTestResultReport)}");
            var projectTestReports = await _testReportService.GetTestReportsAsync(projectWatcherId);

            return Ok(projectTestReports);
        }

        [HttpGet]
        [Route("{reportId}")]
        public async Task<IActionResult> GetTestResultReport(Guid projectWatcherId, Guid reportId)
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(GetTestResultReport)}");
            var testReport = await _testReportService.GetTestReportAsync(projectWatcherId, reportId);
            return Ok(testReport);
        }
    }
}
