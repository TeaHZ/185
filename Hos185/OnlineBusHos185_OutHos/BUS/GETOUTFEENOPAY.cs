using CommonModel;
using Hos185_His.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OnlineBusHos185_OutHos.BUS
{
    internal class GETOUTFEENOPAY
    {
        public static string B_GETOUTFEENOPAY(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_IN _in = JsonConvert.DeserializeObject<Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_IN>(json_in);
            Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_OUT _out = new Model.GETOUTFEENOPAY_M.GETOUTFEENOPAY_OUT();

            Hos185_His.Models.MZ.GETOUTFEENOPAY nopay = new Hos185_His.Models.MZ.GETOUTFEENOPAY()
            {
                cardNo = _in.HOSPATID,  //医院内部就诊卡号,唯一
                clinicCode = "",  //挂号流水号
                idCardNo = _in.SFZ_NO,  //证件号
                idCardType = "01",  //证件类型 01:身份证 03:护照 06:港澳居民来往内地通行证 07:台湾居民来往内地通行证
                lifeEquityCardNo = "",  //权益卡卡号
                lifeEquityCardType = "",  //权益卡类型 2 医慧卡
                mcardNo = "",  //绑定的医疗证号
                mcardNoType = "",  //绑定的医疗证类型 1:就诊卡 4:身份证 5:医保/市民卡/护照
                pactCode = "01"  //合同编号
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopay);

            Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYDATA>> output
   = GlobalVar.CallAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYDATA>>("/hischargesinfo/outpatientfee/recipeinfo", jsonstr);

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message;

            try
            {
                if (output.code != 0)
                {
                    return JsonConvert.SerializeObject(dataReturn);
                }

                List<Hos185_His.Models.MZ.GETOUTFEENOPAYDATA> datas = output.data;

               
                _out.PRELIST = new List<Model.GETOUTFEENOPAY_M.PRE>();

                var query = from a in datas.AsEnumerable()
                            group a by new
                            {
                                OPT_SN = a.cardNo,
                                HOS_SN = a.clinicCode,
                                DEPT_CODE = a.deptCode,
                                DEPT_NAME = a.deptName,
                                DOC_NO = a.doctCode,
                                DOC_NAME = a.doctName,
                                YB_NOPAY_REASON = "",
                                YLLB = "",
                                DIS_CODE = a.mainDiagCode,
                                DIS_TYPE = "",
                                a.billType
                            }
                           into g
                            select
                new
                {
                    g.Key.OPT_SN,
                    g.Key.HOS_SN,
                    PRE_NO = string.Join("#", g.Select(a => FormatHelper.GetStr(a.recipeNo)).ToArray()),
                    g.Key.DEPT_CODE,
                    g.Key.DEPT_NAME,
                    g.Key.DOC_NO,
                    g.Key.DOC_NAME,

                    g.Key.YB_NOPAY_REASON,
                    g.Key.YLLB,
                    g.Key.DIS_CODE,
                    g.Key.DIS_TYPE,
                    g.Key.billType,
                    JEALL = g.Sum(a => FormatHelper.GetDecimal(a.totalFee)),
                    CASH_JE = g.Sum(a => FormatHelper.GetDecimal(a.totalFee))
                };

                foreach (var dr in query)
                {

                    string YBPAY = "0";
                    //只要本次就诊有一个医保明细，则全可以用医保
                    //0正常收费处方信息  1人寿医慧卡 2HMO 3养老医慧卡 4健康大使 5特需 6健保通 7高客 95 绿通 100先诊疗后付费
                    if (dr.billType != "3"&&datas.FindAll(x => x.ybPay == "1" && x.clinicCode==dr.HOS_SN).Count > 0)
                    {
                        YBPAY = "1";
                    }
                 

                    Model.GETOUTFEENOPAY_M.PRE pre = new Model.GETOUTFEENOPAY_M.PRE();
                    pre.OPT_SN = dr.OPT_SN;
                    pre.PRE_NO = dr.PRE_NO;
                    pre.HOS_SN = dr.HOS_SN;
                    pre.DEPT_CODE = dr.DEPT_CODE;
                    pre.DEPT_NAME = dr.DEPT_NAME;
                    pre.DOC_NO = dr.DOC_NO;
                    pre.DOC_NAME = dr.DOC_NAME;
                    pre.JEALL = dr.JEALL.ToString();
                    pre.CASH_JE = dr.CASH_JE.ToString();
                    pre.YB_PAY =YBPAY;//dr.ybPay
                    pre.YB_NOPAY_REASON = dr.YB_NOPAY_REASON;
                    pre.TKBILL_TYPE = dr.billType;
                    pre.YLLB = dr.YLLB;
                    pre.DIS_CODE = dr.DIS_CODE;
                    pre.DIS_TYPE = dr.DIS_TYPE;

                    _out.PRELIST.Add(pre);
                }
            }
            catch
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,未找到ITEMLIST节点,请检查HIS出参";
            }

            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}