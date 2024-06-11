using DB.Core;
using Log.Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace OnlineBusHos185_Tran.Plat.MySQLDAL
{
    /// <summary>
    /// 数据访问类:wechat_tran
    /// </summary>
    public partial class wechat_tran 
    {
        public wechat_tran()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string COMM_SN)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wechat_tran");
            strSql.Append(" where COMM_SN=@COMM_SN ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)          };
            parameters[0].Value = COMM_SN;

            return DbHelperMySQLZZJ.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.wechat_tran model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wechat_tran(");
            strSql.Append("COMM_SN,appid,mch_id,TXN_TYPE,device_info,nonce_str,body,currency_type,JE,time_start,spbill_create_ip,trade_type,openid,request_back_time,return_code,prepay_id,AT_TIME,AT_result_code,transaction_id,time_end,refund_channe,refund_recv_accout,trade_code,trade_message,error_code,error_message,COMM_MAIN,USER_ID,lTERMINAL_SN,PAT_NAME,SFZ_NO,HOSPATID)");
            strSql.Append(" values (");
            strSql.Append("@COMM_SN,@appid,@mch_id,@TXN_TYPE,@device_info,@nonce_str,@body,@currency_type,@JE,@time_start,@spbill_create_ip,@trade_type,@openid,@request_back_time,@return_code,@prepay_id,@AT_TIME,@AT_result_code,@transaction_id,@time_end,@refund_channe,@refund_recv_accout,@trade_code,@trade_message,@error_code,@error_message,@COMM_MAIN,@USER_ID,@lTERMINAL_SN,@PAT_NAME,@SFZ_NO,@HOSPATID)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@appid", MySqlDbType.VarChar,32),
                    new MySqlParameter("@mch_id", MySqlDbType.VarChar,32),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@device_info", MySqlDbType.VarChar,32),
                    new MySqlParameter("@nonce_str", MySqlDbType.VarChar,32),
                    new MySqlParameter("@body", MySqlDbType.VarChar,50),
                    new MySqlParameter("@currency_type", MySqlDbType.VarChar,16),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@time_start", MySqlDbType.DateTime),
                    new MySqlParameter("@spbill_create_ip", MySqlDbType.VarChar,16),
                    new MySqlParameter("@trade_type", MySqlDbType.VarChar,16),
                    new MySqlParameter("@openid", MySqlDbType.VarChar,50),
                    new MySqlParameter("@request_back_time", MySqlDbType.DateTime),
                    new MySqlParameter("@return_code", MySqlDbType.VarChar,16),
                    new MySqlParameter("@prepay_id", MySqlDbType.VarChar,64),
                    new MySqlParameter("@AT_TIME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@AT_result_code", MySqlDbType.VarChar,16),
                    new MySqlParameter("@transaction_id", MySqlDbType.VarChar,32),
                    new MySqlParameter("@time_end", MySqlDbType.DateTime),
                    new MySqlParameter("@refund_channe", MySqlDbType.VarChar,10),
                    new MySqlParameter("@refund_recv_accout", MySqlDbType.VarChar,64),
                    new MySqlParameter("@trade_code", MySqlDbType.VarChar,10),
                    new MySqlParameter("@trade_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@error_code", MySqlDbType.VarChar,10),
                    new MySqlParameter("@error_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@COMM_MAIN", MySqlDbType.VarChar,30),
                    new MySqlParameter("@USER_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,20),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@SFZ_NO", MySqlDbType.VarChar,20),
                    new MySqlParameter("@HOSPATID", MySqlDbType.VarChar,20)};
            parameters[0].Value = model.COMM_SN;
            parameters[1].Value = model.appid;
            parameters[2].Value = model.mch_id;
            parameters[3].Value = model.TXN_TYPE;
            parameters[4].Value = model.device_info;
            parameters[5].Value = model.nonce_str;
            parameters[6].Value = model.body;
            parameters[7].Value = model.currency_type;
            parameters[8].Value = model.JE;
            parameters[9].Value = model.time_start;
            parameters[10].Value = model.spbill_create_ip;
            parameters[11].Value = model.trade_type;
            parameters[12].Value = model.openid;
            parameters[13].Value = model.request_back_time;
            parameters[14].Value = model.return_code;
            parameters[15].Value = model.prepay_id;
            parameters[16].Value = model.AT_TIME;
            parameters[17].Value = model.AT_result_code;
            parameters[18].Value = model.transaction_id;
            parameters[19].Value = model.time_end;
            parameters[20].Value = model.refund_channe;
            parameters[21].Value = model.refund_recv_accout;
            parameters[22].Value = model.trade_code;
            parameters[23].Value = model.trade_message;
            parameters[24].Value = model.error_code;
            parameters[25].Value = model.error_message;
            parameters[26].Value = model.COMM_MAIN;
            parameters[27].Value = model.USER_ID;
            parameters[28].Value = model.lTERMINAL_SN;
            parameters[29].Value = model.PAT_NAME;
            parameters[30].Value = model.SFZ_NO;
            parameters[31].Value = model.HOSPATID;

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
            catch(Exception ex)
            {
                ModSqlError modSqlError = new ModSqlError();
                modSqlError.TYPE = "wechat_tran";
                modSqlError.time = DateTime.Now;
                modSqlError.EXCEPTION = ex.ToString();
                new Log.Core.MySQLDAL.DalSqlERRROR().Add(modSqlError);
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.wechat_tran model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wechat_tran set ");
            strSql.Append("appid=@appid,");
            strSql.Append("mch_id=@mch_id,");
            strSql.Append("device_info=@device_info,");
            strSql.Append("nonce_str=@nonce_str,");
            strSql.Append("body=@body,");
            strSql.Append("currency_type=@currency_type,");
            strSql.Append("JE=@JE,");
            strSql.Append("time_start=@time_start,");
            strSql.Append("spbill_create_ip=@spbill_create_ip,");
            strSql.Append("trade_type=@trade_type,");
            strSql.Append("openid=@openid,");
            strSql.Append("request_back_time=@request_back_time,");
            strSql.Append("return_code=@return_code,");
            strSql.Append("prepay_id=@prepay_id,");
            strSql.Append("AT_TIME=@AT_TIME,");
            strSql.Append("AT_result_code=@AT_result_code,");
            strSql.Append("transaction_id=@transaction_id,");
            strSql.Append("time_end=@time_end,");
            strSql.Append("refund_channe=@refund_channe,");
            strSql.Append("refund_recv_accout=@refund_recv_accout,");
            strSql.Append("trade_code=@trade_code,");
            strSql.Append("trade_message=@trade_message,");
            strSql.Append("error_code=@error_code,");
            strSql.Append("error_message=@error_message,");
            strSql.Append("TXN_TYPE=@TXN_TYPE");
            strSql.Append(" where COMM_SN=@COMM_SN");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@appid", MySqlDbType.VarChar,32),
                    new MySqlParameter("@mch_id", MySqlDbType.VarChar,32),
                    new MySqlParameter("@device_info", MySqlDbType.VarChar,32),
                    new MySqlParameter("@nonce_str", MySqlDbType.VarChar,32),
                    new MySqlParameter("@body", MySqlDbType.VarChar,50),
                    new MySqlParameter("@currency_type", MySqlDbType.VarChar,16),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@time_start", MySqlDbType.DateTime),
                    new MySqlParameter("@spbill_create_ip", MySqlDbType.VarChar,16),
                    new MySqlParameter("@trade_type", MySqlDbType.VarChar,16),
                    new MySqlParameter("@openid", MySqlDbType.VarChar,50),
                    new MySqlParameter("@request_back_time", MySqlDbType.DateTime),
                    new MySqlParameter("@return_code", MySqlDbType.VarChar,16),
                    new MySqlParameter("@prepay_id", MySqlDbType.VarChar,64),
                    new MySqlParameter("@AT_TIME", MySqlDbType.VarChar,10),
                    new MySqlParameter("@AT_result_code", MySqlDbType.VarChar,16),
                    new MySqlParameter("@transaction_id", MySqlDbType.VarChar,32),
                    new MySqlParameter("@time_end", MySqlDbType.DateTime),
                    new MySqlParameter("@refund_channe", MySqlDbType.VarChar,10),
                    new MySqlParameter("@refund_recv_accout", MySqlDbType.VarChar,64),
                    new MySqlParameter("@trade_code", MySqlDbType.VarChar,10),
                    new MySqlParameter("@trade_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@error_code", MySqlDbType.VarChar,10),
                    new MySqlParameter("@error_message", MySqlDbType.VarChar,100),
                    new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.appid;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.device_info;
            parameters[3].Value = model.nonce_str;
            parameters[4].Value = model.body;
            parameters[5].Value = model.currency_type;
            parameters[6].Value = model.JE;
            parameters[7].Value = model.time_start;
            parameters[8].Value = model.spbill_create_ip;
            parameters[9].Value = model.trade_type;
            parameters[10].Value = model.openid;
            parameters[11].Value = model.request_back_time;
            parameters[12].Value = model.return_code;
            parameters[13].Value = model.prepay_id;
            parameters[14].Value = model.AT_TIME;
            parameters[15].Value = model.AT_result_code;
            parameters[16].Value = model.transaction_id;
            parameters[17].Value = model.time_end;
            parameters[18].Value = model.refund_channe;
            parameters[19].Value = model.refund_recv_accout;
            parameters[20].Value = model.trade_code;
            parameters[21].Value = model.trade_message;
            parameters[22].Value = model.error_code;
            parameters[23].Value = model.error_message;
            parameters[24].Value = model.TXN_TYPE;
            parameters[25].Value = model.COMM_SN;


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
            strSql.Append("delete from wechat_tran ");
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
            strSql.Append("delete from wechat_tran ");
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
        public Plat.Model.wechat_tran GetModel(string COMM_SN)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COMM_SN,appid,mch_id,TXN_TYPE,device_info,nonce_str,body,currency_type,JE,time_start,spbill_create_ip,trade_type,openid,request_back_time,return_code,prepay_id,AT_TIME,AT_result_code,transaction_id,time_end,refund_channe,refund_recv_accout from wechat_tran ");
            strSql.Append(" where COMM_SN=@COMM_SN ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,30)          };
            parameters[0].Value = COMM_SN;

            Plat.Model.wechat_tran model = new Plat.Model.wechat_tran();
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
        public Plat.Model.wechat_tran DataRowToModel(DataRow row)
        {
            Plat.Model.wechat_tran model = new Plat.Model.wechat_tran();
            if (row != null)
            {
                if (row["COMM_SN"] != null)
                {
                    model.COMM_SN = row["COMM_SN"].ToString();
                }
                if (row["appid"] != null)
                {
                    model.appid = row["appid"].ToString();
                }
                if (row["mch_id"] != null)
                {
                    model.mch_id = row["mch_id"].ToString();
                }
                if (row["TXN_TYPE"] != null)
                {
                    model.TXN_TYPE = row["TXN_TYPE"].ToString();
                }
                if (row["device_info"] != null)
                {
                    model.device_info = row["device_info"].ToString();
                }
                if (row["nonce_str"] != null)
                {
                    model.nonce_str = row["nonce_str"].ToString();
                }
                if (row["body"] != null)
                {
                    model.body = row["body"].ToString();
                }
                if (row["currency_type"] != null)
                {
                    model.currency_type = row["currency_type"].ToString();
                }
                if (row["JE"] != null && row["JE"].ToString() != "")
                {
                    model.JE = decimal.Parse(row["JE"].ToString());
                }
                if (row["time_start"] != null && row["time_start"].ToString() != "")
                {
                    model.time_start = DateTime.Parse(row["time_start"].ToString());
                }
                if (row["spbill_create_ip"] != null)
                {
                    model.spbill_create_ip = row["spbill_create_ip"].ToString();
                }
                if (row["trade_type"] != null)
                {
                    model.trade_type = row["trade_type"].ToString();
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["request_back_time"] != null && row["request_back_time"].ToString() != "")
                {
                    model.request_back_time = DateTime.Parse(row["request_back_time"].ToString());
                }
                if (row["return_code"] != null)
                {
                    model.return_code = row["return_code"].ToString();
                }
                if (row["prepay_id"] != null)
                {
                    model.prepay_id = row["prepay_id"].ToString();
                }
                if (row["AT_TIME"] != null)
                {
                    model.AT_TIME = row["AT_TIME"].ToString();
                }
                if (row["AT_result_code"] != null)
                {
                    model.AT_result_code = row["AT_result_code"].ToString();
                }
                if (row["transaction_id"] != null)
                {
                    model.transaction_id = row["transaction_id"].ToString();
                }
                if (row["time_end"] != null && row["time_end"].ToString() != "")
                {
                    model.time_end = DateTime.Parse(row["time_end"].ToString());
                }
                if (row["refund_channe"] != null)
                {
                    model.refund_channe = row["refund_channe"].ToString();
                }
                if (row["refund_recv_accout"] != null)
                {
                    model.refund_recv_accout = row["refund_recv_accout"].ToString();
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
            strSql.Append("select COMM_SN,appid,mch_id,TXN_TYPE,device_info,nonce_str,body,currency_type,JE,time_start,spbill_create_ip,trade_type,openid,request_back_time,return_code,prepay_id,AT_TIME,AT_result_code,transaction_id,time_end,refund_channe,refund_recv_accout ");
            strSql.Append(" FROM wechat_tran ");
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
            strSql.Append("select count(1) FROM wechat_tran ");
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
            strSql.Append(")AS Row, T.*  from wechat_tran T ");
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
            parameters[0].Value = "wechat_tran";
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
