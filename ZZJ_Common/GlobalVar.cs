using BusinessInterface;
using CommonModel;
using DB.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.Data;
using System.Text;

namespace ZZJ_Common
{
    internal class GlobalVar
    {
        public static SLBBusinessInfo CallOtherBus(string data, string HOS_ID, string SLB_ID, string subSubId)
        {
            try
            {
                DateTime nowIn = DateTime.Now;
                SLBBusinessInfo OutSLBBOtherBus = new SLBBusinessInfo();

                string key = HOS_ID + "_" + SLB_ID;
                DataTable dtconfig = DictionaryCacheHelper.GetCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                if (dtconfig.Rows.Count == 0)
                {
                    dtconfig = DictionaryCacheHelper.UpdateCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                    if (dtconfig.Rows.Count == 0)
                    {
                        DataReturn dataReturn = new CommonModel.DataReturn();
                        dataReturn.Code = ConstData.CodeDefine.BusError;
                        dataReturn.Msg = "未配置模块对应院端服务";
                        OutSLBBOtherBus.BusData =   JsonConvert.SerializeObject(dataReturn);
                        goto EndPoint;
                    }
                }
                else
                {
                    TimeSpan ts = new TimeSpan();
                    ts = DateTime.Now - DateTime.Parse(dtconfig.Rows[0]["CURRENT_TIMESTAMP"].ToString());
                    if (ts.Minutes > 5)
                    {
                        dtconfig = DictionaryCacheHelper.UpdateCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                    }
                }

                SLBBusinessInfo SLBBOtherBus = new SLBBusinessInfo();
                SLBBOtherBus.BusID = FormatHelper.GetStr(dtconfig.Rows[0]["BUS_ID"]);
                SLBBOtherBus.SubBusID = subSubId;
                SLBBOtherBus.BusData = data;

                bool result = BusServiceAdapter.Ipb_CallOtherBusiness(SLBBOtherBus, out OutSLBBOtherBus);

            EndPoint:
             
                return OutSLBBOtherBus;
            }
            catch (Exception ex)
            {
             
                return null;
            }
        }

        public static DataTable GetSLBBusinessInfo(string SLB_ID, string HOS_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select *,CURRENT_TIMESTAMP from baccountSlbToHos where Slb_ID=@SLB_ID and HOS_ID=@HOS_ID");
            MySqlParameter[] parameters =
            {
                    new MySqlParameter("@SLB_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20)
                };
            parameters[0].Value = SLB_ID;
            parameters[1].Value = HOS_ID;
            DataTable dtconfig = DbHelperPlatZzjSQL.Query(sb.ToString(), parameters).Tables[0];
            return dtconfig;
        }
    }
}