using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.MySQLDAL
{
    public class DalLogWxRefund
    {
        /// <summary>
        /// 微信取消日志
        /// </summary>
        /// <param name="YY_FLAG"></param>
        /// <param name="NOW"></param>
        public bool Add(Log.Core.Model.ModLogWxRefund model)
        {
            try
            {
                string sqlcmd = string.Format("insert into wxrefundlog values('{0}','{1}')", model.FLAG,model.Now);
                DbHelperMySQL.ExecuteSql(sqlcmd);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
