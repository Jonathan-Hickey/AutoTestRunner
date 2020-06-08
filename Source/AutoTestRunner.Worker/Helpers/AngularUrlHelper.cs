using System;

namespace AutoTestRunner.Worker.Helpers
{
    public static class AngularUrlHelper
    {
        private const string CallBackUrl = "https://localhost:5001/projects/{0}/reports/{1}";

        public static string GetCallBackUrl(Guid projectWatcherId, Guid reportId)
        {
            return string.Format(CallBackUrl, projectWatcherId, reportId);
        }
    }
}