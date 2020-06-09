﻿using AutoTestRunner.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoTestRunner.Core.Services.Interfaces;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class FileRepository<T> : IFileRepository<T>
    {
        private readonly Semaphore _semaphore;
        private readonly IJsonService _jsonService;
        private readonly IFileHelper _fileHelper;

        public FileRepository(IJsonService jsonService, IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _jsonService = jsonService;
            _semaphore = new Semaphore(1, 1);
        }

        public async Task WriteAsync(T obj)
        {
            try
            {
                _semaphore.WaitOne();

                List<string> fileContents = new List<string>(); 

                await foreach(var content in _fileHelper.ReadFileAsync())
                {
                    fileContents.Add(content);
                }
                
                fileContents.Add(_jsonService.Serialize(obj));

                await _fileHelper.WriteToFileAsync(fileContents);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async IAsyncEnumerable<T> GetAllAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            try
            {
                _semaphore.WaitOne();

                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }
                    
                await foreach (var fileContent in _fileHelper.ReadFileAsync().WithCancellation(cancellationToken))
                {
                    yield return _jsonService.Deserialize<T>(fileContent);
                }

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
                var fileContents = _fileHelper.ReadFile();

                return fileContents.Select(f => _jsonService.Deserialize<T>(f)).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    public interface IFileHelper
    {
        IAsyncEnumerable<string> ReadFileAsync();
        List<string> ReadFile();
        Task WriteToFileAsync(List<string> filePathsToWatch);
    }

    public class FileHelper : IFileHelper
    {
        private readonly string _filePath;

        public FileHelper(string filePath)
        {
            _filePath = filePath;
        }

        public async IAsyncEnumerable<string> ReadFileAsync()
        {
            using (var fileStream = File.OpenRead(_filePath))
            {
                using (var sr = new StreamReader(fileStream))
                {
                    string line;

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }

        public List<string> ReadFile()
        {
            var fileContents = new List<string>();

            using (var fileStream = File.OpenRead(_filePath))
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

        public async Task WriteToFileAsync(List<string> filePathsToWatch)
        {
            using (var fileStream = File.OpenWrite(_filePath))
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
