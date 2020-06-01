using System;
using Windows.UI.Notifications;
using AutoTestRunner.Services.Interfaces;
using AutoTestRunner.Services.Models;

namespace AutoTestRunner.Services
{
    public class WindowsNotificationService : IWindowsNotificationService
    {
        private readonly ToastNotifier _toastNotifier;

        public WindowsNotificationService()
        {
            _toastNotifier = ToastNotificationManager.CreateToastNotifier("AutoTestRunner");
        }

        public void Push(TestResult testResult)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var textNodes = template.GetElementsByTagName("text");
            textNodes.Item(0).InnerText = testResult.ToString();

            var notification = new ToastNotification(template);
            notification.ExpirationTime = DateTimeOffset.Now.AddSeconds(30);
            _toastNotifier.Show(notification);
        }
    }
}
