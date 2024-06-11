using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_OutHos.HISModels;

using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_OutHos.BUS
{
    internal class GETOUTFEENOPAY
    {
        public static string B_GETOUTFEENOPAY(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_IN _in = JsonConvert.DeserializeObject<Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_IN>(json_in);
            Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_OUT _out = new Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_OUT();
            string duKaFS = "1";

            switch (_in.MDTRT_CERT_TYPE)
            {
                case "01":
                    duKaFS = "5";
                    break;

                case "03":
                    duKaFS = "1";
                    break;

                default:
                    break;
            }

            T5204.input input = new T5204.input()
            {
                bingRenXM = "",//  病人姓名
                zhengJianHM = _in.SFZ_NO,// 证件号码
                hospitalId = "320282466455146",// 医院ID
                bingRenID = _in.HOSPATID,// 病人ID
                yiBaoBH = _in.YLCARD_TYPE == "2" ? _in.YLCARD_NO : "",// 医保编号
                duKaFS = duKaFS,// 读卡方式
                yeWuLX = "5204",// 业务类型        业务类型(固定值为5204)
                yiBaoXX = _in.CARD_INFO,// 医保信息
            };

            PushServiceResult<List<T5204.data>> result = HerenHelper<List<T5204.data>>.pushService("5204-QHZZJ", JsonConvert.SerializeObject(input));

            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            dataReturn.Code = 0;
            dataReturn.Msg = "success";

            if (result.data.Count == 0)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "没有找到待缴费的处方记录";

                return JsonConvert.SerializeObject(dataReturn);
            }
            //if (!string.IsNullOrEmpty(result.warningInfo))
            if (result.warningInfo == "患者存在未出皮试结果的皮试医嘱,请患者先去做皮试！")
            {
                dataReturn.Code = 5;
                dataReturn.Msg = result.warningInfo;

                return JsonConvert.SerializeObject(dataReturn);
            }
            _out.PRELIST = new List<Model.GETOUTFEENOPAY_M.PRE>();

            foreach (var item in result.data)
            {
  
                Model.GETOUTFEENOPAY_M.PRE pre = new Model.GETOUTFEENOPAY_M.PRE();
                pre.OPT_SN = "1063359";
                pre.PRE_NO = item.jiuZhenJLID;//就诊记录，单次处方唯一
                pre.HOS_SN = item.visitNo;//就诊号 ，单次挂号唯一
                pre.DEPT_CODE = item.keShiDM;
                pre.DEPT_NAME = item.keShiMC;
                pre.DOC_NO = item.yiShengDM;
                pre.DOC_NAME = item.yiShengXM;
                pre.JEALL = item.zongJinE;
                pre.CASH_JE = item.zongJinE;
                pre.YB_PAY = "1";
                pre.YB_NOPAY_REASON = "";

                pre.YLLB = "";
                pre.DIS_CODE = "";
                pre.DIS_TYPE = "";
                pre.WARNINGINFO = result.warningInfo;

                _out.PRELIST.Add(pre);
                
            }

            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}