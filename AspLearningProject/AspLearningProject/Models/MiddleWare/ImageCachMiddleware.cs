using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Linq;

namespace AspLearningProject.Models.MiddleWare
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CacheSettings cacheSettings;
        private readonly IConfiguration configuration;
        private readonly IMemoryCaheImage memoryCaheMiddleware;
        private readonly string prefixNameFile = "image_";
       

        public ImageCacheMiddleware(RequestDelegate next, CacheSettings cacheSettings, IConfiguration configuration, IMemoryCaheImage memoryCaheMiddleware)
        {
            this.next = next;
            this.cacheSettings = cacheSettings;
            this.configuration = configuration;
            this.memoryCaheMiddleware = memoryCaheMiddleware;
          
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await memoryCaheMiddleware.TryCachingDateAsync();

            var routeValues = context.Request.RouteValues;
            var keyImage = string.Join("_", routeValues.Select(s => s.Value.ToString()));
            
            if (!memoryCaheMiddleware.ExistImage($"{prefixNameFile}{keyImage}"))
            {
                try
                {
                    
                    var stream = context.Response.Body;
                    using (var buffer = new MemoryStream())
                    {
                        context.Response.Body = buffer;
                        await next.Invoke(context); 
                        if (context.Request.Method == "GET" && context.Response.ContentType == "image/jpeg")
                        {
                           await memoryCaheMiddleware.AddImageToCacheAsync(buffer.ToArray(), $"{prefixNameFile}{keyImage}");
                        }
                        buffer.Seek(0, SeekOrigin.Begin);
                        await buffer.CopyToAsync(stream);
                        context.Response.Body = stream;
                    }

                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }
            else
            {
                var stream = context.Response.Body;
                using (var buffer = new MemoryStream())
                {
                    byte[] bytess = await memoryCaheMiddleware.GetImageFromCacheAsync($"{prefixNameFile}{keyImage}");
                    buffer.Write(bytess);
                    buffer.Seek(0, SeekOrigin.Begin);
                    await buffer.CopyToAsync(stream);
                    context.Response.Body = stream;
                }
            }                  
          
        }          
        
    }

}



