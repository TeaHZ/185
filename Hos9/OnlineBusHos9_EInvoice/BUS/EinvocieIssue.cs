using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_EInvoice.Class;
using OnlineBusHos9_EInvoice.Model;
using OnlineBusHos9_EInvoice.Model.qheinvoice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using static OnlineBusHos9_EInvoice.Model.EinvoiceIssue_OUT;

namespace OnlineBusHos9_EInvoice.BUS
{
    internal class EinvocieIssue
    {
        public static string B_EinvocieIssue(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            string json_out = "";
            Model.EinvoiceIssue_IN _in = JsonConvert.DeserializeObject<Model.EinvoiceIssue_IN>(json_in);
            Model.EinvoiceIssue_OUT _out = new Model.EinvoiceIssue_OUT();
            string patientid = "";

            if (string.IsNullOrEmpty(_in.HOSPATID))
            {
                T1001.Input t1001 = new T1001.Input()
                {
                    zhengJianHM = _in.SFZ_NO,// 证件号码
                    yeWuLX = "1001",// 业务类型        1001:患者信息查询
                    hospitalId = "320282466455146",// 医院ID
                    yiBaoBH = "",//医保编号 医保卡必传
                    yiBaoData = "",//医保信息        医保卡必传
                    duKaFS = "1",// 读卡方式 默认1
                    jiuZhenKH = "",// 就诊卡号
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
                patientid = _in.HOSPATID;
            }

            JObject j52081 = new JObject();

            j52081.Add("patientId", patientid);
            j52081.Add("startDate", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 00:00:00"));
            j52081.Add("endDate", DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            PushServiceResult<List<T52081.data>> result52081 = HerenHelper<List<T52081.data>>.pushService("52081-QHZZJ", j52081.ToString());
            
            if (result52081.code != 1)
            {
                dataReturn.Code = 0;
                dataReturn.Msg = "52081接口交互异常!";
                return JsonConvert.SerializeObject(dataReturn);

            }
            

            //List<INVOICE> INVOICEinfo = new List<INVOICE>();
                foreach (var item in result52081.data)
                {
                if (!item.rcptType.Contains("住院"))
                {
                    continue;
                }
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


                    if (reponse.ROOT.BODY.CLBZ != "0")
                    {
                        continue;
                    }

                    foreach (var rcpt in reponse.ROOT.BODY.RCPTLIST)
                    {
                        if (rcpt.invoice_status == "04")
                        {
                            continue;
                        }

                        //if (rcpt.isprint == "1")
                        //{
                        //    continue;
                        //}

                        Model.qheinvoice.EInvoiceDownload download = new Model.qheinvoice.EInvoiceDownload()
                        {
                            HOS_ID = _in.HOS_ID,
                            app_id = "32028213810201",
                            invoice_code = rcpt.invoice_code,
                            invoice_number = rcpt.invoice_number,
                            //InvoiceType = "9",//9 发票清单一个base64 INVENTORYFILEDATA
                        };

                        Model.qheinvoice.RootRequest<Model.qheinvoice.EInvoiceDownload> root1 = new RootRequest<Model.qheinvoice.EInvoiceDownload>("INVOICEDOWNLOAD");

                        root1.BODY = download;

                        Hashtable hs1 = new Hashtable();
                        hs1.Add("jsonValue", JsonConvert.SerializeObject(root1));

                        string outjson1 = WebServiceHelper.QuerySoapWebService(url, "BusinessElectInvoice", hs1).InnerText;

                        Model.qheinvoice.RootReponse<Model.qheinvoice.EInvoiceDownloaddata> reponse1 = JsonConvert.DeserializeObject<Model.qheinvoice.RootReponse<Model.qheinvoice.EInvoiceDownloaddata>>(outjson1);

                        //INVOICE iNVOICE = new INVOICE()
                        //{
                            
                        //    INVOICEFILEDATA = reponse1.ROOT.BODY.invoicefiledata,
                        //    INVENTORYFILEDATA = reponse1.ROOT.BODY.inventoryfiledata,
                        //};
                        _out.INVOICEFILEDATA = reponse1.ROOT.BODY.invoicefiledata;
                        _out.INVENTORYFILEDATA = reponse1.ROOT.BODY.inventoryfiledata;
                        _out.DATA_TYPE = "1_1";
                        //INVOICEinfo.Add(iNVOICE);
                    }
                }
            
            //_out.INVOICELIST = INVOICEinfo;
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
        private static void SaveBase64AsPdf(string base64String, string destinationFolderPath, string fileName)
        {
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

            // 将 Base64 字符串转换为字节数组
            byte[] fileBytes = Convert.FromBase64String(base64String);

            // 将字节数组保存为 PDF 文件
            File.WriteAllBytes(destinationFilePath, fileBytes);
        }
    }
}