using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IFileRepository<T>
    {
        Task WriteAsync(T obj);
        
        IAsyncEnumerable<T> GetAllAsync(CancellationToken cancellationToken);
        IAsyncEnumerable<T> GetAllAsync() => GetAllAsync(CancellationToken.None);
        IReadOnlyList<T> GetAll();
    }
}