using CommonModel;
using ConstData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_EInvoice.Model;
using OnlineBusHos9_EInvoice.Model.qheinvoice;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_EInvoice.BUS
{
    internal class GetHisIssueBySfzno
    {
        public static string B_GetHisIssueBySfzno(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            try
            {
                GetHisIssueBySfzno_IN _in = JsonConvert.DeserializeObject<GetHisIssueBySfzno_IN>(json_in);
                GetHisIssueBySfzno_OUT _out = new GetHisIssueBySfzno_OUT();

                string patientid="";

                if (_in.YLCARD_TYPE != "1")
                {
                    T1001.Input t1001 = new T1001.Input()
                    {
                        zhengJianHM = _in.SFZ_NO,// 证件号码
                        yeWuLX = "1001",// 业务类型        1001:患者信息查询
                        hospitalId = "320282466455146",// 医院ID
                        yiBaoBH = "",//医保编号 医保卡必传
                        yiBaoData = "",//医保信息        医保卡必传
                        duKaFS = "1",// 读卡方式 默认1
                        jiuZhenKH = _in.YLCARD_NO,// 就诊卡号
                        yiBaoXX = "",// 医保信息        医保卡必传
                        shouJiHao = "",//   手机号码
                    };

                    PushServiceResult<List<T1001.data>> result = HerenHelper<List<T1001.data>>.pushService("1001-QHZZJ", JsonConvert.SerializeObject(t1001));

                    if (result.code == 1)
                    {
                        T1001.data data = result.data.FirstOrDefault();
                        patientid = data.jianDangId;
                    }
                }
                else
                {
                    patientid = _in.YLCARD_NO;
                }

                if (string.IsNullOrEmpty(_in.BEGIN_DATE))
                {
                    _in.BEGIN_DATE = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd");
                    _in.END_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                }

                _out.HISISSUELISTS = new List<GetHisIssueBySfzno_OUT.Hisissuelist>();
                JObject j52081 = new JObject();

                j52081.Add("patientId", patientid);
                j52081.Add("startDate", _in.BEGIN_DATE);
                j52081.Add("endDate", _in.END_DATE);

                PushServiceResult<List<T52081.data>> result52081 = HerenHelper<List<T52081.data>>.pushService("52081-QHZZJ", j52081.ToString());


                if (result52081.code!=1)
                {
                    dataReturn.Code = CodeDefine.UIMessageType_Error;
                    dataReturn.Msg = "无可打印的发票!";

                    return JsonConvert.SerializeObject(dataReturn);

                }
                foreach (var item in result52081.data)
                {
                    Model.qheinvoice.INVOICEQUERYBYHISRCPTNO invoicequerybyhisrcptno = new Model.qheinvoice.INVOICEQUERYBYHISRCPTNO()
                    {
                        HOS_ID = _in.HOS_ID,
                        rcpt_no = item.rcptNo,
                    };

                    Model.qheinvoice.RootRequest<Model.qheinvoice.INVOICEQUERYBYHISRCPTNO> root = new RootRequest<INVOICEQUERYBYHISRCPTNO>("INVOICEQUERYBYHISRCPTNO");

                    root.BODY = invoicequerybyhisrcptno;

                    string url = "http://192.168.17.15:8000/Service.asmx";

                    Hashtable hs = new Hashtable();
                    hs.Add("jsonValue", JsonConvert.SerializeObject(root));

                    string outjson = WebServiceHelper.QuerySoapWebService(url, "BusinessElectInvoice", hs).InnerText;

                    Model.qheinvoice.RootReponse<INVOICEQUERYBYHISRCPTNODATA> reponse = JsonConvert.DeserializeObject<Model.qheinvoice.RootReponse<INVOICEQUERYBYHISRCPTNODATA>>(outjson);


                    if (reponse.ROOT.BODY.CLBZ!="0")
                    {
                        continue;
                    }

                    foreach (var rcpt in reponse.ROOT.BODY.RCPTLIST)
                    {
                        if (rcpt.invoice_status == "04")
                        {
                            continue;
                        }

                        if (rcpt.isprint == "1")
                        {
                            continue;
                        }
                        GetHisIssueBySfzno_OUT.Hisissuelist list = new GetHisIssueBySfzno_OUT.Hisissuelist();
                        list.INVOICE_CODE = rcpt.invoice_code;
                        list.INVOICE_NUMBER = rcpt.invoice_number;
                        list.INVOICING_PARTY_NAME = rcpt.invoicing_party_name;
                        list.PAYER_PARTY_NAME = rcpt.payer_party_name;
                        list.TOTAL_AMOUNT = rcpt.total_amount;
                        list.SFTYPENAME = item.rcptType;
                        list.STATUS = "";
                        list.SAVEDDATE_TIME = item.operationDate;
                        list.ISPRINT = ((int)decimal.Parse(rcpt.isprint)).ToString();
                        list.print_times = "";
                        list.IS_ISSUE = "0";
                        list.IS_CHECK = "0";
                        list.SETTLECODE = "";
                        list.BUSINESS_TYPE = "1";
                        list.INPATIENTNO = "";

                        _out.HISISSUELISTS.Add(list);
                    }
                }

                if (_out.HISISSUELISTS.Count == 0)
                {
                    dataReturn.Code = CodeDefine.UIMessageType_Error;
                    dataReturn.Msg = "无可打印的发票!";
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 7;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }

            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}