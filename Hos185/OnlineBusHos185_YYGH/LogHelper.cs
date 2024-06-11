using System;

namespace OnlineBusHos185_YYGH
{
    public class LogHelper
    {
        public static void SaveLogHos(SqlSugarModel.Loghos loghos)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Loghos>(loghos).ExecuteCommand();
            }
            catch (Exception ex)
            {

            }
        }
        public static void SaveLogZZJ(SqlSugarModel.Logzzj log)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Logzzj>(log).ExecuteCommand();
            }
            catch 
            {

            }
        }
        public static void SaveSqlerror(SqlSugarModel.Sqlerror log)
        {
            try
            {
                var db = new DbMySQLLog().Client;
                db.Insertable<SqlSugarModel.Sqlerror>(log).ExecuteCommand();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
