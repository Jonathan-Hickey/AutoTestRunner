using AutoTestRunner.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class FileRepository<T> : IFileRepository<T>
    {
        public readonly string _filePath;

        private readonly Semaphore _semaphore;

        public FileRepository(string filePath)
        {
            _filePath = filePath;
            _semaphore = new Semaphore(1, 1);
        }

        public async Task WriteAsync(T obj)
        {

            var fileContents = await ReadFileAsync(_filePath);

            fileContents.Add(JsonSerializer.Serialize(obj));

            await WriteToFileAsync(_filePath, fileContents);

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var fileContents = await ReadFileAsync(_filePath);

            return fileContents.Select(f => JsonSerializer.Deserialize<T>(f)).ToList();
        }

        public IReadOnlyList<T> GetAll()
        {
            var fileContents = ReadFile(_filePath);

            return fileContents.Select(f => JsonSerializer.Deserialize<T>(f)).ToList();
        }

        private async Task<List<string>> ReadFileAsync(string fileName)
        {
            try
            {
                _semaphore.WaitOne();

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
            finally
            {
                _semaphore.Release();
            }

        }

        private List<string> ReadFile(string fileName)
        {
            try
            {
                _semaphore.WaitOne();

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
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task WriteToFileAsync(string filePath, List<string> filePathsToWatch)
        {
            try
            {
                _semaphore.WaitOne();

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
            finally
            {
                _semaphore.Release();
            }

        }
    }
}
