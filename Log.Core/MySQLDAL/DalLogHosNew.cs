using MySql.Data.MySqlClient;
using Soft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Log.Core.MySQLDAL
{
    public class DalLogHosNew
    {
        public bool Add(Log.Core.Model.ModLogHosNew model)
        {
            string gu_id = Guid.NewGuid().ToString();
            try
            {
                XmlDocument Doc = XMLHelper.X_GetXmlDocument(model.inXml);
                DataSet dsIRP = XMLHelper.X_GetXmlData(Doc, model.NODE);//请求的数据包
                string SFZ_NO = dsIRP.Tables[0].Columns.Contains("SFZ_NO") ? CommonFunction.GetStr(dsIRP.Tables[0].Rows[0]["SFZ_NO"]) : "";
                string PAT_NAME = dsIRP.Tables[0].Columns.Contains("PAT_NAME") ? CommonFunction.GetStr(dsIRP.Tables[0].Rows[0]["PAT_NAME"]) : "";
                string sqlcmd = @" insert into loghos (UID ,InTime,InXml,OutTime,OutXml,HOS_ID,SFZ_NO,PAT_NAME,TYPE)
values
(@UID,@InTime,@InXml,@OutTime,@OutXm,@HOS_ID,@SFZ_NO,@PAT_NAME,@TYPE)";

                MySqlParameter[] parameters = {
          new MySqlParameter("@UID",gu_id),
          new MySqlParameter("@InTime", model.inTime),
          new MySqlParameter("@InXml", model.inXml),
          new MySqlParameter("@OutTime", model.outTime),
          new MySqlParameter("@OutXm",model.outXml),
                    new MySqlParameter("@HOS_ID",model.HOS_ID),
                    new MySqlParameter("@SFZ_NO",SFZ_NO),
                    new MySqlParameter("@PAT_NAME",PAT_NAME),
                    new MySqlParameter("@TYPE",model.TYPE)};
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    string sqlcmd = @" insert into loghos (UID ,InTime,InXml,OutTime,OutXml,HOS_ID,SFZ_NO,PAT_NAME,TYPE)
values
(@UID,@InTime,@InXml,@OutTime,@OutXm,@HOS_ID,@SFZ_NO,@PAT_NAME,@TYPE)";

                    MySqlParameter[] parameters = {
          new MySqlParameter("@UID",gu_id),
          new MySqlParameter("@InTime", model.inTime),
          new MySqlParameter("@InXml", model.inXml),
          new MySqlParameter("@OutTime", model.outTime),
          new MySqlParameter("@OutXm",model.outXml),
                    new MySqlParameter("@HOS_ID",model.HOS_ID),
                    new MySqlParameter("@SFZ_NO",""),
                    new MySqlParameter("@PAT_NAME",""),
                    new MySqlParameter("@TYPE",model.TYPE)};
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
