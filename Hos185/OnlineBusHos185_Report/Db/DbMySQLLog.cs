using SqlSugar;
using System;
using System.Configuration;
using System.Linq;

namespace OnlineBusHos185_Report
{
    public class DbMySQLLog
    {
        /// <summary>
        /// 获取MySQL连接字符串
        /// </summary>
        private static string connectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionStringLogMySQL"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DesPass.DecryptMysqlConfigPwd(_connectionString);
                }
                return _connectionString;
            }
        }


        //注意：不能写成静态的，不能写成静态的
        public SqlSugarClient Client;//用来处理事务多表查询和复杂的操作

        public DbMySQLLog()
        {
            Client = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Client.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }


        public SimpleClient<T> CreateClient<T>() where T : class, new()
        {
            return Client.GetSimpleClient<T>();
        }
    }
}
