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
using System.Text.Json;
using System.IO;

namespace BotAppData.CasheService
{
    class KeyVal
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    class Cache : ICache
    {
        //private BotAppContext db;
        //private IMemoryCache cashe;
        private ConnectionMultiplexer redis;
        private readonly IDistributedCache distributedCache;
        private string fileName = "keyValue.json";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true
        };
        //Database db;

        public Cache()
        {
            //var jsonString = File.ReadAllText(fileName);
            var casheSettings = new CacheSettings();//TODO не очень. Посмотреть как правильно присоединиться
            //casheSettings.ConnectionStr = JsonSerializer.Deserialize<String>(jsonString);
            if (!casheSettings.Enabled)
            {
                //redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("DefaultConnection"));
                redis = ConnectionMultiplexer.Connect(casheSettings.ConnectionStr);
                casheSettings.Enabled = true;
            }
        }

        public async Task<string> GetCache(string cacheKey)
        {
            if (String.IsNullOrEmpty(cacheKey)) 
            {
                return null; 
            }
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                KeyVal val = await JsonSerializer.DeserializeAsync<KeyVal>(fs);
                return val.Value;
                //Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }//
            //var cache = await distributedCache.GetStringAsync(cacheKey);
            //return String.IsNullOrEmpty(cache) ? null : cache;
            ////throw new NotImplementedException();
        }

        public async Task SetCache(string cacheKey, string keyVal, TimeSpan timeLive)
        {
            if(string.IsNullOrEmpty(cacheKey) || string.IsNullOrEmpty(keyVal))
            {
                return;
            }

            await distributedCache.SetStringAsync(cacheKey, keyVal.ToString(), new DistributedCacheEntryOptions //кажется не правильным keyVal.ToString()
            {
                AbsoluteExpirationRelativeToNow = timeLive
            });
            KeyVal save = new KeyVal { Key = cacheKey, Value = keyVal };
            using (FileStream fs = new FileStream(fileName, FileMode.Append))
            {
                await JsonSerializer.SerializeAsync<KeyVal>(fs, save, options);
            }

            //File.WriteAllText(fileName, jsonString);
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
