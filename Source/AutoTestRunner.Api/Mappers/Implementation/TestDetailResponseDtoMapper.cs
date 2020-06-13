using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Response;

namespace AutoTestRunner.Api.Mappers.Implementation
{
    public class TestDetailResponseDtoMapper : IMapper<TestDetail, TestDetailResponseDto>
    {
        public TestDetailResponseDto Map(TestDetail testDetail)
        {
            return new TestDetailResponseDto
            {
                TestName = testDetail.TestName,
                TimeTakenInMilliseconds = testDetail.TimeTakenInMs,
                TestStatus = testDetail.TestStatus
            };
        }
    }
}