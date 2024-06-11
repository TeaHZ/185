using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ZZJ_EInvoice.Model;

namespace ZZJ_EInvoice.BUS
{
    internal class GetHisIssueBySfzno_B
    {
        public static string B_GetHisIssueBySfzno(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json_in);
                if (!dic.ContainsKey("HOS_ID") || FormatHelper.GetStr(dic["HOS_ID"]) == "")
                {
                    dataReturn.Code = ConstData.CodeDefine.Parameter_Define_Out;
                    dataReturn.Msg = "HOS_ID为必传且不能为空";
                    goto EndPoint;
                }
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_EInvoice", "0001").BusData;
                return out_data;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
        EndPoint:
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_GetHisIssueBySfzno_b(string json_in)
        {
            GetHisIssueBySfzno_IN _in = JsonConvert.DeserializeObject<GetHisIssueBySfzno_IN>(json_in);
            GetHisIssueBySfzno_OUT _out = new GetHisIssueBySfzno_OUT();
            DataReturn dataReturn = new DataReturn();
            if (_in.BARCODE == "1")
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "无明细";
            }
            else
            {
                _out.HISISSUELISTS = new List<GetHisIssueBySfzno_OUT.Hisissuelist>();
                GetHisIssueBySfzno_OUT.Hisissuelist list = new GetHisIssueBySfzno_OUT.Hisissuelist();
                list.INVOICE_CODE = "32060121";
                list.INVOICE_NUMBER = "0212521549";
                list.INVOICING_PARTY_NAME = "南京市江宁区湖熟街道社区卫生服务中心";
                list.PAYER_PARTY_NAME = "金淑暧";
                list.TOTAL_AMOUNT = "67.00";
                list.SFTYPENAME = "门诊";
                list.STATUS = "已红冲";
                list.SAVEDDATE_TIME = "2021-07-09 17:21";
                list.ISPRINT = "0";
                _out.HISISSUELISTS.Add(list);

                list = new GetHisIssueBySfzno_OUT.Hisissuelist();
                list.INVOICE_CODE = "32060121";
                list.INVOICE_NUMBER = "0212521548";
                list.INVOICING_PARTY_NAME = "南京市江宁区湖熟街道社区卫生服务中心";
                list.PAYER_PARTY_NAME = "金淑暧";
                list.TOTAL_AMOUNT = "10.00";
                list.SFTYPENAME = "门诊";
                list.STATUS = "";
                list.SAVEDDATE_TIME = "2021-07-09 17:06";
                list.ISPRINT = "0";
                _out.HISISSUELISTS.Add(list);

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param =   JsonConvert.SerializeObject(_out);
            }
            string json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}