using CommonModel;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OnlineBusHos9_Report.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Report.BUS
{
    internal class ZZJLISPRNBACK
    {
        public static string B_ZZJLISPRNBACK(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                ZZJLISPRNBACK_M.ZZJLISPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJLISPRNBACK_M.ZZJLISPRNBACK_IN>(json_in);
                ZZJLISPRNBACK_M.ZZJLISPRNBACK_OUT _out = new ZZJLISPRNBACK_M.ZZJLISPRNBACK_OUT();



                RMReportBack rmreport=new RMReportBack();
                rmreport.PrintSatate = "1";
                rmreport.ReportID = _in.REPORT_SN;

                RMPrintBack rm =new RMPrintBack();
                rm.Reports=new List<RMReportBack> { rmreport }; 
   



      
                var rmresult = HerenHelper.LisReportService("QHZZJReportPrint", JsonConvert.SerializeObject(rm));



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
                    parameters[1].Value = _in.REPORT_ZL; //"检验报告";全部走一个接口
                    parameters[2].Value = _in.REPORT_SN;
                    parameters[3].Value = _in.LTERMINAL_SN;
                    parameters[4].Value = _in.USER_ID;
                    parameters[5].Value = DateTime.Now;
                    DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
                }
                catch (Exception ex)
                {
                
                }



                #endregion 不管成功失败,记录打印,用于计数

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                goto EndPoint;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}