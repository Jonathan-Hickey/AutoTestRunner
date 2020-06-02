using System;
using System.IO;
using System.Runtime.Caching;

namespace AutoTestRunner.Services
{
    public class CustomFileWatcher 
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly MemoryCache _memCache;

        private readonly CacheItemPolicy _cacheItemPolicy;
        private const int CacheTimeMilliseconds = 500;
        private static readonly string _filter = "*.dll";
        public CustomFileWatcher(MemoryCache memoryCache, string path)
        {
            _memCache = memoryCache;
            _cacheItemPolicy = new CacheItemPolicy()
            {
                RemovedCallback = OnRemovedFromCache
            };

            _fileSystemWatcher = new FileSystemWatcher(path, _filter);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fileSystemWatcher.Changed += OnChange;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public Action<FileSystemEventArgs> OnDllChanged;

        private void OnRemovedFromCache(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;

            var e = (FileSystemEventArgs)args.CacheItem.Value;

            OnDllChanged.Invoke(e);
        }

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);
            _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);
        }
    }
}
