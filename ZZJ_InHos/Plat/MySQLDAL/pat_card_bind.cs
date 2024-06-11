using DB.Core;
using Log.Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.MySQLDAL
{
    /// <summary>
    /// 数据访问类:pat_card_bind
    /// </summary>
    public partial class pat_card_bind : Ipat_card_bind
    {
        public pat_card_bind()
        { }

        #region BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQLZZJ.GetMaxID("PAT_ID", "pat_card_bind");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pat_card_bind");
            strSql.Append(" where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = PAT_ID;
            parameters[2].Value = YLCARTD_TYPE;
            parameters[3].Value = YLCARD_NO;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_card_bind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_card_bind(");
            strSql.Append("HOS_ID,PAT_ID,YLCARTD_TYPE,YLCARD_NO,MARK_BIND,BAND_TIME)");
            strSql.Append(" values (");
            strSql.Append("@HOS_ID,@PAT_ID,@YLCARTD_TYPE,@YLCARD_NO,@MARK_BIND,@BAND_TIME)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MARK_BIND", MySqlDbType.Int16,4),
                    new MySqlParameter("@BAND_TIME", MySqlDbType.DateTime)};
            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.YLCARTD_TYPE;
            parameters[3].Value = model.YLCARD_NO;
            parameters[4].Value = model.MARK_BIND;
            parameters[5].Value = model.BAND_TIME;

            int rows = DbHelperMySQLZZJ.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Update(Plat.Model.pat_card_bind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pat_card_bind set ");
            strSql.Append("MARK_BIND=@MARK_BIND,");
            strSql.Append("BAND_TIME=@BAND_TIME");
            strSql.Append(" where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@MARK_BIND", MySqlDbType.Int16,4),
                    new MySqlParameter("@BAND_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.MARK_BIND;
            parameters[1].Value = model.BAND_TIME;
            parameters[2].Value = model.HOS_ID;
            parameters[3].Value = model.PAT_ID;
            parameters[4].Value = model.YLCARTD_TYPE;
            parameters[5].Value = model.YLCARD_NO;

            int rows = DbHelperMySQLZZJ.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_card_bind ");
            strSql.Append(" where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = PAT_ID;
            parameters[2].Value = YLCARTD_TYPE;
            parameters[3].Value = YLCARD_NO;

            int rows = DbHelperMySQLZZJ.ExecuteSql(strSql.ToString(), parameters);
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
        public Plat.Model.pat_card_bind GetModel(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HOS_ID,PAT_ID,YLCARTD_TYPE,YLCARD_NO,MARK_BIND,BAND_TIME from pat_card_bind ");
            strSql.Append(" where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = PAT_ID;
            parameters[2].Value = YLCARTD_TYPE;
            parameters[3].Value = YLCARD_NO;

            Plat.Model.pat_card_bind model = new Plat.Model.pat_card_bind();
            DataSet ds = DbHelperMySQLZZJ.Query(strSql.ToString(), parameters);
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
        public Plat.Model.pat_card_bind DataRowToModel(DataRow row)
        {
            Plat.Model.pat_card_bind model = new Plat.Model.pat_card_bind();
            if (row != null)
            {
                if (row["HOS_ID"] != null)
                {
                    model.HOS_ID = row["HOS_ID"].ToString();
                }
                if (row["PAT_ID"] != null && row["PAT_ID"].ToString() != "")
                {
                    model.PAT_ID = int.Parse(row["PAT_ID"].ToString());
                }
                if (row["YLCARTD_TYPE"] != null && row["YLCARTD_TYPE"].ToString() != "")
                {
                    model.YLCARTD_TYPE = int.Parse(row["YLCARTD_TYPE"].ToString());
                }
                if (row["YLCARD_NO"] != null)
                {
                    model.YLCARD_NO = row["YLCARD_NO"].ToString();
                }
                if (row["MARK_BIND"] != null && row["MARK_BIND"].ToString() != "")
                {
                    model.MARK_BIND = int.Parse(row["MARK_BIND"].ToString());
                }
                if (row["BAND_TIME"] != null && row["BAND_TIME"].ToString() != "")
                {
                    model.BAND_TIME = DateTime.Parse(row["BAND_TIME"].ToString());
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
            strSql.Append("select HOS_ID,PAT_ID,YLCARTD_TYPE,YLCARD_NO,MARK_BIND,BAND_TIME ");
            strSql.Append(" FROM pat_card_bind ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQLZZJ.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM pat_card_bind ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQLZZJ.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
                strSql.Append("order by T.YLCARD_NO desc");
            }
            strSql.Append(")AS Row, T.*  from pat_card_bind T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQLZZJ.Query(strSql.ToString());
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
            parameters[0].Value = "pat_card_bind";
            parameters[1].Value = "YLCARD_NO";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperMySQLZZJ.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        #region ExtensionMethod

        public bool AddByTran(Plat.Model.pat_card_bind bind, Plat.Model.pat_card card, Plat.Model.pat_card old)
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_card(");
            strSql.Append("PAT_ID,YLCARTD_TYPE,YLCARD_NO,CREATE_TIME,MARK_DEL,DEL_TIME)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@YLCARTD_TYPE,@YLCARD_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime)};
            parameters[0].Value = card.PAT_ID;
            parameters[1].Value = card.YLCARTD_TYPE;
            parameters[2].Value = card.YLCARD_NO;
            parameters[3].Value = card.CREATE_TIME;
            parameters[4].Value = card.MARK_DEL;
            parameters[5].Value = card.DEL_TIME;

            table.Add(strSql.ToString(), parameters);

            strSql = new StringBuilder();
            strSql.Append("insert into pat_card_bind(");
            strSql.Append("HOS_ID,PAT_ID,YLCARTD_TYPE,YLCARD_NO,MARK_BIND,BAND_TIME)");
            strSql.Append(" values (");
            strSql.Append("@HOS_ID,@PAT_ID,@YLCARTD_TYPE,@YLCARD_NO,@MARK_BIND,@BAND_TIME)");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MARK_BIND", MySqlDbType.Int16,4),
                    new MySqlParameter("@BAND_TIME", MySqlDbType.DateTime)};
            parameters1[0].Value = bind.HOS_ID;
            parameters1[1].Value = bind.PAT_ID;
            parameters1[2].Value = bind.YLCARTD_TYPE;
            parameters1[3].Value = bind.YLCARD_NO;
            parameters1[4].Value = bind.MARK_BIND;
            parameters1[5].Value = bind.BAND_TIME;

            table.Add(strSql.ToString(), parameters1);

            if (old != null)
            {
                strSql = new StringBuilder();
                strSql.Append("update pat_card set ");
                strSql.Append("MARK_DEL=@MARK_DEL,");
                strSql.Append("DEL_TIME=@DEL_TIME");
                strSql.Append(" where PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@MARK_DEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)};
                parameters2[0].Value = old.MARK_DEL;
                parameters2[1].Value = old.DEL_TIME;
                parameters2[2].Value = old.PAT_ID;
                parameters2[3].Value = old.YLCARTD_TYPE;
                parameters2[4].Value = old.YLCARD_NO;
                table.Add(strSql.ToString(), parameters2);
            }
            try
            {
                DbHelperMySQLZZJ.ExecuteSqlTran(table);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    ModSqlError modSqlError = new ModSqlError();
                    modSqlError.TYPE = "预约预办卡回写";
                    modSqlError.time = DateTime.Now;
                    modSqlError.EXCEPTION = ex.ToString().Replace("'", "\"");
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(modSqlError);
                }
                catch
                {
                }
                return false;
            }
        }

        #endregion ExtensionMethod
    }
}