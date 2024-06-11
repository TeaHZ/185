using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Log.Core.Model;
namespace Log.Core.MySQLDAL
{
	/// <summary>
	/// 数据访问类:platmonitorlog_correct
	/// </summary>
	public partial class Dalplatmonitorlog_correct
	{
		public Dalplatmonitorlog_correct()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("ID", "platmonitorlog_correct"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from platmonitorlog_correct");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Modplatmonitorlog_correct model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into platmonitorlog_correct(");
			strSql.Append("ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,ACC_TIME,NOW,DEAL_RESULT)");
			strSql.Append(" values (");
			strSql.Append("@ID,@HOS_ID,@PAT_NAME,@SFZ_NO,@M_TYPE,@ACC_TIME,@NOW,@DEAL_RESULT)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11),
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
					new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
					new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
					new MySqlParameter("@M_TYPE", MySqlDbType.VarChar,30),
					new MySqlParameter("@ACC_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@NOW", MySqlDbType.DateTime),
					new MySqlParameter("@DEAL_RESULT", MySqlDbType.VarChar,100)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.HOS_ID;
			parameters[2].Value = model.PAT_NAME;
			parameters[3].Value = model.SFZ_NO;
			parameters[4].Value = model.M_TYPE;
			parameters[5].Value = model.ACC_TIME;
			parameters[6].Value = model.NOW;
			parameters[7].Value = model.DEAL_RESULT;

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
		public bool Update(Modplatmonitorlog_correct model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update platmonitorlog_correct set ");
			strSql.Append("HOS_ID=@HOS_ID,");
			strSql.Append("PAT_NAME=@PAT_NAME,");
			strSql.Append("SFZ_NO=@SFZ_NO,");
			strSql.Append("M_TYPE=@M_TYPE,");
			strSql.Append("ACC_TIME=@ACC_TIME,");
			strSql.Append("NOW=@NOW,");
			strSql.Append("DEAL_RESULT=@DEAL_RESULT");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
					new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
					new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
					new MySqlParameter("@M_TYPE", MySqlDbType.VarChar,30),
					new MySqlParameter("@ACC_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@NOW", MySqlDbType.DateTime),
					new MySqlParameter("@DEAL_RESULT", MySqlDbType.VarChar,100),
					new MySqlParameter("@ID", MySqlDbType.Int32,11)};
			parameters[0].Value = model.HOS_ID;
			parameters[1].Value = model.PAT_NAME;
			parameters[2].Value = model.SFZ_NO;
			parameters[3].Value = model.M_TYPE;
			parameters[4].Value = model.ACC_TIME;
			parameters[5].Value = model.NOW;
			parameters[6].Value = model.DEAL_RESULT;
			parameters[7].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from platmonitorlog_correct ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11)			};
			parameters[0].Value = ID;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from platmonitorlog_correct ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public Modplatmonitorlog_correct GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,ACC_TIME,NOW,DEAL_RESULT from platmonitorlog_correct ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,11)			};
			parameters[0].Value = ID;

            Modplatmonitorlog_correct model = new Modplatmonitorlog_correct();
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
		public Modplatmonitorlog_correct DataRowToModel(DataRow row)
		{
            Modplatmonitorlog_correct model = new Modplatmonitorlog_correct();

            if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["HOS_ID"]!=null)
				{
					model.HOS_ID=row["HOS_ID"].ToString();
				}
				if(row["PAT_NAME"]!=null)
				{
					model.PAT_NAME=row["PAT_NAME"].ToString();
				}
				if(row["SFZ_NO"]!=null)
				{
					model.SFZ_NO=row["SFZ_NO"].ToString();
				}
				if(row["M_TYPE"]!=null)
				{
					model.M_TYPE=row["M_TYPE"].ToString();
				}
				if(row["ACC_TIME"]!=null && row["ACC_TIME"].ToString()!="")
				{
					model.ACC_TIME=DateTime.Parse(row["ACC_TIME"].ToString());
				}
				if(row["NOW"]!=null && row["NOW"].ToString()!="")
				{
					model.NOW=DateTime.Parse(row["NOW"].ToString());
				}
				if(row["DEAL_RESULT"]!=null)
				{
					model.DEAL_RESULT=row["DEAL_RESULT"].ToString();
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
			strSql.Append("select ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,ACC_TIME,NOW,DEAL_RESULT ");
			strSql.Append(" FROM platmonitorlog_correct ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
            /*
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM platmonitorlog_correct ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
            */
            return 0;
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from platmonitorlog_correct T ");
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
			parameters[0].Value = "platmonitorlog_correct";
			parameters[1].Value = "ID";
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

