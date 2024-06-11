using CommonModel;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Report.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using MyHttpClient = System.Net.Http.HttpClient;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETMEDCERTIFICATE
    {
        public static string B_GETMEDCERTIFICATE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETMEDCERTIFICATE_M.GETMEDCERTIFICATE_IN _in = JsonConvert.DeserializeObject<GETMEDCERTIFICATE_M.GETMEDCERTIFICATE_IN>(json_in);
                GETMEDCERTIFICATE_M.GETMEDCERTIFICATE_OUT _out = new GETMEDCERTIFICATE_M.GETMEDCERTIFICATE_OUT();

                JObject jpat015 = new JObject
                {
                    { "patientId",string.IsNullOrEmpty( _in.SFZ_NO)?_in.YLCARD_NO:_in.HOSPATID },
                    { "startDate",  DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")},
                    { "EndDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") }
                };

                PushServiceResult result = HerenHelper.pushService("PAT015-QHZZJ", jpat015.ToString());

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;

                    return JsonConvert.SerializeObject(dataReturn);
                }

                List<PAT015.Data> datalist = JsonConvert.DeserializeObject<List<PAT015.Data>>(result.data.ToString());

                if (datalist.Count == 0)
                {
                    dataReturn.Code = 7;
                    dataReturn.Msg = "暂无医学证明";

                    return JsonConvert.SerializeObject(dataReturn);
                }
                _out.REPORT_ALL_NUM = datalist.Count.ToString();
                _out.REPORT_PRINT_NUM = datalist.Count.ToString();
                _out.REPORT_AUDIT_NUM = datalist.Count.ToString();
                _out.EVIDENCELIST = new List<GETMEDCERTIFICATE_M.MEDICALREPORTITEM>();

                foreach (var item in datalist)
                {
                    GETMEDCERTIFICATE_M.MEDICALREPORTITEM mdeitem = new GETMEDCERTIFICATE_M.MEDICALREPORTITEM
                    {
                        REPORT_SN = item.id.ToString(),
                        REPORT_TYPE = "",
                        REPORT_DATE = DateTimeOffset.FromUnixTimeMilliseconds(item.openDate).ToString("yyyy-MM-dd"),
                        REPORT_NAME = item.diagnosis,
                        PRINT_FLAG = getCount("医学证明", item.id.ToString()),
                        PRINT_TIME = "",
                        NOTE = "",
                        DATA_TYPE = "",
                        REPORTDATA = "",
                        REPORT_DOC_NAME = item.doctorName,
                        REPORT_DEPT_NAME = item.dept,
                        PAT_NAME = item.name,
                    };
                    _out.EVIDENCELIST.Add(mdeitem);
                }

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_GETMEDCERTIFICATEDATA(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETMEDCERTIFICATE_M.GETMEDCERTIFICATEDATA_IN _in = JsonConvert.DeserializeObject<GETMEDCERTIFICATE_M.GETMEDCERTIFICATEDATA_IN>(json_in);
                GETMEDCERTIFICATE_M.GETMEDCERTIFICATEDATA_OUT _out = new GETMEDCERTIFICATE_M.GETMEDCERTIFICATEDATA_OUT();

                _out.DATA_TYPE = "1";

                string url = "http://192.168.31.100/heren-report/api/jasper-prints/doctor-station/print-medical-certificate-pdf?id=" + _in.REPORT_SN;

                if (GetHttpResponse(url) != 200)//异常的url的 StatusCode !=200
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = "API请求失败 ID为空";

                    return JsonConvert.SerializeObject(dataReturn);
                }

                string base64String = GetPdfAsBase64(url);
                _out.REPORTDATA = base64String;

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_ZZJMEDICALEVIDENCEPRNBACK(string json_in)
        {
            ZZJMEDICALEVIDENCEPRNBACK_M.ZZJMEDICALEVIDENCEPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJMEDICALEVIDENCEPRNBACK_M.ZZJMEDICALEVIDENCEPRNBACK_IN>(json_in);
            ZZJMEDICALEVIDENCEPRNBACK_M.ZZJMEDICALEVIDENCEPRNBACK_OUT _out = new ZZJMEDICALEVIDENCEPRNBACK_M.ZZJMEDICALEVIDENCEPRNBACK_OUT();
            DataReturn dataReturn = new DataReturn();

            #region 不管成功失败,记录打印,用于计数

            try
            {
                StringBuilder str_reportmx = new StringBuilder();
                str_reportmx.Append("insert into reportmx(HOS_ID,TYPE,HOS_SN,lTERMINAL_SN,USER_ID,NOW) values (");
                str_reportmx.Append("@HOS_ID,@TYPE,@HOS_SN,@lTERMINAL_SN,@USER_ID,@NOW);");
                MySqlParameter[] parameters =
                {
                        new MySqlParameter("@HOS_ID",MySqlDbType.VarChar,20),
                        new MySqlParameter("@TYPE",MySqlDbType.VarChar,30),
                        new MySqlParameter("@HOS_SN",MySqlDbType.VarChar,100),
                        new MySqlParameter("@lTERMINAL_SN",MySqlDbType.VarChar,30),
                        new MySqlParameter("@USER_ID",MySqlDbType.VarChar,30),
                        new MySqlParameter("@NOW",MySqlDbType.DateTime)
                    };
                parameters[0].Value = _in.HOS_ID;
                parameters[1].Value = "医学证明"; //"检验报告";全部走一个接口
                parameters[2].Value = _in.REPORT_SN;
                parameters[3].Value = _in.LTERMINAL_SN;
                parameters[4].Value = _in.USER_ID;
                parameters[5].Value = DateTime.Now;
                DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
            }
            catch (Exception ex)
            {
            }

            #endregion 不管成功失败,记录打印,用于计数

            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);

            return JsonConvert.SerializeObject(dataReturn);
        }

        private static string GetPdfAsBase64(string url)
        {
            using (MyHttpClient client = new MyHttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                byte[] pdfBytes = response.Content.ReadAsByteArrayAsync().Result;
                string base64String = Convert.ToBase64String(pdfBytes);

                return base64String;
            }
        }

        private static string getCount(string TYPE, string ReportId)
        {
            string strsql = $"SELECT COUNT(*) FROM common.reportmx WHERE TYPE = '{TYPE}' AND HOS_SN = '{ReportId}'";

            DataSet ds = DB.Core.DbHelperMySQLZZJ.Query(strsql);
            int count = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
            string PRINT_FLAG = count >= 1 ? "1" : "0";
            return PRINT_FLAG;
        }
        private static int GetHttpResponse(string requestUrl)
        {
            try
            {
                HttpWebRequest HttpWResp = (HttpWebRequest)WebRequest.Create(requestUrl);
                HttpWResp.Method = "GET";
                HttpWResp.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                HttpWResp.UserAgent = null;

                HttpWebResponse response = (HttpWebResponse)HttpWResp.GetResponse();

                return (int)response.StatusCode;
            }
            catch (Exception ex)
            {
                //   throw new Exception(ex.Message);
                return 0;
            }

        }
    }
}