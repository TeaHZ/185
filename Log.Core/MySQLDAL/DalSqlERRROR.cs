using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class DalSqlERRROR
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Log.Core.Model.ModSqlError model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sqlerror(");
            strSql.Append("TYPE,EXCEPTION,datetime)");
            strSql.Append(" values (");
            strSql.Append("@TYPE,@EXCEPTION,@datetime)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@TYPE", MySqlDbType.VarChar,30),
                    new MySqlParameter("@EXCEPTION", MySqlDbType.VarChar,500),
                    new MySqlParameter("@datetime", MySqlDbType.DateTime)};
            parameters[0].Value = model.TYPE;
            parameters[1].Value = model.EXCEPTION;
            parameters[2].Value = model.time;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
