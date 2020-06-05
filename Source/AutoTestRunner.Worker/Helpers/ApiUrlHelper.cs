using System;

namespace AutoTestRunner.Worker.Helpers
{
    public static class ApiUrlHelper
    {
        private static readonly string CreateTestReportUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports";
        private static readonly string CallBackUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports/{1}";

        public static string GetCreateTestReportUrl(Guid projectWatcherId)
        {
            return string.Format(CreateTestReportUrl, projectWatcherId);
        }

        public static string GetCallBackUrl(Guid projectWatcherId, Guid reportId)
        {
            return string.Format(CallBackUrl, projectWatcherId, reportId);
        }
    }
}