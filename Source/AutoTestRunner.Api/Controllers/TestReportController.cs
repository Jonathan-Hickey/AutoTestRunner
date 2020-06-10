using AutoTestRunner.Api.Factory.Interfaces;
using AutoTestRunner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoTestRunner.Api.Models;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models.Requests;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("api/ProjectWatcher/{projectWatcherId}/TestReports")]
    public class TestReportController : ControllerBase
    {
        private readonly ILogger<TestReportController> _logger;
        private readonly ITestReportService _testReportService;
        private readonly ITestReportFactory _testReportFactory;
        private readonly IMapper<TestReport, TestReportDto> _testReportMapper;

        public TestReportController(ILogger<TestReportController> logger,
                                    ITestReportService testReportService,
                                    ITestReportFactory testReportFactory,
                                    IMapper<TestReport, TestReportDto> testReportMapper)
        {
            _testReportMapper = testReportMapper;
            _testReportFactory = testReportFactory;
            _testReportService = testReportService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddTestResultReportAsync(Guid projectWatcherId, CreateTestReportDto testReportDto)
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(AddTestResultReportAsync)}");
            var testReport = _testReportFactory.CreateTestReport(projectWatcherId, testReportDto);

            _testReportService.CreateTestReportAsync(testReport);

            return Created("", new CreateTestReportResponseDto
            {
                ReportId = testReport.TestReportId
            });
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTestResultReport(Guid projectWatcherId)
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(GetTestResultReport)}");
            var projectTestReports = _testReportService.GetTestReports(projectWatcherId);
            var testReportDtos = _testReportMapper.Map(projectTestReports);
            return Ok(testReportDtos);
        }

        [HttpGet]
        [Route("{reportId}")]
        public IActionResult GetTestResultReport(Guid projectWatcherId, Guid reportId)       
        {
            _logger.LogInformation($"{nameof(TestReportController)}_{nameof(GetTestResultReport)}");
            var testReport = _testReportService.GetTestReport(projectWatcherId, reportId);
            var testReportDto = _testReportMapper.Map(testReport);
            
            return Ok(testReportDto);
        }
    }
}
