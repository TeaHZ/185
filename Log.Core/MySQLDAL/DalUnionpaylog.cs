using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Log.Core.Model;
using System.Configuration;

namespace Log.Core.MySQLDAL
{
	/// <summary>
    /// 数据访问类:DalUnionpaylog
	/// </summary>
	public partial class DalUnionpaylog
	{
        public DalUnionpaylog()
		{}
		#region  BasicMethod
        public bool unionpaylog_insert(string orderId, string merId, string txnTime, string respCode, string queryId, DateTime now, string BType, string DateRe, string DateSend, out int Sys_Id)
        {
            Sys_Id = -1;
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionStringLogMySQL"]))
            {
                string sp_Name = @"unionpaylog_insert";

                if (conn.State != ConnectionState.Open) conn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("orderId_1", orderId);
                sqlcmd.Parameters.AddWithValue("merId_1", merId);
                sqlcmd.Parameters.AddWithValue("txnTime_1", txnTime);
                sqlcmd.Parameters.AddWithValue("respCode_1", respCode);
                sqlcmd.Parameters.AddWithValue("queryId_1", queryId);
                sqlcmd.Parameters.AddWithValue("now_1", now.ToString("yyyy-MM-dd HH:mm:ss"));
                sqlcmd.Parameters.AddWithValue("BType_1", BType);
                sqlcmd.Parameters.AddWithValue("DateRe_1", DateRe);
                sqlcmd.Parameters.AddWithValue("DateSend_1", DateSend);

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
		public bool Add(ModUnionpaylog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into unionpaylog(");
			strSql.Append("orderId,merId,txnTime,respCode,queryId,now,BType,DateRe,DateSend)");
			strSql.Append(" values (");
			strSql.Append("@orderId,@merId,@txnTime,@respCode,@queryId,@now,@BType,@DateRe,@DateSend)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@orderId", MySqlDbType.VarChar,20),
					new MySqlParameter("@merId", MySqlDbType.VarChar,20),
					new MySqlParameter("@txnTime", MySqlDbType.VarChar,16),
					new MySqlParameter("@respCode", MySqlDbType.VarChar,4),
					new MySqlParameter("@queryId", MySqlDbType.VarChar,30),
					new MySqlParameter("@now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateRe", MySqlDbType.Text),
					new MySqlParameter("@DateSend", MySqlDbType.Text)};
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.merId;
			parameters[2].Value = model.txnTime;
			parameters[3].Value = model.respCode;
			parameters[4].Value = model.queryId;
			parameters[5].Value = model.now;
			parameters[6].Value = model.BType;
			parameters[7].Value = model.DateRe;
			parameters[8].Value = model.DateSend;

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
		public bool Update(ModUnionpaylog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update unionpaylog set ");
			strSql.Append("orderId=@orderId,");
			strSql.Append("merId=@merId,");
			strSql.Append("txnTime=@txnTime,");
			strSql.Append("respCode=@respCode,");
			strSql.Append("queryId=@queryId,");
			strSql.Append("now=@now,");
			strSql.Append("BType=@BType,");
			strSql.Append("DateRe=@DateRe,");
			strSql.Append("DateSend=@DateSend");
			strSql.Append(" where ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@orderId", MySqlDbType.VarChar,20),
					new MySqlParameter("@merId", MySqlDbType.VarChar,20),
					new MySqlParameter("@txnTime", MySqlDbType.VarChar,16),
					new MySqlParameter("@respCode", MySqlDbType.VarChar,4),
					new MySqlParameter("@queryId", MySqlDbType.VarChar,30),
					new MySqlParameter("@now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateRe", MySqlDbType.Text),
					new MySqlParameter("@DateSend", MySqlDbType.Text)};
			parameters[0].Value = model.orderId;
			parameters[1].Value = model.merId;
			parameters[2].Value = model.txnTime;
			parameters[3].Value = model.respCode;
			parameters[4].Value = model.queryId;
			parameters[5].Value = model.now;
			parameters[6].Value = model.BType;
			parameters[7].Value = model.DateRe;
			parameters[8].Value = model.DateSend;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from unionpaylog ");
			strSql.Append(" where ");
			MySqlParameter[] parameters = {
			};

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
		/// 得到一个对象实体
		/// </summary>
		public ModUnionpaylog GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select orderId,merId,txnTime,respCode,queryId,now,BType,DateRe,DateSend from unionpaylog ");
			strSql.Append(" where ");
			MySqlParameter[] parameters = {
			};

			ModUnionpaylog model=new ModUnionpaylog();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ModUnionpaylog DataRowToModel(DataRow row)
		{
			ModUnionpaylog model=new ModUnionpaylog();
			if (row != null)
			{
				if(row["orderId"]!=null)
				{
					model.orderId=row["orderId"].ToString();
				}
				if(row["merId"]!=null)
				{
					model.merId=row["merId"].ToString();
				}
				if(row["txnTime"]!=null)
				{
					model.txnTime=row["txnTime"].ToString();
				}
				if(row["respCode"]!=null)
				{
					model.respCode=row["respCode"].ToString();
				}
				if(row["queryId"]!=null)
				{
					model.queryId=row["queryId"].ToString();
				}
				if(row["now"]!=null && row["now"].ToString()!="")
				{
					model.now=DateTime.Parse(row["now"].ToString());
				}
				if(row["BType"]!=null)
				{
					model.BType=row["BType"].ToString();
				}
				if(row["DateRe"]!=null)
				{
					model.DateRe=row["DateRe"].ToString();
				}
				if(row["DateSend"]!=null)
				{
					model.DateSend=row["DateSend"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select orderId,merId,txnTime,respCode,queryId,now,BType,DateRe,DateSend ");
			strSql.Append(" FROM unionpaylog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from unionpaylog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "unionpaylog";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

