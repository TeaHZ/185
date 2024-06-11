using CommonModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using OnlineBusHos185_Report.Model;
using MySql.Data.MySqlClient;
using Hos185_His.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnlineBusHos185_Report.BUS
{
    class ZZJPATHOLOGYPRNBACK
    {
        public static string B_ZZJPATHOLOGYPRNBACK(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                ZZJPATHOLOGYPRNBACK_M.ZZJPATHOLOGYPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJPATHOLOGYPRNBACK_M.ZZJPATHOLOGYPRNBACK_IN>(json_in);
                ZZJPATHOLOGYPRNBACK_M.ZZJPATHOLOGYPRNBACK_OUT _out = new ZZJPATHOLOGYPRNBACK_M.ZZJPATHOLOGYPRNBACK_OUT();


                Hos185_His.Models.Report.updatePrintStatusByBingli updateprint = new Hos185_His.Models.Report.updatePrintStatusByBingli
                {
                    reportType = _in.REPORT_TYPE,
                    reportId = _in.REPORT_SN
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(updateprint);

                Output<object> output = GlobalVar.CallAPI<object>("/hislispacs/updatePrintStatusByBingli", jsonstr);

                JObject jobj = new JObject();

                if (_in.TYPE == "inspecttyp_xindian")
                {

                    jobj.Add("inspectNo", _in.REPORT_TYPE  + _in.REPORT_SN);
                    jobj.Add("inspectType", "inspecttyp_xindian");
                }
                else
                {
                    jobj.Add("inspectNo", _in.REPORT_TYPE + ":" + _in.REPORT_SN);
                    jobj.Add("inspectType", "inspecttyp_bingli");
                }



                Output<object> mzoutput
          = GlobalVar.CallAPI<object>("/hislispacs/tech/updatePrintStatus", jobj.ToString());


                dataReturn.Code = mzoutput.code;
                dataReturn.Msg = mzoutput.message;
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
                    parameters[1].Value = "病理报告";
                    if (_in.TYPE == "inspecttyp_xindian")
                    {
                        parameters[1].Value = "心电报告";

                    }
                    parameters[2].Value = _in.REPORT_SN;
                    parameters[3].Value = _in.LTERMINAL_SN;
                    parameters[4].Value = _in.USER_ID;
                    parameters[5].Value = DateTime.Now;
                    DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    Log.Core.Model.ModSqlError logsql = new Log.Core.Model.ModSqlError();
                    logsql.TYPE = "病理";
                    if (_in.TYPE == "inspecttyp_xindian")
                    {
                        logsql.TYPE = "心电";

                    }
                    logsql.EXCEPTION = ex.ToString();
                    logsql.time = DateTime.Now;
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(logsql);
                }
                #endregion



            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
    }
}
