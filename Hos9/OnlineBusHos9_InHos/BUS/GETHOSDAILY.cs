using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.BUS
{
    internal class GETHOSDAILY
    {

        public static string B_GETHOSDAILY(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETHOSDAILY.In _in = JsonConvert.DeserializeObject<Model.GETHOSDAILY.In>(json_in);
            Model.GETHOSDAILY.Out _out = new Model.GETHOSDAILY.Out();
            HISModels.T1183.Input input = new HISModels.T1183.Input()
            {
                yeWuLX = "1183",//  业务类型        业务类型（1183）
                jiuZhenId = _in.HOSPATID,// 就诊ID        患者ID
                zhengJianHM = _in.SFZ_NO,// 病人证件号码      病人证件号码
                kaiShiSJ = _in.BEGIN_DATE,//   开始时间        YYYY-MM-DD
                jieShuSJ = _in.END_DATE,//  结束时间        YYYY-MM-DD
                hospitalId = "320282466455146",//  医院ID        320282466455146
            };

            PushServiceResult<T1183.Data> result = HerenHelper<T1183.Data>.pushService("1183-QHZZJ", JsonConvert.SerializeObject(input));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }

            _out.JE_PAY = result.data.jiBenXX.yuJiaoKuan.ToString();
            _out.JE_REMAIN = (result.data.jiBenXX.feiYongZE - result.data.jiBenXX.yuJiaoKuan).ToString();

            _out.JE_YET= "";
            _out.JE_TODAY = "";



            _out.BIGITEMLIST = new List<Model.GETHOSDAILY.BIGITEMLISTItem>();
            Model.GETHOSDAILY.BIGITEMLISTItem bigitem = new Model.GETHOSDAILY.BIGITEMLISTItem();
            bigitem.ITEMLIST = new List<Model.GETHOSDAILY.ITEMLISTItem>();
            foreach (var item in result.data.xiangMuXQ)
            {


                Model.GETHOSDAILY.ITEMLISTItem item2 = new Model.GETHOSDAILY.ITEMLISTItem()
                {
                    NAME=item.xiangMuMC,
                    GG=item.yaoPinGG,
                    AMOUNT=item.jinE,
                    CAMT=item.feiYongSL,
                    DJ_DATE=item.faShengRQ,
                    PRICE=item.feiYongDJ
                    
                };

                bigitem.ITEMLIST.Add(item2);

            }
            _out.BIGITEMLIST.Add(bigitem);

            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
