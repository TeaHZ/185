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
    /// 数据访问类:pat_prepay
    /// </summary>
    public partial class pat_prepay : Ipat_prepay
    {
        public pat_prepay()
        { }

        #region BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQLZZJ.GetMaxID("PAY_ID", "pat_prepay");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PAY_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pat_prepay");
            strSql.Append(" where PAY_ID=@PAY_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAY_ID;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_prepay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_prepay(");
            strSql.Append("PAY_ID,PAT_ID,HOS_ID,HOS_PAT_ID,REGPAT_ID,HOS_PAY_SN,CASH_JE,DJ_TIME,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@PAT_ID,@HOS_ID,@HOS_PAT_ID,@REGPAT_ID,@HOS_PAY_SN,@CASH_JE,@DJ_TIME,@lTERMINAL_SN)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@HOS_PAT_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_PAY_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAY_ID;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.HOS_ID;
            parameters[3].Value = model.HOS_PAT_ID;
            parameters[4].Value = model.REGPAT_ID;
            parameters[5].Value = model.HOS_PAY_SN;
            parameters[6].Value = model.CASH_JE;
            parameters[7].Value = model.DJ_TIME;
            parameters[8].Value = model.lTERMINAL_SN;

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
        public bool Update(Plat.Model.pat_prepay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pat_prepay set ");
            strSql.Append("PAT_ID=@PAT_ID,");
            strSql.Append("HOS_ID=@HOS_ID,");
            strSql.Append("HOS_PAT_ID=@HOS_PAT_ID,");
            strSql.Append("REGPAT_ID=@REGPAT_ID,");
            strSql.Append("HOS_PAY_SN=@HOS_PAY_SN,");
            strSql.Append("CASH_JE=@CASH_JE,");
            strSql.Append("DJ_TIME=@DJ_TIME,");
            strSql.Append("lTERMINAL_SN=@lTERMINAL_SN");
            strSql.Append(" where PAY_ID=@PAY_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@HOS_PAT_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_PAY_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11)};
            parameters[0].Value = model.PAT_ID;
            parameters[1].Value = model.HOS_ID;
            parameters[2].Value = model.HOS_PAT_ID;
            parameters[3].Value = model.REGPAT_ID;
            parameters[4].Value = model.HOS_PAY_SN;
            parameters[5].Value = model.CASH_JE;
            parameters[6].Value = model.DJ_TIME;
            parameters[7].Value = model.lTERMINAL_SN;
            parameters[8].Value = model.PAY_ID;

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
        public bool Delete(int PAY_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_prepay ");
            strSql.Append(" where PAY_ID=@PAY_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAY_ID;

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
        public bool DeleteList(string PAY_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from pat_prepay ");
            strSql.Append(" where PAY_ID in (" + PAY_IDlist + ")  ");
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
        public Plat.Model.pat_prepay GetModel(int PAY_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PAY_ID,PAT_ID,HOS_ID,HOS_PAT_ID,REGPAT_ID,HOS_PAY_SN,CASH_JE,DJ_TIME,lTERMINAL_SN from pat_prepay ");
            strSql.Append(" where PAY_ID=@PAY_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11)         };
            parameters[0].Value = PAY_ID;

            Plat.Model.pat_prepay model = new Plat.Model.pat_prepay();
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
        public Plat.Model.pat_prepay DataRowToModel(DataRow row)
        {
            Plat.Model.pat_prepay model = new Plat.Model.pat_prepay();
            if (row != null)
            {
                if (row["PAY_ID"] != null && row["PAY_ID"].ToString() != "")
                {
                    model.PAY_ID = int.Parse(row["PAY_ID"].ToString());
                }
                if (row["PAT_ID"] != null && row["PAT_ID"].ToString() != "")
                {
                    model.PAT_ID = int.Parse(row["PAT_ID"].ToString());
                }
                if (row["HOS_ID"] != null)
                {
                    model.HOS_ID = row["HOS_ID"].ToString();
                }
                if (row["HOS_PAT_ID"] != null)
                {
                    model.HOS_PAT_ID = row["HOS_PAT_ID"].ToString();
                }
                if (row["REGPAT_ID"] != null && row["REGPAT_ID"].ToString() != "")
                {
                    model.REGPAT_ID = int.Parse(row["REGPAT_ID"].ToString());
                }
                if (row["HOS_PAY_SN"] != null)
                {
                    model.HOS_PAY_SN = row["HOS_PAY_SN"].ToString();
                }
                if (row["CASH_JE"] != null && row["CASH_JE"].ToString() != "")
                {
                    model.CASH_JE = decimal.Parse(row["CASH_JE"].ToString());
                }
                if (row["DJ_TIME"] != null && row["DJ_TIME"].ToString() != "")
                {
                    model.DJ_TIME = DateTime.Parse(row["DJ_TIME"].ToString());
                }
                if (row["lTERMINAL_SN"] != null)
                {
                    model.lTERMINAL_SN = row["lTERMINAL_SN"].ToString();
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
            strSql.Append("select PAY_ID,PAT_ID,HOS_ID,HOS_PAT_ID,REGPAT_ID,HOS_PAY_SN,CASH_JE,DJ_TIME,lTERMINAL_SN ");
            strSql.Append(" FROM pat_prepay ");
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
            strSql.Append("select count(1) FROM pat_prepay ");
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
                strSql.Append("order by T.PAY_ID desc");
            }
            strSql.Append(")AS Row, T.*  from pat_prepay T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQLZZJ.Query(strSql.ToString());
        }

        #endregion BasicMethod

        #region ExtensionMethod

        public bool AddByTran(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_prepay(");
            strSql.Append("PAY_ID,PAT_ID,HOS_ID,HOS_PAT_ID,REGPAT_ID,HOS_PAY_SN,CASH_JE,DJ_TIME,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@PAT_ID,@HOS_ID,@HOS_PAT_ID,@REGPAT_ID,@HOS_PAY_SN,@CASH_JE,@DJ_TIME,@lTERMINAL_SN)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_PAT_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_PAY_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAY_ID;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.HOS_ID;
            parameters[3].Value = model.HOS_PAT_ID;
            parameters[4].Value = model.REGPAT_ID;
            parameters[5].Value = model.HOS_PAY_SN;
            parameters[6].Value = model.CASH_JE;
            parameters[7].Value = model.DJ_TIME;
            parameters[8].Value = model.lTERMINAL_SN;

            System.Collections.Hashtable table = new System.Collections.Hashtable();
            table.Add(strSql.ToString(), parameters);

            //现金交易记录表
            strSql = new StringBuilder();
            strSql.Append("insert into pay_info(");
            strSql.Append("PAY_ID,HOS_ID,PAT_ID,REGPAT_ID,BIZ_TYPE,BIZ_SN,CASH_JE,SFZ_NO,DJ_DATE,DJ_TIME,DEAL_TYPE,DEAL_STATES,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@HOS_ID,@PAT_ID,@REGPAT_ID,@BIZ_TYPE,@BIZ_SN,@CASH_JE,@SFZ_NO,@DJ_DATE,@DJ_TIME,@DEAL_TYPE,@DEAL_STATES,@lTERMINAL_SN)");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@REGPAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_DATE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.VarChar,8),
                    new MySqlParameter("@DEAL_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,10),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters1[0].Value = info.PAY_ID;
            parameters1[1].Value = info.HOS_ID;
            parameters1[2].Value = info.PAT_ID;
            parameters1[3].Value = info.REGPAT_ID;
            parameters1[4].Value = info.BIZ_TYPE;
            parameters1[5].Value = info.BIZ_SN;
            parameters1[6].Value = info.CASH_JE;
            parameters1[7].Value = info.SFZ_NO;
            parameters1[8].Value = info.DJ_DATE;
            parameters1[9].Value = info.DJ_TIME;
            parameters1[10].Value = info.DEAL_TYPE;
            parameters1[11].Value = info.DEAL_STATES;
            parameters1[12].Value = info.lTERMINAL_SN;

            table.Add(strSql, parameters1);

            if (zfb != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_zfb(");
                strSql.Append("PAY_ID,BIZ_TYPE,BIZ_SN,SELLER_ID,COMM_SN,JE,DEAL_STATES,TXN_TYPE,DEAL_TIME,lTERMINAL_SN)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BIZ_SN,@SELLER_ID,@COMM_SN,@JE,@DEAL_STATES,@TXN_TYPE,@DEAL_TIME,@lTERMINAL_SN)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SELLER_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,20),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
                parameters2[0].Value = zfb.PAY_ID;
                parameters2[1].Value = zfb.BIZ_TYPE;
                parameters2[2].Value = zfb.BIZ_SN;
                parameters2[3].Value = zfb.SELLER_ID;
                parameters2[4].Value = zfb.COMM_SN;
                parameters2[5].Value = zfb.JE;
                parameters2[6].Value = zfb.DEAL_STATES;
                parameters2[7].Value = zfb.TXN_TYPE;
                parameters2[8].Value = zfb.DEAL_TIME;
                parameters2[9].Value = zfb.lTERMINAL_SN;

                table.Add(strSql, parameters2);
            }
            else if (wc != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_wc(");
                strSql.Append("PAY_ID,WECHAT,PAY_TYPE,BIZ_TYPE,BIZ_SN,COMM_SN,JE,COMM_NAME,DEAL_STATES,DEAL_TIME,DEAL_SN,lTERMINAL_SN,TNX_TYPE)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@WECHAT,@PAY_TYPE,@BIZ_TYPE,@BIZ_SN,@COMM_SN,@JE,@COMM_NAME,@DEAL_STATES,@DEAL_TIME,@DEAL_SN,@lTERMINAL_SN,@TNX_TYPE)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@WECHAT", MySqlDbType.VarChar,10),
                    new MySqlParameter("@PAY_TYPE", MySqlDbType.VarChar,20),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@COMM_NAME", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,20),
                    new MySqlParameter("@DEAL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_SN", MySqlDbType.VarChar,50),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TNX_TYPE", MySqlDbType.VarChar,10)};
                parameters2[0].Value = wc.PAY_ID;
                parameters2[1].Value = wc.WECHAT;
                parameters2[2].Value = wc.PAY_TYPE;
                parameters2[3].Value = wc.BIZ_TYPE;
                parameters2[4].Value = wc.BIZ_SN;
                parameters2[5].Value = wc.COMM_SN;
                parameters2[6].Value = wc.JE;
                parameters2[7].Value = wc.COMM_NAME;
                parameters2[8].Value = wc.DEAL_STATES;
                parameters2[9].Value = wc.DEAL_TIME;
                parameters2[10].Value = wc.DEAL_SN;
                parameters2[11].Value = wc.lTERMINAL_SN;
                parameters2[12].Value = wc.TNX_TYPE;

                table.Add(strSql, parameters2);
            }
            else if (bank != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_bank(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,BANK_TYPE,RETURN_CODE,BANK_CARD,CARD_TYPE,SEARCH_CODE,REFCODE,TERMCODE,CARD_COMPAY,COMM_NAME,COMM_SN,SFZ_NO,DJ_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@BANK_TYPE,@RETURN_CODE,@BANK_CARD,@CARD_TYPE,@SEARCH_CODE,@REFCODE,@TERMCODE,@CARD_COMPAY,@COMM_NAME,@COMM_SN,@SFZ_NO,@DJ_TIME)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@BANK_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@RETURN_CODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@BANK_CARD", MySqlDbType.VarChar,19),
                    new MySqlParameter("@CARD_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@SEARCH_CODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@REFCODE", MySqlDbType.VarChar,6),
                    new MySqlParameter("@TERMCODE", MySqlDbType.VarChar,15),
                    new MySqlParameter("@CARD_COMPAY", MySqlDbType.VarChar,20),
                    new MySqlParameter("@COMM_NAME", MySqlDbType.VarChar,50),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,50),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime)};
                parameters2[0].Value = bank.PAY_ID;
                parameters2[1].Value = bank.BIZ_TYPE;
                parameters2[2].Value = bank.BDj_id;
                parameters2[3].Value = bank.JE;
                parameters2[4].Value = bank.BANK_TYPE;
                parameters2[5].Value = bank.RETURN_CODE;
                parameters2[6].Value = bank.BANK_CARD;
                parameters2[7].Value = bank.CARD_TYPE;
                parameters2[8].Value = bank.SEARCH_CODE;
                parameters2[9].Value = bank.REFCODE;
                parameters2[10].Value = bank.TERMCODE;
                parameters2[11].Value = bank.CARD_COMPAY;
                parameters2[12].Value = bank.COMM_NAME;
                parameters2[13].Value = bank.COMM_SN;
                parameters2[14].Value = bank.SFZ_NO;
                parameters2[15].Value = bank.DJ_TIME;
                table.Add(strSql, parameters2);
            }
            else if (upcap != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_upcap(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,TXN_TYPE,MERID,ORDERID,TN,QUERYID,REFCODE,TERMCODE,SFZ_NO,DJ_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@TXN_TYPE,@MERID,@ORDERID,@TN,@QUERYID,@REFCODE,@TERMCODE,@SFZ_NO,@DJ_TIME)");
                MySqlParameter[] parameters6 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TN", MySqlDbType.VarChar,21),
                    new MySqlParameter("@QUERYID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@REFCODE", MySqlDbType.VarChar,6),
                    new MySqlParameter("@TERMCODE", MySqlDbType.VarChar,15),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime)};
                parameters6[0].Value = upcap.PAY_ID;
                parameters6[1].Value = upcap.BIZ_TYPE;
                parameters6[2].Value = upcap.BDj_id;
                parameters6[3].Value = upcap.JE;
                parameters6[4].Value = upcap.TXN_TYPE;
                parameters6[5].Value = upcap.MERID;
                parameters6[6].Value = upcap.ORDERID;
                parameters6[7].Value = upcap.TN;
                parameters6[8].Value = upcap.QUERYID;
                parameters6[9].Value = upcap.REFCODE;
                parameters6[10].Value = upcap.TERMCODE;
                parameters6[11].Value = upcap.SFZ_NO;
                parameters6[12].Value = upcap.DJ_TIME;
                table.Add(strSql, parameters6);
            }
            else if (ccb != null)
            {
                //strSql = new StringBuilder();
                //strSql.Append("insert into pay_info_ccb(");
                //strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,POSID,BRANCHID,ORDERID,SFZ_NO,DJ_TIME,TXN_TYPE)");
                //strSql.Append(" values (");
                //strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@POSID,@BRANCHID,@ORDERID,@SFZ_NO,@DJ_TIME,@TXN_TYPE)");
                //MySqlParameter[] parameters7 = {
                //    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                //    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                //    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                //    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                //    new MySqlParameter("@POSID", MySqlDbType.VarChar,9),
                //    new MySqlParameter("@BRANCHID", MySqlDbType.VarChar,9),
                //    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                //    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                //    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                //    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10)};
                //parameters7[0].Value = ccb.PAY_ID;
                //parameters7[1].Value = ccb.BIZ_TYPE;
                //parameters7[2].Value = ccb.BDj_id;
                //parameters7[3].Value = ccb.JE;
                //parameters7[4].Value = ccb.POSID;
                //parameters7[5].Value = ccb.BRANCHID;
                //parameters7[6].Value = ccb.ORDERID;
                //parameters7[7].Value = ccb.SFZ_NO;
                //parameters7[8].Value = ccb.DJ_TIME;
                //parameters7[9].Value = ccb.TXN_TYPE;

                //table.Add(strSql, parameters7);

                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_ccb(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,POSID,BRANCHID,ORDERID,SFZ_NO,DJ_TIME,TXN_TYPE,MerchantID)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@POSID,@BRANCHID,@ORDERID,@SFZ_NO,@DJ_TIME,@TXN_TYPE,@MerchantID)");
                MySqlParameter[] parameters7 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@POSID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@BRANCHID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@MerchantID", MySqlDbType.VarChar,20)};
                parameters7[0].Value = ccb.PAY_ID;
                parameters7[1].Value = ccb.BIZ_TYPE;
                parameters7[2].Value = ccb.BDj_id;
                parameters7[3].Value = ccb.JE;
                parameters7[4].Value = ccb.POSID;
                parameters7[5].Value = ccb.BRANCHID;
                parameters7[6].Value = ccb.ORDERID;
                parameters7[7].Value = ccb.SFZ_NO;
                parameters7[8].Value = ccb.DJ_TIME;
                parameters7[9].Value = ccb.TXN_TYPE;
                parameters7[10].Value = ccb.MerchantID;
                table.Add(strSql, parameters7);
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
                    modSqlError.TYPE = "预交金支付保存";
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
        /// 住院预交金保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="info"></param>
        /// <param name="zfb"></param>
        /// <param name="wc"></param>
        /// <param name="bank"></param>
        /// <param name="upcap"></param>
        /// <param name="ccb"></param>
        /// <returns></returns>
        public bool AddByTran_ZZJ(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pat_prepay(");
            strSql.Append("PAY_ID,PAT_ID,HOS_ID,HOS_PAT_ID,USER_ID,HOS_PAY_SN,CASH_JE,DJ_TIME,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@PAT_ID,@HOS_ID,@HOS_PAT_ID,@USER_ID,@HOS_PAY_SN,@CASH_JE,@DJ_TIME,@lTERMINAL_SN)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@HOS_PAT_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@USER_ID", MySqlDbType.VarChar,11),
                    new MySqlParameter("@HOS_PAY_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAY_ID;
            parameters[1].Value = model.PAT_ID;
            parameters[2].Value = model.HOS_ID;
            parameters[3].Value = model.HOS_PAT_ID;
            parameters[4].Value = model.USER_ID;
            parameters[5].Value = model.HOS_PAY_SN;
            parameters[6].Value = model.CASH_JE;
            parameters[7].Value = model.DJ_TIME;
            parameters[8].Value = model.lTERMINAL_SN;

            System.Collections.Hashtable table = new System.Collections.Hashtable();
            table.Add(strSql.ToString(), parameters);

            //现金交易记录表
            strSql = new StringBuilder();
            strSql.Append("insert into pay_info(");
            strSql.Append("PAY_ID,HOS_ID,PAT_ID,USER_ID,BIZ_TYPE,BIZ_SN,CASH_JE,SFZ_NO,DJ_DATE,DJ_TIME,DEAL_TYPE,DEAL_STATES,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@HOS_ID,@PAT_ID,@USER_ID,@BIZ_TYPE,@BIZ_SN,@CASH_JE,@SFZ_NO,@DJ_DATE,@DJ_TIME,@DEAL_TYPE,@DEAL_STATES,@lTERMINAL_SN)");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@USER_ID", MySqlDbType.VarChar,11),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_DATE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.VarChar,8),
                    new MySqlParameter("@DEAL_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,10),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters1[0].Value = info.PAY_ID;
            parameters1[1].Value = info.HOS_ID;
            parameters1[2].Value = info.PAT_ID;
            parameters1[3].Value = info.USER_ID;
            parameters1[4].Value = info.BIZ_TYPE;
            parameters1[5].Value = info.BIZ_SN;
            parameters1[6].Value = info.CASH_JE;
            parameters1[7].Value = info.SFZ_NO;
            parameters1[8].Value = info.DJ_DATE;
            parameters1[9].Value = info.DJ_TIME;
            parameters1[10].Value = info.DEAL_TYPE;
            parameters1[11].Value = info.DEAL_STATES;
            parameters1[12].Value = info.lTERMINAL_SN;

            table.Add(strSql, parameters1);

            if (zfb != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_zfb(");
                strSql.Append("PAY_ID,BIZ_TYPE,BIZ_SN,SELLER_ID,COMM_SN,JE,DEAL_STATES,TXN_TYPE,DEAL_TIME,lTERMINAL_SN)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BIZ_SN,@SELLER_ID,@COMM_SN,@JE,@DEAL_STATES,@TXN_TYPE,@DEAL_TIME,@lTERMINAL_SN)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SELLER_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,20),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DEAL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
                parameters2[0].Value = zfb.PAY_ID;
                parameters2[1].Value = zfb.BIZ_TYPE;
                parameters2[2].Value = zfb.BIZ_SN;
                parameters2[3].Value = zfb.SELLER_ID;
                parameters2[4].Value = zfb.COMM_SN;
                parameters2[5].Value = zfb.JE;
                parameters2[6].Value = zfb.DEAL_STATES;
                parameters2[7].Value = zfb.TXN_TYPE;
                parameters2[8].Value = zfb.DEAL_TIME;
                parameters2[9].Value = zfb.lTERMINAL_SN;

                table.Add(strSql, parameters2);
            }
            else if (wc != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_wc(");
                strSql.Append("PAY_ID,WECHAT,PAY_TYPE,BIZ_TYPE,BIZ_SN,COMM_SN,JE,COMM_NAME,DEAL_STATES,DEAL_TIME,DEAL_SN,lTERMINAL_SN,TNX_TYPE)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@WECHAT,@PAY_TYPE,@BIZ_TYPE,@BIZ_SN,@COMM_SN,@JE,@COMM_NAME,@DEAL_STATES,@DEAL_TIME,@DEAL_SN,@lTERMINAL_SN,@TNX_TYPE)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@WECHAT", MySqlDbType.VarChar,10),
                    new MySqlParameter("@PAY_TYPE", MySqlDbType.VarChar,20),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BIZ_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@COMM_NAME", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DEAL_STATES", MySqlDbType.VarChar,20),
                    new MySqlParameter("@DEAL_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@DEAL_SN", MySqlDbType.VarChar,50),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TNX_TYPE", MySqlDbType.VarChar,10)};
                parameters2[0].Value = wc.PAY_ID;
                parameters2[1].Value = wc.WECHAT;
                parameters2[2].Value = wc.PAY_TYPE;
                parameters2[3].Value = wc.BIZ_TYPE;
                parameters2[4].Value = wc.BIZ_SN;
                parameters2[5].Value = wc.COMM_SN;
                parameters2[6].Value = wc.JE;
                parameters2[7].Value = wc.COMM_NAME;
                parameters2[8].Value = wc.DEAL_STATES;
                parameters2[9].Value = wc.DEAL_TIME;
                parameters2[10].Value = wc.DEAL_SN;
                parameters2[11].Value = wc.lTERMINAL_SN;
                parameters2[12].Value = wc.TNX_TYPE;

                table.Add(strSql, parameters2);
            }
            else if (bank != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_bank(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,BANK_TYPE,RETURN_CODE,BANK_CARD,CARD_TYPE,SEARCH_CODE,REFCODE,TERMCODE,CARD_COMPAY,COMM_NAME,COMM_SN,SFZ_NO,DJ_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@BANK_TYPE,@RETURN_CODE,@BANK_CARD,@CARD_TYPE,@SEARCH_CODE,@REFCODE,@TERMCODE,@CARD_COMPAY,@COMM_NAME,@COMM_SN,@SFZ_NO,@DJ_TIME)");
                MySqlParameter[] parameters2 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@BANK_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@RETURN_CODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@BANK_CARD", MySqlDbType.VarChar,19),
                    new MySqlParameter("@CARD_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@SEARCH_CODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@REFCODE", MySqlDbType.VarChar,6),
                    new MySqlParameter("@TERMCODE", MySqlDbType.VarChar,15),
                    new MySqlParameter("@CARD_COMPAY", MySqlDbType.VarChar,20),
                    new MySqlParameter("@COMM_NAME", MySqlDbType.VarChar,50),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,50),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime)};
                parameters2[0].Value = bank.PAY_ID;
                parameters2[1].Value = bank.BIZ_TYPE;
                parameters2[2].Value = bank.BDj_id;
                parameters2[3].Value = bank.JE;
                parameters2[4].Value = bank.BANK_TYPE;
                parameters2[5].Value = bank.RETURN_CODE;
                parameters2[6].Value = bank.BANK_CARD;
                parameters2[7].Value = bank.CARD_TYPE;
                parameters2[8].Value = bank.SEARCH_CODE;
                parameters2[9].Value = bank.REFCODE;
                parameters2[10].Value = bank.TERMCODE;
                parameters2[11].Value = bank.CARD_COMPAY;
                parameters2[12].Value = bank.COMM_NAME;
                parameters2[13].Value = bank.COMM_SN;
                parameters2[14].Value = bank.SFZ_NO;
                parameters2[15].Value = bank.DJ_TIME;
                table.Add(strSql, parameters2);
            }
            else if (upcap != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_upcap(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,TXN_TYPE,MERID,ORDERID,TN,QUERYID,REFCODE,TERMCODE,SFZ_NO,DJ_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@TXN_TYPE,@MERID,@ORDERID,@TN,@QUERYID,@REFCODE,@TERMCODE,@SFZ_NO,@DJ_TIME)");
                MySqlParameter[] parameters6 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TN", MySqlDbType.VarChar,21),
                    new MySqlParameter("@QUERYID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@REFCODE", MySqlDbType.VarChar,6),
                    new MySqlParameter("@TERMCODE", MySqlDbType.VarChar,15),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime)};
                parameters6[0].Value = upcap.PAY_ID;
                parameters6[1].Value = upcap.BIZ_TYPE;
                parameters6[2].Value = upcap.BDj_id;
                parameters6[3].Value = upcap.JE;
                parameters6[4].Value = upcap.TXN_TYPE;
                parameters6[5].Value = upcap.MERID;
                parameters6[6].Value = upcap.ORDERID;
                parameters6[7].Value = upcap.TN;
                parameters6[8].Value = upcap.QUERYID;
                parameters6[9].Value = upcap.REFCODE;
                parameters6[10].Value = upcap.TERMCODE;
                parameters6[11].Value = upcap.SFZ_NO;
                parameters6[12].Value = upcap.DJ_TIME;
                table.Add(strSql, parameters6);
            }
            else if (ccb != null)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into pay_info_ccb(");
                strSql.Append("PAY_ID,BIZ_TYPE,BDj_id,JE,POSID,BRANCHID,ORDERID,SFZ_NO,DJ_TIME,TXN_TYPE,MerchantID)");
                strSql.Append(" values (");
                strSql.Append("@PAY_ID,@BIZ_TYPE,@BDj_id,@JE,@POSID,@BRANCHID,@ORDERID,@SFZ_NO,@DJ_TIME,@TXN_TYPE,@MerchantID)");
                MySqlParameter[] parameters7 = {
                    new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@BIZ_TYPE", MySqlDbType.Int16,4),
                    new MySqlParameter("@BDj_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@POSID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@BRANCHID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,18),
                    new MySqlParameter("@DJ_TIME", MySqlDbType.DateTime),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@MerchantID", MySqlDbType.VarChar,20)};
                parameters7[0].Value = ccb.PAY_ID;
                parameters7[1].Value = ccb.BIZ_TYPE;
                parameters7[2].Value = ccb.BDj_id;
                parameters7[3].Value = ccb.JE;
                parameters7[4].Value = ccb.POSID;
                parameters7[5].Value = ccb.BRANCHID;
                parameters7[6].Value = ccb.ORDERID;
                parameters7[7].Value = ccb.SFZ_NO;
                parameters7[8].Value = ccb.DJ_TIME;
                parameters7[9].Value = ccb.TXN_TYPE;
                parameters7[10].Value = ccb.MerchantID;
                table.Add(strSql, parameters7);
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
                    modSqlError.TYPE = "预交金支付保存";
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