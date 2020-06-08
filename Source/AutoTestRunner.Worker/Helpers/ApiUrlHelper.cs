using System;

namespace AutoTestRunner.Worker.Helpers
{
    public static class ApiUrlHelper
    {
        private const string CreateTestReportUrl = "https://localhost:5001/api/ProjectWatcher/{0}/TestReports";
        

        public static string GetCreateTestReportUrl(Guid projectWatcherId)
        {
            return string.Format(CreateTestReportUrl, projectWatcherId);
        }
    }
}