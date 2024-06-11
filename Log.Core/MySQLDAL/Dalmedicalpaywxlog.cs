using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class Dalmedicalpaywxlog
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Log.Core.Model.Modmedicalpaywxlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into medicalpaywxlog(");
            strSql.Append("HOS_ID,BUSICODE,cardNum,retCode,NOW,PLAT_LSH,JE,SENDDATA,RECEIVEDATA,SENDYBDATA,RECEIVEYBDATA)");
            strSql.Append(" values (");
            strSql.Append("@HOS_ID,@BUSICODE,@cardNum,@retCode,@NOW,@PLAT_LSH,@JE,@SENDDATA,@RECEIVEDATA,@SENDYBDATA,@RECEIVEYBDATA)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@BUSICODE", MySqlDbType.VarChar,10),
                    new MySqlParameter("@cardNum", MySqlDbType.VarChar,20),
                    new MySqlParameter("@retCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@PLAT_LSH", MySqlDbType.VarChar,30),
                    new MySqlParameter("@JE", MySqlDbType.Decimal,10),
                    new MySqlParameter("@SENDDATA", MySqlDbType.VarChar,3000),
                    new MySqlParameter("@RECEIVEDATA", MySqlDbType.VarChar,3000),
                    new MySqlParameter("@SENDYBDATA", MySqlDbType.VarChar,3000),
                    new MySqlParameter("@RECEIVEYBDATA", MySqlDbType.VarChar,3000)};
            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.BUSICODE;
            parameters[2].Value = model.cardNum;
            parameters[3].Value = model.retCode;
            parameters[4].Value = model.NOW;
            parameters[5].Value = model.PLAT_LSH;
            parameters[6].Value = model.JE;
            parameters[7].Value = model.SENDDATA;
            parameters[8].Value = model.RECEIVEDATA;
            parameters[9].Value = model.SENDYBDATA;
            parameters[10].Value = model.RECEIVEYBDATA;

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
