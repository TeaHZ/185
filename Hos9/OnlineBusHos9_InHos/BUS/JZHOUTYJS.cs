using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_InHos.HISModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_InHos.BUS
{
    internal class JZHOUTYJS
    {
        public static string B_JZHOUTYJS(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.JZHOUTYJS.In _in = JsonConvert.DeserializeObject<Model.JZHOUTYJS.In>(json_in);
            Model.JZHOUTYJS.Out _out = new Model.JZHOUTYJS.Out();

            JObject j1186 = new JObject
            {
                { "patientId", _in.HOSPATID },
                { "operatorId", _in.USER_ID },
                { "visitNo", _in.HOS_NO }
            };

            PushServiceResult<List<T1186.Data>> result = HerenHelper<List<T1186.Data>>.pushService("1186-QHZZJ", j1186.ToString());

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }

            var yjsinfo = result.data.FirstOrDefault();
            _out.CASH_JE = yjsinfo.totalNeedPay.ToString();
            _out.JE_ALL = yjsinfo.totalCharges.ToString();
            _out.JE_REMAIN = yjsinfo.totalNeedPay.ToString();
            _out.YB_PAY = yjsinfo.tongChouAmount.ToString();
            _out.JE_YJJ = yjsinfo.totalPrepayment.ToString();

            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(yjsinfo.totalCharges);
            _out.ACCT_PAY = FormatHelper.GetStr(yjsinfo.zhangHuAmount);
            _out.PSN_CASH_PAY = FormatHelper.GetStr(yjsinfo.selfPayAmount);
            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(yjsinfo.tongChouAmount);
            _out.OTH_PAY = FormatHelper.GetStr(yjsinfo.otherInsAmount);
            _out.BALC = FormatHelper.GetStr(yjsinfo.zhangHuYE);
            _out.ACCT_MULAID_PAY = FormatHelper.GetStr(yjsinfo.gongJiZhangHuAmount);

            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}