
using System;
namespace OnlineBusHos185_GJYB
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

        public static Model.hospital GetRedisHospital(string HOS_ID, string fixmedins_code,string ybtype)
        {
            try
            {

                Model.hospital hospital = null;
                if (hospital == null)
                {

                    hospital = new BLL.hospital().GetModel(HOS_ID, fixmedins_code, ybtype);
                    if (hospital == null)
                    {
                        return null;
                    }
                    return hospital;
                }
                else
                {
                    return hospital;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// 从Redis中取医院信息
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="fixmedins_code"></param>
        /// <returns></returns>
        public static Model.hos_opter GetRedishos_opter(string HOS_ID, string opter_no,string ybtype)
        {
            try
            {
   
                Model.hos_opter hos_opter = null;

                if (hos_opter == null)
                {

                    hos_opter = new BLL.hos_opter().GetModel(HOS_ID, opter_no, ybtype);
                    if (hos_opter == null)
                    {
                        return null;
                    }
    
                    return hos_opter;
                }
                else
                {
                    return hos_opter;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// 从Redis中取psn_insuinfo信息
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="fixmedins_code"></param>
        /// <returns></returns>
        public static Model.psn_insuinfo GetRedisPsnInsuInfo(string psn_no)
        {
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);

                if (string.IsNullOrEmpty(psn_no))
                {
                    return null;
                }
                string key = "psn_insuinfo-" + psn_no;
                Model.psn_insuinfo psn_insuinfo = redis.Get<Model.psn_insuinfo>(key);

                if (psn_insuinfo == null)
                {

                    psn_insuinfo = new BLL.psn_insuinfo().GetInsuinfo(psn_no);
                    if (psn_insuinfo == null)
                    {
                        return null;
                    }
                    TimeSpan span = new TimeSpan(0, 30, 0);//设置超时时间
                    redis.Set(key, psn_insuinfo, span);
                    return psn_insuinfo;
                }
                else
                {
                    return psn_insuinfo;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// 设置Redis中的psn_insuinfo信息
        /// </summary>
        /// <param name="psn_no"></param>
        /// <returns></returns>
        public static Model.psn_insuinfo SetRedisPsnInsuInfo(string psn_no)
        {
            try
            {
                RedisHelperSentinels redis = new RedisHelperSentinels(RedisDatabase);
                if (string.IsNullOrEmpty(psn_no))
                {
                    return null;
                }
                Model.psn_insuinfo psn_insuinfo = new BLL.psn_insuinfo().GetInsuinfo(psn_no);
                if (psn_insuinfo == null)
                {
                    return null;
                }
                string key = "psn_insuinfo-" + psn_no;
                TimeSpan span = new TimeSpan(0, 30, 0);//设置超时时间
                redis.Set(key, psn_insuinfo, span);
                return psn_insuinfo;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
