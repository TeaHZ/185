using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Log.Core.Model;
namespace Log.Core.MySQLDAL
{
  public   class DALPOSLog
  {
      /// <summary>
      /// 增加HIS调用日志
      /// </summary>
      public static bool HisTranlogAdd(histranlog model)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("insert into histranlog(");
          strSql.Append("COMM_SN,PAYTYPE,HOS_ID,COMM_HIS,JE,TXN_TYPE,trade_Status,CallIn,CallRec,client_Info,Now)");
          strSql.Append(" values (");
          strSql.Append("@COMM_SN,@PAYTYPE,@HOS_ID,@COMM_HIS,@JE,@TXN_TYPE,@trade_Status,@CallIn,@CallRec,@client_Info,@Now)");
          MySqlParameter[] parameters = {
					new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,20),
					new MySqlParameter("@PAYTYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
					new MySqlParameter("@COMM_HIS", MySqlDbType.VarChar,30),
					new MySqlParameter("@JE", MySqlDbType.Decimal,10),
					new MySqlParameter("@TXN_TYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@trade_Status", MySqlDbType.VarChar,10),
					new MySqlParameter("@CallIn", MySqlDbType.VarChar,300),
					new MySqlParameter("@CallRec", MySqlDbType.VarChar,1000),
					new MySqlParameter("@client_Info", MySqlDbType.VarChar,50),
					new MySqlParameter("@Now", MySqlDbType.DateTime)};
          parameters[0].Value = model.COMM_SN;
          parameters[1].Value = model.PAYTYPE;
          parameters[2].Value = model.HOS_ID;
          parameters[3].Value = model.COMM_HIS;
          parameters[4].Value = model.JE;
          parameters[5].Value = model.TXN_TYPE;
          parameters[6].Value = model.trade_Status;
          parameters[7].Value = model.CallIn;
          parameters[8].Value = model.CallRec;
          parameters[9].Value = model.client_Info;
          parameters[10].Value = model.Now;

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
      /// 增加农商行POS调用日志
      /// </summary>
      public static bool IfpsPOSlogAdd(ifpsPOSlog model)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("insert into ifpsPOSlog(");
          strSql.Append("COMM_SN,DataIn,DataRec,SaveTime)");
          strSql.Append(" values (");
          strSql.Append("@COMM_SN,@DataIn,@DataRec,@SaveTime)");
          MySqlParameter[] parameters = {
					new MySqlParameter("@COMM_SN", MySqlDbType.VarChar,20),
					new MySqlParameter("@DataIn", MySqlDbType.VarChar,200),
					new MySqlParameter("@DataRec", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@SaveTime", MySqlDbType.DateTime )};
          parameters[0].Value = model.COMM_SN;
          parameters[1].Value = model.DataIn;
          parameters[2].Value = model.DataRec;
          parameters[3].Value = DateTime.Now;

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
