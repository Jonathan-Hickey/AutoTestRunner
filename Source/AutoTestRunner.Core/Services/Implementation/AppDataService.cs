using AutoTestRunner.Core.Services.Interfaces;
using System;
using System.IO;

namespace AutoTestRunner.Core.Services.Implementation
{
    public class AppDataService : IAppDataService
    {
        private static readonly string _autoTestRunnerData = "AutoTestRunnerData";

        private static readonly string _projectWatcherFileName = "ProjectWatcher.json";
        private static readonly string _testResultsFileName = "TestResults.json";

        public string GetProjectWatcherFileName()
        {
            return _projectWatcherFileName;
        }

        public string GetProjectWatcherFilePath()
        {
            var projectWatcherFilePath = Path.Combine(GetAutoTestRunnerDataFolderPath(), _projectWatcherFileName);

            if (!File.Exists(projectWatcherFilePath))
            {
                var fileStream = File.Create(projectWatcherFilePath);
                fileStream.Close();
            }

            return projectWatcherFilePath;
        }

        public string GetTestReportFilePath()
        {
            var testResultsFilePath = Path.Combine(GetAutoTestRunnerDataFolderPath(), _testResultsFileName);

            if (!File.Exists(testResultsFilePath))
            {
                var fileStream = File.Create(testResultsFilePath);
                fileStream.Close();
            }

            return testResultsFilePath;
        }


        public string GetAutoTestRunnerDataFolderPath()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(folder, _autoTestRunnerData);

            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(specificFolder);
            return specificFolder;
        }
    }
}