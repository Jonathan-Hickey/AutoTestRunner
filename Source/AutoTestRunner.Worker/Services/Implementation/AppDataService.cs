using System;
using System.IO;
using AutoTestRunner.Core.Services.Interfaces;

namespace AutoTestRunner.Worker.Services.Implementation

{
    public class AppDataService : IAppDataService
    {
        private static readonly string _autoTestRunnerData = "AutoTestRunnerData";
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