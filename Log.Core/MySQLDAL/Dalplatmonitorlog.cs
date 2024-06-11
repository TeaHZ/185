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
    /// 数据访问类:platmonitorlog
    /// </summary>
    public partial class Dalplatmonitorlog
    {
        public Dalplatmonitorlog()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Modplatmonitorlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into platmonitorlog(");
            strSql.Append("ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,M_LEVEL,M_CONTENT,M_ALL_CONTENT,ACC_TIME,NOW,DEAL_PERSON,DEAL_NOTICE_TIME,DEAL_CONFIRM_TIME,DEAL_REPLY,DEAL_STATUS,DEAL_RESULT,`SOURCE`,`DEAL_ID`,DEAL_TIPS)");
            strSql.Append(" values (");
            strSql.Append("@ID,@HOS_ID,@PAT_NAME,@SFZ_NO,@M_TYPE,@M_LEVEL,@M_CONTENT,@M_ALL_CONTENT,@ACC_TIME,@NOW,@DEAL_PERSON,@DEAL_NOTICE_TIME,@DEAL_CONFIRM_TIME,@DEAL_REPLY,@DEAL_STATUS,@DEAL_RESULT,@SOURCE,@DEAL_ID,@DEAL_TIPS)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@M_TYPE", MySqlDbType.VarChar,30),
                    new MySqlParameter("@M_LEVEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@M_CONTENT", MySqlDbType.VarChar,100),
                    new MySqlParameter("@M_ALL_CONTENT", MySqlDbType.VarChar,5000),
                    new MySqlParameter("@ACC_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_PERSON", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_NOTICE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_CONFIRM_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_REPLY", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_STATUS", MySqlDbType.Bit),
                    new MySqlParameter("@DEAL_RESULT", MySqlDbType.VarChar,100),
                    new MySqlParameter("@SOURCE", MySqlDbType.VarChar,30),
                    new MySqlParameter("@DEAL_ID", MySqlDbType.VarChar,300),
                    new MySqlParameter("@DEAL_TIPS", MySqlDbType.VarChar,300)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.HOS_ID;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SFZ_NO;
            parameters[4].Value = model.M_TYPE;
            parameters[5].Value = model.M_LEVEL;
            parameters[6].Value = model.M_CONTENT;
            parameters[7].Value = model.M_ALL_CONTENT;
            parameters[8].Value = model.ACC_TIME;
            parameters[9].Value = model.NOW;
            parameters[10].Value = model.DEAL_PERSON;
            parameters[11].Value = model.DEAL_NOTICE_TIME;
            parameters[12].Value = model.DEAL_CONFIRM_TIME;
            parameters[13].Value = model.DEAL_REPLY;
            parameters[14].Value = model.DEAL_STATUS;
            parameters[15].Value = model.DEAL_RESULT;
            parameters[16].Value = model.SOURCE;
            parameters[17].Value = model.DEAL_ID;
            parameters[18].Value = model.DEAL_TIPS;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Modplatmonitorlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update platmonitorlog set ");
            strSql.Append("ID=@ID,");
            strSql.Append("HOS_ID=@HOS_ID,");
            strSql.Append("PAT_NAME=@PAT_NAME,");
            strSql.Append("SFZ_NO=@SFZ_NO,");
            strSql.Append("M_TYPE=@M_TYPE,");
            strSql.Append("M_LEVEL=@M_LEVEL,");
            strSql.Append("M_CONTENT=@M_CONTENT,");
            strSql.Append("ACC_TIME=@ACC_TIME,");
            strSql.Append("NOW=@NOW,");
            strSql.Append("DEAL_PERSON=@DEAL_PERSON,");
            strSql.Append("DEAL_NOTICE_TIME=@DEAL_NOTICE_TIME,");
            strSql.Append("DEAL_CONFIRM_TIME=@DEAL_CONFIRM_TIME,");
            strSql.Append("DEAL_REPLY=@DEAL_REPLY,");
            strSql.Append("DEAL_STATUS=@DEAL_STATUS,");
            strSql.Append("DEAL_RESULT=@DEAL_RESULT");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@M_TYPE", MySqlDbType.VarChar,30),
                    new MySqlParameter("@M_LEVEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@M_CONTENT", MySqlDbType.VarChar,100),
                    new MySqlParameter("@ACC_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_PERSON", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_NOTICE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_CONFIRM_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_REPLY", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_STATUS", MySqlDbType.Bit),
                    new MySqlParameter("@DEAL_RESULT", MySqlDbType.VarChar,100)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.HOS_ID;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SFZ_NO;
            parameters[4].Value = model.M_TYPE;
            parameters[5].Value = model.M_LEVEL;
            parameters[6].Value = model.M_CONTENT;
            parameters[7].Value = model.ACC_TIME;
            parameters[8].Value = model.NOW;
            parameters[9].Value = model.DEAL_PERSON;
            parameters[10].Value = model.DEAL_NOTICE_TIME;
            parameters[11].Value = model.DEAL_CONFIRM_TIME;
            parameters[12].Value = model.DEAL_REPLY;
            parameters[13].Value = model.DEAL_STATUS;
            parameters[14].Value = model.DEAL_RESULT;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from platmonitorlog ");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
            };

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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Modplatmonitorlog GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,M_LEVEL,M_CONTENT,ACC_TIME,NOW,DEAL_PERSON,DEAL_NOTICE_TIME,DEAL_CONFIRM_TIME,DEAL_REPLY,DEAL_STATUS,DEAL_RESULT from platmonitorlog ");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
            };

            Modplatmonitorlog model = new Modplatmonitorlog();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public Modplatmonitorlog DataRowToModel(DataRow row)
        {
            Modplatmonitorlog model = new Modplatmonitorlog();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["HOS_ID"] != null)
                {
                    model.HOS_ID = row["HOS_ID"].ToString();
                }
                if (row["PAT_NAME"] != null)
                {
                    model.PAT_NAME = row["PAT_NAME"].ToString();
                }
                if (row["SFZ_NO"] != null)
                {
                    model.SFZ_NO = row["SFZ_NO"].ToString();
                }
                if (row["M_TYPE"] != null)
                {
                    model.M_TYPE = row["M_TYPE"].ToString();
                }
                if (row["M_LEVEL"] != null)
                {
                    model.M_LEVEL = row["M_LEVEL"].ToString();
                }
                if (row["M_CONTENT"] != null)
                {
                    model.M_CONTENT = row["M_CONTENT"].ToString();
                }
                if (row["ACC_TIME"] != null && row["ACC_TIME"].ToString() != "")
                {
                    model.ACC_TIME = DateTime.Parse(row["ACC_TIME"].ToString());
                }
                if (row["NOW"] != null && row["NOW"].ToString() != "")
                {
                    model.NOW = DateTime.Parse(row["NOW"].ToString());
                }
                if (row["DEAL_PERSON"] != null)
                {
                    model.DEAL_PERSON = row["DEAL_PERSON"].ToString();
                }
                if (row["DEAL_NOTICE_TIME"] != null && row["DEAL_NOTICE_TIME"].ToString() != "")
                {
                    model.DEAL_NOTICE_TIME = DateTime.Parse(row["DEAL_NOTICE_TIME"].ToString());
                }
                if (row["DEAL_CONFIRM_TIME"] != null && row["DEAL_CONFIRM_TIME"].ToString() != "")
                {
                    model.DEAL_CONFIRM_TIME = DateTime.Parse(row["DEAL_CONFIRM_TIME"].ToString());
                }
                if (row["DEAL_REPLY"] != null && row["DEAL_REPLY"].ToString() != "")
                {
                    model.DEAL_REPLY = DateTime.Parse(row["DEAL_REPLY"].ToString());
                }
                if (row["DEAL_STATUS"] != null && row["DEAL_STATUS"].ToString() != "")
                {
                    if ((row["DEAL_STATUS"].ToString() == "1") || (row["DEAL_STATUS"].ToString().ToLower() == "true"))
                    {
                        model.DEAL_STATUS = true;
                    }
                    else
                    {
                        model.DEAL_STATUS = false;
                    }
                }
                if (row["DEAL_RESULT"] != null)
                {
                    model.DEAL_RESULT = row["DEAL_RESULT"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,HOS_ID,PAT_NAME,SFZ_NO,M_TYPE,M_LEVEL,M_CONTENT,ACC_TIME,NOW,DEAL_PERSON,DEAL_NOTICE_TIME,DEAL_CONFIRM_TIME,DEAL_REPLY,DEAL_STATUS,DEAL_RESULT ");
            strSql.Append(" FROM platmonitorlog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            /*
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM platmonitorlog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }*/
            return 0;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from platmonitorlog T ");
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
			parameters[0].Value = "platmonitorlog";
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

