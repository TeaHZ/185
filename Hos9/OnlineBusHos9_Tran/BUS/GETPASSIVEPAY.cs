using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;
using PasS.Base.Lib;

using System.Collections.Generic;

namespace OnlineBusHos9_Tran.BUS
{
    internal class GETPASSIVEPAY
    {
        public static string B_GETPASSIVEPAY(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "UN_MERGE_BAR_CODE";

            GETPASSIVEPAY_M.GETPASSIVEPAY_IN _in = JsonConvert.DeserializeObject<GETPASSIVEPAY_M.GETPASSIVEPAY_IN>(json_in);
            GETPASSIVEPAY_M.GETPASSIVEPAY_OUT _out = new GETPASSIVEPAY_M.GETPASSIVEPAY_OUT();



            string TYPE_NAME = "";
            string TitleName = "";

            /*
         appId:2022081925029210（农商行）
         2022081925029211（交行）
         key:539130e1b1fdb52093bb072a67e3c62a
          */

            string appId = "2022081925029210";
            string key = "539130e1b1fdb52093bb072a67e3c62a";
            string channel = "UN_MERGE_BAR_CODE";

            if (_in.TYPE == "0")
            {
                TYPE_NAME = "Z预约挂号费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&1";
                TitleName = "预约挂号费";
            }
            else if (_in.TYPE == "1")
            {
                TYPE_NAME = "Z门诊收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&2";
                TitleName = "门诊收费";
            }
            else if (_in.TYPE == "4")
            {
                appId = "2022081925029211";
                TYPE_NAME = "Z预交金收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&3";
                TitleName = "预交金";
            }
            else if (_in.TYPE == "5")
            {
                appId = "2022081925029211";
                TYPE_NAME = "Z出院结算" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&4";
                TitleName = "出院结算";

            }
            else if (_in.TYPE == "2")
            {
                TYPE_NAME = "Z病历本" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&5";
                TitleName = "病历本";


            }

            string QUERYID = NewIdHelper.NewOrderId20;
            string BANK_NAME = "";


            int codehead = int.Parse(_in.AUTH_CODE.Substring(0, 2));

            if (codehead >= 25 && codehead <= 30)
            {
                QUERYID = "A" + QUERYID;
                BANK_NAME = "支付宝";

            }else
            if (codehead >= 10 && codehead <= 15)
            {
                QUERYID = "W" + QUERYID;
                BANK_NAME = "微信";

            }else

            if (codehead == 62)
            {
                QUERYID = "U" + QUERYID;
                BANK_NAME = "云闪付";

            }
            else
            {
                QUERYID = "Q" + QUERYID;
                BANK_NAME = "第三方支付";
            }

            //SLBModels.ORDERPAY.busdata orderpay = new SLBModels.ORDERPAY.busdata()
            //{
            //    HOS_ID = _in.HOS_ID,
            //    Je = decimal.Parse(_in.CASH_JE),
            //    COMM_HIS = QUERYID,
            //    ORDER_DESC = TYPE_NAME,
            //    PAY_TYPE = _in.DEAL_TYPE == "3" ? "R4" : _in.DEAL_TYPE,
            //    QRCODE = _in.AUTH_CODE
            //};

            //string rtndata;

            //bool suc = new SLBHelper().CallSLB("PBusPay", "ORDERPAY", JsonConvert.SerializeObject(orderpay), out rtndata);

            //appId+ channel+ appTradeNo+ defrayFee+密钥
            string signPlain = appId + channel + QUERYID + _in.CASH_JE + key;
            string sign = MD5Helper.Md5(signPlain).ToLower();






            HerenModels.Pay.bizContent bizContent = new HerenModels.Pay.bizContent()
            {
                channel = channel,// String  30  支付通道    是   参考channel说明
                appTradeNo = QUERYID,//     String  40  应用交易号   是   调用方生成，保证appTradeNo每次请求唯一：202101001
                signNo = "",//  String  30  支付台签约协议编号       签约代扣signNo与contractId二选一
                contractId = "",// String  32  第三方签约协议号 签约代扣signNo与contractId二选一
                userId = "",// String  100 用户登录账号      签约代扣、支付宝小程序必填
                title = TitleName,// String  20  订单标题    是
                body = TYPE_NAME,//    String  200 订单描述    否
                scene = "",//String  64  业务场景    *   根据医院财务对账需求确认是否传值
                authCode = _in.AUTH_CODE,//  String  32         授权码 * ALI_OFFLINE_BAR_CODE,WX_OFFLINE_BAR_CODE, ALI_SMILE必填
                authNo = "",//  String  32  支付台预授权流水号   *             资金预授权确认转支付接口必填
                defrayFee = _in.CASH_JE,//  Price   10  支付金额           是   保留小数点两位，例：1.00
                abnormalUrl = "",// String  200 异常返回页面  否
                returnUrl = "",//  String  200 同步返回页面  否
                payExpire = "",//  String  8   该笔订单失效时间    否 单位：秒，示例值：60
                expireTime = "",// String  19    该笔订单失效时间    否   已废弃
                goodsTag = "",//   String  32  医疗场景值   是   参照医疗场景值说明
                openId = "",//  String      服务商户模式下服务商公众号openId     微信公众号、小程序必传 小程序 openId与subOpenId二选一
                subOpenId = "",//  String      服务商户模式下，被绑定商户的openId
                terminalIp = "127.0.0.1",//  String  16  终端IP    是    调用微信支付API的机器IP
                terminalId = "",// String  64  设备编号    *   工行数币H5必填
                mac = "",// String  32  mac地址   *   行数币H5必填
            };

            string bizcontentJson = JsonConvert.SerializeObject(bizContent);


            Dictionary<string, string> @params = new Dictionary<string, string>
            {
                { "appId", appId },
                { "method", "uniform.trade.pay" },
                { "sign", sign },
                { "notifyUrl", "" },
                { "optional", "" },
                { "bizContent", bizcontentJson }
            };

            var response = HerenHelper.SendTrade(@params);

            _out.QUERYID = QUERYID;
            _out.BANK_NAME = BANK_NAME;

            dataReturn.Param = JsonConvert.SerializeObject(_out);
            dataReturn.Code = 0;
            dataReturn.Msg = "已发起支付请求";
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}