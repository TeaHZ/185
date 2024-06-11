using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.MySQLDAL
{
    /// <summary>
    /// 数据访问类:pat_card
    /// </summary>
    public partial class pat_card : Ipat_card
    {
        public pat_card()
        { }

        #region BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQLZZJ.GetMaxID("PAT_ID", "pat_card");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pat_card");
            strSql.Append(" where PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = PAT_ID;
            parameters[1].Value = YLCARTD_TYPE;
            parameters[2].Value = YLCARD_NO;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_card model)
        {
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
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.YLCARTD_TYPE;
            parameters[2].Value = model.YLCARD_NO;
            parameters[3].Value = model.CREATE_TIME;
            parameters[4].Value = model.MARK_DEL;
            parameters[5].Value = model.DEL_TIME;

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
        public bool Update(Plat.Model.pat_card model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pat_card set ");
            strSql.Append("CREATE_TIME=@CREATE_TIME,");
            strSql.Append("MARK_DEL=@MARK_DEL,");
            strSql.Append("DEL_TIME=@DEL_TIME");
            strSql.Append(" where PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.CREATE_TIME;
            parameters[1].Value = model.MARK_DEL;
            parameters[2].Value = model.DEL_TIME;
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
        public bool Delete(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_card ");
            strSql.Append(" where PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = PAT_ID;
            parameters[1].Value = YLCARTD_TYPE;
            parameters[2].Value = YLCARD_NO;

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
        public Plat.Model.pat_card GetModel(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PAT_ID,YLCARTD_TYPE,YLCARD_NO,CREATE_TIME,MARK_DEL,DEL_TIME from pat_card ");
            strSql.Append(" where PAT_ID=@PAT_ID and YLCARTD_TYPE=@YLCARTD_TYPE and YLCARD_NO=@YLCARD_NO ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30)            };
            parameters[0].Value = PAT_ID;
            parameters[1].Value = YLCARTD_TYPE;
            parameters[2].Value = YLCARD_NO;

            Plat.Model.pat_card model = new Plat.Model.pat_card();
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
        public Plat.Model.pat_card DataRowToModel(DataRow row)
        {
            Plat.Model.pat_card model = new Plat.Model.pat_card();
            if (row != null)
            {
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
                if (row["CREATE_TIME"] != null && row["CREATE_TIME"].ToString() != "")
                {
                    model.CREATE_TIME = DateTime.Parse(row["CREATE_TIME"].ToString());
                }
                if (row["MARK_DEL"] != null)
                {
                    model.MARK_DEL = row["MARK_DEL"].ToString();
                }
                if (row["DEL_TIME"] != null && row["DEL_TIME"].ToString() != "")
                {
                    model.DEL_TIME = DateTime.Parse(row["DEL_TIME"].ToString());
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
            strSql.Append("select PAT_ID,YLCARTD_TYPE,YLCARD_NO,CREATE_TIME,MARK_DEL,DEL_TIME ");
            strSql.Append(" FROM pat_card ");
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
            strSql.Append("select count(1) FROM pat_card ");
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
            strSql.Append(")AS Row, T.*  from pat_card T ");
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
            parameters[0].Value = "pat_card";
            parameters[1].Value = "YLCARD_NO";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperMySQLZZJ.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        /// <summary>
        /// 判断医疗卡是否存在
        /// </summary>
        /// <param name="pat_id">用户代码</param>
        /// <param name="card_no">卡号</param>
        /// <param name="card_type">卡类别</param>
        /// <param name="hsp_id">医院代码</hsp_id>
        /// <returns></returns>
        public bool Exists(string pat_id, string card_no, int card_type, string hsp_id)
        {
            string sqlcmd = string.Format(@"");
            if (hsp_id != "")
            {
                sqlcmd = string.Format(@" select 1 from pat_card_bind a,pat_info b
            where a.PAT_ID=b.PAT_ID and a.HOS_ID='{0}'
            and a.YLCARTD_TYPE='{1}' and a.YLCARD_NO='{2}' and b.pat_id='{3}'
            ", hsp_id, card_type, card_no, pat_id);
            }
            else
            {
                sqlcmd = string.Format(@"select 1 from pat_card a,pat_info b
                where a.PAT_ID=b.PAT_ID
                and a.YLCARTD_TYPE='{0}' and a.YLCARD_NO='{1}' and b.pat_id='{2}' and a.MARK_DEL='0'", card_type, card_no, pat_id);
            }
            DataTable dtReturn = DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];

            return dtReturn.Rows.Count > 0;
        }

        public DataTable GetListBydPatID(int PAT_ID)
        {
            string sqlcmd = string.Format(@"SELECT PAT_ID,YLCARTD_TYPE,YLCARD_NO from pat_card where pat_id={0} and mark_del=0", PAT_ID);
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        /// <summary>
        /// 获取注册人所有对应卡号
        /// </summary>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetListBydREGPATID(int REGPAT_ID)
        {
            string sqlcmd = string.Format(@"SELECT a.PAT_ID,c.PAT_NAME,YLCARTD_TYPE,YLCARD_NO,C.MOBILE_NO from pat_card a,regtopat b,pat_info c where  a.pat_id=c.pat_id and a.pat_id=b.pat_id and a.MARK_DEL=0
and REGPAT_ID={0} order by a.pat_id", REGPAT_ID);
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }

        public DataTable GetListBydREGPATID(int REGPAT_ID, string HOS_ID)
        {
            string sqlcmd = string.Format(@"SELECT a.PAT_ID,c.PAT_NAME,YLCARTD_TYPE,YLCARD_NO,C.MOBILE_NO from pat_card_bind a,regtopat b,pat_info c where  a.pat_id=c.pat_id and a.pat_id=b.pat_id and a.MARK_bind=1
and REGPAT_ID={0} order by a.pat_id", REGPAT_ID);
            return DbHelperMySQLZZJ.Query(sqlcmd).Tables[0];
        }
    }
}