using AutoTestRunner.Worker.Models;
using AutoTestRunner.Worker.Services.Interfaces;
using System;
using Windows.UI.Notifications;
using AutoTestRunner.Core.Services.Interfaces;
using AutoTestRunner.Worker.Helpers;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class WindowsNotificationService : IWindowsNotificationService
    {
        private readonly ToastNotifier _toastNotifier;
        private readonly ICommandLineService _commandLineService;
        
        private static readonly string Launch = "launch";
        private readonly IJsonService _jsonService;

        public WindowsNotificationService(ICommandLineService commandLineService, IJsonService jsonService)
        {
            _jsonService = jsonService;
            _commandLineService = commandLineService;
            _toastNotifier = ToastNotificationManager.CreateToastNotifier("AutoTestRunner");
        }

        public void Push(Guid projectWatcherId, Guid reportId, TestSummary testSummary)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            
            var launchAttribute = template.CreateAttribute(Launch);

            var data = _jsonService.Serialize(new CallbackData
            {
                ProjectWatcherId = projectWatcherId,
                ReportId = reportId
            });

            launchAttribute.Value = data;
            template.DocumentElement.SetAttributeNode(launchAttribute);

            var textNodes = template.GetElementsByTagName("text");
            textNodes.Item(0).InnerText = testSummary.ToString();

            
            var notification = new ToastNotification(template);
            
            notification.ExpirationTime = DateTimeOffset.Now.AddSeconds(30);
            notification.Activated += NotificationActivated;
            
            _toastNotifier.Show(notification);
        }

        private void NotificationActivated(ToastNotification notification, object args)
        {
            var attribute = notification.Content.DocumentElement.Attributes.GetNamedItem(Launch);
            
            var callbackData = _jsonService.Deserialize<CallbackData>(attribute.InnerText);

            var url = AngularUrlHelper.GetCallBackUrl(callbackData.ProjectWatcherId, callbackData.ReportId);

            _commandLineService.OpenBrowser(url);
        }

        private class CallbackData
        {
            public Guid ProjectWatcherId { get; set; }
            public Guid ReportId { get; set; }
        }
    }
}
