using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Report.Model;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static OnlineBusHos9_Report.Model.GETLISRESULT_M;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETLISRESULT
    {
        public static string B_GETLISRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETLISRESULT_M.GETLISRESULT_IN _in = JsonConvert.DeserializeObject<GETLISRESULT_M.GETLISRESULT_IN>(json_in);
                GETLISRESULT_M.GETLISRESULT_OUT _out = new GETLISRESULT_M.GETLISRESULT_OUT();

                var base64 = RedisDataHelper.GetLisResult(_in.REPORT_SN);

                List<ReportData> reportData = new List<ReportData>();

                ReportData report = new ReportData()
                {
                    DATA_TYPE = isBase64(base64) ? "1" : "2",
                    REPORTDATA = base64
                };
                reportData.Add(report);

                _out.REPORTLIST = reportData;
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


        private static bool isBase64(string base64)
        {

            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}