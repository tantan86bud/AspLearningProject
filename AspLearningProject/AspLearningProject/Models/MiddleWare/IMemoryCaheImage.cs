namespace AspLearningProject.Models.MiddleWare
{
    public interface IMemoryCaheImage
    {
        Task AddImageToCacheAsync(byte[] bytesImage, string nameFile);
        Task<byte[]> GetImageFromCacheAsync(string nameFile);
        Task TryCachingDateAsync();
        bool CanAddImageToCache();
        bool ExistImage(string cacheKeyImage);

    }
}