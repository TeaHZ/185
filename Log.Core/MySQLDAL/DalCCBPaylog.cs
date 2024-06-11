using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Log.Core.Model;
using System.Data;
using System.Configuration;

namespace Log.Core.MySQLDAL
{
     
        public partial class DalCCBPaylog  
        {
            public DalCCBPaylog()
            { }
            #region  BasicMethod


            public bool ccbpaylog_insert(string OrderID, string MerchantID, string PosID, string BranchID, decimal Je, DateTime Now, string BType, string DateSend, string DateRe, out int Sys_Id)
            {
                Sys_Id = -1;
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionStringLogMySQL"]))
                {
                    string sp_Name = @"ccbpaylog_insert";

                    if (conn.State != ConnectionState.Open) conn.Open();
                    MySqlCommand sqlcmd = new MySqlCommand(sp_Name, conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    sqlcmd.Parameters.AddWithValue("OrderID_1", OrderID);
                    sqlcmd.Parameters.AddWithValue("MerchantID_1", MerchantID);
                    sqlcmd.Parameters.AddWithValue("PosID_1", PosID);
                    sqlcmd.Parameters.AddWithValue("BranchID_1", BranchID);
                    sqlcmd.Parameters.AddWithValue("Je_1", Je);
                    sqlcmd.Parameters.AddWithValue("Now_1", Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sqlcmd.Parameters.AddWithValue("BType_1", BType);
                    sqlcmd.Parameters.AddWithValue("DateSend_1", DateSend);
                    sqlcmd.Parameters.AddWithValue("DateRe_1", DateRe);

                    MySqlParameter sql_param = sqlcmd.Parameters.Add("RESULT", MySqlDbType.Int32);
                    sql_param.Direction = ParameterDirection.Output;
                    sqlcmd.ExecuteNonQuery();
                    if (sql_param.Value == DBNull.Value)
                    {
                        return false;
                    }
                    else if ((int)sql_param.Value <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        Sys_Id = (int)sql_param.Value;
                        return true;
                    }
                }
            }


            /// <summary>
            /// 增加一条数据
            /// </summary>
            public bool Add(ModCCBpaylog model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ccbpaylog(");
                strSql.Append("OrderID,MerchantID,PosID,BranchID,Je,Now,BType,DateSend,DateRe)");
                strSql.Append(" values (");
                strSql.Append("@OrderID,@MerchantID,@PosID,@BranchID,@Je,@Now,@BType,@DateSend,@DateRe)");
                MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@MerchantID", MySqlDbType.VarChar,20),
					new MySqlParameter("@PosID", MySqlDbType.VarChar,20),
					new MySqlParameter("@BranchID", MySqlDbType.VarChar,20),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = model.OrderID;
                parameters[1].Value = model.MerchantID;
                parameters[2].Value = model.PosID;
                parameters[3].Value = model.BranchID;
                parameters[4].Value = model.Je;
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
            public bool Update(ModCCBpaylog model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ccbpaylog set ");
                strSql.Append("OrderID=@OrderID,");
                strSql.Append("MerchantID=@MerchantID,");
                strSql.Append("PosID=@PosID,");
                strSql.Append("BranchID=@BranchID,");
                strSql.Append("Je=@Je,");
                strSql.Append("Now=@Now,");
                strSql.Append("BType=@BType,");
                strSql.Append("DateSend=@DateSend,");
                strSql.Append("DateRe=@DateRe");
                strSql.Append(" where ");
                MySqlParameter[] parameters = {
					new MySqlParameter("@OrderID", MySqlDbType.VarChar,30),
					new MySqlParameter("@MerchantID", MySqlDbType.VarChar,20),
					new MySqlParameter("@PosID", MySqlDbType.VarChar,20),
					new MySqlParameter("@BranchID", MySqlDbType.VarChar,20),
					new MySqlParameter("@Je", MySqlDbType.Decimal,10),
					new MySqlParameter("@Now", MySqlDbType.DateTime),
					new MySqlParameter("@BType", MySqlDbType.VarChar,20),
					new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
					new MySqlParameter("@DateRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = model.OrderID;
                parameters[1].Value = model.MerchantID;
                parameters[2].Value = model.PosID;
                parameters[3].Value = model.BranchID;
                parameters[4].Value = model.Je;
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
            /// 判断是否已异步成功
            /// </summary>
            /// <param name="orderid"></param>
            /// <returns></returns>
            public bool Select(string orderid)
            {
                string sqlcmd = string.Format(@"select DateSend from ccbpaylog where orderid='" + orderid + "' and DateSend like '%成功%'");
                System.Data.DataTable dt = DbHelperMySQL.Query(sqlcmd).Tables[0];
                return dt.Rows.Count > 0;
            }

           


        

            #endregion  BasicMethod
            #region  ExtensionMethod

            #endregion  ExtensionMethod
        }
    
}
