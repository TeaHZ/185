using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_YYGH.HISModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_YYGH
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

        public static PushServiceResult<List<dept>> GetT2001(string ksdm)
        {
            string key = "2001-" + ksdm.Trim();
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

                PushServiceResult<List<dept>> query = redis.Get<PushServiceResult<List<dept>>>(key);

                if (query == null)
                {
                    HISModels.T2001.input input = new T2001.input()
                    {
                        hospitalId = "320282466455146",//  医院编码
                        keShiDM = ksdm,//科室代码
                        yeWuLX = "2001",//业务类型
                        shiFouCXXL = ksdm == "" ? "0" : "1",// 是否查询大类      1:keShiDM必传,传1表示根据keShiDM查询其下属的科室小类 //0:若科室代码为空，则查询所有科室大类,不为空,则根据keShiDM查询医生排班
                        menZhenLX = "0",//  门诊类型 默认0
                    };
                    PushServiceResult<List<T2001.data>> result = HerenHelper<List<T2001.data>>.pushService("2001-QHZZJ", JsonConvert.SerializeObject(input));

                    List<T2001.data> datas = result.data;

                    var data = datas.GroupBy(d => new { d.keShiDLDM, d.keShiDLMC, d.keShiDM, d.keShiMC ,d.tingZhenBZ});

                    List<dept> depts = new List<dept>();

                    foreach (var item in data)
                    {
                        dept deptdept = new dept() { keShiDLDM = item.Key.keShiDLDM, keShiDLMC = item.Key?.keShiDLMC, keShiDM = item.Key?.keShiDM, keShiMC = item.Key.keShiMC,tingZhenBZ=item.Key.tingZhenBZ };
                        depts.Add(deptdept);
                    }
                    PushServiceResult<List<dept>> result1=new PushServiceResult<List<dept>>()
                    {
                        code=result.code,
                        msg=result.msg,
                        data=depts
                    };
                    TimeSpan span = new TimeSpan(1, 0, 0);//设置超时时间
                    redis.Set(key, result1, span);
                    return result1;
                }

                return query;
            }
            catch (Exception ex)
            {
                return new PushServiceResult<List<dept>>();
            }
        }
        public static List<ZZJ001.DEPT> GetZZJ001()
        {
            string key = "ZZJ001-QHZZJ";
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

                List<ZZJ001.DEPT> query = redis.Get<List<ZZJ001.DEPT>>(key);

                if (query == null)
                {
                    JObject input = new JObject();
                    input.Add("HOSP", "10");

                    QueryServiceResult<List<ZZJ001.DEPT>> result = HerenHelper<List<ZZJ001.DEPT>>.QueryService("ZZJ001-QHZZJ", input);
                    List<ZZJ001.DEPT> datas = result.Body;
                    
                    //var datas1 = from data in datas
                    //             where data.DeptType == "A04"
                    //             select datas;

                    TimeSpan span = new TimeSpan(1, 0, 0);//设置超时时间
                    redis.Set(key, datas, span);
                    return datas;
                }

                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class dept
    {
        public int tingZhenBZ { get; set; }
        public string keShiDLDM { get; set; }
        public string keShiDLMC { get; set; }
        public string keShiDM { get; set; }
        public string keShiMC { get; set; }
    }
}