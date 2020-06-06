namespace AutoTestRunner.Worker.Services.Interfaces
{
    public interface ICommandLineService
    {
        string RunTestProject(string projectPath);
        void OpenBrowser(string url);
    }
}