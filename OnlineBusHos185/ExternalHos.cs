using MySql.Data.MySqlClient;
using Soft.Common;
using Soft.DBUtility;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace OnlineBusHos185
{
    public class ExternalHos
    {
        private static string HOS_ID = "185";

        /// <summary>
        /// 中台交易
        /// </summary>
        bool midservice = true;

        /// <summary>
        /// 统一调用函数
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        private static string CallService(string xmlstr, string source)
        {
            DateTime InTime = DateTime.Now;
            try
            {
                XmlDocument xmlDocument = XMLHelper.X_GetXmlDocument(xmlstr);
                if (source.Contains("A"))
                {
                    XMLHelper.X_XmlInsertNode(xmlDocument, "ROOT/HEADER", "SOURCE", "QHAPP");
                }
                else
                {
                    XMLHelper.X_XmlInsertNode(xmlDocument, "ROOT/HEADER", "SOURCE", "MYNJ");
                }
                xmlstr = xmlDocument.InnerXml;
                XmlDocument doc = new XmlDocument();
                string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "bin", "OnlineBusHos185.dll.config");
                doc.Load(path);
                DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包
                string pherText, strMD5;
                CommonFunction.GetSecretHOS(HOS_ID, xmlstr, out pherText, out strMD5);
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                hashtable.Add("xmlstr", pherText);
                hashtable.Add("user_id", HOS_ID);
                hashtable.Add("signature", strMD5);
                string url = ds.Tables[0].Rows[0]["value"].ToString().Trim();
                string doc_1 = "";
                string result = "";
                StringBuilder param = new StringBuilder();
                WebRequest httpRequest = WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "text/xml;charset=UTF-8";
                param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.tkhealthcare.com/\">");
                param.Append("<soapenv:Header/>");
                param.Append("<soapenv:Body>");
                param.Append("<ser:BusinessElectInvoice_SECRET>");
                param.Append("<xmlstr>");
                param.Append(pherText);
                param.Append("</xmlstr>");
                param.Append("<user_id>");
                param.Append(HOS_ID);
                param.Append("</user_id>");
                param.Append("<signature>");
                param.Append(strMD5);
                param.Append("</signature>");
                param.Append("</ser:BusinessElectInvoice_SECRET>");
                param.Append("</soapenv:Body>");
                param.Append("</soapenv:Envelope>");
                byte[] bytes = Encoding.UTF8.GetBytes(param.ToString());
                httpRequest.ContentLength = param.ToString().Length;
                using (Stream reqStream = httpRequest.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Flush();
                }

                using (WebResponse myResponse = httpRequest.GetResponse())
                {
                    // StreamReader对象
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    // 返回结果
                    string responseString = sr.ReadToEnd();
                    XmlDocument doc_back = new XmlDocument();
                    doc_back.LoadXml(responseString);
                    result = doc_back.FirstChild.InnerText;
                }
                string rtnxml = AESExample.AESDecrypt(result, CommonFunction.GetSecretKEY(HOS_ID));
                ////XmlDocument doc_2 = WebServiceHelper.QuerySoapWebService(url, "BusinessElectInvoice_SECRET", hashtable);
                //if (url.Contains("36061"))//端口36061是测试端口
                //{
                //    TKServiceTest.DateServiceClient serviceClientTest = new TKServiceTest.DateServiceClient();
                //    doc_1= serviceClientTest.BusinessElectInvoice_SECRET(pherText, HOS_ID, strMD5);
                //}
                //else
                //{
                //    TKService.DateServiceClient serviceClient = new TKService.DateServiceClient();
                //    //serviceClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(url);
                //    doc_1 = serviceClient.BusinessElectInvoice_SECRET(pherText, HOS_ID, strMD5);
                //}
                // //
                //string rtnxml = AESExample.AESDecrypt(doc_1, CommonFunction.GetSecretKEY(HOS_ID));
                SaveLog(InTime, xmlstr, DateTime.Now, rtnxml);//保存his接口日志
                return rtnxml;
            }
            catch (Exception ex)
            {
                SaveLog(InTime, xmlstr, DateTime.Now, ex.ToString());//保存his接口日志
                return null;
            }

            //DateTime InTime = DateTime.Now;
            //try
            //{
            //    XmlDocument xmlDocument = XMLHelper.X_GetXmlDocument(xmlstr);
            //    if (source.Contains("A"))
            //    {
            //        XMLHelper.X_XmlInsertNode(xmlDocument, "ROOT/HEADER", "SOURCE", "QHAPP");
            //    }
            //    else
            //    {
            //        XMLHelper.X_XmlInsertNode(xmlDocument, "ROOT/HEADER", "SOURCE", "MYNJ");
            //    }
            //    xmlstr = xmlDocument.InnerXml;
            //    TKXLService.DateAccessServiceImplService service = new TKXLService.DateAccessServiceImplService();
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\bin\OnlineBusHos185.dll.config");
            //    DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包
            //    service.Url = ds.Tables[0].Rows[0]["value"].ToString().Trim();
            //    string pherText, strMD5;

            //    CommonFunction.GetSecretHOS(HOS_ID, xmlstr, out pherText, out strMD5);
            //    string rtnxml = service.BusinessElectInvoice_SECRET(pherText, HOS_ID, strMD5);
            //    rtnxml = AESExample.DecryptByKEY(rtnxml, CommonFunction.GetSecretKEY(HOS_ID));

            //    //ServiceBUS.Log.LogHelper.SaveLogHOS(InTime, xmlstr, DateTime.Now, rtnxml);
            //    Log.Helper.Model.ModLogHos modLogHos = new Log.Helper.Model.ModLogHos();
            //    modLogHos.inTime = InTime;
            //    modLogHos.outTime = DateTime.Now;
            //    modLogHos.inXml = xmlstr;
            //    modLogHos.outXml = rtnxml;
            //    Log.Helper.LogHelper.Addlog(modLogHos);
            //    return rtnxml;
            //}
            //catch (Exception ex)
            //{
            //    Log.Helper.Model.ModLogHos modLogHos = new Log.Helper.Model.ModLogHos();
            //    modLogHos.inTime = InTime;
            //    modLogHos.outTime = DateTime.Now;
            //    modLogHos.inXml = xmlstr;
            //    modLogHos.outXml = ex.ToString();
            //    Log.Helper.LogHelper.Addlog(modLogHos);
            //    return null;
            //}
        }

        /// <summary>
        /// 保存调用HIS的日志
        /// </summary>
        /// <param name="intime"></param>
        /// <param name="inxml"></param>
        /// <param name="outTimem"></param>
        /// <param name="outxml"></param>
        private static void SaveLog(DateTime intime, string inxml, DateTime outTime, string outxml)
        {
            Log.Helper.Model.ModLogHos logHos = new Log.Helper.Model.ModLogHos();
            logHos.inTime = intime;
            logHos.inXml = inxml;
            logHos.outTime = outTime;
            logHos.outXml = outxml;
            Log.Helper.LogHelper.Addlog(logHos);
        }

        public DataTable ReturnFail(string CLBZ, string CLJG)
        {
            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));
            DataRow newrow = dtresult.NewRow();
            newrow["CLBZ"] = CLBZ;
            newrow["CLJG"] = CLJG;
            dtresult.Rows.Add(newrow);
            return dtresult;
        }

        /// <summary>
        /// 获取医保明细数据（医院端）
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETHISYBDETAILINFOBYDATE(string STARTDATE, string ENDDATE, string HXFLAG, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 获取医保明细数据（医保端）
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETNJYBDETAILINFOBYDATE(string STARTDATE, string ENDDATE, string HXFLAG, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 对帐明细支付端数据（不含APP） 20161122
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <returns></returns>
        public DataSet GETZFOTHERMX(string INCHECKDATE, string ZFTYPE, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 获取医院服务器上平台明细数据 20170922
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="ZFTYPE"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETPLATPOSMX(string INCHECKDATE, string ZFTYPE, Dictionary<string, string> dic)
        {
            return null;
        }

        public bool CHECKHOSCARD(string HOS_ID, string YLCARTD_TYPE, string YLCARD_NO, Dictionary<string, string> dic)
        {
            return true;
        }

        /// <summary>
        /// 预约挂号保存
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public DataTable REGISTERAPPTSAVE(string SFZ_NO, string MOBILE_NO, string PAT_NAME, string SEX, string BIRTHDAY, string ADDRESS, string GUARDIAN_NAME, string GUARDIAN_SFZ_NO, int YLCARTD_TYPE, string YLCARD_NO, string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, int SCH_TYPE, string PERIOD_START, string PERIOD_END, string WAIT_ID, string lTERMINAL_SN, string PASSWORD, Dictionary<string, string> dic)
        {
            string Nextday = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string Time = DateTime.Now.ToString("HH:mm");
            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));

            string YNCARDNO = "";
            string PAT_ID = dic.ContainsKey("PAT_ID") ? FormatHelper.GetStr(dic["PAT_ID"]) : "0";
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1  order by BAND_TIME DESC", "YLCARD_NO");
            //if (dtpatcardbind.Rows.Count > 0)
            //{
            //    YNCARDNO = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            //}
            string BARCODE = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID, YLCARTD_TYPE.ToString(), YLCARD_NO);
            if (BARCODE == "")
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "1";
                newrow["CLJG"] = "很抱歉，未能获取到您的院内卡信息，请去人工窗口预约！";
                dtresult.Rows.Add(newrow);
                return dtresult;
            }
            string PRO_TITLE = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERAPPTSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", MOBILE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEX", SEX.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIRTHDAY", BIRTHDAY.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", ADDRESS.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_NAME", GUARDIAN_NAME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_SFZ_NO", GUARDIAN_SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", ((YLCARTD_TYPE.ToString() == "0" || YLCARTD_TYPE.ToString() == "4") ? "4" : YLCARTD_TYPE.ToString()));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", ((YLCARTD_TYPE.ToString() == "0" || YLCARTD_TYPE.ToString() == "4") ? SFZ_NO : YLCARD_NO));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", SCH_DATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TIME", SCH_TIME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", SCH_TYPE.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PERIOD_START", PERIOD_START.Trim().Length > 5 ? PERIOD_START.Trim().Substring(0, 5) : PERIOD_START.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PERIOD_END", PERIOD_END.Trim().Length > 5 ? PERIOD_END.Trim().Substring(0, 5) : PERIOD_END.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "WAIT_ID", WAIT_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRO_TITLE", PRO_TITLE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", BARCODE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", dic["REGISTER_TYPE"]);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev.Rows.Count > 0 && dtrev.Rows[0]["CLJG"].ToString().Contains("已停诊"))
                {
                    string sqlcmd = string.Format(@"update `schedule` set count_rem=0,COUNT_ALL=0 where HOS_ID='{0}' and DEPT_CODE='{1}' and DOC_NO='{2}' and SCH_DATE='{3}' and SCH_TIME='{4}' and SCH_TYPE='{5}';", HOS_ID, DEPT_CODE, DOC_NO, SCH_DATE, SCH_TIME, SCH_TYPE);
                    sqlcmd += string.Format(@"update schedule_period set count_yet=0,COUNT_ALL=0 where HOS_ID='{0}' and DEPT_CODE='{1}' and DOC_NO='{2}' and SCH_DATE='{3}' and SCH_TIME='{4}' and PERIOD_START='{5}';", HOS_ID, DEPT_CODE, DOC_NO, SCH_DATE, SCH_TIME, PERIOD_START);
                    new Plat.BLL.BaseFunction().ExecSql(sqlcmd);
                }
                if (dtrev != null && !dtrev.Columns.Contains("YQBZ"))
                {
                    dtrev.Columns.Add("YQBZ", typeof(string));
                    foreach (DataRow dr in dtrev.Rows)
                    {
                        dr["YQBZ"] = "H" + HOS_ID;
                    }
                }
                else
                {
                    foreach (DataRow dr in dtrev.Rows)
                    {
                        dr["YQBZ"] = "H" + HOS_ID;
                    }
                }
                return dtrev;
            }
            catch (Exception ex)
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "1";
                newrow["CLJG"] = ex.Message; ;
                dtresult.Rows.Add(newrow);
                return dtresult;
            }
        }

        /// <summary>
        /// 预约(实时)挂号支付
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public DataTable REGISTERPAYSAVE(string SFZ_NO, string MOBILE_NO, string PAT_NAME, string SEX, string BIRTHDAY, string ADDRESS, string GUARDIAN_NAME, string GUARDIAN_SFZ_NO, int YLCARTD_TYPE, string YLCARD_NO, string QUERYID, string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, string SCH_TYPE, string PERIOD_START, string WAIT_ID, string HOS_SN, decimal CASH_JE, string DEAL_STATES, string DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, string PassWord, Dictionary<string, string> dic)
        {
            string PAT_ID = dic["PAT_ID"];
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERPAYSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", MOBILE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEX", SEX.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIRTHDAY", BIRTHDAY.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", ADDRESS.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_NAME", GUARDIAN_NAME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_SFZ_NO", GUARDIAN_SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", SCH_DATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TIME", SCH_TIME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", SCH_TYPE.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PERIOD_START", PERIOD_START.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "WAIT_ID", WAIT_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", DEAL_STATES.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DEAL_TIME.Trim());

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", QUERYID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", ((YLCARTD_TYPE.ToString() == "0" || YLCARTD_TYPE.ToString() == "4") ? "4" : YLCARTD_TYPE.ToString()));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", ((YLCARTD_TYPE.ToString() == "0" || YLCARTD_TYPE.ToString() == "4") ? SFZ_NO : YLCARD_NO));

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", "");
            bool issyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "JSSYB";//是否是省医保病人
            bool isnjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "NJSYB";//是否是省医保病人;//是否是南京市医保病人
            bool isgjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "CHSYB";//是否是国家医保病人
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBPAT_TYPE", dic.ContainsKey("YBPAT_TYPE") ? dic["YBPAT_TYPE"] == "CHSYB" ? "GJYB" : dic["YBPAT_TYPE"] : "");
            if (isnjyb)
            {
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", dic.ContainsKey("PAT_CARD_OUT") ? CommonFunction.GetJsonValue(FormatHelper.GetStr(dic["PAT_CARD_OUT"]), "mediInsuOutpam") : "");
            }
            else if (issyb)
            {
                DataTable dtsyb_pat = new Plat.BLL.BaseFunction().GetList("jjsyb_patinfo", "pat_id='" + dic["PAT_ID"] + "'", "YLCARD_NO,MAN_ID,MAN_TYPE,YLLB,UNIT_ID");
                if (dtsyb_pat.Rows.Count > 0)
                {
                    string pat_card_out = CommonFunction.GetStr(dtsyb_pat.Rows[0]["YLCARD_NO"]) + "|" + CommonFunction.GetStr(dtsyb_pat.Rows[0]["MAN_ID"]) + "|" + CommonFunction.GetStr(dtsyb_pat.Rows[0]["MAN_TYPE"]) + "|" + CommonFunction.GetStr(dtsyb_pat.Rows[0]["YLLB"]) + "|" + CommonFunction.GetStr(dtsyb_pat.Rows[0]["UNIT_ID"]);
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", pat_card_out);
                }
            }
            else
            {
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", "");
            }
            MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@comm_sn", QUERYID) };
            DataTable dtunionpay = DbHelperMySQL.Query("select provider from yunhou.yunhos_unionpaytran where comm_sn=@comm_sn and txn_type='01'", spa).Tables[0];
            if (dtunionpay.Rows.Count > 0)
            {
                string provider = CommonFunction.GetStr(dtunionpay.Rows[0]["provider"]);
                if (provider == "1")
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G2");
                }
                else if (provider == "3")
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G1");
                }
                else if (provider == "7")
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G3");
                }
                else
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G");
                }
            }
            else
            {
                DataTable dtConInfo = new Plat.BLL.BaseFunction().GetList("gettnpatinfo", "ORDERID='" + QUERYID + "'", "DEAL_TYPE");
                if (dtConInfo.Rows.Count > 0)
                {
                    switch (FormatHelper.GetStr(dtConInfo.Rows[0]["DEAL_TYPE"]).ToUpper())
                    {
                        case "D"://省医保病人传D1，D2给HIS
                            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", CommonFunction.GetStr(dtConInfo.Rows[0]["DEAL_TYPE"]) + DEAL_TYPE);
                            break;

                        default:
                            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", CommonFunction.GetStr(dtConInfo.Rows[0]["DEAL_TYPE"]));
                            break;
                    }
                }
                else
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", DEAL_TYPE);
                }
            }

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JS_OUT", dic.ContainsKey("JS_OUT") ? dic["JS_OUT"].Trim() : "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JE_ALL", dic.ContainsKey("JE_ALL") ? dic["JE_ALL"].Trim() : "0");

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_IN", dic.ContainsKey("YJS_IN") ? dic["YJS_IN"] : "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_OUT", dic.ContainsKey("YJS_OUT") ? dic["YJS_OUT"].Trim() : "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JS_IN", dic.ContainsKey("YB_IN") ? dic["YB_IN"].Trim() : "");

            if (isgjyb)
            {
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT1101", dic["CHSOUTPUT1101"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSINPUT2201", dic["CHSINPUT2201"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT2201", dic["CHSOUTPUT2201"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT2206", dic["CHSOUTPUT2206"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSINPUT2207", dic["CHSINPUT2207"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT2207", dic["CHSOUTPUT2207"]);
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "EXPCONTENT", dic["EXPCONTENT"]);
                QHSiInterface.RT1101.Root rt1101 = JSONSerializer.Deserialize<QHSiInterface.RT1101.Root>(dic["CHSOUTPUT1101"]);
                string CHSOUTPUT5360 = "";
                if (rt1101.output.data.Count > 0)
                {
                    QHSiInterface.RT5360.Root rt5360 = new QHSiInterface.RT5360.Root();
                    rt5360.output = new QHSiInterface.RT5360.Output();
                    rt5360.output.data = new List<QHSiInterface.RT5360.insuinfo>();
                    rt5360.infcode = "0";
                    rt5360.err_msg = "";
                    rt5360.inf_refmsgid = "";
                    rt5360.respond_time = "";
                    rt5360.refmsg_time = "";
                    for (int i = 0; i < rt1101.output.data.Count; i++)
                    {
                        QHSiInterface.RT5360.insuinfo insuinfo = new QHSiInterface.RT5360.insuinfo()
                        {
                            psn_no = rt1101.output.data[i].psn_no,
                            med_type = rt1101.output.data[i].med_type,
                            insutype = rt1101.output.data[i].insutype,
                            begndate = rt1101.output.data[i].begndate,
                            dise_codg = rt1101.output.data[i].dise_codg,
                            dise_name = rt1101.output.data[i].dise_name,
                            enddate = "",
                            exp_content = "",
                            hilist_code = rt1101.output.data[i].hilist_code,
                            hilist_name = rt1101.output.data[i].hilist_name
                        };
                        rt5360.output.data.Add(insuinfo);
                    }
                    CHSOUTPUT5360 = JSONSerializer.Serialize(rt5360);
                }
                Dictionary<string, string> dicCHS = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput5360", CHSOUTPUT5360);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2201", dicCHS["chsInput2201"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2201", dic["CHSOUTPUT2201"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2203", dicCHS["chsInput2203"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2204", dicCHS["chsInput2204"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2204", dicCHS["chsOutput2204"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2206", dicCHS["chsOutput2206"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2207", dicCHS["chsInput2207"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2207", dic["CHSOUTPUT2207"]);
                XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "EXPCONTENT", dic["EXPCONTENT"]);
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dicCHS.ContainsKey("MZNO") ? dicCHS["MZNO"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", dicCHS.ContainsKey("YBDJH") ? dicCHS["YBDJH"].Trim() : "");
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", dicCHS.ContainsKey("YBDJH") ? dicCHS["YBDJH"] : "");
            }
            else
            {
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", dic.ContainsKey("YBDJH") ? dic["YBDJH"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dic.ContainsKey("MZNO") ? dic["MZNO"].Trim() : "");
            }

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev == null || dtrev.Rows[0]["CLBZ"].ToString().Trim() != "0")
                {
                    dtrev.Rows[0]["CLBZ"] = "222";
                }
                else if (FormatHelper.GetStr(dtrev.Rows[0]["CLBZ"]) == "0")
                {
                    if (SCH_TYPE == "4")
                    {
                        HLWPaysaveMessageSend(lTERMINAL_SN, DOC_NO, PAT_NAME, SCH_DATE, SCH_TIME, PERIOD_START, SCH_TYPE, DEPT_CODE);
                    }
                }

                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 互联网医生短信通知
        /// </summary>
        /// <param name="lTERMINAL_SN"></param>
        /// <param name="DOC_NO"></param>
        /// <param name="PAT_NAME"></param>
        /// <param name="SCH_DATE"></param>
        /// <param name="SCH_TIME"></param>
        /// <param name="PERIOD_START"></param>
        /// <param name="SCH_TYPE"></param>
        private void HLWPaysaveMessageSend(string lTERMINAL_SN, string DOC_NO, string PAT_NAME, string SCH_DATE, string SCH_TIME, string PERIOD_START, string SCH_TYPE, string DEPT_CODE)
        {
            try
            {
                if (lTERMINAL_SN != "yunhosCooperate" && SCH_TYPE == "4")
                {
                    DataTable dtDoc = new Plat.BLL.BaseFunction().GetList("yunhou.yunhos_doc", "HOS_ID='" + HOS_ID + "' and DOC_NO='" + DOC_NO + "'", "MOBILE_NO,DOC_NAME");
                    if (dtDoc.Rows.Count > 0)
                    {
                        if (FormatHelper.GetStr(dtDoc.Rows[0]["MOBILE_NO"]) != "")
                        {
                            try
                            {
                                DataTable dtdeptinfo = new Plat.BLL.BaseFunction().GetList("platform.DEPT_INFO", "HOS_ID='" + HOS_ID + "' and DEPT_CODE='" + DEPT_CODE + "'", "DEPT_NAME");
                                /*{南京明基医院}尊敬的xxxxxxxxxxxxx医生：互联网xxxxxxxxxxxxxxxxx 有患者xxxxxxxxxxxxx 问诊xxxxxxxxxxxxxxx（日期）的号，请您及时接诊。*/
                                string message = string.Format(@"尊敬的{0}医生：互联网{1}有患者{2} 问诊{3}（日期）的号，请您及时接诊。", FormatHelper.GetStr(dtDoc.Rows[0]["DOC_NAME"]), dtdeptinfo.Rows.Count > 0 ? FormatHelper.GetStr(dtdeptinfo.Rows[0]["DEPT_NAME"]) : "", PAT_NAME, SCH_DATE);
                                SENDMSG(HOS_ID, message, FormatHelper.GetStr(dtDoc.Rows[0]["MOBILE_NO"]), "", "");
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查预约是否可以取消
        /// 返回dt中有2个字段，是否可以取消，具体说明
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SNAPPT">院内预约唯一流水号</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <returns></returns>
        public DataTable REGISTERCANCELCHECK(string HOS_ID, string HOS_SNAPPT, string HOS_SN, Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERCANCELCHECK", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SNAPPT", HOS_SNAPPT.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            //支付了不能取消，未支付的都可以取消,没有医院调用
            try
            {
                DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + HOS_ID + "' and HOS_SN='" + HOS_SN + "' ", "APPT_TYPE");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string cancancel = "0";//不可以取消
                    string cnote = "已支付不能取消";
                    if (dt.Rows[0]["APPT_TYPE"].ToString().Trim() == "0")//允许撤销
                    {
                        cancancel = "1";
                        cnote = "可以取消";
                    }
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    dtrev.Columns.Add("CANCANCEL", typeof(string));
                    dtrev.Columns.Add("NOTE", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "SUCCESS";
                    dr["CANCANCEL"] = cancancel;
                    dr["NOTE"] = "";
                    dtrev.Rows.Add(dr);
                    return dtrev;
                }
                else
                {
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    dtrev.Columns.Add("CANCANCEL", typeof(string));
                    dtrev.Columns.Add("NOTE", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "FAIL";
                    dr["CANCANCEL"] = "0";
                    dr["NOTE"] = "支付取消失败";
                    dtrev.Rows.Add(dr);
                    return dtrev;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 预约取消(含支付)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SNAPPT">院内预约唯一流水号</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <param name="CASH_JE">退费金额（以下为已支付退费信息）</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public bool REGISTERPAYCANCEL(string HOS_ID, string HOS_SNAPPT, string HOS_SN, decimal CASH_JE, string DEAL_STATES, DateTime DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, string PASSWORD, out string CLJG, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            CLJG = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERPAYCANCEL", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SNAPPT", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic.ContainsKey("HOS_SN") ? dic["HOS_SN"] : "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", dic.ContainsKey("CASH_JE") ? dic["CASH_JE"] : "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", dic.ContainsKey("ORDERID") ? dic["ORDERID"] : "");
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev.Rows[0]["CLBZ"].ToString().Trim().Equals("0"))
                {
                    try
                    {
                    }
                    catch
                    { }
                    return true;
                }
                else
                {
                    CLJG = dtrev.Rows[0]["CLJG"].ToString().Trim();
                    return false;
                }
            }
            catch (Exception ex)
            {
                //ServiceBUS.Log.LogHelper.SaveLogHOS(DateTime.Now, "REGISTERPAYCANCEL", DateTime.Now, ex.ToString().Replace("'", ""));
                Log.Helper.Model.ModLogHos modLogHos = new Log.Helper.Model.ModLogHos();
                modLogHos.inTime = DateTime.Now;
                modLogHos.outTime = DateTime.Now;
                modLogHos.inXml = "REGISTERPAYCANCEL";
                modLogHos.outXml = ex.ToString().Replace("'", "");
                Log.Helper.LogHelper.Addlog(modLogHos);
                CLJG = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 未缴费诊间费用查询(汇总、按医嘱处方)
        /// 返回未缴费列表dt，多条数据
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <param name="SFZ_NO">身份证号</param>
        /// <returns></returns>
        public DataSet GETOUTFEENOPAY(string HOS_ID, string HOS_SN, string SFZ_NO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAY", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", "-1");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            string SFZ_SECRET = PlatDataSecret.DataSecret.GetSfzNoSecret(SFZ_NO);
            int PAT_ID = dic.ContainsKey("PAT_ID") ? FormatHelper.GetInt(dic["PAT_ID"]) : 0;
            string YLCARD_TYPE = "";
            string YLCARD_NO = "";
            DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.sex,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret,b.mobile_secret,b.pat_name from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and A.mark_del='0' order by a.ylcartd_type", PAT_ID));

            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }
            string PAT_NAME = "";
            string YNCARDNO = "";
            DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1 order by BAND_TIME DESC", "YLCARD_NO");
            if (dtpatcardbind.Rows.Count > 0)
            {
                YNCARDNO = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            }
            else
            {
                if (dtpat_card.Rows.Count > 0)
                {
                    PAT_NAME = dtpat_card.Rows[0]["pat_name"].ToString().Trim();
                    string SEX = dtpat_card.Rows[0]["SEX"].ToString().Trim();
                    string BIRTHDAY = dtpat_card.Rows[0]["BIRTHDAY"].ToString().Trim();
                    string GUARDIAN_NAME = dtpat_card.Rows[0]["GUARDIAN_NAME"].ToString().Trim();
                    string MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtpat_card.Rows[0]["mobile_secret"]));
                    string ADDRESS = dtpat_card.Rows[0]["ADDRESS"].ToString().Trim();
                    YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                }
            }
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", ((YLCARD_TYPE.ToString() == "0" || YLCARD_TYPE.ToString() == "4") ? "4" : YLCARD_TYPE.ToString()));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", ((YLCARD_TYPE.ToString() == "0" || YLCARD_TYPE.ToString() == "4") ? SFZ_NO : YLCARD_NO));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ITERMINAL_SN", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", YNCARDNO);

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";
                DataTable dtrev = new DataTable();
                try
                {
                    dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/PRELIST").Tables[0];
                    Dictionary<string, string> dichossnandghtype = new Dictionary<string, string>();
                    if (dtrev != null && dtrev.Rows.Count > 0)
                    {
                        if (!dtrev.Columns.Contains("ONLINE_PRE"))
                        {
                            dtrev.Columns.Add("ONLINE_PRE", typeof(string));
                        }
                        for (int i = 0; i < dtrev.Rows.Count; i++)
                        {
                            try
                            {
                                DataRow dr = dtrev.Rows[i];
                                //string HOS_SN_NEW = FormatHelper.GetStr(dr["HOS_SN"]);
                                //if (!dichossnandghtype.ContainsKey(HOS_SN_NEW))
                                //{
                                //    string sqlcmd = string.Format(@" select * from platform.register_pay where hos_id='{0}' and hos_sn='{1}' and gh_type='4'", HOS_ID, HOS_SN_NEW);
                                //    DataTable dtregister_pay = new Plat.BLL.BaseFunction().Query(sqlcmd);
                                //    if (dtregister_pay != null && dtregister_pay.Rows.Count > 0)
                                //    {
                                //        dichossnandghtype.Add(HOS_SN_NEW, "1");
                                //    }
                                //    else
                                //    {
                                //        dichossnandghtype.Add(HOS_SN_NEW, "0");

                                //    }
                                //}
                                //dr["ONLINE_PRE"] = dichossnandghtype[HOS_SN_NEW];
                                try
                                {
                                    if (FormatHelper.GetStr(dr["ONLINE_PRE"]) == "1")
                                    {
                                        MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@pre_no", FormatHelper.GetStr(dr["PRE_NO"])), new MySqlParameter("@hos_id", HOS_ID) };
                                        string sqlcmd = string.Format(@"select IFNULL(c.sf_result_code,0)sf_result_dec
from platform.waitsfpreno a left outer join  yunhou.damznopay b on a.pre_no=b.pre_no
left outer join  yunhou.precheck c on b.dj_id=c.DJ_ID
where a.pre_no=@pre_no and a.hos_id=@hos_id
ORDER BY sf_time DESC");
                                        DataTable dtwaitsf = DbHelperMySQL.Query(sqlcmd, spa).Tables[0];
                                        if (dtwaitsf != null && dtwaitsf.Rows.Count > 0)
                                        {
                                            //如果存在还未审方数据，则不显示
                                            int sf_result_dec = FormatHelper.GetInt(dtwaitsf.Rows[0]["sf_result_dec"]);
                                            if (sf_result_dec != 1)
                                            {
                                                //dtrev.Rows.Remove(dr);
                                                //i--;
                                                dtrev.Rows.Clear();
                                            }
                                        }
                                        else
                                        {
                                            // dtrev.Rows.Clear();
                                        }
                                    }
                                }
                                catch
                                { }
                            }
                            catch
                            {
                                dtrev.Rows.Clear();
                            }
                        }
                        //YB_PAY 0纯自费  1 医保  2医保中的自费
                        var query = (from a in dtrev.AsEnumerable()
                                     group a by new { OPT_SN = FormatHelper.GetStr(a["OPT_SN"]), HOS_SN = FormatHelper.GetStr(a["HOS_SN"]), YB_PAY = (dtrev.Columns.Contains("YB_PAY") ? FormatHelper.GetStr(a["YB_PAY"]) : "1") } into temp
                                     select new
                                     {
                                         HOS_ID = FormatHelper.GetStr(temp.FirstOrDefault()["HOS_ID"]),
                                         OPT_SN = temp.Key.OPT_SN,
                                         PRE_NO = string.Join("|", temp.Select(x => FormatHelper.GetStr(x["PRE_NO"])).ToArray()) + "|" + temp.Key.YB_PAY,
                                         HOS_SN = temp.Key.HOS_SN,
                                         DEPT_CODE = FormatHelper.GetStr(temp.FirstOrDefault()["DEPT_CODE"]),
                                         DEPT_NAME = FormatHelper.GetStr(temp.FirstOrDefault()["DEPT_NAME"]),
                                         DOC_NO = FormatHelper.GetStr(temp.FirstOrDefault()["DOC_NO"]),
                                         DOC_NAME = "",
                                         JEALL = temp.Select(x => FormatHelper.GetDecimal(x["JEALL"])).Sum(),
                                         CASH_JE = temp.Select(x => FormatHelper.GetDecimal(x["CASH_JE"])).Sum(),
                                         YB_PAY = (temp.Key.YB_PAY == "1" ? "1" : "0"),
                                         ONLINE_PRE = FormatHelper.GetStr(temp.FirstOrDefault()["ONLINE_PRE"]),
                                         YQBZ = "H" + HOS_ID
                                     }); ;
                        dtrev = Projecttable.CopyToDataTable(query);
                    }
                }
                catch (Exception ex)
                { }
                dtrev.TableName = "dt2";
                dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.01.23 只是普通勿进行重复查询
                dtbody.Rows[0]["no_repeat"] = 1;
                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtrev.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private decimal GetMXJE(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            decimal je = 0;
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAYMX", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", PRE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", "");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtbody.Rows.Count == 0)
                {
                    return 0;
                }
                return Convert.ToDecimal(dtbody.Rows[0]["JEALL"]);

                DataTable dtMed = new DataTable();
                try
                {
                    dtMed = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DAMEDLIST").Tables[0];
                }
                catch
                { }
                if (dtMed.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMed.Rows)
                    {
                        je += Convert.ToDecimal(dr["AMOUNT"]);
                    }
                }

                DataTable dtChkit = new DataTable();
                try
                {
                    dtChkit = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DACHKTLIST").Tables[0];
                }
                catch
                { }
                if (dtChkit.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtChkit.Rows)
                    {
                        je += Convert.ToDecimal(dr["AMOUNT"]);
                    }
                }
            }
            catch
            {
                je = 0;
            }
            return je;
        }

        public static Dictionary<string, string> getpatinfobypat_id(int PAT_ID, string HOS_ID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string PAT_NAME = "";
            string SFZ_NO = "";
            string MOBILE_NO = "";
            string YNCARDNO = "";
            string YLCARTD_TYPE = "";
            string YLCARD_NO = "";
            string SEX = "";
            string BIRTHDAY = "";
            String sqlcmd = string.Format(@"select a.pat_name, a.sfz_secret,a.mobile_secret,b.ylcartd_type,b.ylcard_no,c.ylcard_no YNCARDNO,a.BIRTHDAY,a.sex from platform.pat_info a left OUTER JOIN platform.pat_card b on a.pat_id=b.pat_id  and b.mark_del=0
left outer join platform.pat_card_bind c on a.pat_id=c.pat_id and c.hos_id=@HOS_ID and c.ylcartd_type=1 AND C.MARK_BIND='1'
where  a.pat_id=@PAT_ID
ORDER BY C.BAND_TIME DESC");
            MySqlParameter[] sqlParameters = new MySqlParameter[] { new MySqlParameter("@HOS_ID", HOS_ID), new MySqlParameter("@PAT_ID", PAT_ID) };
            DataTable dtpatinfo = DbHelperMySQL.Query(sqlcmd, sqlParameters).Tables[0];
            if (dtpatinfo != null && dtpatinfo.Rows.Count > 0)
            {
                PAT_NAME = FormatHelper.GetStr(dtpatinfo.Rows[0]["pat_name"]);
                SFZ_NO = PlatDataSecret.DataSecret.DeSfzNoSecretByAes(FormatHelper.GetStr(dtpatinfo.Rows[0]["sfz_secret"]));
                MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtpatinfo.Rows[0]["mobile_secret"]));
                YNCARDNO = FormatHelper.GetStr(dtpatinfo.Rows[0]["YNCARDNO"]);
                SEX = FormatHelper.GetStr(dtpatinfo.Rows[0]["SEX"]);
                BIRTHDAY = FormatHelper.GetStr(dtpatinfo.Rows[0]["BIRTHDAY"]);
                DataRow[] drspat_cards = dtpatinfo.Select("ylcartd_type='2'");
                if (drspat_cards.Length > 0 && FormatHelper.GetStr(drspat_cards[0]["YLCARD_NO"]) != "")
                {
                    YLCARTD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drspat_cards[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARTD_TYPE = FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcartd_type"]) == "" ? "4" : FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = (YLCARTD_TYPE == "4" ? SFZ_NO : FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcard_no"]));
                }
            }
            dic.Add("PAT_NAME", PAT_NAME);
            dic.Add("SFZ_NO", SFZ_NO);
            dic.Add("MOBILE_NO", MOBILE_NO);
            dic.Add("YNCARDNO", YNCARDNO);
            dic.Add("YLCARTD_TYPE", YLCARTD_TYPE);
            dic.Add("YLCARD_NO", YLCARD_NO);
            dic.Add("SEX", SEX);
            dic.Add("BIRTHDAY", BIRTHDAY);
            return dic;
        }

        /// <summary>
        /// 诊间处方费用明细
        /// 该ds含有3个dt，其中dt0为总金额
        /// dt1为药品明细（处方号、执行时间、药品编码、名称、规格、途径、单位、数量、单价、频次等）
        /// dt2为项目明细（执行时间、项目编码、名称、单位、数量、单价等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataSet GETOUTFEENOPAYMX(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            int PAT_ID = dic.ContainsKey("PAT_ID") ? FormatHelper.GetInt(dic["PAT_ID"]) : 0;
            string MB_ID = dic.ContainsKey("MB_ID") ? FormatHelper.GetStr(dic["MB_ID"]) : "";
            XmlDocument doc = new XmlDocument();
            string pre_no = "";
            string yb_pay = "";
            string YLCARD_TYPE = "";
            string YLCARD_NO = "";

            string YNCARDNO = "";
            GETPATBARCODE(HOS_ID, PAT_ID.ToString(), ref YNCARDNO);

            DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and a.mark_del='0' order by a.ylcartd_type", PAT_ID));
            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }
            if (MB_ID == "")
            {
                yb_pay = PRE_NO.Substring(PRE_NO.LastIndexOf("|") + 1);
                pre_no = PRE_NO.Substring(0, PRE_NO.LastIndexOf("|"));
            }
            else
            {
                bool canPay = false;
                string _strWorkingDayAM = "07:30";//工作时间上午08:30
                string _strWorkingDayPM = "17:00";
                TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
                TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

                TimeSpan dspNow = DateTime.Now.TimeOfDay;
                if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
                {
                    canPay = true;
                }

                if (!canPay)
                {
                    DataSet ds = DS_RtnDealInfo("12", "核酸缴费的时间段为07:30至17:00 ");

                    return ds;
                }

                yb_pay = "1";
                pre_no = "";
            }

            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAYMX", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", pre_no);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YB_PAY", yb_pay);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MB_ID", MB_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", YNCARDNO);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";
                DataTable dtmed = new DataTable();
                DataTable dtchk = new DataTable();
                try
                {
                    dtmed = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DAMEDLIST").Tables[0];
                }
                catch (Exception ex)
                { }
                try
                {
                    dtchk = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DACHKTLIST").Tables[0];
                }
                catch (Exception ex)
                {
                }
                dtmed.TableName = "dt2";
                dtchk.TableName = "dt3";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtmed.Copy());
                dsrev.Tables.Add(dtchk.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 诊间支付锁定
        /// ds含有3个dt，其中dt0为信息表（状态、医院代码，门诊处方、金额等）
        /// dt1为药品明细（处方号、执行时间、药品编码、名称、规格、途径、单位、数量、单价、频次等）
        /// dt2为项目明细（执行时间、项目编码、名称、单位、数量、单价等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataSet OUTFEEPAYLOCK(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            int PAT_ID = dic.ContainsKey("PAT_ID") ? FormatHelper.GetInt(dic["PAT_ID"]) : 0;
            string MB_ID = dic.ContainsKey("MB_ID") ? FormatHelper.GetStr(dic["MB_ID"]) : "";
            XmlDocument doc = new XmlDocument();
            string pre_no = "";
            string yb_pay = "";
            string YLCARD_TYPE = "";
            string YLCARD_NO = "";

            string YNCARDNO = "";
            GETPATBARCODE(HOS_ID, PAT_ID.ToString(), ref YNCARDNO);
            DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and a.mark_del='0' order by a.ylcartd_type", PAT_ID));
            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }
            if (MB_ID == "")
            {
                yb_pay = PRE_NO.Substring(PRE_NO.LastIndexOf("|") + 1);
                pre_no = PRE_NO.Substring(0, PRE_NO.LastIndexOf("|"));
            }
            else
            {
                yb_pay = "1";
                pre_no = "";
            }

            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAYMX", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", pre_no);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YB_PAY", yb_pay);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MB_ID", MB_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", YNCARDNO);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);

                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("HOS_SN") && MB_ID != "")
                {
                    HOS_SN = FormatHelper.GetStr(dtbody.Rows[0]["HOS_SN"]);
                }
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("OPT_SN") && MB_ID != "")
                {
                    OPT_SN = FormatHelper.GetStr(dtbody.Rows[0]["OPT_SN"]);
                }
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("PRE_NO") && MB_ID != "")
                {
                    PRE_NO = FormatHelper.GetStr(dtbody.Rows[0]["PRE_NO"]) + "|" + "1";
                }
                DataTable dtmed = new DataTable();
                try
                {
                    dtmed = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DAMEDLIST").Tables[0];
                }
                catch
                {
                }
                DataTable dtchkt = new DataTable();
                try
                {
                    dtchkt = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/DACHKTLIST").Tables[0];
                }
                catch
                {
                }

                if (dtbody.Rows[0]["CLBZ"].ToString().Trim() == "0")//取明细成功
                {
                    DateTime date = DateTime.Now;
                    XmlDocument docrev = new XmlDocument();
                    docrev = QHXmlMode.GetBaseXml("OUTFEEPAYLOCK", "0");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CLBZ", "0");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CLJG", "SUCCESS");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "STATES", "1");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "PRE_NO", PRE_NO.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "JEALL", FormatHelper.GetDecimal(dtbody.Rows[0]["JEALL"]).ToString("0.00"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CASH_JE", FormatHelper.GetDecimal(dtbody.Rows[0]["JEALL"]).ToString("0.00"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "DJ_DATE", date.ToString("yyyy-MM-dd"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "DJ_TIME", date.ToString("HH:mm:ss"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "YQBZ", "H" + HOS_ID);
                    DataTable dta = XMLHelper.X_GetXmlData(docrev, "ROOT/BODY").Tables[0];
                    dta.TableName = "dt1";
                    dtmed.TableName = "dt2";
                    dtchkt.TableName = "dt3";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dta.Copy());
                    ds.Tables.Add(dtmed.Copy());
                    ds.Tables.Add(dtchkt.Copy());
                    return ds;
                }
                else//直接返回失败
                {
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "支付锁定失败";
                    dtrev.Rows.Add(dr);
                    dtrev.TableName = "dt1";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtrev.Copy());
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 诊间支付锁定解除  调用名待改
        /// </summary>
        /// <param name="PAT_ID">用户唯一标识</param>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_SN">院内唯一流水号</param>
        /// <param name="JEALL">总金额</param>
        /// <param name="CASH_JE">现金金额</param>
        /// <param name="DJ_DATE">日期 yyyy-MM-dd</param>
        /// <param name="DJ_TIME">时间 HH:mm:ss</param>
        /// <returns></returns>
        public DataTable OUTFEEPAYUNLOCK(int PAT_ID, string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, decimal JEALL, decimal CASH_JE, string DJ_DATE, string DJ_TIME, Dictionary<string, string> dic)
        {
            DataTable dtpay = new Plat.BLL.BaseFunction().GetList("opt_pay_lock", "HOS_ID='" + HOS_ID + "' AND HOS_SN='" + HOS_SN + "'", "PAY_ID");
            try
            {
                DateTime date = DateTime.Now;
                DataTable dt = new DataTable();
                dt.Columns.Add("CLBZ", typeof(string));
                dt.Columns.Add("CLJG", typeof(string));
                dt.Columns.Add("PAY_ID", typeof(string));
                dt.Columns.Add("STATES", typeof(string));
                dt.Columns.Add("HOS_SN", typeof(string));
                dt.Columns.Add("JEALL", typeof(decimal));
                dt.Columns.Add("CASH_JE", typeof(decimal));
                dt.Columns.Add("DJ_DATE", typeof(string));
                dt.Columns.Add("DJ_TIME", typeof(string));
                DataRow dr = dt.NewRow();
                dr["CLBZ"] = "0";
                dr["CLJG"] = "SUCCESS";
                dr["PAY_ID"] = dtpay.Rows[0]["PAY_ID"].ToString().Trim();
                dr["STATES"] = "4";
                dr["HOS_SN"] = HOS_SN.Trim();
                dr["JEALL"] = JEALL;
                dr["CASH_JE"] = CASH_JE;
                dr["DJ_DATE"] = date.ToString("yyyy-MM-dd");
                dr["DJ_TIME"] = date.ToString("HH:mm:ss");
                dt.Rows.Add(dr);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 诊间支付保存
        /// 返回的dt含有发票号，院内收费唯一流水号等
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="CASH_JE">本次缴费现金支付金额</param>
        /// <param name="PAY_TYPE">现金支付方式</param>
        /// <param name="JEALL">缴费成功总金额</param>
        /// <param name="JZ_CODE">人员费用结算类别编码 必填</param>
        /// <param name="ybDJH">医保单据号</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable OUTFEEPAYSAVE(string HOS_ID, string HOS_SN, decimal CASH_JE, string PAY_TYPE, decimal JEALL, string JZ_CODE, string ybDJH, string QUERYID, string DEAL_STATES, DateTime DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            try
            {
                string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
                XmlDocument doc = new XmlDocument();
                doc = QHXmlMode.GetBaseXml("OUTFEEPAYSAVE", "0");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic.ContainsKey("OPT_SN") ? dic["OPT_SN"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", dic.ContainsKey("PRE_NO") ? dic["PRE_NO"].Trim().Substring(0, dic["PRE_NO"].Trim().LastIndexOf("|")) : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString().Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAY_TYPE", PAY_TYPE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", JEALL.ToString().Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JZ_CODE", JZ_CODE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", DEAL_STATES.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DEAL_TIME.ToString().Trim());

                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", dic.ContainsKey("OPT_SN") ? dic["OPT_SN"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", QUERYID.Trim());

                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dic.ContainsKey("MZNO") ? dic["MZNO"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DIS_NO", "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", dic.ContainsKey("PAT_CARD_OUT") ? CommonFunction.GetJsonValue(FormatHelper.GetStr(dic["PAT_CARD_OUT"]), "mediInsuOutpam") : "");
                //DataTable dt = new Plat.BLL.BaseFunction().GetList("njyb_tran", "comm_main='" + QUERYID + "' and txn_type='01'", "cardNum", "cardType");

                //if (dt.Rows.Count > 0)
                //{
                //    DataTable dtyb = new Plat.BLL.BaseFunction().GetList("yb_pat", "pat_id='" + dic["PAT_ID"] + "' and ylcard_no='" + dt.Rows[0]["cardNum"].ToString().Trim() + "'", "yb_out");
                //    if (dtyb.Rows.Count > 0)
                //    {
                //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", CommonFunction.GetJsonValue(FormatHelper.GetStr(dtyb.Rows[0]["yb_out"]), "mediInsuOutpam"));
                //    }
                //    else
                //    {
                //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_CARD_OUT", "");
                //    }
                //}
                //else
                //{
                //}

                string JS_OUT = dic.ContainsKey("JS_OUT") ? dic["JS_OUT"].Trim() : "";
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JS_IN", dic.ContainsKey("YB_IN") ? dic["YB_IN"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_IN", dic.ContainsKey("YJS_IN") ? dic["YJS_IN"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_OUT", dic.ContainsKey("YJS_OUT") ? dic["YJS_OUT"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JS_OUT_ADD", dic.ContainsKey("JS_OUT_ADD") ? dic["JS_OUT_ADD"].Trim() : "");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JS_OUT", JS_OUT);
                string YLCARD_TYPE = dic.ContainsKey("YLCARD_TYPE") ? dic["YLCARD_TYPE"].Trim() : "4";
                if (JS_OUT == "")
                {
                    if (YLCARD_TYPE == "2")
                    {
                        YLCARD_TYPE = "4";
                    }
                }
                string YBPAT_TYPE = "";
                if (JS_OUT != "")
                {
                    if (YLCARD_TYPE == "2")
                    {
                        YBPAT_TYPE = "NJSYB";
                    }
                    if (YLCARD_TYPE == "6")
                    {
                        YBPAT_TYPE = "JSSYB";
                    }
                }
                if (dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "CHSYB")
                {
                    YBPAT_TYPE = "GJYB";
                }
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBPAT_TYPE", YBPAT_TYPE);
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE);
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YB_PAY", dic.ContainsKey("PRE_NO") ? dic["PRE_NO"].Trim().Substring(dic["PRE_NO"].Trim().LastIndexOf("|") + 1) : "0");
                MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@comm_sn", QUERYID) };
                DataTable dtunionpay = DbHelperMySQL.Query("select provider from yunhou.yunhos_unionpaytran where comm_sn=@comm_sn and txn_type='01'", spa).Tables[0];
                if (dtunionpay.Rows.Count > 0)
                {
                    string provider = CommonFunction.GetStr(dtunionpay.Rows[0]["provider"]);
                    if (provider == "1")
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G2");
                    }
                    else if (provider == "3")
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G1");
                    }
                    else if (provider == "7")
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G3");
                    }
                    else
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "G");
                    }
                }
                else
                {
                    DataTable dtConInfo = new Plat.BLL.BaseFunction().GetList("gettnpatinfo", "ORDERID='" + QUERYID + "'", "DEAL_TYPE");
                    if (dtConInfo.Rows.Count > 0)
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", CommonFunction.GetStr(dtConInfo.Rows[0]["DEAL_TYPE"]));
                    }
                    else
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", DEAL_TYPE);
                    }
                }
                string Address = "";
                string Mobile_no = "";
                string PAY_ID = dic.ContainsKey("PAY_ID") ? dic["PAY_ID"].Trim() : "";
                try
                {
                    if (PAY_ID != "")
                    {
                        //如果是互联网医院病人，且有配送地址传给HIS进行保存
                        spa = new MySqlParameter[] { new MySqlParameter("@PAY_ID", PAY_ID) };
                        string sqlcmdaddress = string.Format(@"
select CONCAT(b.pr_name,b.city_name,b.ct_name,b.address) address,b.mobile_secret from platform.opt_pay_external_info a,yunhou.patexpaddess b
 where a.PAY_ID=@PAY_ID and a.address_id=b.address_id and kd_type=2");
                        DataTable dtaddress = DbHelperMySQL.Query(sqlcmdaddress, spa).Tables[0];
                        if (dtaddress != null && dtaddress.Rows.Count > 0)
                        {
                            Address = FormatHelper.GetStr(dtaddress.Rows[0]["address"]);
                            string mobile_secret = FormatHelper.GetStr(dtaddress.Rows[0]["mobile_secret"]);
                            Mobile_no = PlatDataSecret.DataSecret.DeMobileSecretByAes(mobile_secret);
                        }
                    }
                }
                catch
                {
                }
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", Address);
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", Mobile_no);

                bool isgjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "CHSYB";//是否是国家医保病人

                if (isgjyb)
                {
                    QHSiInterface.RT1101.Root rt1101 = JSONSerializer.Deserialize<QHSiInterface.RT1101.Root>(dic["CHSOUTPUT1101"]);
                    string CHSOUTPUT5360 = "";
                    if (rt1101.output.data.Count > 0)
                    {
                        QHSiInterface.RT5360.Root rt5360 = new QHSiInterface.RT5360.Root();
                        rt5360.output = new QHSiInterface.RT5360.Output();
                        rt5360.output.data = new List<QHSiInterface.RT5360.insuinfo>();
                        rt5360.infcode = "0";
                        rt5360.err_msg = "";
                        rt5360.inf_refmsgid = "";
                        rt5360.respond_time = "";
                        rt5360.refmsg_time = "";
                        for (int i = 0; i < rt1101.output.data.Count; i++)
                        {
                            QHSiInterface.RT5360.insuinfo insuinfo = new QHSiInterface.RT5360.insuinfo()
                            {
                                psn_no = rt1101.output.data[i].psn_no,
                                med_type = rt1101.output.data[i].med_type,
                                insutype = rt1101.output.data[i].insutype,
                                begndate = rt1101.output.data[i].begndate,
                                dise_codg = rt1101.output.data[i].dise_codg,
                                dise_name = rt1101.output.data[i].dise_name,
                                enddate = "",
                                exp_content = "",
                                hilist_code = rt1101.output.data[i].hilist_code,
                                hilist_name = rt1101.output.data[i].hilist_name
                            };
                            rt5360.output.data.Add(insuinfo);
                        }
                        CHSOUTPUT5360 = JSONSerializer.Serialize(rt5360);
                    }
                    Dictionary<string, string> dicCHS = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput5360", CHSOUTPUT5360);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2201", dicCHS["chsInput2201"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2201", dic["CHSOUTPUT2201"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2203", dicCHS["chsInput2203"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2204", dicCHS["chsInput2204"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2204", dicCHS["chsOutput2204"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2206", dicCHS["chsOutput2206"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsInput2207", dicCHS["chsInput2207"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput2207", dic["CHSOUTPUT2207"]);
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "EXPCONTENT", dic["EXPCONTENT"]);
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", dicCHS.ContainsKey("YBDJH") ? dicCHS["YBDJH"] : "");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dicCHS.ContainsKey("MZNO") ? dicCHS["MZNO"] : "");
                }
                else
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", dic.ContainsKey("YBDJH") ? dic["YBDJH"].Trim() : "");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dic.ContainsKey("MZNO") ? dic["MZNO"].Trim() : "");
                }

                try
                {
                    string rtnxml = CallService(doc.OuterXml, SOURCE);
                    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                    DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                    //与医院协商返回code=-808则表示需要平台退费
                    //if (dtrev != null && dtrev.Rows.Count > 0 && dtrev.Rows[0]["CLBZ"].ToString().Trim() == "-808")
                    //{
                    //    XmlDocument doca = QHXmlMode.GetBaseXml("UNIFIEDREFUND", "0");
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "HOS_ID", HOS_ID);
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "DEAL_TYPE", DEAL_TYPE);
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "ORDERID", QUERYID.Trim());
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "CASH_JE", CASH_JE.ToString("0.00"));
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "USER_ID", "0");
                    //    XMLHelper.X_XmlInsertNode(doca, "ROOT/BODY", "TYPE", "1");
                    //    ServiceBUS.WCApp.RegisterInfoXML.UNIFIEDREFUND(doca);
                    //}
                    if (dtrev.Rows[0]["CLBZ"].ToString().Trim() != "0")
                    {
                        dtrev.Rows[0]["CLBZ"] = "222";
                    }
                    return dtrev;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 返回的基础xml
        /// </summary>
        /// <param name="code">编码 9exception 8调用err</param>
        /// <param name="cljg"></param>
        /// <returns></returns>
        private static DataSet DS_RtnDealInfo(string code, string cljg)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("CLBZ", typeof(string));
            dt.Columns.Add("CLJG", typeof(string));
            DataRow dr = dt.NewRow();
            dr["CLBZ"] = code;
            dr["CLJG"] = cljg;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 诊间退费锁定
        /// 返回的dt中包含状态、院内唯一流水号、金额等
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_PAY_SN">院内收费唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable OUTFEERETLOCK(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_PAY_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            DataTable dtpay = new Plat.BLL.BaseFunction().GetList("opt_pay", "HOS_ID='" + HOS_ID + "' and OPT_SN='" + OPT_SN + "'", "HOS_SN,JEALL,CASH_JE");
            try
            {
                DateTime date = DateTime.Now;
                DataTable dt = new DataTable();
                dt.Columns.Add("CLBZ", typeof(string));
                dt.Columns.Add("CLJG", typeof(string));
                dt.Columns.Add("STATES", typeof(string));
                dt.Columns.Add("HOS_SN", typeof(string));
                dt.Columns.Add("JEALL", typeof(decimal));
                dt.Columns.Add("CASH_JE", typeof(decimal));
                dt.Columns.Add("DJ_DATE", typeof(string));
                dt.Columns.Add("DJ_TIME", typeof(string));
                dt.Columns.Add("lTERMINAL_SN", typeof(string));
                DataRow dr = dt.NewRow();
                dr["CLBZ"] = "0";
                dr["CLJG"] = "";
                dr["STATES"] = "11";
                dr["HOS_SN"] = dtpay.Rows[0]["HOS_SN"].ToString().Trim();
                dr["JEALL"] = dtpay.Rows[0]["JEALL"].ToString().Trim();
                dr["CASH_JE"] = dtpay.Rows[0]["CASH_JE"].ToString().Trim();
                dr["DJ_DATE"] = date.ToString("yyyy-MM-dd");
                dr["DJ_TIME"] = date.ToString("HH:mm:ss");
                dr["lTERMINAL_SN"] = "";
                dt.Rows.Add(dr);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取病人检验报告单列表
        /// 返回dt可多行（含报告单流水号、名称、类型等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="PAGEINDEX">分页索引</param>
        /// <param name="PAGESIZE">分页页大小</param>
        /// <returns></returns>
        public DataSet GETLISREPORT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, int PAGEINDEX, int PAGESIZE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            int PAT_ID = (dic.ContainsKey("pat_id") ? FormatHelper.GetInt(dic["pat_id"]) : 0);
            DataTable dtylcard = new Plat.BLL.BaseFunction().Query(string.Format(@"select pat_name,ylcartd_type,ylcard_no,a.pat_id,b.sex,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,B.SFZ_NO,MOBILE_SECRET from pat_card a,pat_info b where
a.pat_id=b.pat_id and a.pat_id='{0}'", PAT_ID));
            string PAT_NAME = "";
            string YLCARD_TYPE = "";
            string YNCARDNO = "";
            DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1 order by BAND_TIME DESC", "YLCARD_NO");
            if (dtpatcardbind.Rows.Count > 0)
            {
                YNCARDNO = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            }
            else
            {
                if (dtylcard.Rows.Count > 0)
                {
                    PAT_NAME = FormatHelper.GetStr(dtylcard.Rows[0]["pat_name"]);
                    YLCARD_TYPE = FormatHelper.GetStr(dtylcard.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtylcard.Rows[0]["ylcard_no"]);

                    string SEX = FormatHelper.GetStr(dtylcard.Rows[0]["SEX"]);
                    string BIRTHDAY = FormatHelper.GetStr(dtylcard.Rows[0]["BIRTHDAY"]);
                    string GUARDIAN_NAME = FormatHelper.GetStr(dtylcard.Rows[0]["GUARDIAN_NAME"]);
                    //string MOBILE_NO = FormatHelper.GetStr(dtylcard.Rows[0]["MOBILE_NO"]);
                    string MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtylcard.Rows[0]["MOBILE_SECRET"]));
                    string ADDRESS = FormatHelper.GetStr(dtylcard.Rows[0]["ADDRESS"]);
                    //SFZ_NO = FormatHelper.GetStr(dtylcard.Rows[0]["SFZ_NO"]);
                    YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                }
            }

            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETLISREPORT", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGEINDEX", PAGEINDEX.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGESIZE", PAGESIZE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PATHOSID", YNCARDNO);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);

                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";
                DataTable dtlist = new DataTable();
                try
                {
                    dtlist = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/LISREPORTLIST").Tables[0];
                }
                catch { }
                dtlist.TableName = "dt2";

                dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.03.01 只是普通勿进行重复查询
                dtbody.Rows[0]["no_repeat"] = 1;

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtlist.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取检验报告结果明细
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">医院唯一代码</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="REPORT_SN">报告单流水号</param>
        /// <returns></returns>
        public DataSet GETLISRESULT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, string REPORT_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETLISRESULT", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REPORT_SN", REPORT_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);

                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";

                DataTable dtLIS = new DataTable();
                try
                {
                    dtLIS = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/LISREPORTCOMMONLIST").Tables[0];

                    foreach (DataRow dataRow in dtLIS.Rows)
                    {
                        if (dataRow["ITEM_NAME"].ToString().Contains("2019"))
                        {
                            dataRow["ITEM_UNIT"] = "";

                            dataRow["ITEM_RESULT"] = dataRow["ITEM_RESULT"].ToString().Replace("危急", "1");
                        }
                    }
                }
                catch { }
                dtLIS.TableName = "dt2";

                DataTable dtList = new DataTable();
                try
                {
                    dtList = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/LISREPORTBACTERIALLIST").Tables[0];
                }
                catch { }
                dtList.TableName = "dt3";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtLIS.Copy());
                dsrev.Tables.Add(dtList.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取病人检查报告
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="PAGEINDEX">分页索引</param>
        /// <param name="PAGESIZE">分页页大小</param>
        /// <returns></returns>
        public DataSet GETRISREPORT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, int PAGEINDEX, int PAGESIZE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            int PAT_ID = (dic.ContainsKey("pat_id") ? FormatHelper.GetInt(dic["pat_id"]) : 0);
            DataTable dtylcard = new Plat.BLL.BaseFunction().Query(string.Format(@"select pat_name,a.ylcartd_type,a.YLCARD_NO,a.pat_id,b.sex,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,B.SFZ_NO,MOBILE_SECRET,A.YLCARD_NO from pat_card a,pat_info b where
a.pat_id=b.pat_id and a.pat_id='{0}'", PAT_ID));
            string PAT_NAME = "";
            string YLCARD_TYPE = "";

            string YNCARDNO = "";
            DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1 order by BAND_TIME DESC", "YLCARD_NO");
            if (dtpatcardbind.Rows.Count > 0)
            {
                YNCARDNO = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            }
            else
            {
                if (dtylcard.Rows.Count > 0)
                {
                    PAT_NAME = FormatHelper.GetStr(dtylcard.Rows[0]["pat_name"]);
                    YLCARD_TYPE = FormatHelper.GetStr(dtylcard.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtylcard.Rows[0]["YLCARD_NO"]);

                    string SEX = FormatHelper.GetStr(dtylcard.Rows[0]["SEX"]);
                    string BIRTHDAY = FormatHelper.GetStr(dtylcard.Rows[0]["BIRTHDAY"]);
                    string GUARDIAN_NAME = FormatHelper.GetStr(dtylcard.Rows[0]["GUARDIAN_NAME"]);
                    string MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtylcard.Rows[0]["MOBILE_SECRET"]));
                    string ADDRESS = FormatHelper.GetStr(dtylcard.Rows[0]["ADDRESS"]);
                    //SFZ_NO = FormatHelper.GetStr(dtylcard.Rows[0]["SFZ_NO"]);
                    YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                }
            }

            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETRISREPORT", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGEINDEX", PAGEINDEX.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGESIZE", PAGESIZE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", YNCARDNO);
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LOCAL_ID_TYPE", "4");
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LOCAL_ID", YLCARD_NO);
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "VER_DATETIME_START", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss"));
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "VER_DATETIME_END", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";
                DataTable dtRIS = null;
                try
                {
                    dtRIS = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/RISREPORTLIST").Tables[0];
                }
                catch { }
                dtRIS.TableName = "dt2";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtRIS.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 实时叫号当前账户信息查询
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <returns></returns>
        public DataSet GETOUTQUEUEMY(string HOS_ID, string HOS_SN, string SFZ_NO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTQUEUEMY", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());

            DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + HOS_ID + "' and HOS_SN='" + HOS_SN + "'", "DEPT_CODE", "DOC_NO", "DEPT_NAME", "DOC_NAME", "PAT_NAME", "APPT_ORDER", "SCH_TIME");
            string DOC_NO = dt.Rows[0]["DOC_NO"].ToString().Trim();
            string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
            string SCH_TIME = dt.Rows[0]["SCH_TIME"].ToString().Trim();
            DataTable dtSchedule = new Plat.BLL.BaseFunction().GetList("schedule", "dept_code='" + DEPT_CODE + "' and HOS_ID='" + HOS_ID + "' and SCH_DATE=curdate() and DOC_NO='" + DOC_NO + "' and SCH_TIME='" + SCH_TIME + "'", "REGISTER_TYPE");
            if (dtSchedule.Rows.Count == 0)
            {
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", "01");
            }
            else
            {
                //01  普通号  02  副主任号  03  正主任号
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", dtSchedule.Rows[0][0].ToString().Trim());
            }

            //DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + HOS_ID + "' and HOS_SN='" + HOS_SN + "'", "DEPT_CODE", "DOC_NO", "DEPT_NAME", "DOC_NAME", "PAT_NAME", "APPT_ORDER", "SCH_TIME");
            //string DOC_NO = dt.Rows[0]["DOC_NO"].ToString().Trim();
            //string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
            //string SCH_TIME = dt.Rows[0]["SCH_TIME"].ToString().Trim();
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO);
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE);
            //DataTable dtSchedule = new Plat.BLL.BaseFunction().GetList("schedule", "dept_code='" + DEPT_CODE + "' and HOS_ID='" + HOS_ID + "' and SCH_DATE=curdate() and DOC_NO='" + DOC_NO + "' and SCH_TIME='" + SCH_TIME + "'", "REGISTER_TYPE");
            //if (dtSchedule.Rows.Count == 0)
            //{
            //    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", "01");
            //}
            //else
            //{
            //    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", dtSchedule.Rows[0][0].ToString().Trim());
            //}
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TIME", SCH_TIME);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";

                DataTable dtwait = new DataTable();
                try
                {
                    dtwait = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/WAITLIST").Tables[0];
                }
                catch { }
                dtwait.TableName = "dt2";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtwait.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 实时叫号信息查询
        /// </summary>
        /// <param name="QUE_TYPE">排队叫号类别</param>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="DOC_NO">医生工号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable GETOUTQUEUELNOW(string QUE_TYPE, string HOS_ID, string DEPT_CODE, string DOC_NO, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTQUEUELNOW", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUE_TYPE", QUE_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());

            //DataTable dtDEPT = new Plat.BLL.BaseFunction().GetList("schedule", "dept_code='" + DEPT_CODE + "' and HOS_ID='" + HOS_ID + "' and SCH_DATE=curdate() and DOC_NO='" + DOC_NO + "'", "register_type");
            //if (dtDEPT.Rows.Count == 0)
            //{
            //    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", "01");
            //}
            //else
            //{
            //    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REGISTER_TYPE", dtDEPT.Rows[0][0].ToString().Trim());
            //}
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 通过住院号获取病人信息
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_NO">病人住院号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable GETPATINFBYHOSNO(string HOS_ID, string HOS_NO, string lTERMINAL_SN, string PAT_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            try
            {
                DataTable dt_name = new Plat.BLL.BaseFunction().GetList("pat_info", "PAT_ID='" + PAT_ID.Trim() + "'", "PAT_NAME");
                doc = QHXmlMode.GetBaseXml("GETPATINFBYHOSNO", "0");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", HOS_NO.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());
            }
            catch
            {
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("CLBZ", typeof(string));
                dtnew.Columns.Add("CLJG", typeof(string));
                DataRow drnew = dtnew.NewRow();
                drnew["CLBZ"] = "1";
                drnew["CLJG"] = "处理过程出错";
                dtnew.Rows.Add(drnew);
                return dtnew;
            }
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取住院病人信息详情
        /// </summary>
        /// <param name="REGPAT_ID">登录用户唯一标识</param>
        /// <param name="PAT_ID">持卡人唯一标识</param>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_NO">病人住院号</param>
        /// <param name="HOS_PAT_ID">病人唯一住院索引</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataSet GETPATHOSINFO(int REGPAT_ID, int PAT_ID, string HOS_ID, string HOS_NO, string HOS_PAT_ID, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETPATHOSINFO", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", HOS_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", HOS_PAT_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                rtnxml = rtnxml.Replace("&lt;", "<").Replace("&gt;", ">");
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";

                DataTable dtpay = new DataTable();
                try
                {
                    dtpay = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/PAYLIST").Tables[0];
                }
                catch { }
                dtpay.TableName = "dt2";

                DataTable dtfee = new DataTable();
                try
                {
                    dtfee = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/FEELIST").Tables[0];
                }
                catch { }
                dtfee.TableName = "dt3";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtpay.Copy());
                dsrev.Tables.Add(dtfee.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 缴纳预交金保存
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_NO">病人住院号</param>
        /// <param name="HOS_PAT_ID">病人唯一住院索引</param>
        /// <param name="CASH_JE">支付金额</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable SAVEINPATYJJ(string HOS_ID, string HOS_NO, string HOS_PAT_ID, decimal CASH_JE, string DEAL_STATES, string DEAL_TIME, string DEAL_TYPE, string QUERYID, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("SAVEINPATYJJ", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", HOS_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", HOS_PAT_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", DEAL_STATES.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DEAL_TIME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", DEAL_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", QUERYID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 验证医疗卡是否已经绑定
        /// </summary>
        /// <param name="SFZ_NO">身份证号</param>
        /// <param name="CARD_NO">医保卡、就诊卡号</param>
        /// <param name="CARD_TYPE">卡类型</param>
        /// <param name="HOS_ID">医院代码</param>
        /// <returns></returns>
        public bool CHECKYLCARD(string SFZ_NO, string CARD_NO, int CARD_TYPE, string HOS_ID, string PassWord, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("CHECKYLCARD", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CARD_NO", CARD_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CARD_TYPE", CARD_TYPE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev.Rows[0]["EXIST"].ToString().Trim().Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取指定医院及科室专家排班列表(当日即实时)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="SCH_TYPE">排班类型</param>
        /// <returns></returns>
        public DataSet GETSCHDOC(string HOS_ID, string DEPT_CODE, string SCH_TYPE, string DOC_NO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETSCHDOC", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", SCH_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "ds1";
                DataTable dtlist = new DataTable();
                try
                {
                    dtlist = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/SCHLIST").Tables[0];
                    string seetime = DateTime.Now.ToString("HH:mm:ss");
                    string sch_date = DateTime.Now.AddDays(15).ToString("yyyy-MM-dd");
                    dtlist = DataTableHelper.GetNewTable(dtlist, "SCH_DATE<='" + sch_date + "'");
                    //dtlist = DataTableHelper.GetNewTable(dtlist, "(DOC_NO<>'' AND DOC_NO<>'0') and END_TIME>='" + seetime + "'");
                    if (SCH_TYPE == "01")
                    {
                        dtlist = DataTableHelper.GetNewTable(dtlist, "(SCH_DATE>'" + DateTime.Now.ToString("yyyy-MM-dd") + "') ");
                    }
                    else if (SCH_TYPE == "02")
                    {
                        dtlist = DataTableHelper.GetNewTable(dtlist, "(SCH_DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "') AND END_TIME>='" + seetime + "'");
                    }
                    else if (SCH_TYPE.PadLeft(2, '0') == "04")
                    {
                        dtlist = DataTableHelper.GetNewTable(dtlist, "(SCH_DATE>'" + DateTime.Now.ToString("yyyy-MM-dd") + "') ");
                        if (dtlist != null && dtlist.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtlist.Rows)
                            {
                                dr["sch_type"] = "4";
                            }
                        }
                    }
                }
                catch { }
                dtlist.TableName = "ds2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtlist.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定医院科室排班列表(当日即实时)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="SCH_TYPE">排班类型</param>
        /// <returns></returns>
        public DataSet GETSCHDEPT(string HOS_ID, string DEPT_CODE, string SCH_TYPE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETSCHDEPT", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", SCH_TYPE.Trim());

  
            DataTable dtlist = new DataTable();
            DataTable dtbody = new DataTable();
            try
            {
                if (SCH_TYPE == "02" || SCH_TYPE == "01")
                {
                    string rtnxml = CallService(doc.OuterXml, SOURCE);
                    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                    dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                    try
                    {
                        dtlist = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/SCHLIST").Tables[0];
                        if (dtlist != null)
                        {
                            string seetime = DateTime.Now.ToString("HH:mm:ss");
                            if (SCH_TYPE == "02")
                            {
                                dtlist = DataTableHelper.GetNewTable(dtlist, " SCH_DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and END_TIME>='" + seetime + "'");
                            }
                            else
                            {
                                dtlist = DataTableHelper.GetNewTable(dtlist, " SCH_DATE>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
                            }
                            foreach (DataRow drlist in dtlist.Rows)
                            {
                                //if (FormatHelper.GetStr(drlist["DOC_NO"]) == "" || FormatHelper.GetStr(drlist["DOC_NO"]) == "0")
                                //{
                                //    drlist["DOC_NO"] = "";
                                //}
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    dtbody.Columns.Add("CLBZ", typeof(string));
                    DataRow dr = dtbody.NewRow();
                    dr["CLBZ"] = "0";
                    dtbody.Rows.Add(dr);

                    Plat.BLL.schedule BLLschedule = new Plat.BLL.schedule();
                    dtlist = BLLschedule.GetListKS(HOS_ID, DEPT_CODE, SCH_TYPE);
                }

                dtbody.TableName = "ds1";
                dtlist.TableName = "ds2";

   
                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtlist.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定医院科室(专家)日期排班时间段
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="DOC_NO">医生工号</param>
        /// <param name="SCH_DATE">筛选日期</param>
        /// <param name="SCH_TIME">排班时间</param>
        /// <returns></returns>
        public DataSet GETSCHPERIOD(string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETSCHPERIOD", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim() == "0" ? "" : DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", SCH_DATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TIME", SCH_TIME.Trim());
            string SCH_TYPE = "";

            if (dic.Keys.Contains("QUERY_TYPE"))
            {
                if (dic["QUERY_TYPE"] == "01" || dic["QUERY_TYPE"] == "03")
                {
                    SCH_TYPE = "1";
                }
                else if (dic["QUERY_TYPE"] == "02" || dic["QUERY_TYPE"] == "04")
                {
                    SCH_TYPE = "2";
                }
            }
            else
            {
                SCH_TYPE = dic["SCH_TYPE"];
            }

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", SCH_TYPE);

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "ds1";
                DataTable dtsch = new DataTable();
                try
                {
                    dtsch = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/PERIODLIST").Tables[0];
                }
                catch { }
                dtsch.TableName = "ds2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtsch.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取物价分类
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <returns></returns>
        public DataSet GETGOODSCATE(string HOS_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETGOODSCATE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";

                DataTable dtcate = new DataTable();
                try
                {
                    dtcate = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/CATELIST").Tables[0];
                }
                catch { }
                dtcate.TableName = "dt2";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtcate.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取物价明细列表
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="CATE_CODE">类别代码</param>
        /// <returns></returns>
        public DataSet GETGOODSMX(string HOS_ID, string CATE_CODE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? dic["SOURCE"] : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETGOODSMX", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CATE_CODE", CATE_CODE.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "dt1";
                DataTable dtitem = new DataTable();
                try
                {
                    dtitem = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/ITEMLIST").Tables[0];
                }
                catch { }
                dtitem.TableName = "dt2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtitem.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 诊间退费保存   不让退费
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_PAY_SN">院内收费唯一流水号</param>
        /// <param name="CASH_JE">本次缴费现金支付金额</param>
        /// <param name="PAY_TYPE">现金支付方式</param>
        /// <param name="JEALL">缴费成功总金额</param>
        /// <param name="JZ_CODE">人员费用结算类别编码 必填</param>
        /// <param name="ybDJH">医保单据号</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable OUTFEERETSAVE(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_PAY_SN, decimal CASH_JE, string PAY_TYPE, decimal JEALL, string JZ_CODE, string ybDJH, string DEAL_STATES, DateTime DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("OUTFEERETSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", PRE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAY_SN", HOS_PAY_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAY_TYPE", PAY_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", JEALL.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JZ_CODE", JZ_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ybDJH", ybDJH.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", DEAL_STATES.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DEAL_TIME.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", DEAL_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 诊间退费锁定解除
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_PAY_SN">院内收费唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public bool OUTFEERETUNLOCK(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_PAY_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            return true;
        }

        /// <summary>
        /// 发送病人检查报告
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="RISREPORTLIST">检验报告列表</param>
        /// <returns></returns>
        public bool SENDRISREPORT(string HOS_ID, string HOS_SN, string SFZ_NO, DataTable RISREPORTLIST, Dictionary<string, string> dic)
        {
            return true;
        }

        /// <summary>
        /// 获取指定挂号的主诉信息
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <returns></returns>
        public DataTable GETPATUOTCC(string HOS_ID, string HOS_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETPATUOTCC", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable SENDCARDINFO(string YLCARD_TYPE, string YLCARD_NO, string PAT_NAME, string SEX, string BIRTHDAY, string GUARDIAN_NAME, string SFZ_NO, string MOBILE_NO, string ADDRESS, string OPERATOR, Dictionary<string, string> dic)
        {
            //XmlDocument doc = new XmlDocument();
            //doc = QHXmlMode.GetBaseXml("SENDCARDINFO", "0");
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim() == "" ? SFZ_NO.Trim() : YLCARD_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEX", SEX.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIRTHDAY", BIRTHDAY.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_NAME", GUARDIAN_NAME.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", MOBILE_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", ADDRESS.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPERATOR", OPERATOR.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CARDTYPE", YLCARD_TYPE);
            //try
            //{
            //    string rtnxml = CallService(doc.OuterXml);
            //    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            //    DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
            //    return dtrev;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            DataTable dt = new DataTable();
            dt.Columns.Add("CLBZ", typeof(string));
            dt.Columns.Add("CLJG", typeof(string));
            dt.Columns.Add("FLAG", typeof(string));
            dt.Columns.Add("YLCARD_NO", typeof(string));

            if (YLCARD_TYPE == "0")//无卡
            {
                if (YLCARD_NO != "")
                {
                    if (CommonFunction.isValidSFZ(YLCARD_NO))//只支持身份证
                    {
                        DataRow dr = dt.NewRow();
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dr["FLAG"] = "2";
                        dr["YLCARD_NO"] = YLCARD_NO;
                        dt.Rows.Add(dr);
                        return dt;
                    }
                    else//其他医院无卡不算
                    {
                        DataRow dr = dt.NewRow();
                        dr["CLBZ"] = "1";
                        dr["CLJG"] = "非本院卡，请重新添加卡号";
                        dr["FLAG"] = "2";
                        dr["YLCARD_NO"] = YLCARD_NO;
                        dt.Rows.Add(dr);
                        return dt;
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "";
                    dr["FLAG"] = "2";
                    dr["YLCARD_NO"] = SFZ_NO;
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["CLBZ"] = "0";
                dr["CLJG"] = "";
                dr["FLAG"] = "2";
                dr["YLCARD_NO"] = YLCARD_NO;
                dt.Rows.Add(dr);
                return dt;
            }
        }

        /// <summary>
        /// 预约取消 不含支付
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SNAPPT"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="lTERMINAL_SN"></param>
        /// <returns></returns>
        public bool REGISTERPAYCANCEL(string HOS_ID, string HOS_SNAPPT, string HOS_SN, string lTERMINAL_SN, string PASSWORD, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERPAYCANCEL", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SNAPPT", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", "");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", "");
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                try
                {
                    if (dtrev.Rows[0]["CLBZ"].ToString().Trim().Equals("0"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="TYPE">短信类型/*1 预约成功发送 2.挂号成功发送3 预约支付成功发送4 挂号支付成功发送5 预约取消发送6 挂号取消发送7预约退费发送8挂号退费发送9 门诊缴费成功发送10门诊退费成功发送11预交金缴费成功发送*/</param>
        /// <param name="BIZ_SN"></param>
        /// <param name="MESSAGE"></param>
        /// <param name="MOBILE_NO"></param>
        /// <param name="PAT_NAME"></param>
        /// <returns></returns>
        public DataTable SENDMSG(string HOS_ID, string TYPE, string BIZ_SN, ref string MESSAGE, string MOBILE_NO, string PAT_NAME)
        {
            string message = "";
            bool result = false;
            if (TYPE == "14" || TYPE == "15")
            {
                return SENDMSG_ERROR(TYPE, BIZ_SN, ref MESSAGE, MOBILE_NO, PAT_NAME);
            }
            string REG_ID = BIZ_SN;

            DataTable dtREG = new Plat.BLL.BaseFunction().GetList("register_appt", "REG_ID='" + REG_ID + "'", "HOS_ID", "PAT_NAME", "HOS_SN", "GH_TYPE", "DEPT_NAME", "DOC_NO", "DOC_NAME", "DEPT_CODE", "SCH_DATE", "SCH_TIME", "PERIOD_START", "APPT_TATE", "appt_order");

            DataTable dtpay = new Plat.BLL.BaseFunction().GetList("register_pay", "REG_ID='" + REG_ID + "'", "OPT_SN");

            //string HOS_ID = dtREG.Rows[0]["HOS_ID"].ToString().Trim();
            string DEPT_CODE = dtREG.Rows[0]["DEPT_CODE"].ToString().Trim();
            string DOC_NO = dtREG.Rows[0]["DOC_NO"].ToString().Trim();
            string DEPT_NAME = dtREG.Rows[0]["DEPT_NAME"].ToString().Trim();
            string DOC_NAME = dtREG.Rows[0]["DOC_NAME"].ToString().Trim();
            string SCH_DATE = dtREG.Rows[0]["SCH_DATE"].ToString().Trim();
            string SCH_TIME = dtREG.Rows[0]["SCH_TIME"].ToString().Trim();
            string PERIOD_START = dtREG.Rows[0]["PERIOD_START"].ToString().Trim();
            string appt_order = FormatHelper.GetStr(dtREG.Rows[0]["appt_order"]);
            string APPT_TATE = FormatHelper.GetStr(dtREG.Rows[0]["APPT_TATE"]);
            string stryyorgh = (APPT_TATE != SCH_DATE) ? "预约" : "挂号";
            string ghtype = (DOC_NAME == "") ? "" : DOC_NAME + "医生";
            string time_perid = "";
            try
            {
                time_perid = FormatHelper.GetStr(dtREG.Rows[0]["PERIOD_START"]).Substring(0, 5) + "~" + FormatHelper.GetStr(dtREG.Rows[0]["PERIOD_START"]).Substring(0, 5);
            }
            catch
            {
                time_perid = "";
            }

            string SCH_TYPE = FormatHelper.GetStr(dtREG.Rows[0]["GH_TYPE"]);
            String OPT_SN = (dtpay != null && dtpay.Rows.Count > 0) ? FormatHelper.GetStr(dtpay.Rows[0]["OPT_SN"]) : "";
            String BH = FormatHelper.GetStr(dtREG.Rows[0]["appt_order"]);
            string URL = "http://wxweb.ztejsapp.cn/GhNotice.html?source=" + HOS_ID + "";

            PERIOD_START = (DOC_NAME == "") ? "" : PERIOD_START;
            if (TYPE == "1" || TYPE == "2")
            {
                //message = PAT_NAME + "||" + GetDeptName(DEPT_CODE, HOS_ID) + "||" + GetDocName(HOS_ID, DEPT_CODE, DOC_NO) + "||" + SCH_DATE + "||" + SCH_TIME + "||" + time_perid;
                //result = SMSDYHelper.SMSDyHelp.SendMsgTemplate(MOBILE_NO, message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), GetHosMessageId(HOS_ID, "4"));
                /*xxxxxxxxxxxxx：您已成功预约xxxxxxxxxxxxx(专家)xxxxxxxxxxxx的号，请在15分钟内完成支付，过期自动取消。*/
                message = string.Format(@"{0}：您已成功预约{1}(专家){2}的号，请在15分钟内完成支付，过期自动取消。", PAT_NAME, DEPT_NAME, DOC_NAME);
            }
            else if (TYPE == "3" || TYPE == "4")
            {
                if (SCH_TYPE == "4")
                {
                    /*尊敬的xxxxxxxxxxxxx，您好！您已成功预约我院xx月xx日周四全天xxxxxxxxxxxxx时段互联网医院（云诊室）xxxxxxxxxxxxx专家，请于就诊当天，提前登陆南京明基医院微信公众号互联网医院排队候诊。如有疑问，请拨打：xxxxxxxxxxxxxxxxxxxxxxxxx*/
                    message = string.Format(@"尊敬的{0}，您好！您已成功预约我院{1}月{2}日{3}{4}{5}时段互联网医院（云诊室）{6}专家，请于就诊当天，提前登陆泰康仙灵鼓楼医院微信公众号互联网医院排队候诊。如有疑问，请拨打：025-52238800-6100",
                        PAT_NAME, SCH_DATE.Substring(5, 2), SCH_DATE.Substring(8, 2), "周" + GetWeek(DateTime.Parse(SCH_DATE).DayOfWeek.ToString()), SCH_TIME, time_perid, DOC_NAME);
                }
                else
                {
                    /*xxxxxxxxxxxxx：您已成功挂取xxxxxxxxxxxxx(专家)xxxxxxxxxxxxx的号，门诊号为xxxxxxxxxxxxx，请凭此挂号记录信息按时到相应科室报到就诊。如需退号，请直接在手机上原路退回，过期不退。*/
                    message = string.Format(@"{0}：您已成功挂取{1}(专家){2}的号，门诊号为{3}，请凭此挂号记录信息按时到相应科室报到就诊。如需退号，请直接在手机上原路退回，过期不退。",
                        PAT_NAME, DEPT_NAME, DOC_NAME, OPT_SN);
                }
            }
            else if (TYPE == "5" || TYPE == "6")
            {
                /*xxxxxxxxxxxxx：您已成功取消xxxxxxxxxxxxx(专家)xxxxxxxxxxxxx的号。*/
                message = string.Format(@"{0}：您已成功取消{1}(专家){2}的号。",
                       PAT_NAME, DEPT_NAME, DOC_NAME);
            }
            else if (TYPE == "7" || TYPE == "8")
            {
                /*xxxxxxxxxxxxx：您已成功取消xxxxxxxxxxxxx(专家)xxxxxxxxxxxxx的号。*/
                message = string.Format(@"{0}：您已成功取消{1}(专家){2}的号。",
                       PAT_NAME, DEPT_NAME, DOC_NAME);
            }
            try
            {
                //DataTable dtrev = SendMessage(MOBILE_NO, message);
                //return dtrev;
                DataTable dtrev = new DataTable();
                dtrev.Columns.Add("CLBZ", typeof(string));
                dtrev.Columns.Add("CLJG", typeof(string));
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "0";
                newrow["CLJG"] = "发送成功";
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
            catch (Exception ex)
            {
                DataTable dtrev = new DataTable();
                dtrev.Columns.Add("CLBZ", typeof(string));
                dtrev.Columns.Add("CLJG", typeof(string));
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "9";
                newrow["CLJG"] = "发送失败";
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
        }

        public DataTable SENDMSG_ERROR(string TYPE, string BIZ_SN, ref string MESSAGE, string MOBILE_NO, string PAT_NAME)
        {
            string REG_ID = BIZ_SN;

            DataTable dtREG = new Plat.BLL.BaseFunction().GetList("register_appt", "REG_ID='" + REG_ID + "'", "HOS_ID", "PAT_NAME", "HOS_SN", "GH_TYPE", "DEPT_NAME");
            DataTable dtPAY = new Plat.BLL.BaseFunction().GetList("register_pay", "REG_ID='" + REG_ID + "'", "cash_je", "dj_date", "dj_time");

            string HOS_ID = dtREG.Rows[0]["HOS_ID"].ToString().Trim();
            DataTable dtHOS = new Plat.BLL.BaseFunction().GetList("hospital", "hos_id='" + HOS_ID + "'", "hos_name", "MSG_SEND");//判断预约支付是否调用短信发送接口

            string DEPT_NAME = dtREG.Rows[0]["DEPT_NAME"].ToString().Trim();
            string HOS_SN = dtREG.Rows[0]["HOS_SN"].ToString().Trim();
            string GH_TYPE = dtREG.Rows[0]["GH_TYPE"].ToString().Trim();
            string HOS_NAME = dtHOS.Rows[0]["HOS_NAME"].ToString().Trim();
            string PAY_TIME = Convert.ToDateTime(dtPAY.Rows[0]["dj_date"].ToString().Trim()).ToString("yyyy-MM-dd") + " " + dtPAY.Rows[0]["dj_time"].ToString().Trim();
            string CASH_JE = dtPAY.Rows[0]["CASH_JE"].ToString().Trim();

            //1 医院发送短信 0 使用公司短信
            DataTable dtCon = new Plat.BLL.BaseFunction().GetList("hos_configuration", "HOS_ID='" + HOS_ID + "'", "AUTOMESSAGE");

            string MSG_SEND = "";
            if (TYPE == "15")
            {
                MSG_SEND = string.Format(@"{0}您好！您于{1}支付的{2}{3}{4}号费用（预约编号{5}）业务处理失败，我们将按照原路径退还金额{6}元，请另行安排预约就诊。",
                    PAT_NAME, PAY_TIME, HOS_NAME, DEPT_NAME, GH_TYPE == "1" ? "普通" : "专家", HOS_SN, CASH_JE);
                MESSAGE = MSG_SEND;
            }

            if (TYPE == "14")
            {
                MSG_SEND = string.Format(@"退款成功{0}您好！您在{1}{2}{3}号（预约编号{4}）已经进行退款处理，金额{5}元按照原路径退还至您的账户。",
               PAT_NAME, HOS_NAME, DEPT_NAME, GH_TYPE == "1" ? "普通" : "专家", HOS_SN, CASH_JE);
                MESSAGE = MSG_SEND;
            }

            try
            {
                DataTable dtrev = SendMessage(MOBILE_NO, MSG_SEND);
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 发送短信(附带IP)
        /// </summary>
        /// <param name="MESSAGE">消息内容</param>
        /// <param name="MOBILE_NO">电话号码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <returns></returns>
        public DataTable SENDMSG(string HOS_ID, string MESSAGE, string MOBILE_NO, string PAT_NAME, string IP)
        {
            DataTable dtHOS = new Plat.BLL.BaseFunction().GetList("hos_configuration", "HOS_ID='" + HOS_ID + "'", "MESSAGE_COUNT", "IPMESSAGE_COUNT");//获取后台配置
            int MESSAGE_COUNT = dtHOS.Rows[0]["MESSAGE_COUNT"] == null ? 0 : Convert.ToInt32(dtHOS.Rows[0]["MESSAGE_COUNT"]);//手机号限制短信条数 目前为10
            int IPMESSAGE_COUNT = dtHOS.Rows[0]["IPMESSAGE_COUNT"] == null ? 0 : Convert.ToInt32(dtHOS.Rows[0]["IPMESSAGE_COUNT"]);//IP地址限制短信条数 目前为10

            if (MESSAGE_COUNT > 0)//零表示不限制短信条数
            {
                string condition = string.Format(@"HOS_ID='" + HOS_ID + "' and phone_no='{0}' and create_time BETWEEN '{1} 00:00:00' and '{1} 23:59:59'", MOBILE_NO, DateTime.Now.ToString("yyyy-MM-dd"));//获取当天同一个手机号短信条数
                DataTable dtCA = new Plat.BLL.BaseFunction().GetList("phone_captcha", condition, "phone_no");

                if (dtCA.Rows.Count >= MESSAGE_COUNT)//同一个手机号超过指定条数
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CLBZ", typeof(string));
                    dt.Columns.Add("CLJG", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "提示：今日短信发送次数已达上限";
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            if (IPMESSAGE_COUNT > 0)
            {
                string condition = string.Format(@"HOS_ID='" + HOS_ID + "' and IP='{0}' and create_time BETWEEN '{1} 00:00:00' and '{1} 23:59:59'", IP, DateTime.Now.ToString("yyyy-MM-dd"));//获取当天同一个IP短信条数
                DataTable dtCA = new Plat.BLL.BaseFunction().GetList("phone_captcha", condition, "IP");
                if (dtCA.Rows.Count >= IPMESSAGE_COUNT)//同一个IP超过指定条数
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CLBZ", typeof(string));
                    dt.Columns.Add("CLJG", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "提示：今日短信发送次数已达上限";
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            try
            {
                DataTable dtrev = SendMessage(MOBILE_NO, MESSAGE);
                return dtrev;
            }
            catch (Exception ex)
            {
                DataTable dtrev = new DataTable();
                dtrev.Columns.Add("CLBZ", typeof(string));
                dtrev.Columns.Add("CLJG", typeof(string));
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "9";
                newrow["CLJG"] = "发送失败";
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
        }

        public string GetWeek(string weekName)
        {
            string week;
            switch (weekName)
            {
                case "Sunday":
                    week = "日";
                    break;

                case "Monday":
                    week = "一";
                    break;

                case "Tuesday":
                    week = "二";
                    break;

                case "Wednesday":
                    week = "三";
                    break;

                case "Thursday":
                    week = "四";
                    break;

                case "Friday":
                    week = "五";
                    break;

                case "Saturday":
                    week = "六";
                    break;

                default:
                    week = "";
                    break;
            }
            return week;
        }

        /// <summary>
        /// 住院预约登记
        /// </summary>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="PHONE_NO">手机号</param>
        /// <param name="DEPT_CODE">预约住院科室</param>
        /// <param name="DIAGONOSE">诊断</param>
        /// <param name="DESCR">病因描述</param>
        /// <param name="DEADLINE">预约截止时间</param>
        /// <returns></returns>
        public bool INPATAPPTWAIT(string PAT_NAME, string PHONE_NO, string DEPT_CODE, string DIAGONOSE, string DESCR, string DEADLINE)
        {
            string SOURCE = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("INPATAPPTWAIT", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PHONE_NO", PHONE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DIAGONOSE", DIAGONOSE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DESCR", DESCR.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEADLINE", DEADLINE.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtbody.Rows[0]["CLBZ"].ToString().Trim() != "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 预约挂号数据同步
        /// </summary>
        /// <param name="SFZ_NO">身份证</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="YLCARD_NO">医疗卡号</param>
        /// <param name="PAT_ID">病人ID</param>
        /// <returns></returns>
        public DataSet YYGHUPLOADSAVE(string SFZ_NO, string PAT_NAME, string YLCARD_NO, string PAT_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            DataTable dtREG = new Plat.BLL.BaseFunction().GetList("register_appt", "PAT_ID='" + PAT_ID + "' and HOS_ID='" + HOS_ID + "' and sch_date>=curdate()", "APPT_TYPE", "SCH_DATE", "APPT_TATE", "HOS_SN", "YLCARD_TYPE", "YLCARD_NO");
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYGHUPLOADSAVE", "0");
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim());

            string HOS_SN = "";
            if (dtREG.Rows.Count == 0)
            {
                return null;
            }

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("HOS_SN", typeof(string));
            dtNew.Columns.Add("APPT_TYPE", typeof(string));
            dtNew.Columns.Add("SCH_TYPE", typeof(string));
            dtNew.Columns.Add("YLCARD_TYPE", typeof(string));
            dtNew.Columns.Add("YLCARD_NO", typeof(string));
            foreach (DataRow dr in dtREG.Rows)
            {
                DataRow dr_new = dtNew.NewRow();
                dr_new["HOS_SN"] = dr["HOS_SN"].ToString().Trim();
                dr_new["APPT_TYPE"] = dr["APPT_TYPE"].ToString().Trim();
                dr_new["SCH_TYPE"] = Convert.ToDateTime(dr["SCH_DATE"]).ToShortDateString() == Convert.ToDateTime(dr["APPT_TATE"]).ToShortDateString() ? "02" : "01";
                dr_new["YLCARD_TYPE"] = FormatHelper.GetStr(dr["YLCARD_TYPE"]);
                dr_new["YLCARD_NO"] = FormatHelper.GetStr(dr["YLCARD_NO"]);
                dtNew.Rows.Add(dr_new);
            }
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SNLIST");
            XMLHelper.X_XmlInsertTable(doc, "ROOT/BODY/SNLIST", dtNew, "SN");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "body";
                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());

                DataTable dtSTATELIST = new DataTable();
                dtSTATELIST.TableName = "STATELIST";
                try
                {
                    dtSTATELIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/STATELIST").Tables[0];
                    if (dtSTATELIST != null && !dtSTATELIST.Columns.Contains("DJ_TIME"))
                    {
                        dtSTATELIST.Columns.Add("DJ_TIME", typeof(string));
                        foreach (DataRow dr in dtSTATELIST.Rows)
                        {
                            dr["DJ_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }
                catch
                { }
                ds.Tables.Add(dtSTATELIST.Copy());

                DataTable dtAPPT_NOPAYLIST = new DataTable();
                dtAPPT_NOPAYLIST.TableName = "APPT_NOPAYLIST";
                try
                {
                    dtAPPT_NOPAYLIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/APPT_NOPAYLIST").Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dtAPPT_NOPAYLIST.Copy());

                DataTable dtAPPT_PAYLIST = new DataTable();
                dtAPPT_PAYLIST.TableName = "APPT_PAYLIST";
                try
                {
                    dtAPPT_PAYLIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/APPT_PAYLIST").Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dtAPPT_PAYLIST.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 HIS 每天对账总计金额
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="INPAYMETHOD"></param>
        /// <param name="HXFLAG"></param>
        /// <returns></returns>
        public DataSet GetCheckLedgerInfo(string INCHECKDATE, string INPAYMETHOD, string HXFLAG, string OUTRECEIVERNO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETCHECKLEDGERINFO", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INCHECKDATE", INCHECKDATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INPAYMETHOD", INPAYMETHOD.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HXFLAG", HXFLAG);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OUTRECEIVERNO", OUTRECEIVERNO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"]);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = new DataTable();
                try
                {
                    dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT").Tables[0];
                    if (dtrev.Rows[0]["APPCODE"].ToString().Trim() == "1")
                    {
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "0";
                        dr_new["CLJG"] = "";
                        dtrev.Rows.Add(dr_new);
                    }
                    else
                    {
                        string error = dtrev.Rows[0]["ERRORMSG"].ToString().Trim();
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "1";
                        dr_new["CLJG"] = error;
                        dtrev.Rows.Add(dr_new);
                    }
                }
                catch
                { }
                DataTable dtData = new DataTable();
                try
                {
                    dtData = XMLHelper.X_GetXmlDataTable(xmldoc, "ROOT/LIST/ITEM");
                }
                catch
                { }

                dtrev.TableName = "rev";
                dtData.TableName = "data";
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                ds.Tables.Add(dtData.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 HIS 每天对账明细记录
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="INPAYMETHOD"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="OUTRECEIVERNO"></param>
        /// <returns></returns>
        public DataSet GETCHECLEDGERDETAIL(string INCHECKDATE, string INPAYMETHOD, string HXFLAG, string OUTRECEIVERNO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETCHECLEDGERDETAIL", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INCHECKDATE", INCHECKDATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INPAYMETHOD", INPAYMETHOD.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HXFLAG", HXFLAG);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OUTRECEIVERNO", OUTRECEIVERNO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"]);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = new DataTable();
                try
                {
                    dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT").Tables[0];
                    if (dtrev.Rows[0]["APPCODE"].ToString().Trim() == "1")
                    {
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "0";
                        dr_new["CLJG"] = "";
                        dtrev.Rows.Add(dr_new);
                    }
                    else
                    {
                        string error = dtrev.Rows[0]["ERRORMSG"].ToString().Trim();
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "1";
                        dr_new["CLJG"] = error;
                        dtrev.Rows.Add(dr_new);
                    }
                }
                catch
                { }
                DataTable dtData = new DataTable();
                try
                {
                    dtData = XMLHelper.X_GetXmlDataTable(xmldoc, "ROOT/LIST/ITEM");
                }
                catch
                { }

                dtrev.TableName = "rev";
                dtData.TableName = "data";
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                ds.Tables.Add(dtData.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 HIS 每天对账退费明细记录
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="INPAYMETHOD"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="OUTRECEIVERNO"></param>
        /// <returns></returns>
        public DataSet GETCHECLEDGERDETAILTF(string INCHECKDATE, string INPAYMETHOD, string HXFLAG, string OUTRECEIVERNO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETCHECLEDGERDETAILTF", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INCHECKDATE", INCHECKDATE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "INPAYMETHOD", INPAYMETHOD.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HXFLAG", HXFLAG);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OUTRECEIVERNO", OUTRECEIVERNO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"]);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = new DataTable();
                try
                {
                    dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT").Tables[0];
                    if (dtrev.Rows[0]["APPCODE"].ToString().Trim() == "1")
                    {
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "0";
                        dr_new["CLJG"] = "";
                        dtrev.Rows.Add(dr_new);
                    }
                    else
                    {
                        string error = dtrev.Rows[0]["ERRORMSG"].ToString().Trim();
                        dtrev = new DataTable();
                        dtrev.Columns.Add("CLBZ", typeof(string));
                        dtrev.Columns.Add("CLJG", typeof(string));
                        DataRow dr_new = dtrev.NewRow();
                        dr_new["CLBZ"] = "1";
                        dr_new["CLJG"] = error;
                        dtrev.Rows.Add(dr_new);
                    }
                }
                catch
                { }
                DataTable dtData = new DataTable();
                try
                {
                    dtData = XMLHelper.X_GetXmlDataTable(xmldoc, "ROOT/LIST/ITEM");
                }
                catch
                { }

                dtrev.TableName = "rev";
                dtData.TableName = "data";
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                ds.Tables.Add(dtData.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 HIS 每天对账退费明细记录
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="INPAYMETHOD"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="OUTRECEIVERNO"></param>
        /// <returns></returns>
        public DataSet GETHISOTHERMX(string INCHECKDATE, string INPAYMETHOD, string HXFLAG, string OUTRECEIVERNO, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 检查是否可以退费
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="TYPE"></param>
        /// <returns></returns>
        public DataTable CHECKCANCELSTATE(string HOS_ID, string HOS_SN, string TYPE, Dictionary<string, string> dic)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            //string REG_ID = dic["REG_ID"];
            string REG_ID = dic.ContainsKey("REG_ID") ? dic["REG_ID"] : "";
            if (TYPE == "0")//挂号
            {
                DataTable dtAPPT = new Plat.BLL.BaseFunction().GetList("register_APPT", "REG_ID='" + REG_ID + "'", "SCH_DATE", "APPT_TYPE");
                if (dtAPPT.Rows[0]["APPT_TYPE"].ToString().Trim() == "1" && FormatHelper.GetStr(dtAPPT.Rows[0]["SCH_DATE"]).CompareTo(DateTime.Now.ToString("yyyy-MM-dd")) > 0)
                {
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "SUCCESS";
                    dtrev.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "不符合手机退费条件，请去人工窗口退费";
                    dtrev.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtrev.NewRow();
                dr["CLBZ"] = "1";
                dr["CLJG"] = "请去人工窗口退费";
                dtrev.Rows.Add(dr);
            }
            return dtrev;
        }

        /// <summary>
        /// 检查是否可以退费
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="TYPE"></param>
        /// <returns></returns>
        public DataTable CHECKCANCELSTATE(string HOS_ID, string HOS_SN, string TF_SOURCE, string TYPE, Dictionary<string, string> dic)
        {
            return CHECKCANCELSTATE(HOS_ID, HOS_SN, TYPE, dic);
        }

        /// <summary>
        /// 门诊退费同步
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="SFZ_NO"></param>
        /// <param name="PAT_NAME"></param>
        /// <param name="YLCARD_NO"></param>
        /// <param name="HOS_ID"></param>
        /// <returns></returns>
        public DataTable MZSTFUPLOAD(string PAT_ID, string SFZ_NO, string PAT_NAME, string YLCARD_NO, string HOS_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYMZUPLOADSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFLIST");

            string BARCODE = "";
            DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("opt_pay", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and dj_date>='" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "' and HOS_PAY_SN<>'ERROR'", "HOS_PAY_SN", "HOS_ID", "LTERMINAL_SN");

            XMLHelper.X_XmlInsertTable(doc, "ROOT/BODY/SFLIST", dtpatcardbind, "SF");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev.Rows[0]["CLBZ"].ToString().Trim() != "0")
                {
                    return null;
                }
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/STATELIST").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询医院数据
        /// </summary>
        /// <param name="SQL_WORD"></param>
        /// <returns></returns>
        public DataTable QUERYSQL(string SQL_WORD, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 医保在线支付预结算
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="SFZ_NO">病人身份证号码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="YLCARD_TYPE">卡类型</param>
        /// <param name="YLCARD_NO">卡号</param>
        /// <param name="HOS_SN">HIS预约唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标示</param>
        ///<param name="external">各医院自己定制的字段</param>
        /// <returns></returns>
        public DataTable YYGHMOBPAYYJS(string HOS_ID, string SFZ_NO, string PAT_NAME, string YLCARD_TYPE, string YLCARD_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            string SOURCE = external.ContainsKey("SOURCE") ? FormatHelper.GetStr(external["SOURCE"]) : "";
            if (external.ContainsKey("MZ"))
            {
                //public DataTable OUTFEEMOBPAYYJS(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, decimal JEALL, decimal CASH_JE, string DJ_DATE, string DJ_TIME, string lTERMINAL_SN)
                return OUTFEEMOBPAYYJS(HOS_ID, external["OPT_SN"], external["PRE_NO"], external["HOS_SN"], decimal.Parse(external["JEALL"]), decimal.Parse(external["CASH_JE"]), external["DJ_DATE"], external["DJ_TIME"], external["lTERMINAL_SN"], external);
            }
            string BARCODE = "";
            string PAT_ID = external["PAT_ID"].ToString().Trim();
            DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1  order by BAND_TIME DESC", "YLCARD_NO");
            if (dtpatcardbind.Rows.Count > 0)
            {
                BARCODE = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            }

            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYGHMOBPAYYJS", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN);
            //SHBZKH MZNO YLTYPE MOBILE_NO
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SHBZKH", external["SHBZKH"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", external["MZNO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLTYPE", external["YLTYPE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", external["MOBILE_NO"]);

            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", external["DOC_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", external["DEPT_CODE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", external["DOC_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", external["SCH_TYPE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRO_TITLE", external["PRO_TITLE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "mediInsuOutpam", external["mediInsuOutpam"]);

            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", external["JEALL"]);

            string rtnxml = CallService(doc.OuterXml, SOURCE);
            XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            DataTable dtrev = new DataTable();
            try
            {
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
            }
            catch
            {
                return null;
            }
            return dtrev;
        }

        /// <summary>
        /// 医保在线支付结算
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="SFZ_NO">病人身份证号码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="YLCARD_TYPE">卡类型</param>
        /// <param name="YLCARD_NO">卡号</param>
        /// <param name="HOS_SN">HIS预约唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标示</param>
        /// <param name="YBDJH">医保单据号</param>
        ///<param name="external">各医院自己定制的字段</param>
        /// <returns></returns>
        public DataTable YYGHMOBPAYJS(string HOS_ID, string SFZ_NO, string PAT_NAME, string YLCARD_TYPE, string YLCARD_NO, string HOS_SN, string lTERMINAL_SN, string YBDJH, Dictionary<string, string> external)
        {
            string SOURCE = external.ContainsKey("SOURCE") ? FormatHelper.GetStr(external["SOURCE"]) : "";
            if (external.ContainsKey("MZ"))
            {
                //public DataTable OUTFEEMOBPAYYJS(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, decimal JEALL, decimal CASH_JE, string DJ_DATE, string DJ_TIME, string lTERMINAL_SN)
                return OUTFEEMOBPAYJS(HOS_ID, external["OPT_SN"], external["PRE_NO"], external["HOS_SN"], decimal.Parse(external["JEALL"]), decimal.Parse(external["CASH_JE"]), external["DJ_DATE"], external["DJ_TIME"], external["lTERMINAL_SN"], external["mediInsuOutpam"], external);
            }
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYGHMOBPAYJS", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARTD_TYPE", YLCARD_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN);

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_OUT", external["YJS_OUT"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YBDJH", external["YBDJH"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SHBZKH", external["SHBZKH"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", external["MZNO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLTYPE", external["YLTYPE"]);

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", external["DOC_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", external["DEPT_CODE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_TYPE", external["SCH_TYPE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRO_TITLE", external["PRO_TITLE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "mediInsuOutpam", external["mediInsuOutpam"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", external["JEALL"]);

            string rtnxml = CallService(doc.OuterXml, SOURCE);
            XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            DataTable dtrev = new DataTable();
            try
            {
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                return dtrev;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// APP支付前判断病人是否在院签到
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">预约挂号医院唯一流水号</param>
        /// <param name="SCH_DATE">排班日期</param>
        /// <param name="SCH_TIME">排班时间</param>
        /// <param name="YLCARD_TYPE">医疗卡类型</param>
        /// <param name="YLCARD_NO">医疗卡号</param>
        /// <param name="lTERMINAL_SN">终端标示</param>
        ///<param name="external">各医院自己定制的字段</param>
        /// <returns></returns>
        public DataTable GETAPPTSIGNININFO(string HOS_ID, string HOS_SN, string SCH_DATE, string SCH_TIME, string YLCARD_TYPE, string YLCARD_NO, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            DataTable dt_rev = new DataTable();
            dt_rev.Columns.Add("CLBZ", typeof(string));
            dt_rev.Columns.Add("CLJG", typeof(string));

            DataRow dr = dt_rev.NewRow();
            dr["CLBZ"] = "0";
            dr["CLJG"] = "";

            dt_rev.Rows.Add(dr);

            return dt_rev;
        }

        public DataTable OUTFEEMOBPAYYJS(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, decimal JEALL, decimal CASH_JE, string DJ_DATE, string DJ_TIME, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            if (PRE_NO == "" || PRE_NO.Split('|').LastOrDefault() != "1")
            {
                return ReturnFail("1", "该处方不符合医保结算条件，请选择自费结算！");
            }
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("OUTFEEMOBPAYYJS", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", PRE_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HIS_YS_NO"]);//HIS预结算单据号
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", JEALL.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_DATE", DJ_DATE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_TIME", DJ_TIME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dic["MZNO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLTYPE", dic["YLTYPE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SHBZKH", dic["SHBZKH"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", dic["MOBILE_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", dic["DEPT_CODE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", dic["DOC_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "mediInsuOutpam", CommonFunction.GetJsonValue(FormatHelper.GetStr(dic["mediInsuOutpam"]), "mediInsuOutpam"));
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", dic.ContainsKey("YLCARD_TYPE") ? dic["YLCARD_TYPE"] : "");

            string rtnxml = CallService(doc.OuterXml, SOURCE);
            XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            DataTable dtrev = new DataTable();
            try
            {
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
            }
            catch
            {
                return null;
            }

            return dtrev;
        }

        public DataTable OUTFEEMOBPAYJS(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, decimal JEALL, decimal CASH_JE, string DJ_DATE, string DJ_TIME, string lTERMINAL_SN, string mediInsuOutpam, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            if (PRE_NO == "" || PRE_NO.Split('|').LastOrDefault() != "1")
            {
                return ReturnFail("1", "该处方不符合医保结算条件，请选择自费结算！");
            }
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("OUTFEEMOBPAYJS", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", PRE_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HIS_YS_NO"]);//HIS预结算单据号
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "JEALL", JEALL.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", CASH_JE.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_DATE", DJ_DATE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_TIME", DJ_TIME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "mediInsuOutpam", CommonFunction.GetJsonValue(mediInsuOutpam, "mediInsuOutpam"));

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MZNO", dic["MZNO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLTYPE", dic["YLTYPE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SHBZKH", dic["SHBZKH"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", dic["MOBILE_NO"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", dic["DEPT_CODE"]);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", dic["DOC_NO"]);

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJS_OUT", dic["YJS_OUT"]);//hlw mod 2017.10.11 增加门诊单据
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", dic.ContainsKey("YLCARD_TYPE") ? dic["YLCARD_TYPE"] : "");
            string rtnxml = CallService(doc.OuterXml, SOURCE);
            XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            DataTable dtrev = new DataTable();
            try
            {
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
            }
            catch
            {
                return null;
            }

            return dtrev;
        }

        /// <summary>
        /// 获取当日就诊病人信息
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="PAGEINDEX"></param>
        /// <param name="PAGESIZE"></param>
        /// <param name="SEARCH_TYPE"></param>
        /// <returns></returns>
        public string GETDAYPATINFO(string HOS_ID, int PAGEINDEX, int PAGESIZE, int SEARCH_TYPE, string lTERMINAL_SN, ref int PAGECOUNT)
        {
            string SOURCE = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETDAYPATINFO", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGEINDEX", PAGEINDEX.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAGESIZE", PAGESIZE.ToString().Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEARCH_TYPE", SEARCH_TYPE.ToString());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", lTERMINAL_SN.Trim());
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                return rtnxml;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GETPATHOSPITALID(string YNCARDNO, string SFZ_NO, string PAT_NAME, string SEX, string BIRTHDAY, string GUARDIAN_NAME, string MOBILE_NO, string ADDRESS, string PAT_ID, string YLCARD_TYPE, string YLCARD_NO)
        {
            string SOURCE = "";
            if (YNCARDNO == "")
            {
                if (YLCARD_TYPE == "2")
                {
                    string sqlcmd = string.Format(@"select * from platform.pat_card where pat_id=@PAT_ID and ylcartd_type='2'  ");
                    MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@PAT_ID", PAT_ID) };
                    DataTable dtpat_card = DbHelperMySQL.Query(sqlcmd, spa).Tables[0];
                    if (dtpat_card.Rows.Count > 0)
                    {
                        YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YBCARD_NO"]);
                    }
                    if (YLCARD_NO == "")
                    {
                        YLCARD_TYPE = "4";
                        YLCARD_NO = SFZ_NO;
                    }
                }
                XmlDocument doc = new XmlDocument();
                doc = QHXmlMode.GetBaseXml("SENDCARDINFO", "0");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", ((YLCARD_TYPE == "0" || YLCARD_TYPE == "4") ? "4" : YLCARD_TYPE));
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", ((YLCARD_TYPE == "0" || YLCARD_TYPE == "4") ? SFZ_NO : YLCARD_NO));
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEX", SEX.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIRTHDAY", BIRTHDAY.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_NAME", GUARDIAN_NAME.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", MOBILE_NO.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", ADDRESS.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPERATOR", "");
                try
                {
                    string rtnxml = CallService(doc.OuterXml, SOURCE);
                    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                    DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                    if (dtrev.Rows[0]["CLBZ"].ToString().Trim() == "0")
                    {
                        YNCARDNO = FormatHelper.GetStr(dtrev.Rows[0]["HOSPATID"]);

                        Plat.BLL.pat_card_bind BLLpat_card_bind = new Plat.BLL.pat_card_bind();
                        bool exists = BLLpat_card_bind.Exists(HOS_ID, FormatHelper.GetInt(PAT_ID), 1, YNCARDNO);
                        if (!exists)
                        {
                            Plat.Model.pat_card_bind pat_card_bind = new Plat.Model.pat_card_bind();
                            pat_card_bind.HOS_ID = HOS_ID;
                            pat_card_bind.PAT_ID = FormatHelper.GetInt(PAT_ID);
                            pat_card_bind.YLCARTD_TYPE = 1;
                            pat_card_bind.YLCARD_NO = YNCARDNO;
                            pat_card_bind.MARK_BIND = 1;
                            pat_card_bind.BAND_TIME = DateTime.Now;
                            BLLpat_card_bind.Add(pat_card_bind);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return YNCARDNO;
        }

        public int GetAgeBySFZ(string identityCard)
        {
            string birthday = "";
            DateTime dpbirthday = DateTime.Now;
            //处理18位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 18)
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
            }
            //处理15位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
            }

            dpbirthday = Convert.ToDateTime(birthday);
            int age = DateTime.Now.Year - dpbirthday.Year;
            if (DateTime.Now.Month < dpbirthday.Month || (DateTime.Now.Month == dpbirthday.Month && DateTime.Now.Day < dpbirthday.Day)) age--;
            return age;
        }

        /// <summary>
        /// 获取病人体检记录 （TYPE：GETPERECORD）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="SFZ_NO">身份证号码</param>
        /// <param name="BEGINDATE">开始查询日期</param>
        /// <param name="ENDDATE">截止查询日期</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETPERECORD(string HOS_ID, string PAT_NAME, string SFZ_NO, string BEGINDATE, string ENDDATE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.2获取体检报告包含明细指标（分科室） （TYPE：GETALLPEREPORT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETALLPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.3获取体检报告科室列表 （TYPE：GETDEPTPEREPORT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETDEPTPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        ///7.4获取体检报告明细（分科室） （TYPE：GETDEPTPEREPORTMX）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETDEPTPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string DEPT_CODE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.5获取体检报告结论（TYPE：GETPEREPORTRESULT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">病人唯一ID</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETPEREPORTRESULT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 通用HIS接口
        /// </summary>
        /// <param name="external"></param>
        /// <returns></returns>
        public DataSet COMMONINTERFACE(Dictionary<string, string> external)
        {
            string TYPE = external["MODULE_TYPE"];

            string SOURCE = external.ContainsKey("SOURCE") ? FormatHelper.GetStr(string.IsNullOrEmpty(external["SOURCE"]) ? "" : external["SOURCE"]) : "";
            switch (TYPE)
            {
                case "GETSTATUSBYORDERID"://订单状态查询
                    return GETSTATUSBYORDERID(external["COMM_MAIN"], external["ZF_TYPE"], external["BIZ_TYPE"], SOURCE);

                case "CHECKMBCANPAY":
                    return CHECKMBCANPAY(external["SFZ_NO"], external["MB_ID"]);

                case "YUNHOSPRESEXAMSAVE"://互联网医师审方结果保存到HIS系统
                    return YUNHOSPRESEXAMSAVE(external["HOS_ID"], external["HOS_SN"], external["PAT_NAME"], external["SFZ_NO"], external["YLCARTD_TYPE"], external["YLCARD_NO"], external["DEPT_CODE"], external["DOC_NO"], external["SF_DOC"], external["RECIPE_NO"], external["SF_STATUS"], external["SF_DESC"], external.ContainsKey("CASign") ? external["CASign"] : "");

                case "GETPATBARCODE"://根据病人ID获取院内卡
                    string YNCARDNO = "";
                    return GETPATBARCODE(external["HOS_ID"], external["PAT_ID"], ref YNCARDNO);

                case "CHECKSCHVALID":
                    return null;

                case "GETHOSZFRESULT":
                    return null;

                default:
                    return COMMONINTERFACE(TYPE, external);
            }
            return null;
        }

        public DataSet COMMONINTERFACE(string TYPE, Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml(TYPE, "0");
            DataSet ds = new DataSet();
            switch (TYPE)
            {
                case "GETCHSREGTRY":
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HOS_SN"].ToString());
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_TYPE", "04");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_NO", dic["MDTRT_CERT_NO"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SIGN_NO", "");

                    break;

                case "CHSREGTRY":
                    string msg = "";
                    QHSiInterface.RTNJ1101.Root rt1101 = JSONSerializer.Deserialize<QHSiInterface.RTNJ1101.Root>(dic["CHSOUTPUT1101"]);
                    QHSiInterface.RTNJ2201.Root rt2201 = JSONSerializer.Deserialize<QHSiInterface.RTNJ2201.Root>(dic["CHSOUTPUT2201"]);
                    Dictionary<string, string> expContent = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                    string chsInput2203 = expContent["chsInput2203"];
                    string chsInput2204 = expContent["chsInput2204"];
                    string chsInput2206 = expContent["chsInput2206"];
                    //string chsInput2207 = expContent["chsInput2207"];
                    string YBDJH = expContent["YBDJH"];

                    #region 2203A

                    QHSiInterface.T2203.Input t2203 = JSONSerializer.Deserialize<QHSiInterface.T2203.Input>(chsInput2203);
                    t2203.mdtrtinfo.mdtrt_id = rt2201.output.data.mdtrt_id;
                    string infno = "2203A";
                    CHSHelper.InputRoot inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2203); //jin2203.ToString();
                    CHSHelper.OutputRoot outputRoot = new CHSHelper.OutputRoot();
                    bool flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2203 = outputRoot.chsInput;
                    string chsOutput2203 = outputRoot.chsOutput;

                    #endregion 2203A

                    #region 2204

                    QHSiInterface.T2204.Input t2204 = JSONSerializer.Deserialize<QHSiInterface.T2204.Input>(chsInput2204);
                    for (int i = 0; i < t2204.feedetail.Count; i++)
                    {
                        t2204.feedetail[i].mdtrt_id = rt2201.output.data.mdtrt_id;
                    }
                    infno = "2204";
                    inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2204); //jin2204.ToString();
                    outputRoot = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2204 = outputRoot.chsInput;
                    string chsOutput2204 = outputRoot.chsOutput;

                    #endregion 2204

                    #region 2206

                    QHSiInterface.T2206.Input t2206 = JSONSerializer.Deserialize<QHSiInterface.T2206.Input>(chsInput2206);
                    t2206.data.mdtrt_id = rt2201.output.data.mdtrt_id;
                    infno = "2206";
                    inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2206); //jin2206.ToString();
                    outputRoot = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2206 = outputRoot.chsInput;

                    string chsOutput2206 = outputRoot.chsOutput;

                    #endregion 2206

                    #region 2207

                    try
                    {
                        QHSiInterface.RT2206.Root rt2206 = JSONSerializer.Deserialize<QHSiInterface.RT2206.Root>(chsOutput2206);
                        //QHSiInterface.T2207.Input t2207 = JSONSerializer.Deserialize<QHSiInterface.T2207.Input>(chsInput2207);
                        //t2207.data.mdtrt_id = rt2206.output.setlinfo.mdtrt_id;
                        //t2207.data.fulamt_ownpay_amt = rt2206.output.setlinfo.fulamt_ownpay_amt;
                        //t2207.data.overlmt_selfpay = rt2206.output.setlinfo.overlmt_selfpay;
                        //t2207.data.preselfpay_amt = rt2206.output.setlinfo.preselfpay_amt;
                        //t2207.data.inscp_scp_amt = rt2206.output.setlinfo.inscp_scp_amt.ToString();
                        //chsInput2207 = JSONSerializer.Serialize(t2207);

                        #region 调用HIS2207

                        doc = QHXmlMode.GetBaseXml("GETCHSREGSAVE", "0");
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HOS_SN"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", dic["HOS_SN"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_TYPE", "04");
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_NO", dic["MDTRT_CERT_NO"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSINPUT2206", chsInput2206);
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT2206", chsOutput2206);
                        string rtnxml = CallService(doc.OuterXml, "");
                        XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                        DataSet dsbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
                        string chsInput2207 = dsbody.Tables[0].Columns.Contains("chsInput2207") ? dsbody.Tables[0].Rows[0]["chsInput2207"].ToString() : "";
                        YBDJH = dsbody.Tables[0].Columns.Contains("YBDJH") ? dsbody.Tables[0].Rows[0]["YBDJH"].ToString() : "";
                        string MZNO = dsbody.Tables[0].Columns.Contains("MZNO") ? dsbody.Tables[0].Rows[0]["MZNO"].ToString() : "";

                        #endregion 调用HIS2207

                        expContent = new Dictionary<string, string>();
                        expContent.Add("YBDJH", YBDJH);
                        expContent.Add("MZNO", MZNO);
                        expContent.Add("insuplc_admdvs", rt1101.output.insuinfo[0].insuplc_admdvs);
                        expContent.Add("chsInput2201", dic["CHSINPUT2201"]);
                        expContent.Add("chsOutput1101", dic["CHSOUTPUT1101"]);
                        expContent.Add("chsInput2203", chsInput2203);
                        expContent.Add("chsInput2204", chsInput2204);
                        expContent.Add("chsOutput2204", chsOutput2204);
                        expContent.Add("chsInput2206", chsInput2206);
                        expContent.Add("chsOutput2206", chsOutput2206);
                        expContent.Add("chsInput2207", chsInput2207);
                        DataTable dtrev1 = new DataTable();
                        dtrev1.Columns.Add("CHSOUTPUT2206", typeof(string));
                        dtrev1.Columns.Add("CHSINPUT2207", typeof(string));
                        dtrev1.Columns.Add("EXPCONTENT", typeof(string));
                        dtrev1.Columns.Add("CLBZ", typeof(string));
                        dtrev1.Columns.Add("CLJG", typeof(string));
                        DataRow dr = dtrev1.NewRow();
                        dr["CHSOUTPUT2206"] = JSONSerializer.Serialize(rt2206.output);
                        dr["CHSINPUT2207"] = chsInput2207;
                        dr["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(expContent);
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dtrev1.Rows.Add(dr);
                        dtrev1.TableName = "BODY";
                        ds.Tables.Add(dtrev1.Copy());
                    }
                    catch (Exception ex)
                    {
                        DataTable dtrev = RtnResultDatatable("99", ex.ToString());
                        dtrev.TableName = "BODY";
                        ds.Tables.Add(dtrev.Copy());
                    }
                    return ds;

                    #endregion 2207

                    break;

                case "GETCHSOUTPTRY":
                    QHSiInterface.RT1101.Root rt1101SF = JSONSerializer.Deserialize<QHSiInterface.RT1101.Root>(dic["CHSOUTPUT1101"]);
                    string CHSOUTPUT5360 = "";
                    if (rt1101SF.output.data.Count > 0)
                    {
                        QHSiInterface.RT5360.Root rt5360 = new QHSiInterface.RT5360.Root();
                        rt5360.output = new QHSiInterface.RT5360.Output();
                        rt5360.output.data = new List<QHSiInterface.RT5360.insuinfo>();
                        rt5360.infcode = "0";
                        rt5360.err_msg = "";
                        rt5360.inf_refmsgid = "";
                        rt5360.respond_time = "";
                        rt5360.refmsg_time = "";
                        for (int i = 0; i < rt1101SF.output.data.Count; i++)
                        {
                            QHSiInterface.RT5360.insuinfo insuinfo = new QHSiInterface.RT5360.insuinfo()
                            {
                                psn_no = rt1101SF.output.data[i].psn_no,
                                med_type = rt1101SF.output.data[i].med_type,
                                insutype = rt1101SF.output.data[i].insutype,
                                begndate = rt1101SF.output.data[i].begndate,
                                dise_codg = rt1101SF.output.data[i].dise_codg,
                                dise_name = rt1101SF.output.data[i].dise_name,
                                enddate = "",
                                exp_content = "",
                                hilist_code = rt1101SF.output.data[i].hilist_code,
                                hilist_name = rt1101SF.output.data[i].hilist_name
                            };
                            rt5360.output.data.Add(insuinfo);
                        }
                        CHSOUTPUT5360 = JSONSerializer.Serialize(rt5360);
                    }
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HOS_SN"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", dic["OPT_SN"].ToString());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", dic["PRE_NO"].ToString());
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"].ToString());
                    XMLHelper.X_XmlInsertNode_NOCHANGE(doc, "ROOT/BODY", "chsOutput5360", CHSOUTPUT5360);
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_TYPE", "04");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_NO", dic["MDTRT_CERT_NO"].ToString());
                    break;

                case "CHSOUTPTRY":
                    string msgTRY = "";
                    QHSiInterface.RTNJ1101.Root rt1101TRY = JSONSerializer.Deserialize<QHSiInterface.RTNJ1101.Root>(dic["CHSOUTPUT1101"]);
                    QHSiInterface.RTNJ2201.Root rt2201TRY = JSONSerializer.Deserialize<QHSiInterface.RTNJ2201.Root>(dic["CHSOUTPUT2201"]);
                    Dictionary<string, string> expContentTRY = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                    string chsInput2203TRY = expContentTRY["chsInput2203"];
                    string chsInput2204TRY = expContentTRY["chsInput2204"];
                    string chsInput2206TRY = expContentTRY["chsInput2206"];
                    string chsInput2207TRY = expContentTRY["chsInput2207"];
                    string YBDJHTRY = expContentTRY["YBDJH"];

                    #region 2203A

                    QHSiInterface.T2203.Input t2203TRY = JSONSerializer.Deserialize<QHSiInterface.T2203.Input>(chsInput2203TRY);
                    t2203TRY.mdtrtinfo.mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    string infnoTRY = "2203A";
                    CHSHelper.InputRoot inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2203TRY); //jin2203.ToString();
                    CHSHelper.OutputRoot outputRootTRY = new CHSHelper.OutputRoot();
                    bool flagTRY = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flagTRY)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2203 = outputRootTRY.chsInput;
                    string chsOutput2203TRY = outputRootTRY.chsOutput;

                    #endregion 2203A

                    #region 2204

                    QHSiInterface.T2204.Input t2204TRY = JSONSerializer.Deserialize<QHSiInterface.T2204.Input>(chsInput2204TRY);
                    for (int i = 0; i < t2204TRY.feedetail.Count; i++)
                    {
                        t2204TRY.feedetail[i].mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    }
                    infnoTRY = "2204";
                    inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2204TRY); //jin2204.ToString();
                    outputRootTRY = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2204TRY = outputRootTRY.chsInput;
                    string chsOutput2204TRY = outputRootTRY.chsOutput;

                    #endregion 2204

                    #region 2206

                    QHSiInterface.T2206.Input t2206TRY = JSONSerializer.Deserialize<QHSiInterface.T2206.Input>(chsInput2206TRY);
                    t2206TRY.data.mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    infnoTRY = "2206";
                    inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2206TRY); //jin2206.ToString();
                    outputRootTRY = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2206TRY = outputRootTRY.chsInput;
                    string chsOutput2206TRY = outputRootTRY.chsOutput;

                    #endregion 2206

                    #region 2207

                    try
                    {
                        QHSiInterface.RT2206.Root rt2206 = JSONSerializer.Deserialize<QHSiInterface.RT2206.Root>(chsOutput2206TRY);
                        //QHSiInterface.T2207.Root t2207 = JSONSerializer.Deserialize<QHSiInterface.T2207.Root>(chsInput2207TRY);
                        //t2207.input.data.mdtrt_id = rt2206.output.setlinfo.mdtrt_id;
                        //t2207.input.data.fulamt_ownpay_amt = rt2206.output.setlinfo.fulamt_ownpay_amt;
                        //t2207.input.data.overlmt_selfpay = rt2206.output.setlinfo.overlmt_selfpay;
                        //t2207.input.data.preselfpay_amt = rt2206.output.setlinfo.preselfpay_amt;
                        //t2207.input.data.inscp_scp_amt = rt2206.output.setlinfo.inscp_scp_amt.ToString();
                        //string chsInput2207 = JSONSerializer.Serialize(t2207.input);

                        #region 调用HIS2207

                        doc = QHXmlMode.GetBaseXml("GETCHSOUTPSAVE", "0");
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", dic["HOS_ID"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", dic["HOS_SN"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", dic["PRE_NO"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "chsOutput1101", dic["CHSOUTPUT1101"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_TYPE", "04");
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MDTRT_CERT_NO", dic["MDTRT_CERT_NO"].ToString());
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSINPUT2206", chsInput2206TRY);
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CHSOUTPUT2206", chsOutput2206TRY);
                        string rtnxml = CallService(doc.OuterXml, "");
                        XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                        DataSet dsbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
                        string chsInput2207 = dsbody.Tables[0].Columns.Contains("chsInput2207") ? dsbody.Tables[0].Rows[0]["chsInput2207"].ToString() : "";
                        YBDJH = dsbody.Tables[0].Columns.Contains("YBDJH") ? dsbody.Tables[0].Rows[0]["YBDJH"].ToString() : "";
                        string MZNO = dsbody.Tables[0].Columns.Contains("MZNO") ? dsbody.Tables[0].Rows[0]["MZNO"].ToString() : "";

                        #endregion 调用HIS2207

                        expContent = new Dictionary<string, string>();
                        //expContent.Add("YBDJH", YBDJHTRY);
                        expContent.Add("YBDJH", YBDJH);
                        expContent.Add("MZNO", MZNO);
                        expContent.Add("insuplc_admdvs", rt1101TRY.output.insuinfo[0].insuplc_admdvs);
                        expContent.Add("chsInput2201", dic["CHSINPUT2201"]);
                        expContent.Add("chsOutput1101", dic["CHSOUTPUT1101"]);
                        expContent.Add("chsInput2203", chsInput2203);
                        expContent.Add("chsInput2204", chsInput2204TRY);
                        expContent.Add("chsOutput2204", chsOutput2204TRY);
                        expContent.Add("chsInput2206", chsInput2206TRY);
                        expContent.Add("chsOutput2206", chsOutput2206TRY);
                        expContent.Add("chsInput2207", chsInput2207);
                        DataTable dtrev1 = new DataTable();
                        dtrev1.Columns.Add("CHSOUTPUT2206", typeof(string));
                        dtrev1.Columns.Add("CHSINPUT2207", typeof(string));
                        dtrev1.Columns.Add("EXPCONTENT", typeof(string));
                        dtrev1.Columns.Add("CLBZ", typeof(string));
                        dtrev1.Columns.Add("CLJG", typeof(string));
                        DataRow dr = dtrev1.NewRow();
                        dr["CHSOUTPUT2206"] = JSONSerializer.Serialize(rt2206.output);
                        dr["CHSINPUT2207"] = chsInput2207;
                        dr["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(expContent);
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dtrev1.Rows.Add(dr);
                        dtrev1.TableName = "BODY";
                        ds.Tables.Add(dtrev1.Copy());
                    }
                    catch (Exception ex)
                    {
                        DataTable dtrev = RtnResultDatatable("99", ex.ToString());
                        dtrev.TableName = "BODY";
                        ds.Tables.Add(dtrev.Copy());
                    }
                    return ds;

                    #endregion 2207

                    break;

                default:
                    foreach (var item in dic)
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", item.Key.ToString(), FormatHelper.GetStr(string.IsNullOrEmpty(item.Value) ? "" : item.Value));
                    }
                    break;
            }
            try
            {
                string rtnxml = CallService(doc.OuterXml, "");

                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataSet dsbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");

                if (!dsbody.Tables[0].Columns.Contains("EXPCONTENT"))
                {
                    dsbody.Tables[0].Columns.Add("EXPCONTENT", typeof(string));
                    dsbody.Tables[0].Columns.Add("RE_DJ", typeof(string));
                }
                switch (TYPE)
                {
                    case "GETCHSREGTRY":
                    case "GETCHSOUTPTRY":
                        string chsInput2201 = dsbody.Tables[0].Columns.Contains("chsInput2201") ? dsbody.Tables[0].Rows[0]["chsInput2201"].ToString() : "";
                        string chsInput2203 = dsbody.Tables[0].Columns.Contains("chsInput2203") ? dsbody.Tables[0].Rows[0]["chsInput2203"].ToString() : "";
                        string chsInput2204 = dsbody.Tables[0].Columns.Contains("chsInput2204") ? dsbody.Tables[0].Rows[0]["chsInput2204"].ToString() : "";
                        string chsInput2206 = dsbody.Tables[0].Columns.Contains("chsInput2206") ? dsbody.Tables[0].Rows[0]["chsInput2206"].ToString() : "";
                        string chsInput2207 = dsbody.Tables[0].Columns.Contains("chsInput2207") ? dsbody.Tables[0].Rows[0]["chsInput2207"].ToString() : "";
                        string YBDJH = dsbody.Tables[0].Columns.Contains("YBDJH") ? dsbody.Tables[0].Rows[0]["YBDJH"].ToString() : "";
                        //QHSiInterface.T2201. rt2201 = JSONSerializer.Deserialize<QHSiInterface.T2201.Root>(chsInput2201);
                        //dsbody.Tables[0].Rows[0]["chsInput2201"] = JSONSerializer.Serialize(rt2201.input);
                        Dictionary<string, string> dicEXP = new Dictionary<string, string>();
                        dicEXP.Add("chsInput2201", chsInput2201);
                        dicEXP.Add("chsInput2203", chsInput2203);
                        dicEXP.Add("chsInput2204", chsInput2204);
                        dicEXP.Add("chsInput2206", chsInput2206);
                        dicEXP.Add("chsInput2207", chsInput2206);
                        dicEXP.Add("YBDJH", YBDJH);
                        dsbody.Tables[0].Rows[0]["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(dicEXP);
                        dsbody.Tables[0].Rows[0]["RE_DJ"] = "1";
                        break;
                }
                return dsbody;
            }
            catch (Exception ex)
            {
                //DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("99", ex.Message);
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
        }

        public static DataSet GETPATBARCODE(string HOS_ID, string PAT_ID, ref string YNCARDNO)
        {
            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));
            dtresult.Columns.Add("YNCARDNO", typeof(string));
            try
            {
                Dictionary<string, string> Dictionary = getpatinfobypat_id(FormatHelper.GetInt(PAT_ID), HOS_ID);
                string PAT_NAME = Dictionary.ContainsKey("PAT_NAME") ? FormatHelper.GetStr(Dictionary["PAT_NAME"]) : "";
                string SFZ_NO = Dictionary.ContainsKey("SFZ_NO") ? FormatHelper.GetStr(Dictionary["SFZ_NO"]) : "";
                string YLCARD_TYPE = Dictionary.ContainsKey("YLCARTD_TYPE") ? FormatHelper.GetStr(Dictionary["YLCARTD_TYPE"]) : "";
                string YLCARD_NO = Dictionary.ContainsKey("YLCARD_NO") ? FormatHelper.GetStr(Dictionary["YLCARD_NO"]) : "";
                string SEX = Dictionary.ContainsKey("SEX") ? FormatHelper.GetStr(Dictionary["SEX"]) : "";
                string BIRTHDAY = Dictionary.ContainsKey("BIRTHDAY") ? FormatHelper.GetStr(Dictionary["BIRTHDAY"]) : "";
                string GUARDIAN_NAME = Dictionary.ContainsKey("GUARDIAN_NAME") ? FormatHelper.GetStr(Dictionary["GUARDIAN_NAME"]) : "";
                string MOBILE_NO = Dictionary.ContainsKey("MOBILE_NO") ? FormatHelper.GetStr(Dictionary["MOBILE_NO"]) : "";
                string ADDRESS = Dictionary.ContainsKey("ADDRESS") ? FormatHelper.GetStr(Dictionary["ADDRESS"]) : "";
                YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                if (YNCARDNO == "")
                {
                    DataRow newrow = dtresult.NewRow();
                    newrow["CLBZ"] = "1";
                    newrow["CLJG"] = "获取院内号失败";
                    dtresult.Rows.Add(newrow);
                }
                else
                {
                    DataRow newrow = dtresult.NewRow();
                    newrow["CLBZ"] = "0";
                    newrow["CLJG"] = "";
                    newrow["YNCARDNO"] = YNCARDNO;
                    dtresult.Rows.Add(newrow);
                }
            }
            catch (Exception ex)
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "2";
                newrow["CLJG"] = "执行失败，失败原因:" + ex.Message + "";
                dtresult.Rows.Add(newrow);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtresult);
            return ds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="COMM_MAIN">支付流水号</param>
        /// <param name="ZF_TYPE">支付方式</param>
        /// <param name="BIZ_TYPE">业务类型  1挂号  2门诊缴费  3住院</param>
        /// <returns></returns>
        public DataSet GETSTATUSBYORDERID(string COMM_MAIN, string ZF_TYPE, string BIZ_TYPE, string SOURCE)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETSTATUSBYORDERID", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "COMM_MAIN", COMM_MAIN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ZF_TYPE", ZF_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIZ_TYPE", BIZ_TYPE);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet CHECKMBCANPAY(string SFZ_NO, string MB_ID)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("CHECKMBCANPAY", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MB_ID", MB_ID);
            try
            {
                DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("0", "");
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public DataTable RtnResultDatatable(string CLBZ, string CLJG)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            DataRow newrow = dtrev.NewRow();
            newrow["CLBZ"] = CLBZ;
            newrow["CLJG"] = CLJG;
            dtrev.Rows.Add(newrow);
            return dtrev;
        }

        public DataSet YUNHOSPRESEXAMSAVE(string HSO_ID, string HOS_SN, string PAT_NAME, string SFZ_NO, string YLCARD_TYPE, string YLCARD_NO, string DEPT_CODE, string DOC_NO, string SF_DOC, string RECIPE_NO, string SF_STATUS, string SF_DESC, string CASign)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YUNHOSPRESEXAMSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_DOC", SF_DOC.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "RECIPE_NO", RECIPE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_STATUS", SF_STATUS.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_DESC", SF_DESC.Trim());

            try
            {
                string rtnxml = CallService(doc.OuterXml, "");
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取互联网医院病人处方信息
        /// </summary>
        /// <param name="HOS_SN"></param>
        /// <returns></returns>
        private DataSet GetPREPDF(string HOS_SN)
        {
            string SOURCE = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("SENDPRE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);

                try
                {
                    DataSet ds = new DataSet();

                    DataTable dtpatinfo = new DataTable();//病人信息表
                    DataTable dtmed = new DataTable();
                    DataTable dtsqd = new DataTable();
                    try
                    {
                        dtpatinfo = XMLHelper.X_GetXmlData(rtnxml, "ROOT/BODY").Tables[0];
                    }
                    catch
                    { }
                    try
                    {
                        DataTable dtPRDPDF = new DataTable();
                        DataTable dtDAMEDLIST = new DataTable();
                        DataTable dtDAMED = new DataTable();
                        DataTable dtSQDMXLIST = new DataTable();
                        DataTable dtSQDMX = new DataTable();
                        DataSet dsPREPDFLIST = XMLHelper.X_GetXmlData(rtnxml, "ROOT/BODY/PREPDFLIST");
                        if (dsPREPDFLIST.Tables.Contains("PREPDF"))
                        {
                            dtPRDPDF = dsPREPDFLIST.Tables["PREPDF"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("DAMEDLIST"))
                        {
                            dtDAMEDLIST = dsPREPDFLIST.Tables["DAMEDLIST"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("DAMED"))
                        {
                            dtDAMED = dsPREPDFLIST.Tables["DAMED"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("SQDMXLIST"))
                        {
                            dtSQDMXLIST = dsPREPDFLIST.Tables["SQDMXLIST"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("SQDMX"))
                        {
                            dtSQDMX = dsPREPDFLIST.Tables["SQDMX"];
                        }
                        if (dtPRDPDF.Rows.Count > 0 && dtDAMEDLIST.Rows.Count > 0 && dtDAMED.Rows.Count > 0)
                        {
                            var query = (from a in dtPRDPDF.AsEnumerable()
                                         join b in dtDAMEDLIST.AsEnumerable() on FormatHelper.GetInt(a["PREPDF_ID"]) equals FormatHelper.GetInt(b["PREPDF_ID"])
                                         join c in dtDAMED.AsEnumerable() on FormatHelper.GetInt(b["DAMEDLIST_ID"]) equals FormatHelper.GetInt(c["DAMEDLIST_ID"])
                                         select new
                                         {
                                             OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]),
                                             PRE_NO = FormatHelper.GetStr(a["PRE_NO"]),
                                             MED_ID = FormatHelper.GetStr(c["med_id"]),
                                             US_ID = FormatHelper.GetStr(c["US_ID"]),
                                             CAMT = FormatHelper.GetStr(c["CAMT"]),
                                             CAMTALL = FormatHelper.GetStr(c["CAMTALL"]),
                                             AUT_ID = FormatHelper.GetStr(c["AUT_ID"]),
                                             AUT_IDALL = FormatHelper.GetStr(c["AUT_IDALL"]),
                                             PC_ID = FormatHelper.GetStr(c["PC_ID"]),
                                             GROUP_ID = FormatHelper.GetStr(c["GROUP_ID"]),
                                             DAYS = FormatHelper.GetStr(c["DAYS"]),
                                             MED_COST = FormatHelper.GetStr(c["MED_COST"]),
                                             MED_JE_ALL = FormatHelper.GetStr(c["MED_JE_ALL"]),
                                         });
                            dtmed = Projecttable.CopyToDataTable(query);
                        }
                        if (dtpatinfo.Rows.Count > 0 && dtSQDMXLIST.Rows.Count > 0 && dtSQDMX.Rows.Count > 0)
                        {
                            var query = (from a in dtPRDPDF.AsEnumerable()
                                         join b in dtSQDMXLIST.AsEnumerable() on FormatHelper.GetInt(a["PREPDF_ID"]) equals FormatHelper.GetInt(b["PREPDF_ID"])
                                         join c in dtSQDMX.AsEnumerable() on FormatHelper.GetInt(b["SQDMXLIST_ID"]) equals FormatHelper.GetInt(c["SQDMXLIST_ID"])
                                         select new
                                         {
                                             OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]),
                                             PRE_NO = FormatHelper.GetStr(a["PRE_NO"]),
                                             SQD_ID = FormatHelper.GetStr(c["SQD_ID"]),
                                             SQD_NAME = FormatHelper.GetStr(c["SQD_NAME"]),
                                             CAMT = FormatHelper.GetStr(c["CAMT"]),
                                             ZS = FormatHelper.GetStr(c["ZS"]),
                                             XBS = "",
                                             JWS = FormatHelper.GetStr(c["JWS"]),
                                             JCBW = FormatHelper.GetStr(c["JCBW"]),
                                             JCYQ = FormatHelper.GetStr(c["JCYQ"]),
                                             JCDD = FormatHelper.GetStr(c["JCDD"]),
                                             JCZD = ""
                                         });
                            dtsqd = Projecttable.CopyToDataTable(query);
                        }
                    }
                    catch (Exception ex)
                    { }
                    if (dtpatinfo != null && dtpatinfo.Rows.Count > 0)
                    {
                        dtpatinfo.TableName = "patinfo";
                        ds.Tables.Add(dtpatinfo.Copy());
                    }
                    if (dtmed != null && dtmed.Rows.Count > 0)
                    {
                        dtmed.TableName = "med";
                        ds.Tables.Add(dtmed);
                    }
                    if (dtsqd != null && dtsqd.Rows.Count > 0)
                    {
                        dtsqd.TableName = "sqd";
                        ds.Tables.Add(dtsqd);
                    }
                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool SAVEHOSPREINFO()
        {
            try
            {
                string injson = "";
                string SendYunWmDetailURL = "";
                XmlDocument xmlconfigurationdoc = new XmlDocument();
                xmlconfigurationdoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\bin\OnlineBusHos185.dll.config");
                DataSet dsconfiguration = XMLHelper.X_GetXmlData(xmlconfigurationdoc, "configuration/appSettings");//请求的数据包
                if (dsconfiguration != null && dsconfiguration.Tables[0].Rows.Count >= 2 && FormatHelper.GetStr(dsconfiguration.Tables[0].Rows[1]["value"]) != "")//互联网医院不连接HIS
                {
                    SendYunWmDetailURL = FormatHelper.GetStr(dsconfiguration.Tables[0].Rows[1]["value"]);
                }
                else
                {
                    SendYunWmDetailURL = "https://fbo.ztejsapp.cn/SLB/MySpringAES/PBusyunhosPre";
                }

                MySqlParameter[] sqlParameter = new MySqlParameter[] { new MySqlParameter("@HOS_ID", HOS_ID), new MySqlParameter("@SCH_DATE", DateTime.Now.ToString("yyyy-MM-dd")) };
                string sqlcmd = string.Format(@" select b.HOS_SN,c.sfz_secret,a.HOS_SN as DJ_ID,a.pat_name,a.opt_sn
                from platform.register_pay a,platform.register_appt b,platform.pat_info c
                 where a.reg_id=b.reg_id and a.HOS_ID=@HOS_ID and a.gh_type='4' and is_th=0 and a.pat_id=c.pat_id
                and b.sch_date=@SCH_DATE");
                DataTable dtpat = Soft.DBUtility.DbHelperMySQL.Query(sqlcmd, sqlParameter).Tables[0];
                foreach (DataRow drpat in dtpat.Rows)
                {
                    string HOS_SN = FormatHelper.GetStr(drpat["HOS_SN"]);
                    String SFZ_NO = PlatDataSecret.DataSecret.DeSfzNoSecretByAes(FormatHelper.GetStr(drpat["sfz_secret"]));
                    string PAT_NAME = FormatHelper.GetStr(drpat["pat_name"]);
                    string DJ_ID = FormatHelper.GetStr(drpat["DJ_ID"]);
                    string OPT_SN = FormatHelper.GetStr(drpat["OPT_SN"]);
                    //string DJ_ID = "3294101";
                    //string HOS_SN = "228712";
                    //string PAT_NAME = "潘银磊";
                    //string OPT_SN = "1000750712";
                    DataSet ds = GetPREPDF(DJ_ID);
                    if (ds != null)
                    {
                        if (!ds.Tables.Contains("patinfo") || (ds.Tables["patinfo"] == null || ds.Tables["patinfo"].Rows.Count == 0))
                        {
                            return false;
                        }
                        DataRow drpatinfo = ds.Tables["patinfo"].Rows[0];
                        if (drpatinfo != null && (ds.Tables.Contains("med") || ds.Tables.Contains("sqd")))
                        {
                            string DIS_ID = FormatHelper.GetStr(drpatinfo["DIS_ID"]);
                            string DIS_NAME = FormatHelper.GetStr(drpatinfo["DIS_NAME"]);
                            string DEPT_CODE = FormatHelper.GetStr(drpatinfo["DEPT_CODE"]);
                            string DEPT_NAME = FormatHelper.GetStr(drpatinfo["DEPT_NAME"]);
                            string DOC_NO = FormatHelper.GetStr(drpatinfo["DOC_NO"]);
                            string DOC_NAME = FormatHelper.GetStr(drpatinfo["DOC_NAME"]);
                            string JEALL = FormatHelper.GetStr(drpatinfo["DOC_NAME"]);

                            if (ds.Tables.Contains("med"))
                            {
                                DataTable dtmed = ds.Tables["med"];
                                var query = (from a in dtmed.AsEnumerable() select new { PRE_NO = FormatHelper.GetStr(a["PRE_NO"]), OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]) }).Distinct();
                                DataTable dtpre = Projecttable.CopyToDataTable(query);
                                if (dtpre != null && dtpre.Rows.Count > 0)
                                {
                                    foreach (DataRow drpre in dtpre.Rows)
                                    {
                                        string PRE_NO = FormatHelper.GetStr(drpre["PRE_NO"]);
                                        string OPERA_TYPE = FormatHelper.GetStr(drpre["OPERA_TYPE"]);
                                        if (OPERA_TYPE == "1")
                                        {
                                            //新增处方
                                            var queryMED = (from a in dtmed.AsEnumerable()
                                                            where FormatHelper.GetStr(a["PRE_NO"]) == PRE_NO
                                                            select new
                                                            {
                                                                med_id = FormatHelper.GetStr(a["MED_ID"]),
                                                                aut_id = FormatHelper.GetStr(a["aut_id"]),
                                                                us_id = FormatHelper.GetStr(a["US_ID"]),
                                                                PC_ID = FormatHelper.GetStr(a["PC_ID"]),
                                                                AUT_IDALL = FormatHelper.GetStr(a["AUT_IDALL"]),
                                                                camt = FormatHelper.GetStr(a["CAMT"]),
                                                                CAMTALL = FormatHelper.GetStr(a["CAMTALL"]),
                                                            });

                                            DataTable dtdamzmxmed = Projecttable.CopyToDataTable(queryMED);
                                            MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@HOS_ID", HOS_ID), new MySqlParameter("@PRE_NO", PRE_NO) };
                                            sqlcmd = string.Format(@"SELECT a.PRE_NO,b.MED_ID,b.AUT_ID,b.US_ID,b.PC_ID,b.AUT_IDALL,b.AUT_NUM,b.AUT_NUMALL
FROM yunhou.damznopay a left outer join yunhou.damznopaymed b on a.DJ_ID=b.DJ_ID
 where a.HOS_ID=@HOS_ID and a.PRE_NO=@PRE_NO", HOS_ID, PRE_NO);

                                            DataTable dtdamznopay = DbHelperMySQL.Query(sqlcmd, spa).Tables[0];
                                            if (dtdamznopay != null && dtdamznopay.Rows.Count > 0)
                                            {
                                                var queryexists = (from a in dtdamzmxmed.AsEnumerable()
                                                                   join b in dtdamznopay.AsEnumerable() on new
                                                                   {
                                                                       med_id = FormatHelper.GetStr(a["MED_ID"]),
                                                                       aut_id = FormatHelper.GetStr(a["aut_id"]),
                                                                       us_id = FormatHelper.GetStr(a["US_ID"]),
                                                                       PC_ID = FormatHelper.GetStr(a["PC_ID"]),
                                                                       AUT_IDALL = FormatHelper.GetStr(a["AUT_IDALL"]),
                                                                       camt = FormatHelper.GetStr(a["CAMT"]),
                                                                       CAMTALL = FormatHelper.GetStr(a["CAMTALL"])
                                                                   } equals new
                                                                   {
                                                                       med_id = FormatHelper.GetStr(b["MED_ID"]),
                                                                       aut_id = FormatHelper.GetStr(b["aut_id"]),
                                                                       us_id = FormatHelper.GetStr(b["US_ID"]),
                                                                       PC_ID = FormatHelper.GetStr(b["PC_ID"]),
                                                                       AUT_IDALL = FormatHelper.GetStr(b["AUT_IDALL"]),
                                                                       camt = FormatHelper.GetStr(b["AUT_NUM"]),
                                                                       CAMTALL = FormatHelper.GetStr(b["AUT_NUMALL"])
                                                                   } into NewSalaries
                                                                   from c in NewSalaries.DefaultIfEmpty()
                                                                   where c is null
                                                                   select FormatHelper.GetStr(a["med_id"])
                                                                   ).Union(from a in dtdamznopay.AsEnumerable()
                                                                           join b in dtdamzmxmed.AsEnumerable() on new
                                                                           {
                                                                               med_id = FormatHelper.GetStr(a["MED_ID"]),
                                                                               aut_id = FormatHelper.GetStr(a["aut_id"]),
                                                                               us_id = FormatHelper.GetStr(a["US_ID"]),
                                                                               PC_ID = FormatHelper.GetStr(a["PC_ID"]),
                                                                               AUT_IDALL = FormatHelper.GetStr(a["AUT_IDALL"]),
                                                                               camt = FormatHelper.GetStr(a["AUT_NUM"]),
                                                                               CAMTALL = FormatHelper.GetStr(a["AUT_NUMALL"])
                                                                           } equals new
                                                                           {
                                                                               med_id = FormatHelper.GetStr(b["MED_ID"]),
                                                                               aut_id = FormatHelper.GetStr(b["aut_id"]),
                                                                               us_id = FormatHelper.GetStr(b["US_ID"]),
                                                                               PC_ID = FormatHelper.GetStr(b["PC_ID"]),
                                                                               AUT_IDALL = FormatHelper.GetStr(b["AUT_IDALL"]),
                                                                               camt = FormatHelper.GetStr(b["CAMT"]),
                                                                               CAMTALL = FormatHelper.GetStr(b["CAMTALL"])
                                                                           } into NewSalaries
                                                                           from c in NewSalaries.DefaultIfEmpty()
                                                                           where c is null
                                                                           select FormatHelper.GetStr(a["med_id"]));

                                                if (queryexists.Count() == 0)
                                                {
                                                    continue;
                                                }
                                            }

                                            HEADER header = new HEADER();
                                            header.TYPE = "SAVEHOSPREINFO";
                                            header.MODULE = "BusinessWCApp";
                                            header.source = "H001S40";
                                            ROOT root = new ROOT();

                                            SAVEHOSPREINFOBODY BODY = new SAVEHOSPREINFOBODY();
                                            DAMEDLIST MEDLIST = new DAMEDLIST();
                                            MEDLIST.MED_JEALL = dtmed.AsEnumerable().Where(x => FormatHelper.GetStr(x["PRE_NO"]) == PRE_NO).Select(x => FormatHelper.GetDecimal(x["MED_JE_ALL"])).Sum();
                                            MEDLIST.DAMED = dtdamzmxmed;
                                            BODY.DOC_NO = DOC_NO;
                                            BODY.BQ = "";
                                            BODY.DIS_ID = DIS_ID;
                                            BODY.DIS_NAME = DIS_NAME;
                                            BODY.HOS_ID = HOS_ID;
                                            BODY.HOS_SN = HOS_SN;
                                            BODY.PAT_NAME = PAT_NAME;
                                            BODY.PRE_NO = PRE_NO;
                                            BODY.OPT_SN = OPT_SN;
                                            BODY.DAMEDLIST = MEDLIST;

                                            // BODY.SQD_LIST = FormatHelper.GetStr(drdamz["DOC_NO"]);

                                            root.HEADER = header;
                                            root.BODY = BODY;
                                            Root Root = new Root();
                                            Root.ROOT = root;
                                            injson = JsonHelper.SerializeObject(Root);
                                        }
                                        else if (OPERA_TYPE == "2")
                                        {
                                            //删除处方
                                            HEADER header = new HEADER();
                                            header.TYPE = "OFFLINEPREUNDO";
                                            header.MODULE = "BusinessWCApp";
                                            header.source = "H001S185";
                                            ROOT root = new ROOT();

                                            OFFLINEPREUNDO BODY = new OFFLINEPREUNDO();

                                            BODY.HOS_ID = HOS_ID;
                                            BODY.HOS_SN = HOS_SN;
                                            BODY.PAT_NAME = PAT_NAME;
                                            BODY.PRE_NO = PRE_NO;
                                            // BODY.SQD_LIST = FormatHelper.GetStr(drdamz["DOC_NO"]);

                                            root.HEADER = header;
                                            root.BODY = BODY;
                                            Root Root = new Root();
                                            Root.ROOT = root;
                                            injson = JsonHelper.SerializeObject(Root);
                                        }
                                        if (SAVEHOSPRE.SLBSendToPlatform(injson, SendYunWmDetailURL))
                                        {
                                            if (OPERA_TYPE == "1")
                                            {
                                                //foreach (string mobile_no in new string[] { "13776650482", "13813003097" })
                                                //{
                                                //    SENDMSG("请您及时审核南医大二附院互联网医院处方。", mobile_no, "");
                                                //}
                                                string strinsert = string.Format(@" INSERT INTO waitsfpreno(HOS_ID,PRE_NO,saved_datetime) values(@hos_id,@pre_no,@saved_datetime)
                                                                             ON DUPLICATE KEY UPDATE saved_datetime=@saved_datetime");
                                                MySqlParameter[] parameters = {new MySqlParameter("@HOS_ID",HOS_ID),
                                            new MySqlParameter("@pre_no",PRE_NO),
                                            new MySqlParameter("@saved_datetime",DateTime.Now)};

                                                DbHelperMySQL.ExecuteSql(strinsert, parameters);
                                            }
                                            else if (OPERA_TYPE == "2")
                                            {
                                                string strinsert = string.Format(@" delete from  waitsfpreno where HOS_ID=@hos_id and PRE_NO=@pre_no");
                                                MySqlParameter[] parameters = {new MySqlParameter("@HOS_ID",HOS_ID),
                                            new MySqlParameter("@pre_no",PRE_NO) };
                                                DbHelperMySQL.ExecuteSql(strinsert, parameters);
                                            }
                                        }
                                    }
                                }
                            }
                            if (ds.Tables.Contains("sqd"))
                            {
                                DataTable dtsqd = ds.Tables["sqd"];
                                var query = (from a in dtsqd.AsEnumerable() select new { PRE_NO = FormatHelper.GetStr(a["PRE_NO"]), OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]) }).Distinct();
                                DataTable dtpre = Projecttable.CopyToDataTable(query);
                                foreach (DataRow dr in dtpre.Rows)
                                {
                                    System.Collections.Hashtable table = new System.Collections.Hashtable();
                                    String strSql = "";
                                    string PRE_NO = FormatHelper.GetStr(dr["PRE_NO"]);
                                    int OPERA_TYPE = FormatHelper.GetInt(dr["OPERA_TYPE"]);
                                    if (OPERA_TYPE == 1)
                                    {
                                        var queryMED = (from a in dtsqd.AsEnumerable()
                                                        where FormatHelper.GetStr(a["PRE_NO"]) == PRE_NO
                                                        group a by FormatHelper.GetStr(a["SQD_ID"]) into temp
                                                        select new
                                                        {
                                                            SQD_ID = FormatHelper.GetStr(temp.Key),
                                                            SQD_NAME = FormatHelper.GetStr(temp.FirstOrDefault()["SQD_NAME"]),
                                                            CAMT = temp.Sum(x => FormatHelper.GetDecimal(x["CAMT"])),
                                                            ZS = FormatHelper.GetStr(temp.FirstOrDefault()["ZS"]),
                                                            JCBW = FormatHelper.GetStr(temp.FirstOrDefault()["JCBW"]),
                                                            DIS_NAME = FormatHelper.GetStr(temp.FirstOrDefault()["JCZD"]),
                                                            AUT_NAME = "次"
                                                        }); ;
                                        DataTable dtsqdmx = Projecttable.CopyToDataTable(queryMED);
                                        if (dtsqdmx != null && dtsqdmx.Rows.Count > 0)
                                        {
                                            string BSZY = FormatHelper.GetStr(dtsqdmx.Rows[0]["ZS"]);
                                            string JCBW = FormatHelper.GetStr(dtsqdmx.Rows[0]["JCBW"]);
                                            string DISNAME = FormatHelper.GetStr(dtsqdmx.Rows[0]["DIS_NAME"]);
                                            strSql = string.Format(@" delete from yunhou.yunhossqdmx where HOS_ID=@HOS_ID  AND PRE_NO=@PRE_NO;");
                                            MySqlParameter[] spadelete = new MySqlParameter[] {new MySqlParameter("@HOS_ID",HOS_ID),
                                        new MySqlParameter("@PRE_NO",PRE_NO)};
                                            table.Add(strSql, spadelete);

                                            strSql = string.Format(@"INSERT INTO yunhou.yunhossqd(HOS_ID,HOS_SN,PRE_NO,BSZY,JCBW,DEPT_NAME,DOC_NAME,SAVE_DATETIME,DIS_NAME)
VALUES(@HOS_ID,@HOS_SN,@PRE_NO,@BSZY,@JCBW,@DEPT_NAME,@DOC_NAME,@SAVE_DATETIME,@DIS_NAME)
ON DUPLICATE KEY UPDATE DEPT_NAME=@DEPT_NAME,DOC_NAME=@DOC_NAME,DIS_NAME=@DIS_NAME;");
                                            MySqlParameter[] spa = new MySqlParameter[] {new MySqlParameter("@HOS_ID",HOS_ID), new MySqlParameter("@HOS_SN", DJ_ID),
                                        new MySqlParameter("@PRE_NO",PRE_NO),new MySqlParameter("@BSZY",BSZY),new MySqlParameter("@JCBW",JCBW),new MySqlParameter("@DEPT_NAME",DEPT_NAME)
                                        ,new MySqlParameter("@DOC_NAME",DOC_NAME),new MySqlParameter("@SAVE_DATETIME",DateTime.Now),new MySqlParameter("@DIS_NAME",DISNAME)};
                                            table.Add(strSql, spa);
                                            int i = 1;
                                            foreach (DataRow drsqdmx in dtsqdmx.Rows)
                                            {
                                                string SQD_NAME = FormatHelper.GetStr(drsqdmx["SQD_NAME"]);
                                                string SQD_ID = FormatHelper.GetStr(drsqdmx["SQD_ID"]);
                                                decimal CAMT = FormatHelper.GetDecimal(drsqdmx["CAMT"]);
                                                string AUT_NAME = FormatHelper.GetStr(drsqdmx["AUT_NAME"]);
                                                strSql = string.Format(@"select " + i.ToString() + ";" +
                                                    "insert into yunhou.yunhossqdmx(HOS_ID,PRE_NO,SQD_ID,SQD_NAME,CAMT,AUT_NAME) VALUES(@HOS_ID,@PRE_NO,@SQD_ID,@SQD_NAME,@CAMT,@AUT_NAME)" +
                                                    "ON DUPLICATE KEY UPDATE SQD_NAME=@SQD_NAME,CAMT=@CAMT");
                                                MySqlParameter[] spasqdmx = new MySqlParameter[] {new MySqlParameter("@HOS_ID",HOS_ID),
                                        new MySqlParameter("@PRE_NO",PRE_NO),new MySqlParameter("@SQD_ID",SQD_ID),new MySqlParameter("@SQD_NAME",SQD_NAME),new MySqlParameter("@CAMT",CAMT)
                                        ,new MySqlParameter("@AUT_NAME",AUT_NAME)};
                                                table.Add(strSql, spasqdmx);
                                                i++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strSql = string.Format(@" delete from yunhou.yunhossqdmx where HOS_ID=@HOS_ID  AND PRE_NO=@PRE_NO;
delete from yunhou.yunhossqd where HOS_ID=@HOS_ID  AND PRE_NO=@PRE_NO;");
                                        MySqlParameter[] spa = new MySqlParameter[] {new MySqlParameter("@HOS_ID",HOS_ID),
                                        new MySqlParameter("@PRE_NO",PRE_NO)};
                                        table.Add(strSql, spa);
                                    }
                                    if (table.Count > 0)
                                    {
                                        DbHelperMySQL.ExecuteSqlTran(table);
                                    }
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private DataTable SendMessage(string mobile, string message)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            try
            {
                string poststr = string.Format("mobiles={0}&message={1}&sysid=MA5URL", mobile, message);
                HttpClient httpclient = new HttpClient("http://hbi.tkxlglyy.com:6180/sms/home/sms/sendMessage");
                string result = "";
                int status = httpclient.Send(poststr, Encoding.UTF8, out result);
                if (status == 200)
                {
                    MessageResult messageResult = JsonHelper.DeserializeJsonToObject<MessageResult>(result);
                    if (messageResult != null)
                    {
                        if (messageResult.success == "true")
                        {
                            DataRow newrow = dtrev.NewRow();
                            newrow["CLBZ"] = "0";
                            newrow["CLJG"] = "发送成功";
                            dtrev.Rows.Add(newrow);
                            return dtrev;
                        }
                        else
                        {
                            DataRow newrow = dtrev.NewRow();
                            newrow["CLBZ"] = "9";
                            newrow["CLJG"] = FormatHelper.GetStr(messageResult.desc);
                            dtrev.Rows.Add(newrow);
                            return dtrev;
                        }
                    }
                }
                DataRow newrow1 = dtrev.NewRow();
                newrow1["CLBZ"] = "9";
                newrow1["CLJG"] = "发送失败";
                dtrev.Rows.Add(newrow1);
                return dtrev;
            }
            catch (Exception ex)
            {
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "9";
                newrow["CLJG"] = ex.Message;
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
        }
    }

    public class MessageResult
    {
        /// <summary>
        ///
        /// </summary>
        public string success { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int rtn { get; set; }

        public string desc { get; set; }
    }
}