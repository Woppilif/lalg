using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Data.Entity;
using BotAppData.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;

namespace BotAppData.CasheService
{
    class Cache : ICache
    {
        //private BotAppContext db;
        //private IMemoryCache cashe;
        private ConnectionMultiplexer redis;
        private readonly IDistributedCache distributedCache;
        //Database db;

        public Cache(IConfiguration configuration)
        {
            var casheSettings = new CacheSettings();//TODO не очень. Посмотреть как правильно присоединиться
            if (!casheSettings.Enabled)
            {
                //redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("DefaultConnection"));
                redis = ConnectionMultiplexer.Connect(casheSettings.ConnectionStr);
            }
        }

        public async Task<string> GetCache(string cacheKey)
        {
            var cache = await distributedCache.GetStringAsync(cacheKey);
            return String.IsNullOrEmpty(cache) ? null : cache;
            //throw new NotImplementedException();
        }

        public async Task SetCache(string cacheKey, object keyVal, TimeSpan timeLive)
        {
            if(keyVal == null)
            {
                return;
            }

            await distributedCache.SetStringAsync(cacheKey, keyVal.ToString(), new DistributedCacheEntryOptions //кажется не правильным keyVal.ToString()
            {
                AbsoluteExpirationRelativeToNow = timeLive
            });
            //throw new NotImplementedException();
        }

        /*public Cashe(BotAppContext context, IMemoryCache memoryCache)
        {
            db = context;
            cashe = memoryCache;
        }*/

        /*public async Task<Guid> GetKey(string id)
        {
            Guid key;
            if (!cashe.TryGetValue(id, out key))
            {
                cashe.Set(id, key); 
                return key;
            }
            return key;
        }*/

        /*public void SetKey(Guid key)
        {
            //var id = DateTime.Now;
            if (key != null)
            {
                cashe.Set(DateTime.Now, key,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            return;
        }*/
    }
}
