using System;
using System.Diagnostics;
using System.IO;
using AutoTestRunner.Worker.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class CommandLineService : ICommandLineService
    {
        private static string cmdProgramName = "cmd.exe";

        public string RunTestProject(string projectPath)
        {
            var info = CreateProcessStartInfo(projectPath);

            using (var process = Process.Start(info))
            {
                StreamReader reader = process.StandardOutput;

                return reader.ReadToEnd();
            }
        }

        public void OpenBrowser(string url)
        { 
            var fileName = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Google\Chrome\Application\chrome.exe";
            Process.Start(fileName, url );
        }

        private static ProcessStartInfo CreateProcessStartInfo(string projectPath)
        {
            ProcessStartInfo info = new ProcessStartInfo(cmdProgramName);

            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            info.Arguments = $"/c cd {projectPath} & dotnet test";

            return info;
        }
    }
}
