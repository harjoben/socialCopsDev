using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CoreService.Cache
{
    public class CachingConfig
    {
            private static bool? _cachingEnabled;

            public static bool CachingEnabled
            {
                get
                {
                    if (_cachingEnabled == null)
                    {
                        string keyStrVal = ConfigurationManager.AppSettings["CachingEnabled"] as string;
                        if (!string.IsNullOrEmpty(keyStrVal))
                        {
                            _cachingEnabled = keyStrVal.ToLower() == "true";
                            return _cachingEnabled.GetValueOrDefault();
                        }

                    }
                    else
                    {
                        return _cachingEnabled.GetValueOrDefault();
                    }

                    //set to default value
                    _cachingEnabled = true;
                    return true;
                }
                set
                {
                    _cachingEnabled = value;
                }
            }

            private static int _slidingExpirationTime = 20;

            /// <summary>
            /// Sliding expiration time in seconds
            /// </summary>
            public static int SlidingExpirationTime
            {
                get
                {
                    return _slidingExpirationTime;
                }
                set
                {
                    _slidingExpirationTime = value;
                }
            }



        }
    }
}