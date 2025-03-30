using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache cache;

        public RedisService(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public async Task DeleteUserAsync(string key)
        {
            await cache.RemoveAsync(key);
        }

        public async Task<T?> GetUserAsync<T>(string key)
        {
            var jsonData = await cache.GetStringAsync(key);
            return jsonData == null ? default : JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task SetUserAsync(string key, object value, int expirationMinutes)
        {
            var options = new DistributedCacheEntryOptions()
               .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));

            var jsonData = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, jsonData, options);
        }
    }
}
