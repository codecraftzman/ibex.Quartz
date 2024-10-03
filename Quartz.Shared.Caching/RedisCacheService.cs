using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quartz.Shared.Caching
{
    
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
            _jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _cache.GetAsync(key);
            if (cachedData == null || cachedData.Length == 0)
            {
                return default;
            }

            var jsonData = System.Text.Encoding.UTF8.GetString(cachedData);
            return JsonSerializer.Deserialize<T>(jsonData, _jsonOptions);
        }

        //public async Task<T?> GetAsync<T>(string key, JsonSerializerContext context)
        //{
        //    var cachedData = await _cache.GetAsync(key);
        //    if (cachedData == null || cachedData.Length == 0)
        //    {
        //        return default;
        //    }

        //    var jsonData = System.Text.Encoding.UTF8.GetString(cachedData);
        //    return JsonSerializer.Deserialize<T>(jsonData, context.GetTypeInfo<T>());
        //}

        public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5)
            };

            var jsonData = JsonSerializer.Serialize(value, _jsonOptions);
            var byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            await _cache.SetAsync(key, byteArray, options);
        }
    }
}
