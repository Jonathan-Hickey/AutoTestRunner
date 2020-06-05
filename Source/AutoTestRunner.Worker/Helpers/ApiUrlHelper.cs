using System;

namespace AutoTestRunner.Worker.Helpers
{
    public static class ApiUrlHelper
    {
        private static readonly string _createTestReportUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports";
        private static readonly string _callBackUrl = "https://localhost:5001/ProjectWatcher/{0}/TestReports/{1}";

        public static string GetCreateTestReportUrl(Guid projectWatcherId)
        {
            return string.Format(_createTestReportUrl, projectWatcherId);
        }

        public static string GetCallBackUrl(Guid projectWatcherId, Guid reportId)
        {
            return string.Format(_callBackUrl, projectWatcherId, reportId);
        }
    }
}