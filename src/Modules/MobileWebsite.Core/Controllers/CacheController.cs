using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MimeKit.Cryptography;
using OrchardCore.Environment.Cache;
using OrchardCore.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWebsite.Core.Controllers
{
    public class CacheController : Controller
    {
        private const string CacheKey = "MobileWebsite.Core.CacheController.DemoCacheEntry";
        private const string SignalKey = "MobileWebsite.Core.CacheController.DemoSignal";

        private readonly IMemoryCache _memoryCache;
        private readonly ILocalClock _localClock;
        private readonly ISignal _signal;

        public CacheController(IMemoryCache memoryCache, ILocalClock localClock, ISignal signal)
        {
            _memoryCache = memoryCache;
            _localClock = localClock;
            _signal = signal;
        }

        public async Task<string> ReadCache()
        {
            DateTimeOffset localtime;

            if(!_memoryCache.TryGetValue(CacheKey, out localtime))
            {
                localtime = _memoryCache.Set(CacheKey, await _localClock.LocalNowAsync, _signal.GetToken(SignalKey));
            }

            return localtime.ToString();
        }
        public string InvalidateCache()
        {
            _memoryCache.Remove(CacheKey);
            return "Ok";
        }

        public string InvalidateCacheWightSignal()
        {
            _signal.SignalTokenAsync(SignalKey);
            return "Ok";
        }
    }
}
