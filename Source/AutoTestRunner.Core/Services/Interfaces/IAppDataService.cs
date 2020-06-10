namespace AutoTestRunner.Core.Services.Interfaces
{
    public interface IAppDataService
    {
        string GetProjectWatcherFileName();
        string GetLiteDatabaseConnectionString();
        string GetAutoTestRunnerDataFolderPath();

    }
}