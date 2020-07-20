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

namespace BotAppData.CacheService
{
    //class KeyVal
    //{
    //    public string Key { get; set; }
    //    public string Value { get; set; }
    //}
    public class Cache : ICache
    {        
        //private BotAppContext db;
        //private IMemoryCache cashe;
        private ConnectionMultiplexer redis;
        private readonly IDistributedCache distributedCache;
        //private string fileName = "keyValue.json";
        /*private JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true
        };*/
        CacheSettings casheSettings;

        public Cache()//IConfiguration configuration возможно надо добавить в параметры
        {
            //var jsonString = File.ReadAllText(fileName);
            /*casheSettings = new CacheSettings();//TODO не очень. Посмотреть как правильно присоединиться
            //casheSettings.ConnectionStr = JsonSerializer.Deserialize<String>(jsonString);
            if (!casheSettings.Enabled)
            {
                //redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("DefaultConnection"));
                redis = ConnectionMultiplexer.Connect(casheSettings.ConnectionStr);
                casheSettings.Enabled = true;
            }*/
        }

        public async Task<string> GetCache(string cacheKey)
        {
            if (String.IsNullOrEmpty(cacheKey))
            {
                return null;
            }
            //using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            //{
            //    KeyVal val = await JsonSerializer.DeserializeAsync<KeyVal>(fs);
            //    return val.Value;
            //    //Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            //}//
            var cache = await distributedCache.GetStringAsync(cacheKey);
            return String.IsNullOrEmpty(cache) ? null : cache;
        }

        public async Task SetCache(string cacheKey, string keyVal, TimeSpan timeLive)
        {          
            if(string.IsNullOrEmpty(cacheKey) || string.IsNullOrEmpty(keyVal) || timeLive == null)
            {
                return;
            }
            await distributedCache.SetStringAsync(cacheKey, keyVal, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeLive
            });
            //KeyVal save = new KeyVal { Key = cacheKey, Value = keyVal };
            //using (FileStream fs = new FileStream(fileName, FileMode.Append))
            //{
            //    await JsonSerializer.SerializeAsync<KeyVal>(fs, save, options);
            //}
            //File.WriteAllText(fileName, jsonString);
        }
        public void CloseCache()
        {
            if (casheSettings.Enabled)
            {
                casheSettings.Enabled = false;
                redis.Close();
            }
            else
            {
                return;
            }
        }
    }
}
