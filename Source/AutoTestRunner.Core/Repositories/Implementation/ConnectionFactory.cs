using System.Threading;
using AutoTestRunner.Core.Repositories.Interfaces;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _path;

        private readonly Semaphore _semaphore;

        public ConnectionFactory(string path)
        {
            _path = path;
            _semaphore = new Semaphore(1,1 );
        }

        public IThreadSafeLiteDatabase CreateConnection()
        {
            var myLiteDatabase = new ThreadSafeLiteDatabase(_semaphore, _path);
            myLiteDatabase.CreateDatabase();
            return myLiteDatabase;
        }
    }
}