using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;

using System.Collections.Generic;
using System.Linq;
using static OnlineBusHos9_InHos.Model.GETPATHOSINFO;

namespace OnlineBusHos9_InHos.BUS
{
    internal class GETPATHOSINFO
    {
        public static string B_GETPATHOSINFO(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.GETPATHOSINFO.In _in = JsonConvert.DeserializeObject<Model.GETPATHOSINFO.In>(json_in);
            Model.GETPATHOSINFO.OUt _out = new Model.GETPATHOSINFO.OUt();
            #region 注释
            //HISModels.T1183.Input input = new HISModels.T1183.Input()
            //{
            //    yeWuLX = "1183",//  业务类型        业务类型（1183）
            //    jiuZhenId = _in.HOSPATID,// 就诊ID        患者ID
            //    zhengJianHM = _in.SFZ_NO,// 病人证件号码      病人证件号码
            //    kaiShiSJ = "",//   开始时间        YYYY-MM-DD
            //    jieShuSJ = "",//  结束时间        YYYY-MM-DD
            //    hospitalId = "320282466455146",//  医院ID        320282466455146
            //};

            //PushServiceResult<T1183.Data> result = HerenHelper<T1183.Data>.pushService("1183-QHZZJ", JsonConvert.SerializeObject(input));

            //if (result.code != 1)
            //{
            //    dataReturn.Code = 6;
            //    dataReturn.Msg = result.msg;

            //    return JsonConvert.SerializeObject(dataReturn);
            //}
            //_out.FEELIST = new List<Model.GETPATHOSINFO.FEELISTItem>();

            //var feelist = from a in result.data.xiangMuXQ.AsEnumerable()

            //              group a by new
            //              {
            //                  FEE_NOTE = a.feiYongDLMC
            //              }
            //             into g
            //              select new
            //              {
            //                  g.Key.FEE_NOTE,
            //                  JE = g.Sum(a => FormatHelper.GetDecimal(a.jinE))
            //              };

            //foreach (var item in feelist)
            //{
            //    Model.GETPATHOSINFO.FEELISTItem fee = new Model.GETPATHOSINFO.FEELISTItem();
            //    fee.FEE_NOTE = item.FEE_NOTE;
            //    fee.JE = item.JE.ToString();

            //    _out.FEELIST.Add(fee);
            //}

            //HISModels.T1184.Input input1184 = new HISModels.T1184.Input()
            //{
            //    yeWuLX = "1184",// 业务类型        业务类型（1184）
            //    jiuZhenId = _in.HOSPATID,//就诊ID        患者ID
            //    zhengJianHM = _in.SFZ_NO,//病人证件号码      病人证件号码
            //    hospitalId = "320282466455146",// 医院ID        320282466455146
            //};
            //PushServiceResult<List<HISModels.T1184.Data>> result1184 = HerenHelper<List<HISModels.T1184.Data>>.pushService("1184-QHZZJ", JsonConvert.SerializeObject(input1184));


            //_out.PAYLIST = new List<Model.GETPATHOSINFO.PAYLISTItem>();
            //if (result1184.code == 1)
            //{
            //    foreach (var item in result1184.data)
            //    {
            //        Model.GETPATHOSINFO.PAYLISTItem pay = new Model.GETPATHOSINFO.PAYLISTItem();

            //        pay.HIN_TIME = item.jiaoKuanRQ;
            //        pay.JE = item.jiaoKuanJE.ToString();
            //        pay.JE_NOTE = item.zhiFuLYMC + "预交金充值";
            //        _out.PAYLIST.Add(pay);
            //    }
            //}
            //_out.HOSPATID = result.data.jiBenXX.bingAnHao;
            //_out.JE_PAY = result.data.jiBenXX.yuJiaoKuan.ToString();
            //_out.JE_REMAIN = (result.data.jiBenXX.yuJiaoKuan - result.data.jiBenXX.feiYongZE).ToString("0.00");
            //_out.CAN_PAY = "1";
            //_out.JE_YET = result.data.jiBenXX.feiYongZE.ToString();
            #endregion
            HISModels.T5210.input input = new HISModels.T5210.input()
            {
                zhengJianHM = _in.SFZ_NO,
                zhuYuanHao = _in.HOSPATID
            };

            //PushServiceResult<T5210.output> result = HerenHelper<T5210.output>.pushService("5210-QHZZJ", JsonConvert.SerializeObject(input));
            PushServiceResult<List<T5210.data>> result = HerenHelper<List<T5210.data>>.pushService("5210-QHZZJ", JsonConvert.SerializeObject(input));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            //beg ------------------
            List<ZYList> zylist = new List<ZYList>();
            foreach (var item in result.data)
            {
                
                ZYList zylist1 = new ZYList()
                {
                    HOS_NO=item.patientId,
                    HOSPATID=item.VisitNo,
                    JE_PAY=item.yuJiaoJZE.ToString(),
                    JE_YET=item.zhuYuanFY.ToString(),
                    JE_REMAIN=item.yuJiaoJYE.ToString(),
                    CAN_PAY= "1",
                    HIN_TIME=item.ruYuanRQ,
                    GREENINDICATOR =item.greenIndicator
                };
                zylist.Add(zylist1);
            }

            //_out.HOSPATID = result.data.patientId;
            //_out.JE_PAY = result.data.yuJiaoJZE.ToString();
            //_out.JE_REMAIN = result.data.yuJiaoJYE.ToString();
            //_out.CAN_PAY = "1";
            //_out.JE_YET = result.data.zhuYuanFY.ToString();

            _out.ZYLIST = zylist;
            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}