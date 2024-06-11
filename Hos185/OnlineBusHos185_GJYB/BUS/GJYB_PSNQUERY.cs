using CommonModel;
using Newtonsoft.Json;

namespace OnlineBusHos185_GJYB.BUS
{
    class GJYB_PSNQUERY
    {
        public static string B_GJYB_PSNQUERY(string json_in)
        {
            DataReturn dataReturn = GlobalVar.business.PSNQUERY(json_in);
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
