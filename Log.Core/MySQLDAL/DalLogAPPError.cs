using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class DalLogAPPError
    {
        public bool Add(Log.Core.Model.ModLogAPPError model)
        {
            try
            {
                //hlw mod 2017.10.31 改成标准模式，防止特殊字符无法存进去
                string sqlcmd = @" insert into logapperror (TYPE,InTime,InXml,OutTime ,OutXml)
values
(@TYPE,@InTime,@InXml,@OutTime,@OutXm)";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@TYPE",model.TYPE),
                    new MySqlParameter("@InTime", model.inTime),
                    new MySqlParameter("@InXml", model.inXml),
                    new MySqlParameter("@OutTime", model.outTime),
                    new MySqlParameter("@OutXm",model.outXml)         };
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
