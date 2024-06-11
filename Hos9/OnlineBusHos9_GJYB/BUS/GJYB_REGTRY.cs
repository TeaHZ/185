using Newtonsoft.Json;


namespace OnlineBusHos9_GJYB.BUS
{
    class GJYB_REGTRY
    {
        public static string B_GJYB_REGTRY(string json_in)
        {
            return JsonConvert.SerializeObject(GlobalVar.business.REGTRY(json_in));
        }
    }
}
