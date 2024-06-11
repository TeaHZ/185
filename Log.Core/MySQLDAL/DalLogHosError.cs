using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class DalLogHosError
    {
        /// <summary>
        /// 记录医院错误信息
        /// </summary>
        /// <param name="TYPE">TYPE 1 医院返回处理失败  2医院无应答 3医院返回金额与异步返回金额不一致</param>
        /// <param name="InTime"></param>
        /// <param name="InXml"></param>
        /// <param name="OutTime"></param>
        /// <param name="OutXml"></param>
        public  bool Add(Log.Core.Model.ModLogHosError model)
        {
            try
            {
                //hlw mod 2017.10.31 改成标准模式，防止特殊字符无法存进去
                string sqlcmd = @" insert into LogHOSERROR (TYPE,InTime,InXml,OutTime ,OutXml)
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
