using CommonModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Data;
using OnlineBusHos185_Tran.Model;
using Newtonsoft.Json;

namespace OnlineBusHos185_Tran.BUS
{
    class GETPASSIVEPAY
    {
        public static string B_GETPASSIVEPAY(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETPASSIVEPAY_M.GETPASSIVEPAY_IN _in = JsonConvert.DeserializeObject<GETPASSIVEPAY_M.GETPASSIVEPAY_IN>(json_in);
                GETPASSIVEPAY_M.GETPASSIVEPAY_OUT _out = new GETPASSIVEPAY_M.GETPASSIVEPAY_OUT();

                string his_rtnxml = "";


            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
        EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
