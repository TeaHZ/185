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
    /// 数据访问类:default_appt_log
    /// </summary>
    public partial class Daldefault_appt_log
    {
        public Daldefault_appt_log()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Moddefault_appt_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into default_appt_log(");
            strSql.Append("HOS_ID,DEPT_CODE,SCH_DATE,SCH_TIME,PAT_ID,DOC_NO,APPT_TIME,GH_FEE,ZL_FEE,PERIOD_START,PERIOD_CAUSE,DEF_TIME,SAVE_TYPE,APPT_TYPE,REG_ID)");
            strSql.Append(" values (");
            strSql.Append("@HOS_ID,@DEPT_CODE,@SCH_DATE,@SCH_TIME,@PAT_ID,@DOC_NO,@APPT_TIME,@GH_FEE,@ZL_FEE,@PERIOD_START,@PERIOD_CAUSE,@DEF_TIME,@SAVE_TYPE,@APPT_TYPE,@REG_ID)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20),
					new MySqlParameter("@DEPT_CODE", MySqlDbType.VarChar,10),
					new MySqlParameter("@SCH_DATE", MySqlDbType.VarChar,10),
					new MySqlParameter("@SCH_TIME", MySqlDbType.VarChar,8),
					new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
					new MySqlParameter("@DOC_NO", MySqlDbType.VarChar,10),
					new MySqlParameter("@APPT_TIME", MySqlDbType.VarChar,8),
					new MySqlParameter("@GH_FEE", MySqlDbType.Decimal,10),
					new MySqlParameter("@ZL_FEE", MySqlDbType.Decimal,10),
					new MySqlParameter("@PERIOD_START", MySqlDbType.VarChar,8),
					new MySqlParameter("@PERIOD_CAUSE", MySqlDbType.VarChar,50),
					new MySqlParameter("@DEF_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@SAVE_TYPE", MySqlDbType.VarChar,20),
					new MySqlParameter("@APPT_TYPE", MySqlDbType.VarChar,20),
					new MySqlParameter("@REG_ID", MySqlDbType.Int32,11)};
            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.DEPT_CODE;
            parameters[2].Value = model.SCH_DATE;
            parameters[3].Value = model.SCH_TIME;
            parameters[4].Value = model.PAT_ID;
            parameters[5].Value = model.DOC_NO;
            parameters[6].Value = model.APPT_TIME;
            parameters[7].Value = model.GH_FEE;
            parameters[8].Value = model.ZL_FEE;
            parameters[9].Value = model.PERIOD_START;
            parameters[10].Value = model.PERIOD_CAUSE;
            parameters[11].Value = model.DEF_TIME;
            parameters[12].Value = model.SAVE_TYPE;
            parameters[13].Value = model.APPT_TYPE;
            parameters[14].Value = model.REG_ID;

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

