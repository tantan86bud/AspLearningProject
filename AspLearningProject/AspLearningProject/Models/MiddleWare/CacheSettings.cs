namespace AspLearningProject.Models.MiddleWare
{
    public class CacheSettings
    {
        public string CacheDirectory { get; set; }
        public int ImageAmount { get; set; }
        public int CacheExpirationTime { get; set; }
    }
}
