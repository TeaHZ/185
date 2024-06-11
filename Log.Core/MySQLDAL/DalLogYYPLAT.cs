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
    class DalLogYYPLAT
    {
        public bool Add(Log.Core.Model.ModLogYYPLAT model)
        {
            string InXml = model.inXml;
            string gu_id = Guid.NewGuid().ToString();
            try
            {
                string sqlcmd = @" insert into logyyplat (UID ,InTime,InXml,OutTime,OutXml)
values
(@UID,@InTime,@InXml,@OutTime,@OutXml)";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                    new MySqlParameter("@InTime", model.inTime),
                    new MySqlParameter("@InXml", model.inXml),
                    new MySqlParameter("@OutTime", model.outTime),
                    new MySqlParameter("@OutXml",model.outXml)};
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    string sqlcmd = @" insert into loghos (UID,InTime,InXml,OutTime,OutXml)
values
(@UID,@InTime,@InXml,@OutTime,@OutXml)";

                    MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                    new MySqlParameter("@InTime", model.inTime),
                    new MySqlParameter("@InXml", model.inXml),
                    new MySqlParameter("@OutTime", model.outTime),
                    new MySqlParameter("@OutXml",model.outXml)};
                    DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                    return true;
                }
                catch
                {

                }
            }
            return false;
        }
    }
}
