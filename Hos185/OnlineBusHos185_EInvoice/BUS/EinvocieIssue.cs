using CommonModel;
using Hos185_His.Models.Einvoice;
using Newtonsoft.Json;
using OnlineBusHos185_EInvoice.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos185_EInvoice.BUS
{
    internal class EinvocieIssue
    {
        public static string B_EinvocieIssue(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                Model.EinvoiceIssue_IN _in = JsonConvert.DeserializeObject<Model.EinvoiceIssue_IN>(json_in);

                List<EInvoiceXLMakeOutWinnex> queries = new List<EInvoiceXLMakeOutWinnex>();

                List<EinvoiceIssueInfo> list = JsonConvert.DeserializeObject<List<EinvoiceIssueInfo>>(_in.ISSUELIST);

                foreach (var item in list)
                {
                    EInvoiceXLMakeOutWinnex MakeOut = new Hos185_His.Models.Einvoice.EInvoiceXLMakeOutWinnex()
                    {
                        bizType = item.BUSINESS_TYPE,
                        inpatientNo = item.INPATIENTNO,
                        invoiceNo = item.INVOICE_CODE,
                        serialNo = item.INVOICE_NUMBER,
                        settleCode = item.SETTLECODE,
                        transType = "1",
                        invoiceSource= item.invoiceSource
                    };

                    queries.Add(MakeOut);
                }

                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(queries);

                Hos185_His.Models.Output<List<EInvoiceDirectQueryData>> output
          = GlobalVar.CallAPI<List<EInvoiceDirectQueryData>>("/smartmedical/eInvoiceNN/EInvoiceXLMakeOutWinnex", jsonstr);
                dataReturn.Code = output.code;
                dataReturn.Msg = output.message;
                if (output.code != 0)
                {
                    return JsonConvert.SerializeObject(dataReturn);
                }

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
              
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