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
    /// 数据访问类:compaylog
    /// </summary>
    public partial class DalCompaylog
    {
        public DalCompaylog()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ModCompaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into compaylog(");
            strSql.Append("ORDERID,JE,BTYPE,ACCOUNT,REFUND_STATUS,RETURN_CODE,DATERE,DATESEND,now)");
            strSql.Append(" values (");
            strSql.Append("@ORDERID,@JE,@BTYPE,@ACCOUNT,@REFUND_STATUS,@RETURN_CODE,@DATERE,@DATESEND,@now)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
					new MySqlParameter("@JE", MySqlDbType.Decimal,10),
					new MySqlParameter("@BTYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@ACCOUNT", MySqlDbType.VarChar,32),
					new MySqlParameter("@REFUND_STATUS", MySqlDbType.VarChar,28),
					new MySqlParameter("@RETURN_CODE", MySqlDbType.VarChar,20),
					new MySqlParameter("@DATERE", MySqlDbType.Text),
					new MySqlParameter("@DATESEND", MySqlDbType.Text),
					new MySqlParameter("@now", MySqlDbType.DateTime)};
            parameters[0].Value = model.ORDERID;
            parameters[1].Value = model.JE;
            parameters[2].Value = model.BTYPE;
            parameters[3].Value = model.ACCOUNT;
            parameters[4].Value = model.REFUND_STATUS;
            parameters[5].Value = model.RETURN_CODE;
            parameters[6].Value = model.DATERE;
            parameters[7].Value = model.DATESEND;
            parameters[8].Value = model.now;

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
        public bool Update(ModCompaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update compaylog set ");
            strSql.Append("ORDERID=@ORDERID,");
            strSql.Append("JE=@JE,");
            strSql.Append("BTYPE=@BTYPE,");
            strSql.Append("ACCOUNT=@ACCOUNT,");
            strSql.Append("REFUND_STATUS=@REFUND_STATUS,");
            strSql.Append("RETURN_CODE=@RETURN_CODE,");
            strSql.Append("DATERE=@DATERE,");
            strSql.Append("DATESEND=@DATESEND,");
            strSql.Append("now=@now");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
					new MySqlParameter("@JE", MySqlDbType.Decimal,10),
					new MySqlParameter("@BTYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@ACCOUNT", MySqlDbType.VarChar,32),
					new MySqlParameter("@REFUND_STATUS", MySqlDbType.VarChar,28),
					new MySqlParameter("@RETURN_CODE", MySqlDbType.VarChar,20),
					new MySqlParameter("@DATERE", MySqlDbType.Text),
					new MySqlParameter("@DATESEND", MySqlDbType.Text),
					new MySqlParameter("@now", MySqlDbType.DateTime)};
            parameters[0].Value = model.ORDERID;
            parameters[1].Value = model.JE;
            parameters[2].Value = model.BTYPE;
            parameters[3].Value = model.ACCOUNT;
            parameters[4].Value = model.REFUND_STATUS;
            parameters[5].Value = model.RETURN_CODE;
            parameters[6].Value = model.DATERE;
            parameters[7].Value = model.DATESEND;
            parameters[8].Value = model.now;

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
            strSql.Append("delete from compaylog ");
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
        public ModCompaylog GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ORDERID,JE,BTYPE,ACCOUNT,REFUND_STATUS,RETURN_CODE,DATERE,DATESEND,now from compaylog ");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
			};

            ModCompaylog model = new ModCompaylog();
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
        public ModCompaylog DataRowToModel(DataRow row)
        {
            ModCompaylog model = new ModCompaylog();
            if (row != null)
            {
                if (row["ORDERID"] != null)
                {
                    model.ORDERID = row["ORDERID"].ToString();
                }
                if (row["JE"] != null && row["JE"].ToString() != "")
                {
                    model.JE = decimal.Parse(row["JE"].ToString());
                }
                if (row["BTYPE"] != null)
                {
                    model.BTYPE = row["BTYPE"].ToString();
                }
                if (row["ACCOUNT"] != null)
                {
                    model.ACCOUNT = row["ACCOUNT"].ToString();
                }
                if (row["REFUND_STATUS"] != null)
                {
                    model.REFUND_STATUS = row["REFUND_STATUS"].ToString();
                }
                if (row["RETURN_CODE"] != null)
                {
                    model.RETURN_CODE = row["RETURN_CODE"].ToString();
                }
                if (row["DATERE"] != null)
                {
                    model.DATERE = row["DATERE"].ToString();
                }
                if (row["DATESEND"] != null)
                {
                    model.DATESEND = row["DATESEND"].ToString();
                }
                if (row["now"] != null && row["now"].ToString() != "")
                {
                    model.now = DateTime.Parse(row["now"].ToString());
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
            strSql.Append("select ORDERID,JE,BTYPE,ACCOUNT,REFUND_STATUS,RETURN_CODE,DATERE,DATESEND,now ");
            strSql.Append(" FROM compaylog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
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
            strSql.Append(")AS Row, T.*  from compaylog T ");
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
            parameters[0].Value = "compaylog";
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

