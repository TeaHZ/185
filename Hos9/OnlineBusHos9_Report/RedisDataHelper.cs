using PasS.Base.Lib;
using System;

namespace OnlineBusHos9_Report
{
    public class RedisDataHelper
    {
        public static int RedisDatabase
        {
            get
            {
                try
                {
                    return FormatHelper.GetInt(ConfigHelper.GetConfiguration("RedisDatabase").Trim()); //RedisDatabase
                }
                catch (Exception ex) { return 0; }
            }
        }

        public static string GetLisResult(string key)
        {
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

                return redis.Get<string>(key);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void SetLisResult(string key, object value)
        {
            RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

            TimeSpan span = new TimeSpan(0, 10, 0);//设置超时时间
            redis.Set(key, value, span);
        }
    }
}