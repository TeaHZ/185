using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Core
{
    public class GetSysId
    {
        /// <summary>
        /// 获取指定基础表ID 
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        public bool GetSysIdBase(string SYSID_NAME, out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySQL.connectionString))
            {
                string sp_Name = @"GETSYSIDBASE";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("SYSIDNAME", SYSID_NAME);
                MySqlParameter sql_param = sqlcmd.Parameters.AddWithValue("SYSID", SqlDbType.Int); //sqlcmd.Parameters.Add("SYSID", SqlDbType.Int);
                sql_param.Direction = ParameterDirection.Output;
                sqlcmd.ExecuteNonQuery();
                if (sql_param.Value == DBNull.Value)
                {
                    return false;
                }
                else if ((int)sql_param.Value <= 0)
                {
                    return false;
                }
                else
                {
                    Sys_Id = (int)sql_param.Value;
                    return true;
                }
            }
        }
    }
}
