using System;
using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IWindowsNotificationService
    {
        void Push(Guid projectWatcherId, Guid reportId, TestResult testResult);
    }
}