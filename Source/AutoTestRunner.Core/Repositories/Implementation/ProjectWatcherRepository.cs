using System;
using System.Collections.Generic;
using System.Linq;
using AutoTestRunner.Core.Models;
using AutoTestRunner.Core.Repositories.Interfaces;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class ProjectWatcherRepository : IProjectWatcherRepository
    {
        private readonly IConnectionFactory _connectionFactory;


        public ProjectWatcherRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public ProjectWatcher GetProjectWatcher(Guid projectWatcherId)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                return db.GetCollection<ProjectWatcher>()
                         .Find(p => p.ProjectWatcherId == projectWatcherId)
                         .Single();
            }
        }

        public void AddProjectWatcher(ProjectWatcher projectWatcher)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                var projectWatchers = db.GetCollection<ProjectWatcher>();

                projectWatchers.EnsureIndex(p => p.ProjectWatcherId);

                projectWatchers.Insert(projectWatcher);
            }
        }
        
        public IReadOnlyList<ProjectWatcher> GetProjectWatchers()
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                return db.GetCollection<ProjectWatcher>().FindAll().ToList();
            }
        }

        public ProjectWatcher GetProjectWatcher(string fullPathHash)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                return db.GetCollection<ProjectWatcher>().Find(p => p.FullProjectPathHash == fullPathHash)
                    .SingleOrDefault();
            }
        }
    }
}