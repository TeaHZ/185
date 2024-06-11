using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Report.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETLISREPORT
    {
        public static string B_GETLISREPORT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            GETLISREPORT_M.GETLISREPORT_IN _in = JsonConvert.DeserializeObject<GETLISREPORT_M.GETLISREPORT_IN>(json_in);
            GETLISREPORT_M.GETLISREPORT_OUT _out = new GETLISREPORT_M.GETLISREPORT_OUT();

            bool isEightDigitNumber = Regex.IsMatch(_in.YLCARD_NO, @"^\d{8}$");

            if (_in.YLCARD_TYPE == "1" && isEightDigitNumber != true)
            {
                dataReturn.Code = 7;
                dataReturn.Msg = "请扫（输入）八位数字凭条码！";
                return JsonConvert.SerializeObject(dataReturn);
            }

            if (_in.YLCARD_TYPE == "1")
            {
                T1001.Input t1001 = new T1001.Input()
                {
                    zhengJianHM = "",// 证件号码
                    yeWuLX = "1001",// 业务类型        1001:患者信息查询
                    hospitalId = "320282466455146",// 医院ID
                    yiBaoBH = "",//医保编号 医保卡必传
                    yiBaoData = "",//医保信息        医保卡必传
                    duKaFS = "1",// 读卡方式 默认1
                    jiuZhenKH = _in.YLCARD_NO,// 就诊卡号
                    yiBaoXX = "",// 医保信息        医保卡必传
                    shouJiHao = "",//   手机号码
                };

                PushServiceResult result = HerenHelper.pushService("1001-QHZZJ", JsonConvert.SerializeObject(t1001));

                if (result.code != 1)
                {
                    dataReturn.Code = 7;
                    dataReturn.Msg = "没有找到身份信息 请检查条码是否正确";
                    return JsonConvert.SerializeObject(dataReturn);
                }

                List<T1001.data> T1001 = JsonConvert.DeserializeObject<List<T1001.data>>(result.data.ToString());
                T1001.data data = T1001.FirstOrDefault();


                //if (string.IsNullOrEmpty(data.zhengJianHM))
                //{
                //    dataReturn.Code = 7;
                //    dataReturn.Msg = "没有找到身份信息";
                //    return JsonConvert.SerializeObject(dataReturn);
                //}
 
                _in.SFZ_NO = data.zhengJianHM;

            }


            #region 检验

            List<GETLISREPORT_M.LisReport> listreprot = new List<GETLISREPORT_M.LisReport>();
            JObject jrmlis = new JObject
                {
                    //{ "IDCardNO", _in.SFZ_NO },
                    { "IDCardNO", string.IsNullOrEmpty( _in.SFZ_NO)?_in.YLCARD_NO:_in.SFZ_NO },
                    { "StartDate", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") },
                    { "EndDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") }
                };

            var rmresult = HerenHelper.LisReportService("QHZZJGetReports", JsonConvert.SerializeObject(jrmlis));

            if (rmresult.ResultCode == "1")
            {
                foreach (var dr in rmresult.Reports)
                {
                    if (dr.ReportState == "限制打印" || dr.ReportState == "已打印")
                    {
                        continue;
                    }

                    GETLISREPORT_M.LisReport lis = new GETLISREPORT_M.LisReport();

                    lis.REPORT_SN = dr.ReportID;
                    lis.REPORT_TYPE = dr.Req_Items;
                    lis.TEST_REPORT_SOURCE = "";
                    lis.REPORT_DATE = dr.Req_Date;
                    lis.REPORT_ZL = "检验";

                    lis.PRINT_FLAG = getCount(lis.REPORT_ZL, dr.ReportID);
                    lis.PRINT_TIME = "";
                    lis.TEST_DATE = dr.Req_Date;
                    lis.TEST_DOC_NAME = "";
                    lis.TEST_DEPT_NAME = dr.Req_Dept;
                    lis.AUDIT_DATE = dr.Rechk_Dt;
                    lis.AUDIT_DOC_NAME = dr.Rechk_User;
                    lis.AUDIT_FLAG = dr.ReportState == "正在检验" ? "0" : "1";
                    lis.MACHINE = "";
                    //lis.DATA_TYPE = "1";
                    //lis.REPORTDATA = dr.PDFBase64;
                    listreprot.Add(lis);
                    RedisDataHelper.SetLisResult(dr.ReportID, dr.PDFBase64);
                }
            }

            #endregion 检验

            #region 检查和病理

            int[] jclxs = { 0, 1 };

            foreach (int jclx in jclxs)
            {
                JObject jquery = new JObject
                {
                    { "zhengJianHM", _in.SFZ_NO },
                    { "PatientId", string.IsNullOrEmpty( _in.SFZ_NO)?_in.YLCARD_NO:"" },//有身份证信息 先取身份证 针对急诊没有身份证情况
                    { "StartDateTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") },
                    { "EndDateTime", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") },
                    {"Jclx",jclx }
                };

                QueryServiceResult result = HerenHelper.QueryService("EXAM005-QHZZJ", jquery);

                if (result.Head.TradeStatus != "AA")
                {
                    continue;
                }
                List<HRReport> reports = result.Body;

                foreach (var dr in reports)
                {
                    if (dr.TsStatus == "1")
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = "存在托收费用 请缴费后再次打印";
                        return JsonConvert.SerializeObject(dataReturn);
                    }
                    GETLISREPORT_M.LisReport lis = new GETLISREPORT_M.LisReport();

                    if (listreprot.Find(x => x.REPORT_SN == dr.ReportId) != null)
                    {
                        continue;
                    }

                    if (dr.UrlPath.Contains("ECG360View"))
                    {
                        continue;
                    }
                    lis.REPORT_SN = dr.ReportId;
                    lis.REPORT_TYPE = dr.ExamItemName;
                    lis.TEST_REPORT_SOURCE = "";
                    lis.REPORT_DATE = dr.ReportDateTime;
                    lis.REPORT_ZL = dr.ExamClass;


                    lis.PRINT_FLAG = getCount(lis.REPORT_ZL, dr.ReportId);
                    lis.PRINT_TIME = "";
                    lis.TEST_DATE = dr.ExamDateTime;
                    lis.TEST_DOC_NAME = "";
                    lis.TEST_DEPT_NAME = dr.ExamDateTime;
                    lis.AUDIT_DATE = dr.ReportDateTime;
                    lis.AUDIT_DOC_NAME = "";
                    lis.AUDIT_FLAG = dr.Reporttype;
                    lis.MACHINE = "";
                    //lis.DATA_TYPE = "2";
                    //lis.REPORTDATA = dr.UrlPath;
                    listreprot.Add(lis);
                    RedisDataHelper.SetLisResult(dr.ReportId, dr.UrlPath);
                }
            }

            #endregion 检查和病理

            if (listreprot.Count == 0)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "没有尚未打印的报告，请稍后再试";
                return JsonConvert.SerializeObject(dataReturn);
            }

            _out.LISREPORT = listreprot;
            _out.REPORT_ALL_NUM = listreprot.Count.ToString();
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
        private static string getCount(string TYPE, string ReportId)
        {
            string strsql = $"SELECT COUNT(*) FROM common.reportmx WHERE TYPE = '{TYPE}' AND HOS_SN = '{ReportId}'";

            DataSet ds = DB.Core.DbHelperMySQLZZJ.Query(strsql);
            int count = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
            string PRINT_FLAG = count >= 1 ? "1" : "0";
            return PRINT_FLAG;
        }
    }
}