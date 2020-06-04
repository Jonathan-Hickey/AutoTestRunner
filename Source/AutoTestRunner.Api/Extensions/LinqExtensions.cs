using System.Collections.Generic;
using System.Threading.Tasks;

//This is a kewl trick so anytime we use linq we will get a suggestion to use these methods 
namespace System.Linq
{
    public static class LinqExtensions
    {
        public static async Task<IEnumerable<T>> WhereAsync<T>(this Task<IEnumerable<T>> enumerable, Func<T, bool> predict)
        {
            return (await enumerable).Where(predict);
        }

        public static async Task<T> SingleAsync<T>(this Task<IEnumerable<T>> enumerable, Func<T, bool> predict)
        {
            return (await enumerable).Single(predict);
        }
    }
}