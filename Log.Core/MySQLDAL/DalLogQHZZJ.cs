using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

namespace Log.Core.MySQLDAL
{
    public class DalLogQHZZJ
    {
        public bool Add(Log.Core.Model.ModLogQHZZJ model)
        {
            string TYPE = "";
            try
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(model.InData);
                string HOS_ID = dic.ContainsKey("HOS_ID") ? FormatHelper.GetStr(dic["HOS_ID"]) : "";
                string SFZ_NO = dic.ContainsKey("SFZ_NO") ? FormatHelper.GetStr(dic["SFZ_NO"]) : "";
                string PAT_NAME = dic.ContainsKey("PAT_NAME") ? FormatHelper.GetStr(dic["PAT_NAME"]) : "";

                string gu_id = Guid.NewGuid().ToString();
                string sqlcmd = @" insert into LogQHZZJ (UID ,InTime,InData,OutTime ,OutData,BUS,BUS_NAME,SUB_BUSNAME,HOS_ID,SFZ_NO,PAT_NAME)
values
(@UID,@InTime,@InData,@OutTime,@OutData,@BUS,@BUS_NAME,@SUB_BUSNAME,@HOS_ID,@SFZ_NO,@PAT_NAME)";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                    new MySqlParameter("@InTime", model.InTime),
                    new MySqlParameter("@InData", model.InData),
                    new MySqlParameter("@OutTime", model.OutTime),
                    new MySqlParameter("@OutData",model.OutData),
                    new MySqlParameter("@BUS",model.BUS),
                    new MySqlParameter("@BUS_NAME",model.BUS_NAME),
                    new MySqlParameter("@SUB_BUSNAME",model.SUB_BUSNAME),
                    new MySqlParameter("@SFZ_NO",SFZ_NO),
                    new MySqlParameter("@PAT_NAME",PAT_NAME),
                    new MySqlParameter("@HOS_ID",HOS_ID)};
                DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                return true;
            }
            catch
            {
                try
                {
                    string gu_id = Guid.NewGuid().ToString();
                    string sqlcmd = @" insert into LogQHZZJ (UID ,InTime,InData,OutTime ,OutData,BUS,BUS_NAME,SUB_BUSNAME,HOS_ID,SFZ_NO,PAT_NAME)
values
(@UID,@InTime,@InData,@OutTime,@OutData,@BUS,@BUS_NAME,@SUB_BUSNAME,@HOS_ID,@SFZ_NO,@PAT_NAME)";

                    MySqlParameter[] parameters = {
                    new MySqlParameter("@UID",gu_id),
                    new MySqlParameter("@InTime", model.InTime),
                    new MySqlParameter("@InData", model.InData),
                    new MySqlParameter("@OutTime", model.OutTime),
                    new MySqlParameter("@OutData",model.OutData),
                    new MySqlParameter("@BUS",model.BUS),
                    new MySqlParameter("@BUS_NAME",model.BUS_NAME),
                    new MySqlParameter("@SUB_BUSNAME",model.SUB_BUSNAME),
                    new MySqlParameter("@SFZ_NO",""),
                    new MySqlParameter("@PAT_NAME",""),
                    new MySqlParameter("@HOS_ID","")};
                    DbHelperMySQL.ExecuteSql(sqlcmd, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    Write_CommandText("logQHZZJ:" + ex.ToString());
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
