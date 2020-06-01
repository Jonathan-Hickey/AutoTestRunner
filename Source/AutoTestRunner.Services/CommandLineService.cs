using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AutoTestRunner.Services
{
    public interface ICommandLineService
    {
        Task<string> RunTestProjectAsync(string projectPath);
    }

    public class CommandLineService : ICommandLineService
    {
        private static string cmdProgramName = "cmd.exe";

        public Task<string> RunTestProjectAsync(string projectPath)
        {
            ProcessStartInfo info = new ProcessStartInfo(cmdProgramName);

            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            info.Arguments = $"/c cd {projectPath} & dotnet test"; ;

            using (var process = Process.Start(info))
            {
                StreamReader reader = process.StandardOutput;

                return reader.ReadToEndAsync();
            }
        }
    }
}
