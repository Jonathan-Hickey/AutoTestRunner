using System;
using System.Threading;
using AutoTestRunner.Core.Repositories.Interfaces;
using LiteDB;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class ThreadSafeLiteDatabase :  IThreadSafeLiteDatabase
    {
        private readonly string _path;
        private readonly Semaphore _semaphore;
        private LiteDatabase _liteDatabase;

        public ThreadSafeLiteDatabase(Semaphore semaphore, string path)
        {
            _path = path;
            _semaphore = semaphore;
        }
        
        public LiteDatabase CreateDatabase()
        {
            try
            {
                _semaphore.WaitOne();
                _liteDatabase = new LiteDatabase(_path);
                return _liteDatabase;
            }
            catch(Exception e)
            {
                _liteDatabase?.Dispose();
                _semaphore.Release();
                throw e;
            }
        }

        public void Dispose()
        {
            _liteDatabase?.Dispose();
            _semaphore.Release();
        }

        public ILiteCollection<T> GetCollection<T>()
        {
            return _liteDatabase.GetCollection<T>(typeof(T).Name);
        }
    }
}