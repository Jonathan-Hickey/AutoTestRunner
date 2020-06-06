using AutoTestRunner.Worker.Models;
using AutoTestRunner.Worker.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Text.Json;
using Windows.UI.Notifications;
using AutoTestRunner.Worker.Helpers;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class WindowsNotificationService : IWindowsNotificationService
    {
        private readonly ToastNotifier _toastNotifier;
        private readonly ICommandLineService _commandLineService;
        
        private static readonly string Launch = "launch";
        
        public WindowsNotificationService(ICommandLineService commandLineService)
        {
            _commandLineService = commandLineService;
            _toastNotifier = ToastNotificationManager.CreateToastNotifier("AutoTestRunner");
        }

        public void Push(Guid projectWatcherId, Guid reportId, TestResult testResult)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            
            var launchAttribute = template.CreateAttribute(Launch);

            var data = JsonSerializer.Serialize(new CallbackData
            {
                ProjectWatcherId = projectWatcherId,
                ReportId = reportId
            });

            launchAttribute.Value = data;
            template.DocumentElement.SetAttributeNode(launchAttribute);

            var textNodes = template.GetElementsByTagName("text");
            textNodes.Item(0).InnerText = testResult.ToString();

            
            var notification = new ToastNotification(template);
            
            notification.ExpirationTime = DateTimeOffset.Now.AddSeconds(30);
            notification.Activated += NotificationActivated;
            
            _toastNotifier.Show(notification);
        }

        private void NotificationActivated(ToastNotification notification, object args)
        {
            var attribute = notification.Content.DocumentElement.Attributes.GetNamedItem(Launch);
            
            var callbackData = JsonSerializer.Deserialize<CallbackData>(attribute.InnerText);

            var url = ApiUrlHelper.GetCallBackUrl(callbackData.ProjectWatcherId, callbackData.ReportId);

            _commandLineService.OpenBrowser(url);
        }

        private class CallbackData
        {
            public Guid ProjectWatcherId { get; set; }
            public Guid ReportId { get; set; }
        }
    }
}
