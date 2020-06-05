using AutoTestRunner.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class FileRepository<T> : IFileRepository<T>
    {
        public readonly string _filePath;

        private readonly Semaphore _semaphore;
        private readonly IJsonService _jsonService;

        public FileRepository(string filePath, IJsonService jsonService)
        {
            _jsonService = jsonService;
            _filePath = filePath;
            _semaphore = new Semaphore(1, 1);
        }

        public async Task WriteAsync(T obj)
        {
            try
            {
                _semaphore.WaitOne();

                var fileContents = await ReadFileAsync(_filePath);

                fileContents.Add(_jsonService.Serialize(obj));

                await WriteToFileAsync(_filePath, fileContents);
            }
            finally
            {
                _semaphore.Release();
            }

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                _semaphore.WaitOne();
                var fileContents = await ReadFileAsync(_filePath);

                return fileContents.Select(f => _jsonService.Deserialize<T>(f)).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IReadOnlyList<T> GetAll()
        {
            try
            {
                _semaphore.WaitOne();
                var fileContents = ReadFile(_filePath);

                return fileContents.Select(f => _jsonService.Deserialize<T>(f)).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<List<string>> ReadFileAsync(string fileName)
        {
            var fileContents = new List<string>();

            using (var fileStream = File.OpenRead(fileName))
            {
                using (var sr = new StreamReader(fileStream))
                {
                    string line;

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        fileContents.Add(line);
                    }

                    return fileContents;
                }
            }
        }

        private List<string> ReadFile(string fileName)
        {
            var fileContents = new List<string>();

            using (var fileStream = File.OpenRead(fileName))
            {
                using (var sr = new StreamReader(fileStream))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        fileContents.Add(line);
                    }

                    return fileContents;
                }
            }
        }

        private async Task WriteToFileAsync(string filePath, List<string> filePathsToWatch)
        {
            using (var fileStream = File.OpenWrite(filePath))
            {
                using (var streamWrite = new StreamWriter(fileStream))
                {
                    foreach (var filePathToWatch in filePathsToWatch)
                    {
                        await streamWrite.WriteLineAsync(filePathToWatch);
                    }
                }
            }
        }
    }
}
