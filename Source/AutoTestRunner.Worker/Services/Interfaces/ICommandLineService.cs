using System.Threading.Tasks;

namespace AutoTestRunner.Worker.Interfaces
{
    public interface ICommandLineService
    {
        Task<string> RunTestProjectAsync(string projectPath);
        string RunTestProject(string projectPath);
    }
}