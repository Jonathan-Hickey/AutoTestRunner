namespace AutoTestRunner.Worker.Models
{
    public class TestSummary
    {
        public string ProjectName { get; set; }
        public int? NumberOfPassedTests { get; set; }
        public int? NumberOfFailedTests { get; set; }
        public int? NumberOfIgnoredTests { get; set; }
        public int? TotalNumberOfTests { get; set; }
        public decimal TimeTakenInSecond { get; set; }

        public override string ToString()
        {
            return $"ProjectName: {ProjectName}, Passed : {NumberOfPassedTests}, Failed: {NumberOfFailedTests}, Ignored: {NumberOfIgnoredTests}, Time Taken In Seconds : {TimeTakenInSecond}";
        }
    }
}