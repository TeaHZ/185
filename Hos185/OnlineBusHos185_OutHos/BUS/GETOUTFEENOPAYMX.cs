using CommonModel;
using Hos185_His.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using static OnlineBusHos185_OutHos.Model.GETOUTFEENOPAYMX_M;

namespace OnlineBusHos185_OutHos.BUS
{
    internal class GETOUTFEENOPAYMX
    {
        public static string B_GETOUTFEENOPAYMX(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN _in = JsonConvert.DeserializeObject<Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN>(json_in);
            Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT _out = new Model.GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT();

            Hos185_His.Models.MZ.GETOUTFEENOPAYMX nopaymx = new Hos185_His.Models.MZ.GETOUTFEENOPAYMX()
            {
                clinicCode = _in.HOS_SN,  //挂号流水号
                invoiceNo = "",  //发票号
                invoiceSeq = "",  //发票流水号
                lifeEquityCardNo = "",  //权益卡卡号
                lifeEquityCardType = "",  //权益卡类型
                pactCode = "01",  //合同编号
                recipeNo = _in.PRE_NO,  //处方号,多个以#分割
                sequenceNo = "0",  //处方流水号
                ybPay = "1"  //是否能够医保支付 0 不可以 1可以
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopaymx);

            Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>> output
   = GlobalVar.CallAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>>("/hischargesinfo/outpatientfee/recipedetailinfo", jsonstr);

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message;

            if (output.code != 0)
            {
                return JsonConvert.SerializeObject(dataReturn);
            }

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> datas = output.data;

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> meds = datas.FindAll(x => x.drugFlag == "1").ToList();

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> ckts = datas.FindAll(x => x.drugFlag == "0").ToList();

            _out.DAMEDLIST = new List<MED>();

            if (meds.Count > 0)
            {
                foreach (var dr in meds)
                {
                    MED med = new MED();
                    med.PRENO = dr.recipeNo;
                    med.DATIME = dr.confirmDate;
                    med.DAID = dr.moOrder;
                    med.MED_ID = dr.itemCode;
                    med.MED_NAME = dr.itemName;
                    med.MED_GG = dr.specs;
                    med.GROUPID = dr.groupCode;
                    med.USAGE = dr.usageCode;
                    med.AUT_NAME = dr.priceUnit;
                    med.CAMT = dr.qty.ToString();
                    med.AUT_NAMEALL = dr.priceUnit;
                    med.CAMTALL = dr.qty.ToString();
                    med.TIMES = dr.frequencyCode;
                    med.PRICE = dr.unitPrice.ToString();
                    med.AMOUNT = (dr.unitPrice * dr.qty).ToString();
                    med.YB_CODE = dr.centerCode;
                    med.YB_CODE_GJM = dr.centerCode;
                    med.IS_QX = "0";
                    med.MINAUT_FLAG = "";
                    _out.DAMEDLIST.Add(med);
                }
            }

            _out.DACHKTLIST = new List<CHKT>();

            if (ckts.Count > 0)
            {
                foreach (var dr in ckts)
                {
                    Model.GETOUTFEENOPAYMX_M.CHKT chkt = new Model.GETOUTFEENOPAYMX_M.CHKT();
                    chkt.DATIME = dr.confirmDate;
                    chkt.DAID = dr.moOrder;
                    chkt.CHKIT_ID = dr.itemCode;
                    chkt.CHKIT_NAME = dr.itemName;
                    chkt.AUT_NAME = dr.priceUnit;
                    chkt.CAMTALL = dr.qty.ToString();
                    chkt.PRICE = dr.unitPrice.ToString();
                    chkt.AMOUNT = (dr.unitPrice * dr.qty).ToString();
                    chkt.YB_CODE = dr.centerCode;
                    chkt.YB_CODE_GJM = dr.centerCode;
                    chkt.IS_QX = "1";
                    chkt.MINAUT_FLAG = "";
                    chkt.FEE_TYPE = "";
                    _out.DACHKTLIST.Add(chkt);
                }
            }

            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}