﻿using System;

namespace AutoTestRunner.Core.Models
{
    public class ProjectWatcher
    {
        public Guid ProjectWatcherId { get; set; }
        public string FullProjectPath { get; set; }
        public string ProjectWatchPath { get; set; }
        public string FileToWatch { get; set; }
    }
}