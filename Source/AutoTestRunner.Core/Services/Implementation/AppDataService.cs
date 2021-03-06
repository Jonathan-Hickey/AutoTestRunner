﻿using AutoTestRunner.Core.Services.Interfaces;
using System;
using System.IO;

namespace AutoTestRunner.Core.Services.Implementation
{
    public class AppDataService : IAppDataService
    {
        private static readonly string _autoTestRunnerData = "AutoTestRunnerData";
        private static readonly string _autoWatcherDb = "AutoWatcher.db";

        public string GetAutoWatcherDb()
        {
            return _autoWatcherDb;
        }

        public string GetLiteDatabaseConnectionString()
        {
            return Path.Combine(GetAutoTestRunnerDataFolderPath(), _autoWatcherDb);
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