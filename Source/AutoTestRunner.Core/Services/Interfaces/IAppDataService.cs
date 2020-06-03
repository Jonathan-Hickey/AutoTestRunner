namespace AutoTestRunner.Core.Services.Interfaces
{
    public interface IAppDataService
    {
        string GetProjectWatcherFileName();
        string GetProjectWatcherFilePath();
        string GetAutoTestRunnerDataFolderPath();

    }
}