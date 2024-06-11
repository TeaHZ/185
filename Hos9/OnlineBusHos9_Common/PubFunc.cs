using Newtonsoft.Json;
using SqlSugar;
using System.Collections;
using System.Collections.Generic;

namespace OnlineBusHos9_Common
{
    public class PubFunc
    {

        public static bool GetSysID(string SYSIDNAME, out int SYSID)
        {
            var db = new DbMySQLZZJ().Client;
            //支持output
            var SYSIDNAMEP = new SugarParameter("@SYSIDNAME", SYSIDNAME);
            var SYSIDP = new SugarParameter("@SYSID", null, true);
            db.Ado.UseStoredProcedure().GetDataTable("GETSYSIDBASE", SYSIDNAMEP, SYSIDP);
            SYSID = FormatHelper.GetInt(SYSIDP.Value);
            return true;
        }

        public static Dictionary<string, string> Get_Filter(string filter)
        {
            Dictionary<string, string> dic_filter = new Dictionary<string, string>();
            try
            {
                if (FormatHelper.GetStr(filter) != "")
                {
                    try
                    {
                        dic_filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(filter);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
            return dic_filter;
        }

        private static SqlSugarModel.HosServiceConfig GetHosServiceConfig(string HOS_ID)
        {
            var db = new DbMySQLZZJ().Client;
            var config = db.Queryable<SqlSugarModel.HosServiceConfig>().InSingle(HOS_ID);
            return config;
        }


        private class indata
        {
            public string xmlstr { get; set; }
            public string user_id { get; set; }
            public string signature { get; set; }
        }

        private class outdata
        {
            public string outxml { get; set; }
        }
    }
}