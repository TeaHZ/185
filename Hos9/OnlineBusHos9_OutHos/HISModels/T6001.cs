using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_OutHos.HISModels
{
    internal class T6001
    {
        public class Input
        {

            public string operatorId { get; set; }
            public string clinicItemType { get; set; }

        }

        public class Outdata
        {

       
            public string itemCode { get; set; }
            public string clinicItemName { get; set; }
            public string price { get; set; }
            public string itemClass { get; set; }
        }

    }
}
