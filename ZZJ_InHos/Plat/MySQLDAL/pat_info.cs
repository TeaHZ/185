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
    /// 数据访问类:pat_info
    /// </summary>
    public partial class pat_info : Ipat_info
    {
        public pat_info()
        { }

        #region BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQLZZJ.GetMaxID("PAT_ID", "pat_info");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PAT_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pat_info");
            strSql.Append(" where PAT_ID=@PAT_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAT_ID;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_info(");
            strSql.Append("PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@SFZ_NO,@PAT_NAME,@SEX,@BIRTHDAY,@ADDRESS,@MOBILE_NO,@GUARDIAN_NAME,@GUARDIAN_SFZ_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME,@OPER_TIME,@NOTE)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SEX", MySqlDbType.VarChar,6),
                    new MySqlParameter("@BIRTHDAY", MySqlDbType.VarChar,10),
                    new MySqlParameter("@ADDRESS", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MOBILE_NO", MySqlDbType.VarChar,15),
                    new MySqlParameter("@GUARDIAN_NAME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@GUARDIAN_SFZ_NO", MySqlDbType.VarChar,10),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOTE", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.SFZ_NO;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTHDAY;
            parameters[5].Value = model.ADDRESS;
            parameters[6].Value = model.MOBILE_NO;
            parameters[7].Value = model.GUARDIAN_NAME;
            parameters[8].Value = model.GUARDIAN_SFZ_NO;
            parameters[9].Value = model.CREATE_TIME;
            parameters[10].Value = model.MARK_DEL;
            parameters[11].Value = model.DEL_TIME;
            parameters[12].Value = model.OPER_TIME;
            parameters[13].Value = model.NOTE;

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
        /// 增加一条数据
        /// </summary>
        public bool AddZZJ(Plat.Model.pat_info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_info(");
            strSql.Append("PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE,YB_CARDNO)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@SFZ_NO,@PAT_NAME,@SEX,@BIRTHDAY,@ADDRESS,@MOBILE_NO,@GUARDIAN_NAME,@GUARDIAN_SFZ_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME,@OPER_TIME,@NOTE,@YB_CARDNO)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SEX", MySqlDbType.VarChar,6),
                    new MySqlParameter("@BIRTHDAY", MySqlDbType.VarChar,10),
                    new MySqlParameter("@ADDRESS", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MOBILE_NO", MySqlDbType.VarChar,15),
                    new MySqlParameter("@GUARDIAN_NAME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@GUARDIAN_SFZ_NO", MySqlDbType.VarChar,10),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOTE", MySqlDbType.VarChar,50),
                    new MySqlParameter("@YB_CARDNO", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.SFZ_NO;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTHDAY;
            parameters[5].Value = model.ADDRESS;
            parameters[6].Value = model.MOBILE_NO;
            parameters[7].Value = model.GUARDIAN_NAME;
            parameters[8].Value = model.GUARDIAN_SFZ_NO;
            parameters[9].Value = model.CREATE_TIME;
            parameters[10].Value = model.MARK_DEL;
            parameters[11].Value = model.DEL_TIME;
            parameters[12].Value = model.OPER_TIME;
            parameters[13].Value = model.NOTE;
            parameters[14].Value = model.YB_CARDNO;

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
        public bool Update(Plat.Model.pat_info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pat_info set ");
            strSql.Append("SFZ_NO=@SFZ_NO,");
            strSql.Append("PAT_NAME=@PAT_NAME,");
            strSql.Append("SEX=@SEX,");
            strSql.Append("BIRTHDAY=@BIRTHDAY,");
            strSql.Append("ADDRESS=@ADDRESS,");
            strSql.Append("MOBILE_NO=@MOBILE_NO,");
            strSql.Append("GUARDIAN_NAME=@GUARDIAN_NAME,");
            strSql.Append("GUARDIAN_SFZ_NO=@GUARDIAN_SFZ_NO,");
            strSql.Append("CREATE_TIME=@CREATE_TIME,");
            strSql.Append("MARK_DEL=@MARK_DEL,");
            strSql.Append("DEL_TIME=@DEL_TIME,");
            strSql.Append("OPER_TIME=@OPER_TIME,");
            strSql.Append("NOTE=@NOTE");
            strSql.Append(" where PAT_ID=@PAT_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SEX", MySqlDbType.VarChar,6),
                    new MySqlParameter("@BIRTHDAY", MySqlDbType.VarChar,10),
                    new MySqlParameter("@ADDRESS", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MOBILE_NO", MySqlDbType.VarChar,15),
                    new MySqlParameter("@GUARDIAN_NAME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@GUARDIAN_SFZ_NO", MySqlDbType.VarChar,10),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOTE", MySqlDbType.VarChar,50),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11)};
            parameters[0].Value = model.SFZ_NO;
            parameters[1].Value = model.PAT_NAME;
            parameters[2].Value = model.SEX;
            parameters[3].Value = model.BIRTHDAY;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.MOBILE_NO;
            parameters[6].Value = model.GUARDIAN_NAME;
            parameters[7].Value = model.GUARDIAN_SFZ_NO;
            parameters[8].Value = model.CREATE_TIME;
            parameters[9].Value = model.MARK_DEL;
            parameters[10].Value = model.DEL_TIME;
            parameters[11].Value = model.OPER_TIME;
            parameters[12].Value = model.NOTE;
            parameters[13].Value = model.PAT_ID;

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
        public bool Delete(int PAT_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_info ");
            strSql.Append(" where PAT_ID=@PAT_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAT_ID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string PAT_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_info ");
            strSql.Append(" where PAT_ID in (" + PAT_IDlist + ")  ");
            int rows = DbHelperMySQLZZJ.ExecuteSql(strSql.ToString());
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
        public Plat.Model.pat_info GetModel(int PAT_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE from pat_info ");
            strSql.Append(" where PAT_ID=@PAT_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAT_ID;

            Plat.Model.pat_info model = new Plat.Model.pat_info();
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
        public Plat.Model.pat_info DataRowToModel(DataRow row)
        {
            Plat.Model.pat_info model = new Plat.Model.pat_info();
            if (row != null)
            {
                if (row["PAT_ID"] != null && row["PAT_ID"].ToString() != "")
                {
                    model.PAT_ID = int.Parse(row["PAT_ID"].ToString());
                }
                if (row["SFZ_NO"] != null)
                {
                    model.SFZ_NO = row["SFZ_NO"].ToString();
                }
                if (row["PAT_NAME"] != null)
                {
                    model.PAT_NAME = row["PAT_NAME"].ToString();
                }
                if (row["SEX"] != null)
                {
                    model.SEX = row["SEX"].ToString();
                }
                if (row["BIRTHDAY"] != null)
                {
                    model.BIRTHDAY = row["BIRTHDAY"].ToString();
                }
                if (row["ADDRESS"] != null)
                {
                    model.ADDRESS = row["ADDRESS"].ToString();
                }
                if (row["MOBILE_NO"] != null)
                {
                    model.MOBILE_NO = row["MOBILE_NO"].ToString();
                }
                if (row["GUARDIAN_NAME"] != null)
                {
                    model.GUARDIAN_NAME = row["GUARDIAN_NAME"].ToString();
                }
                if (row["GUARDIAN_SFZ_NO"] != null)
                {
                    model.GUARDIAN_SFZ_NO = row["GUARDIAN_SFZ_NO"].ToString();
                }
                if (row["CREATE_TIME"] != null && row["CREATE_TIME"].ToString() != "")
                {
                    model.CREATE_TIME = DateTime.Parse(row["CREATE_TIME"].ToString());
                }
                if (row["MARK_DEL"] != null && row["MARK_DEL"].ToString() != "")
                {
                    if ((row["MARK_DEL"].ToString() == "1") || (row["MARK_DEL"].ToString().ToLower() == "true"))
                    {
                        model.MARK_DEL = true;
                    }
                    else
                    {
                        model.MARK_DEL = false;
                    }
                }
                if (row["DEL_TIME"] != null && row["DEL_TIME"].ToString() != "")
                {
                    model.DEL_TIME = DateTime.Parse(row["DEL_TIME"].ToString());
                }
                if (row["OPER_TIME"] != null && row["OPER_TIME"].ToString() != "")
                {
                    model.OPER_TIME = DateTime.Parse(row["OPER_TIME"].ToString());
                }
                if (row["NOTE"] != null)
                {
                    model.NOTE = row["NOTE"].ToString();
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
            strSql.Append("select PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE ");
            strSql.Append(" FROM pat_info ");
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
            strSql.Append("select count(1) FROM pat_info ");
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
                strSql.Append("order by T.PAT_ID desc");
            }
            strSql.Append(")AS Row, T.*  from pat_info T ");
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
            parameters[0].Value = "pat_info";
            parameters[1].Value = "PAT_ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperMySQLZZJ.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion BasicMethod

        /// <summary>
        ///根据身份证和注册ID获取持卡人信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public DataTable GetBySfzAndRegID(string SFZ_NO, int REGPAT_ID)
        {
            DataTable dtReturn = DbHelperMySQLZZJ.Query(string.Format(@" select * from regtopat a,register_pat b,pat_info c
            where a.PAT_ID=c.PAT_ID and a.REGPAT_ID=b.REGPAT_ID
            and c.SFZ_NO='{0}' and b.REGPAT_ID={1}", SFZ_NO, REGPAT_ID)).Tables[0];
            return dtReturn;
        }

        /// <summary>
        /// 根据据注册人ID和终端唯一码获取持卡人信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetBySnAndReg(string lTERMINAL_SN, int REGPAT_ID)
        {
            DataTable dtReturn = DbHelperMySQLZZJ.Query(string.Format(@" select c.PAT_ID,c.SFZ_NO,c.MOBILE_NO,c.PAT_NAME,c.SEX,
c.BIRTHDAY,c.ADDRESS,c.GUARDIAN_NAME,c.GUARDIAN_SFZ_NO from regtopat a,register_pat b,pat_info c
            where a.PAT_ID=c.PAT_ID and a.REGPAT_ID=b.REGPAT_ID
            and a.lTERMINAL_SN='{0}' and b.REGPAT_ID={1}", lTERMINAL_SN, REGPAT_ID)).Tables[0];
            return dtReturn;
        }

        /// <summary>
        /// 根据注册人ID获取持卡人列表信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetByRegID(int REGPAT_ID)
        {
            DataTable dtReturn = DbHelperMySQLZZJ.Query(string.Format(@" select c.PAT_ID,c.SFZ_NO,c.MOBILE_NO,c.PAT_NAME,c.SEX,
c.BIRTHDAY,c.ADDRESS,c.GUARDIAN_NAME,c.GUARDIAN_SFZ_NO from regtopat a,register_pat b,pat_info c
            where a.PAT_ID=c.PAT_ID and a.REGPAT_ID=b.REGPAT_ID
            and b.REGPAT_ID={0} and c.MARK_DEL=0", REGPAT_ID)).Tables[0];
            return dtReturn;
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        { return null; }

        /// <summary>
        /// 根据注册人ID判断持卡人是否存在
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public bool Exists(int PAT_ID, int REGPAT_ID)
        {
            DataTable dtReturn = DbHelperMySQLZZJ.Query(string.Format(@" select 1 from regtopat a,register_pat b,pat_info c
            where a.PAT_ID=c.PAT_ID and a.REGPAT_ID=b.REGPAT_ID
            and c.pat_id='{0}' and b.REGPAT_ID={1}", PAT_ID, REGPAT_ID)).Tables[0];
            return dtReturn.Rows.Count > 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_info(");
            strSql.Append("PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@SFZ_NO,@PAT_NAME,@SEX,@BIRTHDAY,@ADDRESS,@MOBILE_NO,@GUARDIAN_NAME,@GUARDIAN_SFZ_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME,@OPER_TIME,@NOTE)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SEX", MySqlDbType.VarChar,6),
                    new MySqlParameter("@BIRTHDAY", MySqlDbType.VarChar,10),
                    new MySqlParameter("@ADDRESS", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MOBILE_NO", MySqlDbType.VarChar,15),
                    new MySqlParameter("@GUARDIAN_NAME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@GUARDIAN_SFZ_NO", MySqlDbType.VarChar,10),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOTE", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.SFZ_NO;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTHDAY;
            parameters[5].Value = model.ADDRESS;
            parameters[6].Value = model.MOBILE_NO;
            parameters[7].Value = model.GUARDIAN_NAME;
            parameters[8].Value = model.GUARDIAN_SFZ_NO;
            parameters[9].Value = model.CREATE_TIME;
            parameters[10].Value = model.MARK_DEL;
            parameters[11].Value = model.DEL_TIME;
            parameters[12].Value = model.OPER_TIME;
            parameters[13].Value = model.NOTE;

            System.Collections.Hashtable table = new System.Collections.Hashtable();
            table.Add(strSql.ToString(), parameters);

            strSql = new StringBuilder();
            strSql.Append("insert into regtopat(");
            strSql.Append("REGPAT_ID,PAT_ID,BAND_TIME,MARK_DEL,DEL_TIME,lTERMINAL_SN,DEL_lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@REGPAT_ID,@PAT_ID,@BAND_TIME,@MARK_DEL,@DEL_TIME,@lTERMINAL_SN,@DEL_lTERMINAL_SN)");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@BAND_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@DEL_lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters1[0].Value = regtopat.REGPAT_ID;
            parameters1[1].Value = regtopat.PAT_ID;
            parameters1[2].Value = regtopat.BAND_TIME;
            parameters1[3].Value = regtopat.MARK_DEL;
            parameters1[4].Value = regtopat.DEL_TIME;
            parameters1[5].Value = regtopat.lTERMINAL_SN;
            parameters1[6].Value = regtopat.DEL_lTERMINAL_SN;

            table.Add(strSql.ToString(), parameters1);

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
                    modSqlError.TYPE = "添加持卡人失败";
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

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddByTran_ZZJ(Plat.Model.pat_info model, Plat.Model.pat_card card, Plat.Model.pat_card_bind bind)
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_info(");
            strSql.Append("PAT_ID,SFZ_NO,PAT_NAME,SEX,BIRTHDAY,ADDRESS,MOBILE_NO,GUARDIAN_NAME,GUARDIAN_SFZ_NO,CREATE_TIME,MARK_DEL,DEL_TIME,OPER_TIME,NOTE,YB_CARDNO,SMK_CARDNO,XNH_CARDNO)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@SFZ_NO,@PAT_NAME,@SEX,@BIRTHDAY,@ADDRESS,@MOBILE_NO,@GUARDIAN_NAME,@GUARDIAN_SFZ_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME,@OPER_TIME,@NOTE,@YB_CARDNO,@SMK_CARDNO,@XNH_CARDNO)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SEX", MySqlDbType.VarChar,6),
                    new MySqlParameter("@BIRTHDAY", MySqlDbType.VarChar,10),
                    new MySqlParameter("@ADDRESS", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MOBILE_NO", MySqlDbType.VarChar,15),
                    new MySqlParameter("@GUARDIAN_NAME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@GUARDIAN_SFZ_NO", MySqlDbType.VarChar,10),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@NOTE", MySqlDbType.VarChar,50),
                    new MySqlParameter("@YB_CARDNO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SMK_CARDNO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@XNH_CARDNO", MySqlDbType.VarChar,30),
            new MySqlParameter("@HOS_CARDNO", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.SFZ_NO;
            parameters[2].Value = model.PAT_NAME;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTHDAY;
            parameters[5].Value = model.ADDRESS;
            parameters[6].Value = model.MOBILE_NO;
            parameters[7].Value = model.GUARDIAN_NAME;
            parameters[8].Value = model.GUARDIAN_SFZ_NO;
            parameters[9].Value = model.CREATE_TIME;
            parameters[10].Value = model.MARK_DEL;
            parameters[11].Value = model.DEL_TIME;
            parameters[12].Value = model.OPER_TIME;
            parameters[13].Value = model.NOTE;
            parameters[14].Value = model.YB_CARDNO;
            parameters[15].Value = model.SMK_CARDNO;
            parameters[16].Value = model.XNH_CARDNO;
            parameters[17].Value = model.HOS_CARDNO;

            table.Add(strSql.ToString(), parameters);

            strSql = new StringBuilder();
            strSql.Append("insert into pat_card(");
            strSql.Append("PAT_ID,YLCARTD_TYPE,YLCARD_NO,CREATE_TIME,MARK_DEL,DEL_TIME)");
            strSql.Append(" values (");
            strSql.Append("@PAT_ID,@YLCARTD_TYPE,@YLCARD_NO,@CREATE_TIME,@MARK_DEL,@DEL_TIME)");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CREATE_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime)};
            parameters1[0].Value = card.PAT_ID;
            parameters1[1].Value = card.YLCARTD_TYPE;
            parameters1[2].Value = card.YLCARD_NO;
            parameters1[3].Value = card.CREATE_TIME;
            parameters1[4].Value = card.MARK_DEL;
            parameters1[5].Value = card.DEL_TIME;

            table.Add(strSql.ToString(), parameters1);

            strSql = new StringBuilder();

            strSql.Append("insert into pat_card_bind(");
            strSql.Append("HOS_ID,PAT_ID,YLCARTD_TYPE,YLCARD_NO,MARK_BIND,BAND_TIME)");
            strSql.Append(" values (");
            strSql.Append("@HOS_ID,@PAT_ID,@YLCARTD_TYPE,@YLCARD_NO,@MARK_BIND,@BAND_TIME)");
            MySqlParameter[] parameters2 = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@YLCARTD_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@YLCARD_NO", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MARK_BIND", MySqlDbType.Int16,4),
                    new MySqlParameter("@BAND_TIME", MySqlDbType.DateTime)};
            parameters2[0].Value = bind.HOS_ID;
            parameters2[1].Value = bind.PAT_ID;
            parameters2[2].Value = bind.YLCARTD_TYPE;
            parameters2[3].Value = bind.YLCARD_NO;
            parameters2[4].Value = bind.MARK_BIND;
            parameters2[5].Value = bind.BAND_TIME;

            table.Add(strSql.ToString(), parameters2);

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
                    modSqlError.TYPE = "添加持卡人失败";
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

        public bool DeleteByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat, Plat.Model.pat_card card)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pat_info set DEL_TIME=@DEL_TIME,MARK_DEL=@MARK_DEL,OPER_TIME=@OPER_TIME");
            strSql.Append(" where PAT_ID=@PAT_ID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@OPER_TIME", MySqlDbType.DateTime)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.MARK_DEL;
            parameters[2].Value = model.DEL_TIME;
            parameters[3].Value = model.OPER_TIME;

            System.Collections.Hashtable table = new System.Collections.Hashtable();
            table.Add(strSql.ToString(), parameters);

            strSql = new StringBuilder();
            strSql.Append("update regtopat set MARK_DEL=@MARK_DEL,DEL_lTERMINAL_SN=@DEL_lTERMINAL_SN,DEL_TIME=@DEL_TIME");
            strSql.Append(" where PAT_ID=@PAT_ID and REGPAT_ID=@REGPAT_ID");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@MARK_DEL", MySqlDbType.Bit),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters1[0].Value = regtopat.REGPAT_ID;
            parameters1[1].Value = regtopat.PAT_ID;
            parameters1[2].Value = regtopat.MARK_DEL;
            parameters1[3].Value = regtopat.DEL_TIME;
            parameters1[4].Value = regtopat.DEL_lTERMINAL_SN;

            table.Add(strSql.ToString(), parameters1);

            strSql = new StringBuilder();
            strSql.Append("update pat_card set ");
            strSql.Append("MARK_DEL=@MARK_DEL,");
            strSql.Append("DEL_TIME=@DEL_TIME");
            strSql.Append(" where PAT_ID=@PAT_ID");
            MySqlParameter[] parameters2 = {
                    new MySqlParameter("@MARK_DEL", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11)};
            parameters2[0].Value = card.MARK_DEL;
            parameters2[1].Value = card.DEL_TIME;
            parameters2[2].Value = card.PAT_ID;

            table.Add(strSql.ToString(), parameters2);

            try
            {
                DbHelperMySQLZZJ.ExecuteSqlTran(table);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}