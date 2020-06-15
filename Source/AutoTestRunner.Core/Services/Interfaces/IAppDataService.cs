namespace AutoTestRunner.Core.Services.Interfaces
{
    public interface IAppDataService
    {
        string GetAutoWatcherDb();
        string GetLiteDatabaseConnectionString();
        string GetAutoTestRunnerDataFolderPath();
    }
}