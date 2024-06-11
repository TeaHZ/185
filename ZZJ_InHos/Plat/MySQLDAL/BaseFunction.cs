using DB.Core;
using Log.Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.MySQLDAL
{
    /// <summary>
    /// 数据访问类:BaseFunction
    /// </summary>
    public partial class BaseFunction : IBaseFunction
    {
        public BaseFunction()
        {
        }

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string where, params string[] list)
        {
            string sqlcmd = "";
            foreach (string s in list)
            {
                sqlcmd += s + ",";
            }
            sqlcmd = sqlcmd.TrimEnd(',');
            sqlcmd = "select " + sqlcmd + " from " + tableName + " where " + where;
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        public DataTable GetList_ZZJ(string tableName, string where, params string[] list)
        {
            string sqlcmd = "";
            foreach (string s in list)
            {
                sqlcmd += s + ",";
            }
            sqlcmd = sqlcmd.TrimEnd(',');
            sqlcmd = "select " + sqlcmd + " from " + tableName + " where " + where;
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        public DataTable ExecPara(string sqlcmd, MySqlParameter[] para)
        {
            return DbHelperMySQLZZJ.Query(sqlcmd, para).Tables[0];
        }

        public DataSet RunProcedureForQuery(string storedProcName, IDataParameter[] parameteres)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySQLZZJ.connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    cm.CommandText = storedProcName;
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddRange(parameteres);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cm);
                    adapter.Fill(ds);
                    conn.Close();
                    return ds;
                }
                catch (Exception EX)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        public bool UpdateList(string tableName, string condiction, string setWord)
        {
            string sqlcmd = "";
            sqlcmd = @"update " + tableName + " set  " + setWord + " where " + condiction;
            //Log.Helper.LogHelper.SaveLogZZJ("UpdateList", DateTime.Now, sqlcmd, DateTime.Now, tableName);
            return DbHelperMySQLZZJ.ExecuteSql(sqlcmd) > 0;
            //UpdateList("register_appt"," set in_pay_status=1 where reg_id='"+reg_id+"');
        }

        /// <summary>
        /// 更新列表(自助机)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        public bool UpdateList_ZZJ(string tableName, string condiction, string setWord)
        {
            string sqlcmd = "";
            sqlcmd = @"update " + tableName + " set  " + setWord + " where " + condiction;
            return DbHelperMySQLZZJ.ExecuteSql(sqlcmd) > 0;
        }

        /// <summary>
        /// 获得查询数据记录总数
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public static int PAGECount(string sqlcmd)
        {
            int index = sqlcmd.IndexOf("from");
            if (index < 0)
            {
                index = sqlcmd.IndexOf("FROM");
            }
            sqlcmd = "select count(*)" + sqlcmd.Substring(index);
            return Convert.ToInt32(DbHelperMySQLZZJ.GetSingle(sqlcmd));
        }

        /// <summary>
        /// 获得查询数据记录总数
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public static int PAGECount_ZZJ(string sqlcmd)
        {
            int index = sqlcmd.IndexOf("from");
            if (index < 0)
            {
                index = sqlcmd.IndexOf("FROM");
            }
            sqlcmd = "select count(*)" + sqlcmd.Substring(index);
            return Convert.ToInt32(DbHelperMySQLZZJ.GetSingle(sqlcmd));
        }

        /// <summary>
        /// 获得查询数据记录总数(unionall)
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public static int PAGECountUnionAll(string sqlcmd)
        {
            DataTable dt = DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
            return dt.Rows.Count;
        }

        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        public bool GetSysIdBase(string SYSID_NAME, out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySQLZZJ.connectionString))
            {
                string sp_Name = @"GETSYSIDBASE";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("SYSIDNAME", SYSID_NAME);
                MySqlParameter sql_param = sqlcmd.Parameters.Add("SYSID", MySqlDbType.Int32);
                sql_param.Direction = ParameterDirection.Output;
                sqlcmd.ExecuteNonQuery();
                if (sql_param.Value == DBNull.Value)
                {
                    string strSQL = " INSERT INTO `sysidbase` VALUES('" + SYSID_NAME + "',  1);";
                    if (DbHelperMySQLZZJ.ExecuteSql(strSQL) > 0)
                    {
                        Sys_Id = 1;
                        return true;
                    }
                    return false;
                }
                else if ((int)sql_param.Value <= 0)
                {
                    ModSqlError modSqlError = new ModSqlError();
                    modSqlError.TYPE = "SYSID";
                    modSqlError.time = DateTime.Now;
                    modSqlError.EXCEPTION = "获取指定sysid_name失败:" + SYSID_NAME;
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(modSqlError);
                    return false;
                }
                else
                {
                    Sys_Id = (int)sql_param.Value;
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        public bool GetSysIdBase_ZZJ(string SYSID_NAME, out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySQLZZJ.connectionString))
            {
                string sp_Name = @"GETSYSIDBASE";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("SYSIDNAME", SYSID_NAME);
                MySqlParameter sql_param = sqlcmd.Parameters.Add("SYSID", MySqlDbType.Int32);
                sql_param.Direction = ParameterDirection.Output;
                sqlcmd.ExecuteNonQuery();
                if (sql_param.Value == DBNull.Value)
                {
                    string strSQL = " INSERT INTO `sysidbase` VALUES('" + SYSID_NAME + "',  1);";
                    if (DbHelperMySQLZZJ.ExecuteSql(strSQL) > 0)
                    {
                        Sys_Id = 1;
                        return true;
                    }
                    return false;
                }
                else if ((int)sql_param.Value <= 0)
                {
                    ModSqlError modSqlError = new ModSqlError();
                    modSqlError.TYPE = "SYSID";
                    modSqlError.time = DateTime.Now;
                    modSqlError.EXCEPTION = "获取指定sysid_name失败:" + SYSID_NAME;
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(modSqlError);
                    return false;
                }
                else
                {
                    Sys_Id = (int)sql_param.Value;
                    return true;
                }
            }
        }

        /// <summary>
        /// 根据reg_id和pat_id获取身份证信息
        /// </summary>
        /// <param name="reg_id"></param>
        /// <param name="pat_id"></param>
        /// <returns></returns>
        public string GetSFZByByPatAndReg(int reg_id, int pat_id)
        {
            string sqlcmd = string.Format(@"select b.SFZ_NO from regtopat a,pat_info b where a.PAT_ID=b.PAT_ID and a.PAT_ID='{0}' and a.REGPAT_ID='{1}'", pat_id, reg_id);
            DataTable dt = DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
            return dt.Rows.Count == 0 ? "" : dt.Rows[0][0].ToString().Trim();
        }

        public bool ExecSql(string sqlcmd)
        {
            return DbHelperMySQLZZJ.ExecuteSql(sqlcmd) > 0;
        }

        public DataTable Query(string sqlcmd)
        {
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        public DataTable Query_ZZJ(string sqlcmd)
        {
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            //判断当前索引
            if (PageIndex == 0)
                return dt;
            //从数据集合拷贝数据
            DataTable newdt = dt.Copy();
            //数据清空
            newdt.Clear();
            //开始数据索引 = 当前页-1 x 每页大小
            int rowbegin = (PageIndex - 1) * PageSize;
            //结束数据索引 = 当前页 x 每页大小
            int rowend = PageIndex * PageSize;
            //开始数据索引 大于等于 当前数据集合大小
            if (rowbegin >= dt.Rows.Count)
                return newdt;
            //结束数据索引 大于 当前数据集合大小
            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            //遍历数据
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }

        // /// <summary>
        // /// 读取身份证
        // /// </summary>
        // /// <param name="sqlcmd"></param>
        // /// <returns></returns>
        //public string GETBLOBDATA(string sqlcmd)
        //{
        //    DbHelperMySQLZZJ.exe(sqlcmd).Tables[0];
        //}
    }
}