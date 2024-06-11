using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_OutHos.HISModels;
using SqlSugar;
using static OnlineBusHos9_OutHos.Model.SAVESELFBILLING_M;

namespace OnlineBusHos9_OutHos.BUS
{

    internal class SAVESELFBILLING
    {
        public static string B_SAVESELFBILLING(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.SAVESELFBILLING_M.SAVESELFBILLING_IN _in = JsonConvert.DeserializeObject<Model.SAVESELFBILLING_M.SAVESELFBILLING_IN>(json_in);
            Model.SAVESELFBILLING_M.SAVESELFBILLING_OUT _out = new Model.SAVESELFBILLING_M.SAVESELFBILLING_OUT();

            string billingData = _in.BILLING_DATA;

            string[] billingArray = billingData.Split('|');

            if (_in.BUS_TYPE == "0") //开单撤销
            {
                string[] orderLists = JsonConvert.DeserializeObject<string[]>(_in.ORDERLISTS);
                HISModels.T6004.Input input6004 = new HISModels.T6004.Input()
                {
                    orderlds = orderLists
                };
                PushServiceResult<List<T6004.Outdata>> result6004 = HerenHelper<List<T6004.Outdata>>.pushService("6004-QHZZJ", JsonConvert.SerializeObject(input6004));

                if (result6004.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = "交易失败，请去人工窗口办理业务"+result6004.msg;
                    return JsonConvert.SerializeObject(dataReturn);
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "success";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
                json_out = JsonConvert.SerializeObject(dataReturn);
                return json_out;

            }
            else//开单保存  his返回的json有点
            {
                HISModels.T6002.Input input = new HISModels.T6002.Input()
                {
                    operatorId = _in.LTERMINAL_SN,
                    patientId = _in.HOSPATID,
                    itemCodes = billingArray,
                    hdIndicator = _in.HDINDICATOR=="1" ? "1" : ""//血透项目传 1 
                };
                DateTime itime = DateTime.Now;
                Hashtable hs = new Hashtable
                {
                    { "DataType", "JSON" },
                    { "TradeCode", "6002-QHZZJ"},
                    { "pInput", JsonConvert.SerializeObject(input) }
                };
                string HerenHIS = ConfigurationManager.AppSettings["HerenHIS"];
                string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "pushService", hs).InnerText;
                //string rtnstr = "{\"code\":1,\"data\":{\"examItemCodeMapList\":{\"2208081358368114\":2023102500048653},\"labItemCodeMapList\":{},\"treatItemCodeMapList\":{},\"visitNo\":20231025002601},\"msg\":\"成功\"}";
                T6002.Outdata.Response result6002 = JsonConvert.DeserializeObject<T6002.Outdata.Response>(rtnstr);

                DateTime otime = DateTime.Now;
                WriteLogdb("6002-QHZZJ", input.ToString(), itime, rtnstr, otime);

                if (result6002.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result6002.msg;
                    return JsonConvert.SerializeObject(dataReturn);
                }

                Dictionary<string, long> labItemCodeMapList = result6002.data.labItemCodeMapList;
                Dictionary<string, long> treatItemCodeMapList = result6002.data.treatItemCodeMapList;
                Dictionary<string, long> examItemCodeMapList = result6002.data.examItemCodeMapList;

                List<long> ORDERLIST = new List<long>();

                foreach (var item in labItemCodeMapList)
                {
                    ORDERLIST.Add(item.Value);
                }
                foreach (var item in treatItemCodeMapList)
                {
                    ORDERLIST.Add(item.Value);
                }
                foreach (var item in examItemCodeMapList)
                {
                    ORDERLIST.Add(item.Value);
                }
                _out.ORDERLISTS = ORDERLIST;

                dataReturn.Code = 0;
                dataReturn.Msg = "success";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
                json_out = JsonConvert.SerializeObject(dataReturn);
                return json_out;
            }
        }
        private static void WriteLogdb(string bustype, string input, DateTime intime, string output, DateTime outtime)
        {
            #region 日志

            SqlSugarModel.Loghos loghos = new SqlSugarModel.Loghos()
            {
                HOS_ID = "9",
                UID = Guid.NewGuid().ToString(),//todo:ddd
                InTime = intime,
                OutTime = outtime,
                InXml = input,
                OutXml = output,
                TYPE = bustype

            };
            LogHelper.SaveLogHos(loghos);

            #endregion 日志
        }
    }
}
