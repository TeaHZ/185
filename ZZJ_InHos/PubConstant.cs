using PasS.Base.Lib;
using System;
using System.Configuration;

namespace ZZJ_InHos
{
    public class PubConstant
    {
        /// <summary>
        /// 获取默认连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionStringPlatZZJ"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DecryptMysqlConfigPwd(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 将MySQL配置文件参数的密码Password 从密码改为明文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string DecryptMysqlConfigPwd(string connectionString)
        {
            string _connectionString = connectionString;
            try
            {
                string[] cons = _connectionString.Split(';');
                foreach (string con in cons)
                {
                    string tcon = con.Trim();
                    if (tcon.StartsWith("password", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] tcons = tcon.Split('=');
                        string pwd = tcons[1];
                        pwd = pwd.Trim('\'');
                        string pwdmw = DESEncrypt.Decrypt(pwd);
                        _connectionString = _connectionString.Replace(pwd, pwdmw);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return _connectionString;
        }

        /// <summary>
        /// 获取默认连接字符串
        /// </summary>
        public static string ConnectionStringSync
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionStringSync"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DecryptMysqlConfigPwd(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 获取MySQL连接字符串
        /// </summary>
        public static string ConnectionStringMySQL
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionStringMySQL"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DecryptMysqlConfigPwd(_connectionString);
                }
                return _connectionString;
            }
        }
    }
}