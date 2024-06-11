using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class DalLogPatData
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Log.Core.Model.ModLogPatData model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into logpatdata(");
                strSql.Append("GUID,NOW,PLAT_ID,HOS_ID,BIZ_NAME,BIZ_INFO,PAT_ID,PAT_NAME,TRADE_CODE,TRADE_MESSAGE,HIS_LSH)");
                strSql.Append(" values (");
                strSql.Append("@GUID,@NOW,@PLAT_ID,@HOS_ID,@BIZ_NAME,@BIZ_INFO,@PAT_ID,@PAT_NAME,@TRADE_CODE,@TRADE_MESSAGE,@HIS_LSH)");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@GUID", MySqlDbType.VarChar,60),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@PLAT_ID", MySqlDbType.VarChar,40),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@BIZ_NAME", MySqlDbType.VarChar,40),
                    new MySqlParameter("@BIZ_INFO", MySqlDbType.VarChar,300),
                    new MySqlParameter("@PAT_ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@PAT_NAME", MySqlDbType.VarChar,20),
                    new MySqlParameter("@TRADE_CODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@TRADE_MESSAGE", MySqlDbType.VarChar,100),
                    new MySqlParameter("@HIS_LSH", MySqlDbType.VarChar,100)};
                parameters[0].Value = model.GUID;
                parameters[1].Value = model.NOW;
                parameters[2].Value = model.PLAT_ID;
                parameters[3].Value = model.HOS_ID;
                parameters[4].Value = model.BIZ_NAME;
                parameters[5].Value = model.BIZ_INFO;
                parameters[6].Value = model.PAT_ID;
                parameters[7].Value = model.PAT_NAME;
                parameters[8].Value = model.TRADE_CODE;
                parameters[9].Value = model.TRADE_MESSAGE;
                parameters[10].Value = model.HIS_LSH;

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
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
