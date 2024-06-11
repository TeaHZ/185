using CommonModel;
using Newtonsoft.Json;


namespace OnlineBusHos9_GJYB.BUS
{
    class GJYB_OUTPTRY
    {
        public static string B_GJYB_OUTPTRY(string json_in)
        {
            DataReturn dataReturn = GlobalVar.business.OUTPTRY(json_in);
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
       
        }
    }
}
