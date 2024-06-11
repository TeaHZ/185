using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos185_Tran.Model;

using System;

namespace OnlineBusHos185_Tran.BUS
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


                Hos185_His.Models.OriginPay.P0103 p0103 = new Hos185_His.Models.OriginPay.P0103()
                {
                    tradeChannel = "yqtyzf", //交易渠道 参考附录【交易渠道】
                    outTradeNo = out_trade_no, //商⼾订单号
                    description = TYPE_NAME, //订单描述
                    totalFee = decimal.Parse(_in.CASH_JE), //⾦额（单位：元）
                    name = _in.PAT_NAME,
                    macNumber = "78-2B-46-CA-A5-9D",
                    identityId = _in.HOSPATID,
                    mobile = "",
                    type = type,//交易类型 reg:⻔诊挂号 fee:⻔诊缴费 pre:住院预交⾦

                    //以下为可选项
                    operCode = _in.USER_ID, //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
                    notifyUrl = "", //⽀付成功回调地址 具体⻅【5.⽀付成功回调】
                    payActiveTime = 2,//⽀付有效期（单位：分）默认2分钟
                    cardNo = _in.HOSPATID, //患者ID
                    optIptNo = _in.HOSPATID, //⻔诊流⽔号/住院流⽔号
                    recipeNoList = "",//⻔诊缴费清单号，多个清单⽤,分割
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0103);


                Hos185_His.Models.Output<Hos185_His.Models.OriginPay.P0103DATA> output
                          = GlobalVar.CallAPI<Hos185_His.Models.OriginPay.P0103DATA>("/platformpayment/pay/qrCodeOrder", jsonstr);

                dataReturn = new DataReturn() { Code = output.code, Msg = output.message };
                if (output.code == 0)
                {


                    Plat.Model.Hoshmpay hoshmpay = new Plat.Model.Hoshmpay()
                    {
                        COMM_MAIN = output.data.transactionId,

                        TXN_TYPE = "03",//01 支付，03下单，02 退成功 04 退请求
                        HOS_ID = "185",
                        gmt_create_time = DateTime.Now,
                        notify_time = null,
                        JE = decimal.Parse(_in.CASH_JE),
                        APPT_SN = "",//reg_id  pay_id
                        BIZ_TYPE = biz_type,
                        ThirdPayType = "",
                        ThirdTradeNo = ""
                    };



                    var db = new DbMySQLZZJ().Client;

                    if (db.Insertable(hoshmpay).ExecuteCommand() == 1)
                    {
                        _out = new GETQRCODE_M.GETQRCODE_OUT()
                        {
                            QRCODE = output.data.codeUrl,
                            QUERYID = output.data.transactionId,
                            HIS_RTNXML = "",
                            PARAMETERS = ""
                        };
                    }

                    dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
                }
                else
                {
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;
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
