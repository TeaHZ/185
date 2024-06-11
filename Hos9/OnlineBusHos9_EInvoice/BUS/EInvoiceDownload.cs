using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_EInvoice.Class;
using OnlineBusHos9_EInvoice.Model.qheinvoice;
using System;
using System.Collections;

namespace OnlineBusHos9_EInvoice.BUS
{
    internal class EInvoiceDownload
    {
        public static string B_EInvoiceDownload(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                EInvoiceDownload_IN _in = JsonConvert.DeserializeObject<EInvoiceDownload_IN>(json_in);
                EInvoiceDownload_OUT _out = new EInvoiceDownload_OUT();

                Model.qheinvoice.EInvoiceDownload download = new Model.qheinvoice.EInvoiceDownload()
                {
                    HOS_ID = _in.HOS_ID,
                    app_id = "32028213810201",
                    invoice_code = _in.INVOICE_CODE,
                    invoice_number = _in.INVOICE_NUMBER,
                };

                Model.qheinvoice.RootRequest<Model.qheinvoice.EInvoiceDownload> root = new RootRequest<Model.qheinvoice.EInvoiceDownload>("INVOICEDOWNLOAD");

                root.BODY = download;

                string url = "http://192.168.17.15:8000/Service.asmx";

                Hashtable hs = new Hashtable();
                hs.Add("jsonValue", JsonConvert.SerializeObject(root));

                string outjson = WebServiceHelper.QuerySoapWebService(url, "BusinessElectInvoice", hs).InnerText;

                Model.qheinvoice.RootReponse<Model.qheinvoice.EInvoiceDownloaddata> reponse = JsonConvert.DeserializeObject<Model.qheinvoice.RootReponse<Model.qheinvoice.EInvoiceDownloaddata>>(outjson);

                _out.DATA_TYPE = "1_1";

                _out.INVOICE_URL = reponse.ROOT.BODY.invoice_url;
                _out.INVOICEFILEDATA = reponse.ROOT.BODY.invoicefiledata;
                _out.INVENTORYFILEDATA = reponse.ROOT.BODY.inventoryfiledata;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}