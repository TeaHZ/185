using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Core.Model;
namespace Log.Core.MySQLDAL
{
    /// <summary>
 /// 数据访问类:alipaylog
 /// </summary>
    public partial class DalJsBankPaylog
    {

        public DalJsBankPaylog()
        { }

       

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(jsbankpaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into jsbankpaylog(");
            strSql.Append("OrderId,MechID,Je,trade_no,trade_status,Now,DateSend,DateRe,errcode,errmsg,BType)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@MechID,@Je,@trade_no,@trade_status,@Now,@DateSend,@DateRe,@errcode,@errmsg,@BType)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@OrderId", MySqlDbType.VarChar,50),
                    new MySqlParameter("@MechID", MySqlDbType.VarChar,35),
                    new MySqlParameter("@Je", MySqlDbType.Decimal,16),
                    new MySqlParameter("@trade_no", MySqlDbType.VarChar,50),
                    new MySqlParameter("@trade_status", MySqlDbType.VarChar,20),
                    new MySqlParameter("@Now", MySqlDbType.DateTime),
                    new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DateRe", MySqlDbType.VarChar,2000),
                    new MySqlParameter("@errcode", MySqlDbType.VarChar,128),
                    new MySqlParameter("@errmsg", MySqlDbType.VarChar,128),
             new MySqlParameter("@BType", MySqlDbType.VarChar,20)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.MechID;
            parameters[2].Value = model.Je;
            parameters[3].Value = model.trade_no;
            parameters[4].Value = model.trade_status;
            parameters[5].Value = model.Now;
            parameters[6].Value = model.DateSend;
            parameters[7].Value = model.DateRe;
            parameters[8].Value = model.errcode;
            parameters[9].Value = model.errmsg;
            parameters[10].Value = model.BType;
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
