using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_OutHos.HISModels;

using SqlSugarModel;
using System.Collections.Generic;
using System.Linq;
using static OnlineBusHos9_OutHos.Model.GETOUTFEENOPAYMX_M;

namespace OnlineBusHos9_OutHos.BUS
{
    internal class GETOUTFEENOPAYMX
    {
        public static string B_GETOUTFEENOPAYMX(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN _in = JsonConvert.DeserializeObject<Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN>(json_in);
            Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT _out = new Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT();

            T1241.input input = new T1241.input()
            {
                zhengJianHM = _in.SFZ_NO,//  证件号码        证件号码
                yeWuLX = "1241",//           业务类型(固定值为1241)
                hospitalId = "320282466455146",//   医院ID        320282466455146
                fuKuanZT = "0",//     付款状态        默认0
                ziFeiBZ = "0",//   医保编号        自费标识（0：自费；1：医保）
                jiuZhenId = _in.PRE_NO,//   就诊记录ID      回传5204接口返回的jiuZhenJLID

            };




            PushServiceResult<T1241.data> result = HerenHelper<T1241.data>.pushService("1241-QHZZJ", JsonConvert.SerializeObject(input));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }




            _out.DAMEDLIST = new List<MED>();
            _out.DACHKTLIST = new List<CHKT>();

            foreach (var item in result.data.shouFeiGLJEList)
            {
                string isqx = "1";

                switch (item.guiLeiDM)
                {
                    case "A":
                    case "B":
                        isqx = "0";
                        break;
                    case "C":
                        break;
                    case "D":
                        break;
                    default:
                        break;
                }
   
                if (isqx == "0")
                {
                    foreach (var detail in item.mingXiJEs)
                    {


                        MED med = new MED();
                        med.PRENO = "";
                        med.DATIME = "";
                        med.DAID = detail.chuFangId;
                        med.MED_ID = detail.xiangMUDM;
                        med.MED_NAME = detail.xiangMuMC;
                        med.MED_GG = "";
                        med.GROUPID = "";
                        med.USAGE = "";
                        med.AUT_NAME = "";
                        med.CAMT = "";
                        med.AUT_NAMEALL = detail.danWei;
                        med.CAMTALL = detail.shuLiang;
                        med.TIMES = "";
                        med.PRICE = "";
                        med.AMOUNT = detail.jinE;
                        med.YB_CODE = "";
                        med.YB_CODE_GJM = "";
                        med.IS_QX = "0";
                        med.MINAUT_FLAG = "";
                        _out.DAMEDLIST.Add(med);
                    }

                }
                else
                {
                    foreach (var detail in item.mingXiJEs)
                    {


                        Model.GETOUTFEENOPAYMX_M.CHKT chkt = new Model.GETOUTFEENOPAYMX_M.CHKT();
                        chkt.DATIME = "";
                        chkt.DAID = detail.chuFangId;
                        chkt.CHKIT_ID = detail.xiangMUDM;
                        chkt.CHKIT_NAME = detail.xiangMuMC;
                        chkt.AUT_NAME = detail.danWei;
                        chkt.CAMTALL = detail.shuLiang;
                        chkt.PRICE = detail.danJia;
                        chkt.AMOUNT = detail.jinE;
                        chkt.YB_CODE = "";
                        chkt.YB_CODE_GJM = "";
                        chkt.IS_QX = "1";
                        chkt.MINAUT_FLAG = "";
                        chkt.FEE_TYPE = "";
                        _out.DACHKTLIST.Add(chkt);
                    }
                }

            }


            _out.JEALL = result.data.zongJinE.ToString();




            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}