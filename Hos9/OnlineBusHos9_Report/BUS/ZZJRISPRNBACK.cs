using CommonModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using OnlineBusHos9_Report.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace OnlineBusHos9_Report.BUS
{
    class ZZJRISPRNBACK
    {
        public static string B_ZZJRISPRNBACK(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                ZZJRISPRNBACK_M.ZZJRISPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJRISPRNBACK_M.ZZJRISPRNBACK_IN>(json_in);
                ZZJRISPRNBACK_M.ZZJRISPRNBACK_OUT _out = new ZZJRISPRNBACK_M.ZZJRISPRNBACK_OUT();

                #region 不管成功失败,记录打印,用于计数
                try
                {
                    StringBuilder str_reportmx = new StringBuilder();
                    str_reportmx.Append("insert into reportmx(HOS_ID,TYPE,HOS_SN,lTERMINAL_SN,USER_ID,NOW) values (");
                    str_reportmx.Append("@HOS_ID,@TYPE,@HOS_SN,@lTERMINAL_SN,@USER_ID,@NOW);");
                    MySqlParameter[] parameters =
                    {
                    new MySqlParameter("@HOS_ID",MySqlDbType.VarChar,20),
                    new MySqlParameter("@TYPE",MySqlDbType.VarChar,30),
                    new MySqlParameter("@HOS_SN",MySqlDbType.VarChar,100),
                    new MySqlParameter("@lTERMINAL_SN",MySqlDbType.VarChar,30),
                    new MySqlParameter("@USER_ID",MySqlDbType.VarChar,30),
                    new MySqlParameter("@NOW",MySqlDbType.DateTime)
                    };
                    parameters[0].Value = _in.HOS_ID;
                    parameters[1].Value = "检查报告";
                    parameters[2].Value = _in.REPORT_SN;
                    parameters[3].Value = _in.LTERMINAL_SN;
                    parameters[4].Value = _in.USER_ID;
                    parameters[5].Value = DateTime.Now;
                    DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
                }
                catch (Exception ex)
                {
              
                }
                #endregion
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                goto EndPoint;

            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                goto EndPoint;
            }
EndPoint:
            return JsonConvert.SerializeObject(dataReturn);

        }
    }
}
