using System;
using LiteDB;

namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IThreadSafeLiteDatabase : IDisposable
    {
        ILiteCollection<T> GetCollection<T>();
    }
}