using AutoTestRunner.Core.Mappers.Interfaces;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Models.Requests;

namespace AutoTestRunner.Worker.Mappers.Implementation
{
    public class TestDetailRequestDtoMapper : IMapper<TestDetail, TestDetailRequestDto>
    {
        public TestDetailRequestDto Map(TestDetail testDetail)
        {
            return new TestDetailRequestDto
            {
                TestName = testDetail.TestName,
                TestStatus = testDetail.TestStatus,
                TimeTakenInMilliseconds = testDetail.TimeTakenInMs
            };
        }
    }
}