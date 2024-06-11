using System;
using System.Runtime.Caching;

namespace ZZJ_Tran.Plat.DALFactory
{
    /// <summary>
    /// 缓存操作类
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            return MemoryCache.Default.Get(CacheKey);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            CacheItemPolicy policy = new CacheItemPolicy()
            {
                //缓存被删除是的回调
                RemovedCallback = (arguments) => { Console.WriteLine($"缓存被移除的原因：{arguments.RemovedReason}"); },
                //滑动过期时间
                SlidingExpiration = TimeSpan.FromSeconds(5),
                //绝对过期时间
                //AbsoluteExpiration = DateTime.Now.AddSeconds(5),
                //优先级有两种：Default，NotRemovable(不可移除)
                Priority = System.Runtime.Caching.CacheItemPriority.NotRemovable
            };
            System.Runtime.Caching.MemoryCache.Default.Add(CacheKey, objObject, policy);
        }
    }
}