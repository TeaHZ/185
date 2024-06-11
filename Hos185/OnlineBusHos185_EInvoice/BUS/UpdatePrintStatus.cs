using CommonModel;
using Hos185_His.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_EInvoice.Class;

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_EInvoice.BUS
{
    internal class UpdatePrintStatus
    {
        public static string B_UpdatePrintStatus(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                UpdatePrintStatus_IN _in = JsonConvert.DeserializeObject<UpdatePrintStatus_IN>(json_in);
                //挂号发票，取列表出参的queryid字段
                //门诊发票，取列表出参的jssjh字段
                JObject jobj = new JObject();
                jobj.Add("inspectNo", _in.INVOICE_NUMBER);
                jobj.Add("inspectType", "inspecttyp_fapiao");

                Output<object> mzoutput
          = GlobalVar.CallAPI<object>("/hislispacs/tech/updatePrintStatus", jobj.ToString());

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
                    parameters[1].Value = "电子发票";
                    parameters[2].Value = FormatHelper.GetStr(_in.INVOICE_CODE) + "-" + FormatHelper.GetStr(_in.INVOICE_NUMBER);
                    parameters[3].Value = _in.LTERMINAL_SN;
                    parameters[4].Value = _in.USER_ID;
                    parameters[5].Value = DateTime.Now;
                    DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    Log.Core.Model.ModSqlError logsql = new Log.Core.Model.ModSqlError();
                    logsql.TYPE = "电子发票";
                    logsql.EXCEPTION = ex.ToString();
                    logsql.time = DateTime.Now;
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(logsql);
                }

                #endregion 不管成功失败,记录打印,用于计数

         
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
        EndPoint:
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_UpdatePrintStatus_b(string json_in)
        {
            UpdatePrintStatus_IN _in = JsonConvert.DeserializeObject<UpdatePrintStatus_IN>(json_in);
            DataReturn dataReturn = new DataReturn();
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public class BODY
        {
            public string CLBZ { get; set; }
            public string CLJG { get; set; }
        }
    }
}