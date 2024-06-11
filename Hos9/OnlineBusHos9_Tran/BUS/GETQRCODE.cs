using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;

using System;

namespace OnlineBusHos9_Tran.BUS
{
    class GETQRCODE
    {
        public static string B_GETQRCODE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETQRCODE_M.GETQRCODE_IN _in = JsonConvert.DeserializeObject<GETQRCODE_M.GETQRCODE_IN>(json_in);
                GETQRCODE_M.GETQRCODE_OUT _out = new GETQRCODE_M.GETQRCODE_OUT();


                string his_rtnxml = "";

                string Key = EncryptionKey.KeyData.AESKEY(_in.HOS_ID);
                string Service_name = _in.DEAL_TYPE == "1" ? "WXPAYPRECREATE" : "ALIPAYPRECREATE";
                string TYPE_NAME = "";

                string type = "reg";
                string biz_type = "1";


                if (_in.TYPE == "0")
                {
                    TYPE_NAME = "Z预约挂号费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&1";
                    type = "reg";
                    biz_type = "1";

                }
                else if (_in.TYPE == "1")
                {
                    TYPE_NAME = "Z门诊收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&2";
                    type = "fee";
                    biz_type = "2";

                }
                else if (_in.TYPE == "4")
                {
                    TYPE_NAME = "Z预交金收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&3";
                    type = "pre";
                    biz_type = "3";

                }
                else if (_in.TYPE == "5")
                {
                    TYPE_NAME = "Z出院结算" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&4";
                }
                else if (_in.TYPE == "2")
                {
                    TYPE_NAME = "Z病历本" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&5";
                }
                string out_trade_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;




                SLBModels.GETQRCODE.busdata busdata = new SLBModels.GETQRCODE.busdata()
                {
                    HOS_ID=_in.HOS_ID,
                    COMM_HIS=out_trade_no,
                    Je=decimal.Parse(_in.CASH_JE),
                    ORDER_DESC= TYPE_NAME,
                    PAY_TYPE=_in.DEAL_TYPE

                };

                string rtndata;

                bool suc = new SLBHelper().CallSLB("PBusPay", "GETQRCODE", JsonConvert.SerializeObject(busdata), out rtndata);

                if (suc)
                {

                    SLBModels.GETQRCODE.outbody outbody=JsonConvert.DeserializeObject<SLBModels.GETQRCODE.outbody>(rtndata);

                    if (outbody.CLBZ!="0")
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = outbody.CLJG;
                        return JsonConvert.SerializeObject(dataReturn);
                    }
                    _out.QRCODE = outbody.QRCODE;

                    dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
                }
            

            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
