using System;
using System.IO;

namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface ITestRunnerService
    {
        void RunTests(Guid projectWatcherId, FileSystemEventArgs e);
    }
}