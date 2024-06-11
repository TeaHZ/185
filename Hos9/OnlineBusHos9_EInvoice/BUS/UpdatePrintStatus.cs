using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_EInvoice.Class;
using OnlineBusHos9_EInvoice.Model.qheinvoice;
using System;
using System.Collections;

namespace OnlineBusHos9_EInvoice.BUS
{
    internal class UpdatePrintStatus
    {
        public static string B_UpdatePrintStatus(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                UpdatePrintStatus_IN _in = JsonConvert.DeserializeObject<UpdatePrintStatus_IN>(json_in);


                Model.qheinvoice.UPDATEPRINTSTATUS download = new Model.qheinvoice.UPDATEPRINTSTATUS()
                {
                    HOS_ID = _in.HOS_ID,
                    invoice_code = _in.INVOICE_CODE,
                    invoice_number = _in.INVOICE_NUMBER,
                };

                Model.qheinvoice.RootRequest<Model.qheinvoice.UPDATEPRINTSTATUS> root = new RootRequest<Model.qheinvoice.UPDATEPRINTSTATUS>("UPDATEPRINTSTATUS");

                root.BODY = download;
                string url = "http://192.168.17.15:8000/Service.asmx";

                Hashtable hs = new Hashtable();
                hs.Add("jsonValue", JsonConvert.SerializeObject(root));

                string outjson = WebServiceHelper.QuerySoapWebService(url, "BusinessElectInvoice", hs).InnerText;

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }

            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}