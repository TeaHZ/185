﻿using System;
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
	public partial class DalWxpaylog
	{
        public DalWxpaylog()
		{}
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ModWxpaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wxpaylog(");
            strSql.Append("OrderID,mch_id,Je,trade_no,trade_status,Now,BType,DateSend,DateRe)");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@mch_id,@Je,@trade_no,@trade_status,@Now,@BType,@DateSend,@DateRe)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@mch_id", MySqlDbType.VarChar,32),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@trade_no", MySqlDbType.VarChar,64),
					new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.Je;
            parameters[3].Value = model.trade_no;
            parameters[4].Value = model.trade_status;
            parameters[5].Value = model.Now;
            parameters[6].Value = model.BType;
            parameters[7].Value = model.DateSend;
            parameters[8].Value = model.DateRe;

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

        public static  bool AddNotExists(ModWxpaylog model   )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wxpaylog(");
            strSql.Append("OrderID,mch_id,Je,trade_no,trade_status,Now,BType,DateSend,DateRe)");

            strSql.Append(" select @OrderID,@mch_id,@Je,@trade_no,@trade_status,@Now,@BType,@DateSend,@DateRe  FROM dual");
            if (model.OrderID != "" && model.BType == "BackRcvRe" && model.trade_status == "SUCCESS")
            {
                strSql.Append(" WHERE NOT EXISTS (SELECT OrderID FROM wxpaylog WHERE OrderID=@OrderID  and trade_status = @trade_status and BType=@BType)");
            }
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@mch_id", MySqlDbType.VarChar,32),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@trade_no", MySqlDbType.VarChar,64),
					new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.Je;
            parameters[3].Value = model.trade_no;
            parameters[4].Value = model.trade_status;
            parameters[5].Value = model.Now;
            parameters[6].Value = model.BType;
            parameters[7].Value = model.DateSend;
            parameters[8].Value = model.DateRe;
            

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
        public bool Update(ModWxpaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wxpaylog set ");
            strSql.Append("OrderID=@OrderID,");
            strSql.Append("mch_id=@mch_id,");
            strSql.Append("Je=@Je,");
            strSql.Append("trade_no=@trade_no,");
            strSql.Append("trade_status=@trade_status,");
            strSql.Append("Now=@Now,");
            strSql.Append("BType=@BType,");
            strSql.Append("DateSend=@DateSend,");
            strSql.Append("DateRe=@DateRe");
            strSql.Append(" where ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@mch_id", MySqlDbType.VarChar,32),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@trade_no", MySqlDbType.VarChar,64),
					new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.Je;
            parameters[3].Value = model.trade_no;
            parameters[4].Value = model.trade_status;
            parameters[5].Value = model.Now;
            parameters[6].Value = model.BType;
            parameters[7].Value = model.DateSend;
            parameters[8].Value = model.DateRe;

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

	}
}

