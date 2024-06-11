using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Log.Core.Model;
using Log.Core.MySQLDAL;
using MySql.Data.MySqlClient;
namespace Log.Core
{
   public  class LogHelper
    {
       public static bool Addlog(ModCCBpaylog Modlog)
       {
            try
            {
                return new DalCCBPaylog().Add(Modlog); ; ;
            }
            catch
            {
                return false;
            }
       }

       public static bool Addlog(ModUnionpaylog Modlog)
       {
            try
            {
                return new DalUnionpaylog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
       }

       public static bool Addlog(ModAlipaylog Modlog)
       {
            try
            {
                return new DalAlipaylog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
       }

       public static bool Addlog(ModCompaylog Modlog)
       {
            try
            {
                return new DalCompaylog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
       }

       public static bool Addlog(ModWxpaylog Modlog)
       {
            try
            {
                return new DalWxpaylog().Add(Modlog);
            }
            catch
            {
                return false;
            }
       }
       public static bool Addlog(ModIfsppaylog Modlog)
       {
            try
            {
                return new DalIfsppaylog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
       }
       public static bool Addlog(Modopt_pay_log Modlog)
       {
            try
            {
                return new DALopt_pay_log().Add(Modlog);
            }
            catch
            {
                return false;
            }
       }
       public static bool Addlog(Moddefault_appt_log Modlog)
       {
            try
            {
                return new Daldefault_appt_log().Add(Modlog);
            }
            catch
            {
                return false;
            }
       }
        public static bool Addlog(Modplatmonitorlog Modlog)
        {
            try
            {
                return new Dalplatmonitorlog().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(Modplatmonitorlog_correct Modlog)
        {
            try
            {
                return new Dalplatmonitorlog_correct().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(jsbankpaylog Modlog)
        {
            try
            {
                return new DalJsBankPaylog().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModSqlError Modlog)
        {
            try
            {
                return new DalSqlERRROR().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogHos Modlog)
        {
            try
            {
                return new DalLogHos().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogHosError Modlog)
        {
            try
            {
                return new DalLogHosError().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogHosNew Modlog)
        {
            try
            {
                return new DalLogHosNew().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogAPP Modlog)
        {
            try
            {
                return new DalLogAPP().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogAPPError Modlog)
        {
            try
            {
                return new DalLogAPPError().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogWxRefund Modlog)
        {
            try
            {
                return new DalLogWxRefund().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogSaveMessage Modlog)
        {
            try
            {
                return new DalLogSendMsg().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(xzlhfkpaylog Modlog)
        {
            try
            {
                return new DalXzlhfkPaylog().Add(Modlog);
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(icbcpaylog Modlog)
        {
            try
            {
                return new DalIcbcPaylog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(Modmedicalpaywxlog Modlog)
        {
            try
            {
                return new Dalmedicalpaywxlog().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
        }
        public static bool Addlog(ModLogPatData Modlog)
        {
            try
            {
                return new DalLogPatData().Add(Modlog); ;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// F2F接口日志记录表
        /// </summary>
        /// <param name="InTime"></param>
        /// <param name="Hos_Id"></param>
        /// <param name="ServicesName"></param>
        /// <param name="Code"></param>
        /// <param name="InData"></param>
        /// <param name="OutTime"></param>
        /// <param name="OutData"></param>
        public static void SaveLogF2F(DateTime InTime, string Hos_Id, string ServicesName, string Code, string InBody, string InData, DateTime OutTime, string OutBody, string OutData)
       {



           try
           {
               string sqlcmd = @"insert into logf2f (Hos_id ,ServicesName,Code,InTime,OutTime,InData ,OutData,InBody,OutBody)
values
(@Hos_id,@ServicesName,@Code,@InTime,@OutTime,@InData,@OutData,@InBody,@OutBody)";

               MySqlParameter[] parameters = {
					new MySqlParameter("@Hos_id",Hos_Id),
					new MySqlParameter("@ServicesName", ServicesName),
					new MySqlParameter("@Code", Code),
					new MySqlParameter("@InTime", InTime),
					new MySqlParameter("@OutTime",OutTime),
                    new MySqlParameter("@InData",InData),
                    new MySqlParameter("@OutData",OutData),
                    new MySqlParameter("@InBody",InBody),
                    new MySqlParameter("@OutBody",OutBody)

                                                };
               DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
           }
           catch
           {

           }
       }

       public static bool Addlog(ModMedicalpaylog  Modlog)
       {
           try
           {
               return new DalMedicalpaylog().Add(Modlog); ;
           }
           catch(Exception ex)
           {
               return false;
           }
       }

        public static void WriteLog(string type, string className, string content)
        {
            try
            {
                string path = "";
                try
                {
                    path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
                }
                catch (Exception ex)
                {
                }

                if (!Directory.Exists(path))//如果日志目录不存在就创建
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                    string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
                                                                                                                       //创建或打开日志文件，向日志文件末尾追加记录
                    StreamWriter mySw = File.AppendText(filename);

                    //向日志文件写入内容
                    string write_content = className == "" ? content : className + ": " + content;
                    mySw.WriteLine(time + " " + type);
                    mySw.WriteLine(write_content);
                    mySw.WriteLine("");
                    //关闭日志文件
                    mySw.Close();
                }
                catch (Exception ex)
                {

                }
            }
            catch
            { }
        }
    }
}
