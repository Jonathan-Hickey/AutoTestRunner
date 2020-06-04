using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IFileRepository<T>
    {
        Task WriteAsync(T obj);
        Task<IReadOnlyList<T>> GetAllAsync();
        IReadOnlyList<T> GetAll();
    }
}