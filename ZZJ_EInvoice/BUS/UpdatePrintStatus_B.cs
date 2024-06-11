﻿using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ZZJ_EInvoice.Class;

namespace ZZJ_EInvoice.BUS
{
    internal class UpdatePrintStatus_B
    {
        public static string B_UpdatePrintStatus(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_EInvoice", "0003").BusData;
                return out_data;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
        EndPoint:
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_UpdatePrintStatus_b(string json_in)
        {
            UpdatePrintStatus_IN _in = JsonConvert.DeserializeObject<UpdatePrintStatus_IN>(json_in);
            DataReturn dataReturn = new DataReturn();
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            string json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}