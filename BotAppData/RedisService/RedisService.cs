using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace BotAppData.RedisService
{
    public class RedisService : IRedisService
    {
        private readonly string _redisHost;
        private readonly int _redisPort;
        private ConnectionMultiplexer _redis;
        //private ILogger<RedisService> logger;

        public RedisService(IConfiguration config)
        {
            _redisHost = config["Redis:Host"];
            _redisPort = Convert.ToInt32(config["Redis:Port"]);
        }

        public void Connect()
        {
            try
            {
                var configString = "5.63.152.213:5432,connectRetry=5,user=postgres,password=whatsmyage123";
                _redis = ConnectionMultiplexer.Connect(configString);
            }
            catch (RedisConnectionException err)
            {
                //logger.LogError(err.ToString());
                throw err;
            }
            //logger.LogDebug("Connected to Redis");
            //Log.Debug("Connected to Redis");
        }

        public async Task Set(string key, string value)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value);
            return;
        }

        public async Task<string> Get(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }
    }
}
