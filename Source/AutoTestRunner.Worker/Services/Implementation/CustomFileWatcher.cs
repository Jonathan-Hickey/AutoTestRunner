using System;
using System.IO;
using System.Runtime.Caching;

namespace AutoTestRunner.Worker.Services.Implementation
{
    public class CustomFileWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly MemoryCache _memCache;
        private readonly CacheItemPolicy _cacheItemPolicy;
        private readonly Guid _id;

        private const int CacheTimeMilliseconds = 500;
        
        public CustomFileWatcher(MemoryCache memoryCache, Guid id, string path, string filter)
        {
            _id = id;
            _memCache = memoryCache;
            _cacheItemPolicy = new CacheItemPolicy()
            {
                RemovedCallback = OnRemovedFromCache
            };

            _fileSystemWatcher = new FileSystemWatcher(path, filter);
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fileSystemWatcher.Changed += OnFileWatcherOnChange;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public Action<Guid, FileSystemEventArgs> OnChange;

        public void Dispose()
        {
            _fileSystemWatcher.Dispose();
        }

        private void OnRemovedFromCache(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;

            var e = (FileSystemEventArgs)args.CacheItem.Value;

            OnChange.Invoke(_id, e);
        }

        private void OnFileWatcherOnChange(object sender, FileSystemEventArgs e)
        {
            _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);
            _memCache.AddOrGetExisting($"{_id}_{e.Name}", e, _cacheItemPolicy);
        }
    }
}
