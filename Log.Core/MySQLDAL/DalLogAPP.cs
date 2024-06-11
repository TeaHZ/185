using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Soft.Common;

namespace Log.Core.MySQLDAL
{
    public class DalLogAPP
    {
        public bool Add(Log.Core.Model.ModLogAPP model)
        {
            string TYPE = "";
            try
            {
                XmlDocument Doc = XMLHelper.X_GetXmlDocument(model.inXml);
                DataSet dsIRP = XMLHelper.X_GetXmlData(Doc, "ROOT/BODY");//请求的数据包
                DataSet dsHeader = XMLHelper.X_GetXmlData(Doc, "ROOT/HEADER");//请求的数据包

                TYPE = dsHeader.Tables[0].Rows[0]["TYPE"].ToString().Trim();
                int PAT_ID = dsIRP.Tables[0].Columns.Contains("PAT_ID") ? CommonFunction.GetInt(dsIRP.Tables[0].Rows[0]["PAT_ID"].ToString().Trim()) : 0;
                int REGPAT_ID = dsIRP.Tables[0].Columns.Contains("REGPAT_ID") ? CommonFunction.GetInt(dsIRP.Tables[0].Rows[0]["REGPAT_ID"].ToString().Trim()) : 0;
                string SFZ_NO = dsIRP.Tables[0].Columns.Contains("SFZ_NO") ? CommonFunction.GetStr(dsIRP.Tables[0].Rows[0]["SFZ_NO"].ToString().Trim()) : "";
                string PAT_NAME = dsIRP.Tables[0].Columns.Contains("PAT_NAME") ? CommonFunction.GetStr(dsIRP.Tables[0].Rows[0]["PAT_NAME"].ToString().Trim()) : "";
                string HOS_ID = dsIRP.Tables[0].Columns.Contains("HOS_ID") ? CommonFunction.GetStr(dsIRP.Tables[0].Rows[0]["HOS_ID"].ToString().Trim()) : "";

                string gu_id = Guid.NewGuid().ToString();
                string sqlcmd = @" insert into LogAPP (UID ,InTime,InXml,OutTime ,OutXml,TYPE,PAT_ID,SFZ_NO,PAT_NAME,REGPAT_ID,HOS_ID)
values
(@UID,@InTime,@InXml,@OutTime,@OutXm,@TYPE,@PAT_ID,@SFZ_NO,@PAT_NAME,@REGPAT_ID,@HOS_ID)";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                    new MySqlParameter("@InTime", model.inTime),
                    new MySqlParameter("@InXml", model.inXml),
                    new MySqlParameter("@OutTime", model.outTime),
                    new MySqlParameter("@OutXm",model.outXml),
                    new MySqlParameter("@TYPE",TYPE),
                    new MySqlParameter("@PAT_ID",PAT_ID),
                    new MySqlParameter("@SFZ_NO",SFZ_NO),
                    new MySqlParameter("@PAT_NAME",PAT_NAME),
                    new MySqlParameter("@REGPAT_ID",REGPAT_ID),
                    new MySqlParameter("@HOS_ID",HOS_ID)};
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch
            {
                try
                {
                    string gu_id = Guid.NewGuid().ToString();
                    string sqlcmd = @" insert into LogAPP (UID ,InTime,InXml,OutTime ,OutXml,TYPE,PAT_ID,SFZ_NO,PAT_NAME,REGPAT_ID)
values
(@UID,@InTime,@InXml,@OutTime,@OutXm,@TYPE,@PAT_ID,@SFZ_NO,@PAT_NAME,@REGPAT_ID)";

                    MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                  new MySqlParameter("@InTime", model.inTime),
                    new MySqlParameter("@InXml", model.inXml),
                    new MySqlParameter("@OutTime", model.outTime),
                    new MySqlParameter("@OutXm",model.outXml),
                    new MySqlParameter("@TYPE",TYPE),
                    new MySqlParameter("@PAT_ID","0"),
                    new MySqlParameter("@SFZ_NO",""),
                    new MySqlParameter("@PAT_NAME",""),
                    new MySqlParameter("@REGPAT_ID","0"),
                    new MySqlParameter("@HOS_ID","")};
                    DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                    return true;
                }
                catch(Exception ex)
                {
                    Write_CommandText("logapp:" + ex.ToString());
                }
            }
            return false;
        }



        /// <summary>
        /// 写本地日志文件
        /// </summary>
        /// <param name="CommandText"></param>
        public void Write_CommandText(string CommandText, string flag = "")
        {
            string SavePath = AppDomain.CurrentDomain.BaseDirectory + @"\CommandText";
            try
            {
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
                StreamWriter sr1 = new StreamWriter(System.IO.Path.Combine(SavePath, DateTime.Now.ToString("yyyyMMdd") + "." + flag), true);
                sr1.WriteLine(DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " " + CommandText);
                sr1.Close();
            }
            catch
            {
            }
        }
    }
}
