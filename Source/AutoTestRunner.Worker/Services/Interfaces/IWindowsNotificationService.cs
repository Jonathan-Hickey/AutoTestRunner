using AutoTestRunner.Worker.Models;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface IWindowsNotificationService
    {
        void Push(TestResult testResult);
    }
}