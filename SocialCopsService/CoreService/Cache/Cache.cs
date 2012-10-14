using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreService.Cache
{
    public class Cache
    {
        public void AddToCache(object key, object temp)
        {
            if (CachingConfig.CachingEnabled)
            {
                if (CachingConfig.SlidingExpirationTime <= 0 || CachingConfig.SlidingExpirationTime == int.MaxValue)
                {
                    WCFCache.Current[key] = temp;
                }
                else
                {
                    WCFCache.Current.Insert(key, temp, new TimeSpan(0, 0, CachingConfig.SlidingExpirationTime), true);
                }
            } 
        }
    }
}