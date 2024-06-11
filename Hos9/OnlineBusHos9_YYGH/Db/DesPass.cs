using EncryptionKey;
using System;

namespace OnlineBusHos9_YYGH
{
    class DesPass
    {
        /// <summary>
        /// 将MySQL配置文件参数的密码Password 从密码改为明文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        internal static string DecryptMysqlConfigPwd(string connectionString)
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
            catch 
            {
            }
            return _connectionString;
        }
    }
}
