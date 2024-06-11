using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Common.HISModels;
using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Common
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


        public static QueryServiceResult<List<PRICE001.data>> GetPrice001(string querytype)
        {

            string key = querytype;
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

                QueryServiceResult<List<PRICE001.data>> query = redis.Get<QueryServiceResult<List<PRICE001.data>>>(key);


                if (query == null)
                {
                    JObject jin = new JObject
                    {
                        { "ItemClass", querytype }
                    };
                    QueryServiceResult<List<PRICE001.data>> result = HerenHelper<List<PRICE001.data>>.QueryService("PRICE001", jin);
                    TimeSpan span = new TimeSpan(24, 0, 0);//设置超时时间
                    redis.Set(key, result, span);
                    return result;
                }


                return query;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("OnlineBusHos9_Common", "GetPrice001", JsonConvert.SerializeObject(ex));

                return new  QueryServiceResult<List<PRICE001.data>>();
            }

        }






    }
}
