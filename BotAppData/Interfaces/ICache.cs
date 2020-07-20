﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotAppData.Interfaces
{
    interface ICache
    {
        void CloseCache();
        Task<string> GetCache(string cacheKey);
        Task SetCache(string cacheKey, string keyVal, TimeSpan timeLive);
    }
}
