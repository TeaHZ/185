using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;
using System.Collections.Generic;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class REGISTERPAYCANCEL
    {
        public static string B_REGISTERPAYCANCEL(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_IN _in = JsonConvert.DeserializeObject<REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_IN>(json_in);
                REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_OUT _out = new REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_OUT();

                T5203.input input5203 = new T5203.input()
                {
                    zhengJianHM = "",//证件号码        
                    yiBaoBH = "",//医保编号        医保编号
                    yeWuLX = "5203",//业务类型     
                    bingRenID = _in.HOSPATID,//  病人ID        
                    bingRenXM = "",// 病人姓名
                    hospitalId = "320282466455146"
                };

                PushServiceResult<List<T5203.data>> result5203 = HerenHelper<List<T5203.data>>.pushService("5203-QHZZJ", JsonConvert.SerializeObject(input5203));

                foreach (var item in result5203.data)
                {
                    if (item.zhuangTai != "1")
                    {
                        continue;
                    }

                    string[] hos_SN = _in.HOS_SN.Split('|');


                    HISModels.T2022.input input = new HISModels.T2022.input()
                    {

                        hospitalId = "320282466455146",//  医院id        医院id
                        bingRenXM = "",//    病人姓名        病人姓名
                        shenFenZH = "",//   身份证号        身份证号
                        yuYueGHId = hos_SN[0],//    预约Id，预约取消时必传        预约Id，预约取消时必传
                        guaHaoID = "",//   挂号ID，退号时必传      挂号ID，退号时必传
                        shangHuDDH = "",//   挂号有支付时的商户订单号        挂号有支付时的商户订单号
                        jinE = "",//    现金金额        现金金额
                        yiBaoCXJG = "",//   医保撤销结果      医保撤销结果
                        yeWuLX = "2022",//   业务类型        业务类型(固定值：  2022)
                    };

                    PushServiceResult<string> result = HerenHelper<string>.pushService("2022-QHZZJ", JsonConvert.SerializeObject(input));

                    if (result.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = result.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }
                    dataReturn.Code = 0;
                    dataReturn.Msg = "success";

                    if (string.IsNullOrEmpty(_in.HOS_SN))
                    {
                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    //dataReturn.Code = 0;
                    //dataReturn.Msg = "";
                }
            }
            catch
            {
            }
            dataReturn.Code = 0;
            dataReturn.Msg = "";
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}