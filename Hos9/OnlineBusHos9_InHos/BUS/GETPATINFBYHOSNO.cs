using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;

using System;
using System.Collections.Generic;
using System.Text;
using static OnlineBusHos9_InHos.Model.GETPATHOSINFO;
using static OnlineBusHos9_InHos.Model.GETPATINFBYHOSNO;

namespace OnlineBusHos9_InHos.BUS
{
    internal class GETPATINFBYHOSNO
    {

        public static string B_GETPATINFBYHOSNO(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETPATINFBYHOSNO.In _in = JsonConvert.DeserializeObject<Model.GETPATINFBYHOSNO.In>(json_in);
            Model.GETPATINFBYHOSNO.Out1 _out = new Model.GETPATINFBYHOSNO.Out1();

            string dkfs = "";
            string dzpzxx = "";
            if (_in.MDTRT_CERT_TYPE == "01")//MDTRT_CERT_TYPE：01电子凭证，03实体医保卡
            {
                dkfs = "5";
                dzpzxx = _in.CARD_INFO;
            }

            HISModels.T5210.input input = new HISModels.T5210.input()
            {
                zhengJianHM = _in.SFZ_NO,
                zhuYuanHao = _in.HOSPATID,
                duKaFS = dkfs,//操作员ID
                Ybxx = dzpzxx//操作员ID
            };

            //PushServiceResult<T5210.data> result = HerenHelper<T5210.data>.pushService("5210-QHZZJ", JsonConvert.SerializeObject(input));
           PushServiceResult<List<T5210.data>> result = HerenHelper<List<T5210.data>>.pushService("5210-QHZZJ", JsonConvert.SerializeObject(input));
            
            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            //beg ------------------
            List<ZYList1> zylist = new List<ZYList1>();
            foreach (var item in result.data)
            {

                ZYList1 zylist1 = new ZYList1()
                {
                HIN_TIME = item.ruYuanRQ,
                HOSPATID = item.patientId,
                HOS_NO = item.VisitNo,
                PAT_NAME = item.bingRenXM,
                SFZ_NO = item.idNo,
                DEPT_NAME = item.bingQu,
                BED_NO = item.chuangWei,
                CAN_PAY = "1",
                JE_PAY = item.yuJiaoJZE.ToString(),
                JE_YET = item.zhuYuanFY.ToString(),
                JE_REMAIN = item.yuJiaoJYE.ToString(),
                GREENINDICATOR = item.greenIndicator
                };
                zylist.Add(zylist1);
            }
            //_out.HIN_TIME = result.data.ruYuanRQ;
            //_out.HOSPATID = result.data.patientId;
            //_out.HOS_NO = result.data.VisitNo;
            //_out.PAT_NAME = result.data.bingRenXM;
            //_out.SFZ_NO = result.data.idNo;
            //_out.DEPT_NAME = result.data.bingQu;
            //_out.BED_NO = result.data.chuangWei;
            _out.ZYLIST = zylist;
            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
