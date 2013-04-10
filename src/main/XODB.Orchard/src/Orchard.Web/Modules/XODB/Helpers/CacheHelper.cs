using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.Caching;

namespace XODB.Helpers
{
    public static class CacheHelper
    {
        private static HttpRuntime _httpRuntime;

        public static Cache Cache
        {
            get
            {
                EnsureHttpRuntime();
                return HttpRuntime.Cache;
            }
        }

        public static T AddToCache<T>(this Func<object> toRun, string cacheKey)
        {
            object c = CacheHelper.Cache[cacheKey];
            if (c == null)
            {
                c = toRun.Invoke();
                CacheHelper.Cache.Insert(cacheKey, c, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }
            return (T)c;
        }


        private static void EnsureHttpRuntime()
        {
            if (null == _httpRuntime)
            {
                try
                {
                    Monitor.Enter(typeof(CacheHelper));
                    if (null == _httpRuntime)
                    {
                        // Create an Http Content to give us access to the cache.
                        _httpRuntime = new HttpRuntime();
                    }
                }
                finally
                {
                    Monitor.Exit(typeof(CacheHelper));
                }
            }
        }
    }
}
