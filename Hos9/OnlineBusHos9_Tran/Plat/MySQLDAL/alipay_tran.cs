using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace OnlineBusHos9_Tran.Plat.MySQLDAL
{
    /// <summary>
    /// 数据访问类:alipay_tran
    /// </summary>
    public partial class alipay_tran 
    {
        public alipay_tran()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string COMM_SN)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from alipay_tran");
            strSql.Append(" where COMM_SN=@COMM_SN ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)          };
            parameters[0].Value = COMM_SN;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }

        public bool AddOrUpdate(Plat.Model.alipay_tran model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into alipay_tran(");
            strSql.Append("COMM_SN,TXN_TYPE,TRADE_NO,TRADE_STATUS,JE,gmt_create,gmt_payment,notify_time,notify_type,notify_id,payment_type,seller_id,seller_email,buyer_id,buyer_email,body,subject,refund_status,gmt_refund,batch_no,trade_code,trade_message,error_code,error_message,COMM_MAIN)");
            strSql.Append(" values (");
            strSql.Append("@COMM_SN,@TXN_TYPE,@TRADE_NO,@TRADE_STATUS,@JE,@gmt_create,@gmt_payment,@notify_time,@notify_type,@notify_id,@payment_type,@seller_id,@seller_email,@buyer_id,@buyer_email,@body,@subject,@refund_status,@gmt_refund,@batch_no,@trade_code,@trade_message,@error_code,@error_message,@COMM_MAIN)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@TRADE_NO", MySqlDbType.VarChar,64),
                    new MySqlParameter("@TRADE_STATUS", MySqlDbType.VarChar,20),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@gmt_create", MySqlDbType.DateTime),
                    new MySqlParameter("@gmt_payment", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_time", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_type", MySqlDbType.VarChar,20),
                    new MySqlParameter("@notify_id", MySqlDbType.VarChar,50),
                    new MySqlParameter("@payment_type", MySqlDbType.VarChar,4),
                    new MySqlParameter("@seller_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@seller_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@buyer_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@buyer_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@body", MySqlDbType.VarChar,100),
                    new MySqlParameter("@subject", MySqlDbType.VarChar,50),
                    new MySqlParameter("@refund_status", MySqlDbType.VarChar,20),
                    new MySqlParameter("@gmt_refund", MySqlDbType.DateTime),
                    new MySqlParameter("@batch_no", MySqlDbType.VarChar,20),
                    new MySqlParameter("@trade_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@trade_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@error_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@error_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@COMM_MAIN",MySqlDbType.VarChar,30)};
            parameters[0].Value = model.COMM_SN;
            parameters[1].Value = model.TXN_TYPE;
            parameters[2].Value = model.TRADE_NO;
            parameters[3].Value = model.TRADE_STATUS;
            parameters[4].Value = model.JE;
            parameters[5].Value = model.gmt_create;
            parameters[6].Value = model.gmt_payment;
            parameters[7].Value = model.notify_time;
            parameters[8].Value = model.notify_type;
            parameters[9].Value = model.notify_id;
            parameters[10].Value = model.payment_type;
            parameters[11].Value = model.seller_id;
            parameters[12].Value = model.seller_email;
            parameters[13].Value = model.buyer_id;
            parameters[14].Value = model.buyer_email;
            parameters[15].Value = model.body;
            parameters[16].Value = model.subject;
            parameters[17].Value = model.refund_status;
            parameters[18].Value = model.gmt_refund;
            parameters[19].Value = model.batch_no;
            parameters[20].Value = model.trade_code;
            parameters[21].Value = model.trade_message;
            parameters[22].Value = model.error_code;
            parameters[23].Value = model.error_message;
            parameters[24].Value = model.COMM_MAIN;


            strSql.Append(" ON DUPLICATE KEY UPDATE trade_status='" + model.TRADE_STATUS + "',trade_code='" + model.trade_code + "',trade_message='" + model.trade_message + "',error_code='" + model.error_code + "',error_message='" + model.error_message + "'");

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
        public bool Add(Plat.Model.alipay_tran model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into alipay_tran(");
            strSql.Append("COMM_SN,TXN_TYPE,TRADE_NO,TRADE_STATUS,JE,gmt_create,gmt_payment,notify_time,notify_type,notify_id,payment_type,seller_id,seller_email,buyer_id,buyer_email,body,subject,refund_status,gmt_refund,batch_no,trade_code,trade_message,error_code,error_message,COMM_MAIN,USER_ID,lTERMINAL_SN,PAT_NAME,SFZ_NO,HOSPATID)");
            strSql.Append(" values (");
            strSql.Append("@COMM_SN,@TXN_TYPE,@TRADE_NO,@TRADE_STATUS,@JE,@gmt_create,@gmt_payment,@notify_time,@notify_type,@notify_id,@payment_type,@seller_id,@seller_email,@buyer_id,@buyer_email,@body,@subject,@refund_status,@gmt_refund,@batch_no,@trade_code,@trade_message,@error_code,@error_message,@COMM_MAIN,@USER_ID,@lTERMINAL_SN,@PAT_NAME,@SFZ_NO,@HOSPATID)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@TRADE_NO", MySqlDbType.VarChar,64),
                    new MySqlParameter("@TRADE_STATUS", MySqlDbType.VarChar,20),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@gmt_create", MySqlDbType.DateTime),
                    new MySqlParameter("@gmt_payment", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_time", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_type", MySqlDbType.VarChar,20),
                    new MySqlParameter("@notify_id", MySqlDbType.VarChar,50),
                    new MySqlParameter("@payment_type", MySqlDbType.VarChar,4),
                    new MySqlParameter("@seller_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@seller_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@buyer_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@buyer_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@body", MySqlDbType.VarChar,100),
                    new MySqlParameter("@subject", MySqlDbType.VarChar,50),
                    new MySqlParameter("@refund_status", MySqlDbType.VarChar,20),
                    new MySqlParameter("@gmt_refund", MySqlDbType.DateTime),
                    new MySqlParameter("@batch_no", MySqlDbType.VarChar,20),
                    new MySqlParameter("@trade_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@trade_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@error_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@error_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@COMM_MAIN",MySqlDbType.VarChar,30),
                    new MySqlParameter("@USER_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@HOSPATID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.COMM_SN;
            parameters[1].Value = model.TXN_TYPE;
            parameters[2].Value = model.TRADE_NO;
            parameters[3].Value = model.TRADE_STATUS;
            parameters[4].Value = model.JE;
            parameters[5].Value = model.gmt_create;
            parameters[6].Value = model.gmt_payment;
            parameters[7].Value = model.notify_time;
            parameters[8].Value = model.notify_type;
            parameters[9].Value = model.notify_id;
            parameters[10].Value = model.payment_type;
            parameters[11].Value = model.seller_id;
            parameters[12].Value = model.seller_email;
            parameters[13].Value = model.buyer_id;
            parameters[14].Value = model.buyer_email;
            parameters[15].Value = model.body;
            parameters[16].Value = model.subject;
            parameters[17].Value = model.refund_status;
            parameters[18].Value = model.gmt_refund;
            parameters[19].Value = model.batch_no;
            parameters[20].Value = model.trade_code;
            parameters[21].Value = model.trade_message;
            parameters[22].Value = model.error_code;
            parameters[23].Value = model.error_message;
            parameters[24].Value = model.COMM_MAIN;
            parameters[25].Value = model.USER_ID;
            parameters[26].Value = model.lTERMINAL_SN;
            parameters[27].Value = model.PAT_NAME;
            parameters[28].Value = model.SFZ_NO;
            parameters[29].Value = model.HOSPATID;


            try
            {
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
            catch (Exception ex)
            {
          
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.alipay_tran model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update alipay_tran set ");
            strSql.Append("TRADE_NO=@TRADE_NO,");
            strSql.Append("TRADE_STATUS=@TRADE_STATUS,");
            strSql.Append("JE=@JE,");
            strSql.Append("TXN_TYPE=@TXN_TYPE,");
            strSql.Append("gmt_create=@gmt_create,");
            strSql.Append("gmt_payment=@gmt_payment,");
            strSql.Append("notify_time=@notify_time,");
            strSql.Append("notify_type=@notify_type,");
            strSql.Append("notify_id=@notify_id,");
            strSql.Append("payment_type=@payment_type,");
            strSql.Append("seller_id=@seller_id,");
            strSql.Append("seller_email=@seller_email,");
            strSql.Append("buyer_id=@buyer_id,");
            strSql.Append("buyer_email=@buyer_email,");
            strSql.Append("body=@body,");
            strSql.Append("subject=@subject,");
            strSql.Append("refund_status=@refund_status,");
            strSql.Append("gmt_refund=@gmt_refund,");
            strSql.Append("batch_no=@batch_no,");
            strSql.Append("trade_code=@trade_code,");
            strSql.Append("trade_message=@trade_message,");
            strSql.Append("error_code=@error_code,");
            strSql.Append("error_message=@error_message");
            strSql.Append(" where COMM_SN=@COMM_SN");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@TRADE_NO", MySqlDbType.VarChar,64),
                    new MySqlParameter("@TRADE_STATUS", MySqlDbType.VarChar,20),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@gmt_create", MySqlDbType.DateTime),
                    new MySqlParameter("@gmt_payment", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_time", MySqlDbType.DateTime),
                    new MySqlParameter("@notify_type", MySqlDbType.VarChar,20),
                    new MySqlParameter("@notify_id", MySqlDbType.VarChar,50),
                    new MySqlParameter("@payment_type", MySqlDbType.VarChar,4),
                    new MySqlParameter("@seller_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@seller_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@buyer_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("@buyer_email", MySqlDbType.VarChar,100),
                    new MySqlParameter("@body", MySqlDbType.VarChar,100),
                    new MySqlParameter("@subject", MySqlDbType.VarChar,50),
                    new MySqlParameter("@refund_status", MySqlDbType.VarChar,20),
                    new MySqlParameter("@gmt_refund", MySqlDbType.DateTime),
                    new MySqlParameter("@batch_no", MySqlDbType.VarChar,20),
                    new MySqlParameter("@trade_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@trade_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@error_code", MySqlDbType.VarChar,2),
                    new MySqlParameter("@error_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.TRADE_NO;
            parameters[1].Value = model.TRADE_STATUS;
            parameters[2].Value = model.JE;
            parameters[3].Value = model.TXN_TYPE;
            parameters[4].Value = model.gmt_create;
            parameters[5].Value = model.gmt_payment;
            parameters[6].Value = model.notify_time;
            parameters[7].Value = model.notify_type;
            parameters[8].Value = model.notify_id;
            parameters[9].Value = model.payment_type;
            parameters[10].Value = model.seller_id;
            parameters[11].Value = model.seller_email;
            parameters[12].Value = model.buyer_id;
            parameters[13].Value = model.buyer_email;
            parameters[14].Value = model.body;
            parameters[15].Value = model.subject;
            parameters[16].Value = model.refund_status;
            parameters[17].Value = model.gmt_refund;
            parameters[18].Value = model.batch_no;
            parameters[19].Value = model.trade_code;
            parameters[20].Value = model.trade_message;
            parameters[21].Value = model.error_code;
            parameters[22].Value = model.error_message;
            parameters[23].Value = model.COMM_SN;

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
        public bool Delete(string COMM_SN)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from alipay_tran ");
            strSql.Append(" where COMM_SN=@COMM_SN ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)          };
            parameters[0].Value = COMM_SN;

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
        public bool DeleteList(string COMM_SNlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from alipay_tran ");
            strSql.Append(" where COMM_SN in (" + COMM_SNlist + ")  ");
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
        public Plat.Model.alipay_tran GetModel(string COMM_SN)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COMM_SN,TRADE_NO,TRADE_STATUS,JE,TXN_TYPE,gmt_create,gmt_payment,notify_time,notify_type,notify_id,payment_type,seller_id,seller_email,buyer_id,buyer_email,body,subject,refund_status,gmt_refund,batch_no from alipay_tran ");
            strSql.Append(" where COMM_SN=@COMM_SN ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)          };
            parameters[0].Value = COMM_SN;

            Plat.Model.alipay_tran model = new Plat.Model.alipay_tran();
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
        public Plat.Model.alipay_tran DataRowToModel(DataRow row)
        {
            Plat.Model.alipay_tran model = new Plat.Model.alipay_tran();
            if (row != null)
            {
                if (row["COMM_SN"] != null)
                {
                    model.COMM_SN = row["COMM_SN"].ToString();
                }
                if (row["TRADE_NO"] != null)
                {
                    model.TRADE_NO = row["TRADE_NO"].ToString();
                }
                if (row["TRADE_STATUS"] != null)
                {
                    model.TRADE_STATUS = row["TRADE_STATUS"].ToString();
                }
                if (row["JE"] != null && row["JE"].ToString() != "")
                {
                    model.JE = decimal.Parse(row["JE"].ToString());
                }
                if (row["TXN_TYPE"] != null)
                {
                    model.TXN_TYPE = row["TXN_TYPE"].ToString();
                }
                if (row["gmt_create"] != null && row["gmt_create"].ToString() != "")
                {
                    model.gmt_create = DateTime.Parse(row["gmt_create"].ToString());
                }
                if (row["gmt_payment"] != null && row["gmt_payment"].ToString() != "")
                {
                    model.gmt_payment = DateTime.Parse(row["gmt_payment"].ToString());
                }
                if (row["notify_time"] != null && row["notify_time"].ToString() != "")
                {
                    model.notify_time = DateTime.Parse(row["notify_time"].ToString());
                }
                if (row["notify_type"] != null)
                {
                    model.notify_type = row["notify_type"].ToString();
                }
                if (row["notify_id"] != null)
                {
                    model.notify_id = row["notify_id"].ToString();
                }
                if (row["payment_type"] != null)
                {
                    model.payment_type = row["payment_type"].ToString();
                }
                if (row["seller_id"] != null)
                {
                    model.seller_id = row["seller_id"].ToString();
                }
                if (row["seller_email"] != null)
                {
                    model.seller_email = row["seller_email"].ToString();
                }
                if (row["buyer_id"] != null)
                {
                    model.buyer_id = row["buyer_id"].ToString();
                }
                if (row["buyer_email"] != null)
                {
                    model.buyer_email = row["buyer_email"].ToString();
                }
                if (row["body"] != null)
                {
                    model.body = row["body"].ToString();
                }
                if (row["subject"] != null)
                {
                    model.subject = row["subject"].ToString();
                }
                if (row["refund_status"] != null)
                {
                    model.refund_status = row["refund_status"].ToString();
                }
                if (row["gmt_refund"] != null && row["gmt_refund"].ToString() != "")
                {
                    model.gmt_refund = DateTime.Parse(row["gmt_refund"].ToString());
                }
                if (row["batch_no"] != null)
                {
                    model.batch_no = row["batch_no"].ToString();
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
            strSql.Append("select COMM_SN,TRADE_NO,TRADE_STATUS,JE,TXN_TYPE,gmt_create,gmt_payment,notify_time,notify_type,notify_id,payment_type,seller_id,seller_email,buyer_id,buyer_email,body,subject,refund_status,gmt_refund,batch_no ");
            strSql.Append(" FROM alipay_tran ");
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
            strSql.Append("select count(1) FROM alipay_tran ");
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
                strSql.Append("order by T.COMM_SN desc");
            }
            strSql.Append(")AS Row, T.*  from alipay_tran T ");
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
            parameters[0].Value = "alipay_tran";
            parameters[1].Value = "COMM_SN";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQLZZJ.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
