using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Log.Core.MySQLDAL
{
    public class DalLogSendMsg
    {
        public bool Add(Log.Core.Model.ModLogSaveMessage model)
        {
            try
            {
                string sqlcmd = @" insert into messagelog (TYPE,MESSAGE,PAT_NAME,DJ_TIME ,MOBILE_NO)
values
(@TYPE,@MESSAGE,@PAT_NAME,@DJ_TIME,@MOBILE_NO)";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@TYPE", model.TYPE),
                    new MySqlParameter("@MESSAGE", model.MESSAGE),
                    new MySqlParameter("@PAT_NAME", model.PAT_NAME),
                    new MySqlParameter("@DJ_TIME",model.DJ_TIME),
                    new MySqlParameter("@MOBILE_NO",model.MOBILE_NO)};
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
