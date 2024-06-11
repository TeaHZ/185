

using Newtonsoft.Json;

namespace OnlineBusHos185_GJYB.BUS
{
    class GJYB_REGTRY
    {
        public static string B_GJYB_REGTRY(string json_in)
        {
            return JsonConvert.SerializeObject(GlobalVar.business.REGTRY(json_in));
        }
    }
}
