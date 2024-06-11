using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static OnlineBusHos9_Report.Model.GETLISRESULT_M;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETMEDICALRECORD
    {
        public static string B_GETMEDICALRECORD(string json_in)
        {
            GETMEDICALRECORD_M.GETMEDICALRECORD_IN _in = JsonConvert.DeserializeObject<GETMEDICALRECORD_M.GETMEDICALRECORD_IN>(json_in);
            GETMEDICALRECORD_M.GETMEDICALRECORD_OUT _out = new GETMEDICALRECORD_M.GETMEDICALRECORD_OUT();
            _out.MEDICALREPORT = new List<GETMEDICALRECORD_M.MEDICALREPORT>();

            if (_in.YLCARD_TYPE == "1")//条码
            {
                _in.HOSPATID = _in.YLCARD_NO;
            }

            DataReturn dataReturn = new DataReturn();

            try
            {


                JObject jpat014 = new JObject();
                jpat014.Add("patientId", _in.HOSPATID);
                jpat014.Add("startDate", DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"));
                jpat014.Add("endDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));



               PushServiceResult result = HerenHelper.pushService("PAT014-QHZZJ", jpat014.ToString());




                HRMedicalRecord recor = JsonConvert.DeserializeObject<HRMedicalRecord>( result.data.ToString());

                _out.REPORT_ALL_NUM = recor.report_ALL_NUM;
                _out.REPORT_AUDIT_NUM = recor.report_AUDIT_NUM;
                _out.REPORT_PRINT_NUM = recor.report_PRINT_NUM;
                foreach (var item in recor.medicalreport)
                {
                    GETMEDICALRECORD_M.MEDICALREPORT mEDICALREPORT = new GETMEDICALRECORD_M.MEDICALREPORT
                    {
                        REPORT_SN = item.report_SN,
                        REPORT_DEPT_NAME = item.prport_DEPT_NAME,
                        REPORT_DOC_NAME = item.prport_DOC_NAME,
                        REPORT_DATE = item.REPORT_DATE,
                        PRINT_FLAG = item.print_FLAG,//测试用，0， item.isPrint,
                        DATA_TYPE = "2",
                        REPORTDATA=item.reportdata,
                        REPORT_TYPE = item.report_TYPE,
                        
                    };
                    RedisDataHelper.SetLisResult(item.report_SN, item.reportdata);

                    _out.MEDICALREPORT.Add(mEDICALREPORT);
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);

                return JsonConvert.SerializeObject(dataReturn);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 8;
                dataReturn.Msg += ex.ToString();
                return JsonConvert.SerializeObject(dataReturn);
            }
        }

        public static string B_GETMEDICALRESULT(string json_in)
        {
            GETMEDICALRESULT_M.GETMEDICALRESULT_IN _in = JsonConvert.DeserializeObject<GETMEDICALRESULT_M.GETMEDICALRESULT_IN>(json_in);
            GETMEDICALRESULT_M.GETMEDICALRESULT_OUT _out = new GETMEDICALRESULT_M.GETMEDICALRESULT_OUT();

     
            DataReturn dataReturn = new DataReturn();

  
            try
            {

                JObject jpat113 = new JObject();

                jpat113.Add("encounterNo", _in.REPORT_SN);

                PushServiceResult result = HerenHelper.pushService("PAT013-QHZZJ", jpat113.ToString());

                HRMedicalResult medical = JsonConvert.DeserializeObject<HRMedicalResult>(result.data.ToString());



                List<ReportData> reportData = new List<ReportData>();

                ReportData report = new ReportData()
                {
                    DATA_TYPE = medical.data_TYPE.ToString(),
                    REPORTDATA = medical.reportdata
                };
                reportData.Add(report);

                _out.REPORTLIST = reportData;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);

                return JsonConvert.SerializeObject(dataReturn);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                return JsonConvert.SerializeObject(dataReturn);
            }
        }
        private static bool isBase64(string base64)
        {

            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        public static string B_ZZJMEDICALPRNBACK(string json_in)
        {
            ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_IN>(json_in);
            ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_OUT _out = new ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_OUT();
            DataReturn dataReturn = new DataReturn();


            JObject jpat114 = new JObject();

            jpat114.Add("encounterNo", _in.REPORT_SN);

            PushServiceResult result = HerenHelper.pushService("PAT114-QHZZJ", jpat114.ToString());



            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);

            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}