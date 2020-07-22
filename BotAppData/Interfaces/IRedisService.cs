using System.Threading.Tasks;

namespace BotAppData.RedisService
{
    public interface IRedisService
    {
        void Connect();
        Task<string> Get(string key);
        Task Set(string key, string value);
    }
}