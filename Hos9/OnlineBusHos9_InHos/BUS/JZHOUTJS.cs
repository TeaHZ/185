using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_InHos.BUS
{
    internal class JZHOUTJS
    {
        public static string B_JZHOUTJS(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.JZHOUTJS.In _in = JsonConvert.DeserializeObject<Model.JZHOUTJS.In>(json_in);
            Model.JZHOUTJS.Out _out = new Model.JZHOUTJS.Out();

            string laiyuan = "";
            string dkfs = "";
            string dzpzxx = "";
            if (_in.MDTRT_CERT_TYPE == "01")//MDTRT_CERT_TYPE：01电子凭证，03实体医保卡
            {
                dkfs = "5";
                dzpzxx = _in.CARD_INFO;
            }

            if (!string.IsNullOrEmpty(_in.QUERYID))
            {
                string dealflag = _in.QUERYID.Substring(0, 1);
                //支付方式 1-支付宝 2-微信 3-云闪付 4-银联卡 空-现金
                switch (dealflag)
                {
                    case "A":
                        laiyuan = "1";
                        _in.DEAL_TYPE = "2";

                        break;

                    case "W":
                        laiyuan = "2";
                        _in.DEAL_TYPE = "1";

                        break;

                    case "U":
                        laiyuan = "3";
                        _in.DEAL_TYPE = "3";

                        break;

                    default:
                        break;
                }
            }

            //1 - 支付宝  2 - 微信  3 - 云闪付  4 - 银联卡  空 - 现金
            HISModels.T1187.Input t1187 = new T1187.Input()
            {
                payChannel = laiyuan,//  支付方式
                selfPayAmount = _in.CASH_JE,//  第三方支付总金额
                thirdPartHisNo = _in.QUERYID,// 支付平台流水号
                tradeNo = _in.QUERYID,//支付平台订单号
                appId = _in.APPID,// 支付平台appId 住院固定的
                defrayNo = _in.DEFRAYNO,//
                channelTradeNo = _in.CHANNELTRADENO,// 第三方支付流水号
                visitNo = _in.HOS_NO,// 就诊流水号
                patientId = _in.HOSPATID,// 患者ID
                operatorId = _in.USER_ID,//操作员ID
                duKaFS = dkfs,//操作员ID
                zhengJianHM = dzpzxx,//操作员ID
            }
            ;

            PushServiceResult<List<T1187.Data>> result = HerenHelper<List<T1187.Data>>.pushService("1187-QHZZJ", JsonConvert.SerializeObject(t1187));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }

            var jsinfo = result.data.FirstOrDefault();
            _out.HOS_PAY_SN = jsinfo.totalNeedPay.ToString();
            _out.RCPT_NO = "";

            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(jsinfo.totalCharges);
            _out.ACCT_PAY = FormatHelper.GetStr(jsinfo.zhangHuAmount);
            _out.PSN_CASH_PAY = FormatHelper.GetStr(jsinfo.selfPayAmount);
            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(jsinfo.tongChouAmount);
            _out.OTH_PAY = FormatHelper.GetStr(jsinfo.otherInsAmount);
            _out.BALC = FormatHelper.GetStr(jsinfo.zhangHuYE);
            _out.ACCT_MULAID_PAY = FormatHelper.GetStr(jsinfo.gongJiZhangHuAmount);

            _out.RCPTNOLIST = JsonConvert.SerializeObject(jsinfo.rcptNoList);
            _out.SETTLENOLIST = JsonConvert.SerializeObject(jsinfo.settleNoList);

            //todo

            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}