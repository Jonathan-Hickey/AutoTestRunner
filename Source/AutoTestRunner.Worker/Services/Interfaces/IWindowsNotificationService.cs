using System;
using AutoTestRunner.Core.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IWindowsNotificationService
    {
        void Push(Guid projectWatcherId, Guid reportId, TestSummary testSummary);
    }
}