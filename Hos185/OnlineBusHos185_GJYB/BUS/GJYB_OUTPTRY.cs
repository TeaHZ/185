using CommonModel;
using Newtonsoft.Json;

namespace OnlineBusHos185_GJYB.BUS
{
    class GJYB_OUTPTRY
    {
        public static string B_GJYB_OUTPTRY(string json_in)
        {
            //多个逻辑类实现接口，具体用哪个逻辑类，得看GlobalVar初始化的时候从配置中载入的哪个
            DataReturn dataReturn = GlobalVar.business.OUTPTRY(json_in);
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
            //    DataReturn dataReturn = new DataReturn();
            //    string json_out = "";
            //    try
            //    {
            //        Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json_in);
            //        if (!dic.ContainsKey("HOS_ID") || FormatHelper.GetStr(dic["HOS_ID"]) == "")
            //        {
            //            dataReturn.Code = ConstData.CodeDefine.Parameter_Define_Out;
            //            dataReturn.Msg = "HOS_ID为必传且不能为空";
            //            goto EndPoint;
            //        }
            //        string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "OnlineBusHos153_GJYB", "0004").BusData;
            //        return out_data;
            //    }
            //    catch (Exception ex)
            //    {
            //        dataReturn.Code = 6;
            //        dataReturn.Msg = "程序处理异常";
            //        dataReturn.Param = ex.ToString();
            //    }
            //EndPoint:
            //    json_out = JsonConvert.SerializeObject(dataReturn);
            //    return json_out;
        }
    }
}
