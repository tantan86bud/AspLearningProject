
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading;

namespace AspLearningProject.Models.MiddleWare
{
    public class MemoryCaheImage : IMemoryCaheImage
    {
        private readonly CacheSettings cacheSettings;
        private readonly IMemoryCache memoryCache;
        private string cacheKeyDate = "DataLastResponse";
        private object cachLock = new object ();

        public MemoryCaheImage(CacheSettings cacheSettings, IMemoryCache memoryCache)
        {
            this.cacheSettings = cacheSettings;
            this.memoryCache = memoryCache;
           
        }
        public async Task TryCachingDateAsync()
        {
            DateTime PreviousCacheDate;           

            lock (cachLock)
            {
                if (memoryCache.TryGetValue(cacheKeyDate, out PreviousCacheDate))
                {
                    if ((DateTime.Now - PreviousCacheDate).TotalSeconds > cacheSettings.CacheExpirationTime)
                    {
                        CleanCache();
                    }
                }
                memoryCache.Set(cacheKeyDate, DateTime.Now);
            }           
        }
        public async Task AddImageToCacheAsync(byte[] bytesImage,  string cacheKeyImage)
        {
            lock (cachLock)
            {
                if (!ExistImage(cacheKeyImage) && CanAddImageToCache())
                
                {
                    using var writer = new BinaryWriter(File.OpenWrite(Path.Combine(cacheSettings.CacheDirectory, cacheKeyImage)));
                    writer.Write(bytesImage);
                }
                
            }
        }
        public async Task<byte[]> GetImageFromCacheAsync(string cacheKeyImage)
        {
            lock (cachLock)
            {
                byte[] bytess = new byte[0];

                if (ExistImage(cacheKeyImage))
                {
                    using (FileStream file = new FileStream(Path.Combine(cacheSettings.CacheDirectory, cacheKeyImage), FileMode.Open, FileAccess.Read))
                    {
                        bytess = new byte[file.Length];
                        file.Read(bytess, 0, (int)file.Length);
                       
                    }
                }
                return bytess;
            }
        }
        public bool ExistImage(string cacheKeyImage)
        {
            if (!Directory.Exists(cacheSettings.CacheDirectory))
            {
               Directory.CreateDirectory(cacheSettings.CacheDirectory);
            }
            DirectoryInfo dir = new DirectoryInfo(cacheSettings.CacheDirectory);

            FileInfo[] Files = dir.GetFiles();            
            return Files.Any(a => a.Name == cacheKeyImage);
        }
        public bool CanAddImageToCache()
        {
            DirectoryInfo dir = new DirectoryInfo(cacheSettings.CacheDirectory);
            return dir.GetFiles().Count() < cacheSettings.ImageAmount;
        }
        private void CleanCache()
        {
            DirectoryInfo dir = new DirectoryInfo(cacheSettings.CacheDirectory);
            if (dir.Exists)
            {
                dir.Delete(true);
            }

        }

    }
}