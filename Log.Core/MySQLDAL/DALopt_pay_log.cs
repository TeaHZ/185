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
    /// <summary>
    /// 数据访问类:opt_pay_log
    /// </summary>
    public partial class DALopt_pay_log
    {
        public DALopt_pay_log()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Modopt_pay_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into opt_pay_log(");
            strSql.Append("PAY_ID,STATES,HOS_ID,PAT_ID,HSP_SN,JEALL,CASH_JE,DJ_DATE,DJ_TIME,lTERMINAL_SN)");
            strSql.Append(" values (");
            strSql.Append("@PAY_ID,@STATES,@HOS_ID,@PAT_ID,@HSP_SN,@JEALL,@CASH_JE,@DJ_DATE,@DJ_TIME,@lTERMINAL_SN)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PAY_ID", MySqlDbType.VarChar,10),
					new MySqlParameter("@STATES", MySqlDbType.VarChar,10),
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
					new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
					new MySqlParameter("@HSP_SN", MySqlDbType.VarChar,30),
					new MySqlParameter("@JEALL", MySqlDbType.Decimal,10),
					new MySqlParameter("@CASH_JE", MySqlDbType.Decimal,10),
					new MySqlParameter("@DJ_DATE", MySqlDbType.DateTime),
					new MySqlParameter("@DJ_TIME", MySqlDbType.VarChar,8),
					new MySqlParameter("@lTERMINAL_SN", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.PAY_ID;
            parameters[1].Value = model.STATES;
            parameters[2].Value = model.HOS_ID;
            parameters[3].Value = model.PAT_ID;
            parameters[4].Value = model.HSP_SN;
            parameters[5].Value = model.JEALL;
            parameters[6].Value = model.CASH_JE;
            parameters[7].Value = model.DJ_DATE;
            parameters[8].Value = model.DJ_TIME;
            parameters[9].Value = model.lTERMINAL_SN;

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

