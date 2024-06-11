using Hos185_His.Models;
using Hos185_His;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_OutHos
{
    internal class GlobalVar
    {

        public static Output<T> CallAPI<T>(string routepath, string inputjson)
        {
            TKHelper main = new TKHelper();
            Output<T> output = main.CallServiceAPI<T>(routepath, inputjson);
            return output;


        }
    }
}
