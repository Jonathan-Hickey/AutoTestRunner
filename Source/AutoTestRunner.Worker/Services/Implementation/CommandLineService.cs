using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AutoTestRunner.Worker.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class CommandLineService : ICommandLineService
    {
        private static string cmdProgramName = "cmd.exe";

        public async Task<string> RunTestProjectAsync(string projectPath)
        {
            var info = CreateProcessStartInfo(projectPath);

            using (var process = Process.Start(info))
            {
                StreamReader reader = process.StandardOutput;

                return await reader.ReadToEndAsync();
            }
        }

        public string RunTestProject(string projectPath)
        {
            var info = CreateProcessStartInfo(projectPath);

            using (var process = Process.Start(info))
            {
                StreamReader reader = process.StandardOutput;

                return reader.ReadToEnd();
            }
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
