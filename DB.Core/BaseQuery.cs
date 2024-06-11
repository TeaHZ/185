using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DB.Core;
using System.IO;

namespace SQL
{
    public class BaseQuery
    {
        public static bool InsertValid(string GUID)
        {
            try
            {
                string sqlcmd = string.Format(@"INSERT INTO `checkduprequest` (`GUID`) VALUES (@GUID)");

                MySqlParameter[] parameters = {
                    new MySqlParameter("@GUID", MySqlDbType.VarChar,36)         };
                parameters[0].Value = GUID;

                var data = DbHelperMySQLZZJ.ExecuteSql(sqlcmd,parameters);
                return true;
            }
            catch(Exception ex)
            {
                WriteLog("InsertValid", "InsertValid", ex.ToString());
                return false;
            }
        }

        protected static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLogInsertValid");
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
