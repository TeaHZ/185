using Log.Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public partial class DalXzlhfkPaylog
    {
        public DalXzlhfkPaylog()
        { }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(xzlhfkpaylog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into xzlhfkpaylog(");
            strSql.Append("OrderID,HOS_ID,Je,trade_no,Now,DateSend,DateRe,BType)");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@HOS_ID,@Je,@trade_no,@Now,@DateSend,@DateRe,@BType)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@OrderID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@HOS_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@Je", MySqlDbType.Decimal,10),
                    new MySqlParameter("@trade_no", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Now", MySqlDbType.DateTime),
                    new MySqlParameter("@DateSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DateRe", MySqlDbType.VarChar,2000),
                    new MySqlParameter("@BType", MySqlDbType.VarChar,20)};
            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.HOS_ID;
            parameters[2].Value = model.Je;
            parameters[3].Value = model.trade_no;
            parameters[4].Value = model.Now;
            parameters[5].Value = model.DateSend;
            parameters[6].Value = model.DateRe;
            parameters[7].Value = model.BType;
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
