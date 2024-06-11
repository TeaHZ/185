using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;

namespace OnlineBusHos9_InHos.BUS
{
    internal class SAVEINPATYJJ
    {
        public static string B_SAVEINPATYJJ(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.SAVEINPATYJJ.In _in = JsonConvert.DeserializeObject<Model.SAVEINPATYJJ.In>(json_in);
            Model.SAVEINPATYJJ.Out _out = new Model.SAVEINPATYJJ.Out();
            string laiyuan = "";

            string dealflag = _in.QUERYID.Substring(0, 1);
            //支付方式 1-支付宝 2-微信 3-云闪付 4-银联卡 空-现金
            switch (dealflag)
            {
                case "A":
                    laiyuan = "1";
                    break;

                case "W":
                    laiyuan = "2";
                    break;

                case "U":
                    laiyuan = "3";
                    break;

                default:
                    break;
            }
            HISModels.T5119.Input input = new HISModels.T5119.Input()
            {
                visitNo=_in.HOS_NO,//单次的住院流水号
                //liuShuiHao = "",//  就诊流水号       同待缴费就诊记录id
                //bingRenXM = "",// 病人姓名        商户流水号，此处为平台支付流水号
                //xingBie = "",//  病人性别
                zongJinE = _in.CASH_JE,//    总金额     总金额
                jiuZhenKH = "",//  就诊卡号        因院内没有就诊卡，可不传
                hospitalId = "320282466455146",//  医院ID        320282466455146
                //yiBaoJE = "",//  医保金额        医保金额
                //dingDanHao ="",//   三方支付流水号     三方支付流水号，只有接入第三方支付（如支付宝、微信等）才需要此字段
                yeWuLX = "5119",//  业务类型        业务类型（5119：通知医院结算成功）
                appID = _in.APPID,//  支付平台ID
                shangHuDDH = _in.QUERYID, //  订单号
                dingDanHao = _in.DEFRAYNO, // 第三方流水号
                channelTradeNo = _in.CHANNELTRADENO,
                yiYuanYWLX = "2",//   医院业务类型      医院业务类型，默认为1  ( 1 ：门诊缴费；  2：住 院预缴金充值)
                jiuZhenKLX = "1",//  就诊卡类型       默认1
                laiYuan = laiyuan,//  支付来源        1 微信2 支付宝3 APP4 医保
                //yeWuLY = "",
                sheBeiBH = _in.LTERMINAL_SN,//    设备编号        同操作人即可
                bingRenID = _in.HOSPATID,//   病人ID        病人档案号
                jinE = _in.CASH_JE,//    自费金额        自费金额
                zhengJianHM = _in.SFZ_NO,//  证件号码        证件号码
                //duKaFS = "",//   读卡方式        1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5-电子 医保凭证6-身份证(脱卡支付时使用)
                //bingRenLX = "",//   病人类型        病人类型（1：自费；2：医保；）
                //ziFeiJE = "",// 自费金额        自费金额
                //jiuZhenJLID = "",// 就诊记录ID
                caoZuoRId = _in.USER_ID,//  操作人工号
                //yiBaoXX = "",//  医保信息        读医保卡返回的原始信息
                //yiBaoData = "",//   医保交互数据      duKaFS=1时 社会保障卡卡号|pBusiCardInfo duKaFS=5时 电子凭证令牌， duKaFS=6时 医保1101返回的output 内容
            };

            PushServiceResult<T5119.Data> result = HerenHelper<T5119.Data>.pushService("5119-QHZZJ", JsonConvert.SerializeObject(input));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            _out.HOSPATID= _in.HOSPATID;
            _out.HOS_PAY_SN = result.data.shouFeiId;
            _out.JE_PAY = "0.00";
            _out.CASH_JE = _in.CASH_JE;
            _out.JE_REMAIN = result.data.zongJinE.ToString();
            _out.HIS_RTNXML = "";
            _out.PARAMETERS = "";



            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}