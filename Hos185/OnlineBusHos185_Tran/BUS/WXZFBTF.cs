using CommonModel;
using Hos185_His.Models.OriginPay;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_Tran.Model;
using OnlineBusHos185_Tran.Plat.Model;

using System;

namespace OnlineBusHos185_Tran.BUS
{
    internal class WXZFBTF
    {
        public static string B_WXZFBTF(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                WXZFBTF_M.WXZFBTF_IN _in = JsonConvert.DeserializeObject<WXZFBTF_M.WXZFBTF_IN>(json_in);
                WXZFBTF_M.WXZFBTF_OUT _out = new WXZFBTF_M.WXZFBTF_OUT();

                if (false)
                {
                    #region 退费

                    string outRefundNo = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                    P0301 p0301 = new P0301()
                    {
                        outRefundNo = outRefundNo,// 1662597711457, //商⼾退款订单号

                        //outTradeNo和transactionId⼆选⼀，任填⼀个即可
                        outTradeNo = _in.QUERYID,// 1662597711456, //商⼾订单号 （原交易）
                        transactionId = "",//2022081911061501000000, //⽀付中台订单号（原交易）
                        refundFee = decimal.Parse(_in.CASH_JE),// 1, //退款⾦额（元）
                        refundReason = _in.REASON,//退款原因,
                        macNumber = "",//调⽤⽅机器mac地址/ip地址,

                        //以下选填
                        operCode = _in.USER_ID,//hlwyy, //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
                        name = _in.PAT_NAME,//患者姓名, //不填的就使⽤下单时的数据
                        identityId = "",//患者证件号, //不填的就使⽤下单时的数据
                        mobile = "",//患者⼿机号 //不填的就使⽤下单时的数据
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0301);

                    Hos185_His.Models.Output<P0301DATA> output
                              = GlobalVar.CallAPI<P0301DATA>("/platformpayment/pay/refundOrder", jsonstr);

                    dataReturn = new DataReturn() { Code = output.code, Msg = output.message };
                    if (output.code == 0)
                    {
                        /*
                         * refunding?
                        退款中?
                        refund_success?
                        退款成功?
                        refund_failed?
                        退款失败
                        */
                        if (output.data.tradeState == "refund_success")
                        {
                            _out.STATUS = "1";

                            string tradeType = "";
                            switch (output.data.tradeChannel)
                            {
                                case "wx":
                                    tradeType = "1";

                                    break;

                                case "ali":
                                    tradeType = "2";
                                    break;

                                case "wxyb":
                                    break;

                                case "union":
                                    break;

                                case "abcjhzf":
                                    break;

                                case "yqtyzf":
                                    break;

                                default:
                                    break;
                            }
                            Hoshmpay hoshmpay = new Hoshmpay()
                            {
                                COMM_MAIN = _in.QUERYID,

                                TXN_TYPE = "02",//01 支付，03下单，02 退成功 04 退请求
                                HOS_ID = "185",
                                gmt_create_time = DateTime.Now,
                                notify_time = null,
                                JE = decimal.Parse(_in.CASH_JE),
                                APPT_SN = "",//reg_id  pay_id
                                BIZ_TYPE = "",
                                ThirdPayType = tradeType,
                                ThirdTradeNo = output.data.outRefundNo
                            };
                            var db = new DbMySQLZZJ().Client;

                            if (db.Insertable(hoshmpay).ExecuteCommand() == 1)
                            {
                            }
                            else
                            {
                                dataReturn.Msg = "退款成功，平台数据保存失败";
                            }
                        }
                        else
                        {
                            _out.STATUS = "0";
                        }
                    }
                    dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);

                    #endregion 退费
                }
                else
                {
                    #region 交易确认（fail）

                    P0601 p0601 = new P0601()
                    {
                        outTradeNo = "",
                        transactionId = _in.QUERYID,
                        confirmDate = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"),
                        confirmState = "fail",// 确认状态 success 确认成功 fail 失败
                        receiptNo = ""
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);
                    Hos185_His.Models.Output<JObject> output
                              = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);

                    dataReturn = new DataReturn() { Code = output.code, Msg = output.message };
                    if (output.code == 0)
                    {
                    }

                    #endregion 交易确认（fail）
                }
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