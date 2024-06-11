using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos185_Tran.Model;
using OnlineBusHos185_Tran.Plat.Model;

using System;

namespace OnlineBusHos185_Tran.BUS
{
    class GETORDERSTATUS
    {
        public static string B_GETORDERSTATUS(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETORDERSTATUS_M.GETORDERSTATUS_IN _in = JsonConvert.DeserializeObject<GETORDERSTATUS_M.GETORDERSTATUS_IN>(json_in);
                GETORDERSTATUS_M.GETORDERSTATUS_OUT _out = new GETORDERSTATUS_M.GETORDERSTATUS_OUT();

                Hos185_His.Models.OriginPay.P0201 p0201 = new Hos185_His.Models.OriginPay.P0201()
                {
                    outTradeNo = "",
                    transactionId =_in.QUERYID
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0201);

                Hos185_His.Models.Output<Hos185_His.Models.OriginPay.P0201DATA> output
          = GlobalVar.CallAPI<Hos185_His.Models.OriginPay.P0201DATA>("/platformpayment/pay/queryOrder", jsonstr);


                if (output.code == 0)
                {
                    #region 支付状态说明

                    /*
                    create?
                    已创建?
                    wait?
                    待⽀付?
                    success?
                    ⽀付成功?
                    nopay?
                    未⽀付?
                    failed?
                    ⽀付失败?
                    closed?
                    ⽀付关系
                     
                     */
                    #endregion


                    /*
                    create?
                    已创建?
                    wait?
                    待⽀付?
                    success?
                    ⽀付成功?
                    nopay?
                    未⽀付?
                    failed?
                    ⽀付失败?
                    closed?
                    ⽀付关系?
                     */
                    if (output.data.tradeState== "success")
                    {

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
                            COMM_MAIN = output.data.transactionId,
                         
                            TXN_TYPE = "01",//01 支付，03下单，02 退成功 04 退请求
                            HOS_ID = "185",
                            gmt_create_time = DateTime.Now,
                            notify_time = null,
                            JE = output.data.totalFee,
                            APPT_SN = "",//reg_id  pay_id
                            BIZ_TYPE = output.data.orderType == "reg" ? "1" : "2",////订单类型 reg:⻔诊挂号 fee:⻔诊缴费 pre:住院预交⾦
                            ThirdPayType = tradeType,
                            ThirdTradeNo = output.data.extraData.thirdTransactionId
                        };

                        var db = new DbMySQLZZJ().Client;

                        if (db.Queryable<Hoshmpay>().Any(x => x.COMM_MAIN == output.data.transactionId && x.TXN_TYPE == "01"))
                        {
                            _out.STATUS = "1";
                        }
                        else   if (db.Insertable(hoshmpay).ExecuteCommand() == 1)
                        {
                            _out.STATUS = "1";
                        }

                     
                    }
                    else
                    {
                        _out.STATUS = "0";

                    }


                }
                else
                {
                    _out.STATUS = "0";

                }

                dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);

            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = ex.Message;
            }
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
