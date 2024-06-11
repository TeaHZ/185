using System;
using System.IO;

namespace OnlineBusHos9_Common
{
    public class LogHelper
    {
        public static void SaveLogHos(SqlSugarModel.Loghos loghos)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Loghos>(loghos).ExecuteCommand();
            }
            catch (Exception ex)
            {
            }
        }

        public static void SaveLogZZJ(SqlSugarModel.Logzzj log)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Logzzj>(log).ExecuteCommand();
            }
            catch (Exception ex)
            {
            }
        }

        public static void SaveSqlerror(SqlSugarModel.Sqlerror log)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Sqlerror>(log).ExecuteCommand();
            }
            catch (Exception ex)
            {
            }
        }


        public static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
            }
            catch (Exception ex)
            {
                //   path = HttpContent.Current.Server.MapPath("MySpringlog");
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
                string write_content = className + ": " + content;
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

    }
}