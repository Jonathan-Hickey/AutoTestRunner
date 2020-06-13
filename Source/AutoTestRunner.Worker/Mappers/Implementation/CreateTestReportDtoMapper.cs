using System.Collections.Generic;
using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Worker.Mappers.Implementation
{
    public class CreateTestReportDtoMapper : IMapper<TestSummary, IReadOnlyList<TestDetail>, CreateTestReportDto>
    {
        private readonly IMapper<TestDetail, TestDetailRequestDto> _testDetailRequestDtoMapper;

        public CreateTestReportDtoMapper(IMapper<TestDetail, TestDetailRequestDto> testDetailRequestDtoMapper)
        {
            _testDetailRequestDtoMapper = testDetailRequestDtoMapper;
        }

        public CreateTestReportDto Map(TestSummary testSummary, IReadOnlyList<TestDetail> testDetails)
        {
            return new CreateTestReportDto
            {
                TotalNumberOfTests = testSummary.TotalNumberOfTests,
                TimeTakenInSecond = testSummary.TimeTakenInSecond,
                ProjectName = testSummary.ProjectName,
                NumberOfIgnoredTests = testSummary.NumberOfIgnoredTests,
                NumberOfPassedTests = testSummary.NumberOfPassedTests,
                NumberOfFailedTests = testSummary.NumberOfFailedTests,
                TestDetails = _testDetailRequestDtoMapper.Map(testDetails)
            };
        }
    }
}