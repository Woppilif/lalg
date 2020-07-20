using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.CacheService
{
    class CacheSettings
    {
        public bool Enabled { get; set; }//включен кэш или нет
        public string ConnectionStr { get; set; }
    }
}
