using BusinessInterface;
using CommonModel;
using DB.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.Data;
using System.Text;
using System.Text.Json;

namespace ZZJ_Common
{
    public class GlobalVar
    {
        public static SLBBusinessInfo CallOtherBus(string data, string HOS_ID, string SLB_ID, string subSubId)
        {
            SLBBusinessInfo OutSLBBOtherBus = new SLBBusinessInfo();
            try
            {
                DateTime nowIn = DateTime.Now;

                string key = HOS_ID + "_" + SLB_ID;
                DataTable dtconfig = new DataTable();
                try
                {
                    dtconfig = GetSLBBusinessInfo(SLB_ID, HOS_ID);
                }
                catch (Exception ex)
                {
                    DataReturn dataReturn = new CommonModel.DataReturn();
                    dataReturn.Code = ConstData.CodeDefine.BusError;
                    dataReturn.Msg = "查询数据库错误：" + ex.Message;
                    //OutSLBBOtherBus.BusData = JSONSerializer.Serialize(dataReturn);
                    OutSLBBOtherBus.BusData = JsonConvert.SerializeObject(dataReturn);
                    goto EndPoint;
                }

                if (dtconfig.Rows.Count == 0)
                {
                    DataReturn dataReturn = new CommonModel.DataReturn();
                    dataReturn.Code = ConstData.CodeDefine.BusError;
                    dataReturn.Msg = "未配置模块对应院端服务";
                    OutSLBBOtherBus.BusData = JsonConvert.SerializeObject(dataReturn);
                    goto EndPoint;
                }
                SLBBusinessInfo SLBBOtherBus = new SLBBusinessInfo();
                SLBBOtherBus.BusID = FormatHelper.GetStr(dtconfig.Rows[0]["BUS_ID"]);
                SLBBOtherBus.SubBusID = subSubId;
                SLBBOtherBus.BusData = data;

                bool result = BusServiceAdapter.Ipb_CallOtherBusiness(SLBBOtherBus, out OutSLBBOtherBus);
            EndPoint:
                Log.Core.Model.ModLogAPP modLogAPP = new Log.Core.Model.ModLogAPP();
                modLogAPP.inTime = nowIn;
                modLogAPP.outTime = DateTime.Now;
                modLogAPP.inXml = data;
                modLogAPP.outXml = OutSLBBOtherBus.BusData;
                Log.Core.LogHelper.Addlog(modLogAPP);
                return OutSLBBOtherBus;
            }
            catch (Exception ex)
            {
                Log.Core.Model.ModLogAPPError modLogAPPError = new Log.Core.Model.ModLogAPPError();
                modLogAPPError = new Log.Core.Model.ModLogAPPError();
                modLogAPPError.inTime = DateTime.Now;
                modLogAPPError.outTime = DateTime.Now;
                modLogAPPError.inXml = data;
                modLogAPPError.TYPE = "2020";
                modLogAPPError.outXml = ex.ToString();

                Log.Core.LogHelper.Addlog(modLogAPPError);

                DataReturn dataReturn = new CommonModel.DataReturn();
                dataReturn.Code = ConstData.CodeDefine.BusError;
                dataReturn.Msg = "GlobalVar未知错误：" + ex.Message;
                OutSLBBOtherBus.BusData = JsonConvert.SerializeObject(dataReturn);
                return OutSLBBOtherBus;
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