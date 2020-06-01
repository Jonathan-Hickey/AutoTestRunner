using AutoTestRunner.Services.Models;

namespace AutoTestRunner.Services.Interfaces
{
    public interface IWindowsNotificationService
    {
        void Push(TestResult testResult);
    }
}