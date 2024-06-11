using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Log.Core.Model;
using System.Data;
using System.Configuration;

namespace Log.Core.MySQLDAL
{
	/// <summary>
	/// 数据访问类:alipaylog
	/// </summary>
	public partial class DalAlipaylog
	{
        public DalAlipaylog()
		{}

        public bool alipaylog_insert(string OrderID, string seller_id, decimal Je, string trade_no, string trade_status, DateTime Now, string BType, string DateSend, string DateRe, out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionStringLogMySQL"]))
            {
                string sp_Name = @"alipaylog_insert";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("OrderID_1", OrderID);
                sqlcmd.Parameters.AddWithValue("seller_id_1", seller_id);
                sqlcmd.Parameters.AddWithValue("Je_1", Je);
                sqlcmd.Parameters.AddWithValue("trade_no_1", trade_no);
                sqlcmd.Parameters.AddWithValue("trade_status_1", trade_status);
                sqlcmd.Parameters.AddWithValue("Now_1", Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sqlcmd.Parameters.AddWithValue("BType_1", BType);
                sqlcmd.Parameters.AddWithValue("DateSend_1", DateSend);
                sqlcmd.Parameters.AddWithValue("DateRe_1", DateRe);

                MySqlParameter sql_param = sqlcmd.Parameters.Add("RESULT", MySqlDbType.Int32);
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
        /// <summary>
        /// 验证是否已经支付成功
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="Sys_Id"></param>
        /// <returns></returns>
        public bool PROC_ZF_CHECK(string OrderID,out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                string sp_Name = @"PROC_ZF_CHECK";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("OrderID_1", OrderID);
                MySqlParameter sql_param = sqlcmd.Parameters.Add("RESULT", MySqlDbType.Int32);
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

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(ModAlipaylog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into alipaylog(");
			strSql.Append("OrderID,seller_id,Je,trade_no,trade_status,Now,BType,DateSend,DateRe)");
			strSql.Append(" values (");
			strSql.Append("@OrderID,@seller_id,@Je,@trade_no,@trade_status,@Now,@BType,@DateSend,@DateRe)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@seller_id", MySqlDbType.VarChar,16),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@trade_no", MySqlDbType.VarChar,64),
					new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
			parameters[0].Value = model.OrderID;
			parameters[1].Value = model.seller_id;
			parameters[2].Value = model.Je;
			parameters[3].Value = model.trade_no;
			parameters[4].Value = model.trade_status;
			parameters[5].Value = model.Now;
			parameters[6].Value = model.BType;
			parameters[7].Value = model.DateSend;
			parameters[8].Value = model.DateRe;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(ModAlipaylog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update alipaylog set ");
			strSql.Append("OrderID=@OrderID,");
			strSql.Append("seller_id=@seller_id,");
			strSql.Append("Je=@Je,");
			strSql.Append("trade_no=@trade_no,");
			strSql.Append("trade_status=@trade_status,");
			strSql.Append("Now=@Now,");
			strSql.Append("BType=@BType,");
			strSql.Append("DateSend=@DateSend,");
			strSql.Append("DateRe=@DateRe");
			strSql.Append(" where ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@seller_id", MySqlDbType.VarChar,16),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@trade_no", MySqlDbType.VarChar,64),
					new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
			parameters[0].Value = model.OrderID;
			parameters[1].Value = model.seller_id;
			parameters[2].Value = model.Je;
			parameters[3].Value = model.trade_no;
			parameters[4].Value = model.trade_status;
			parameters[5].Value = model.Now;
			parameters[6].Value = model.BType;
			parameters[7].Value = model.DateSend;
			parameters[8].Value = model.DateRe;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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

