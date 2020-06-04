using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoTestRunner.Core.Repositories.Interfaces;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class FileRepository<T> : IFileRepository<T>
    {
        public readonly string _filePath;
        public FileRepository(string filePath)
        {
            _filePath = filePath;
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

            return fileContents.Select( f => JsonSerializer.Deserialize<T>(f)).ToList();
        }

        public IReadOnlyList<T> GetAll()
        {
            var fileContents = ReadFile(_filePath);

            return fileContents.Select(f => JsonSerializer.Deserialize<T>(f)).ToList();
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
