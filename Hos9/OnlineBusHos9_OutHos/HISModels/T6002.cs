using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_OutHos.HISModels
{
    internal class T6002
    {
        public class Input
        {
            public string operatorId { get; set; }
            public string patientId { get; set; }
            public string hdIndicator { get; set; }
            
            public string[] itemCodes { get; set; }
        }

        public class Outdata
        {
            public class Data
            {
                public Dictionary<string, long> labItemCodeMapList { get; set; }
                public Dictionary<string, long> treatItemCodeMapList { get; set; }
                public Dictionary<string, long> examItemCodeMapList { get; set; }
                public string visitNo { get; set; }
            }

            public class Response
            {
                public int code { get; set; }
                public Data data { get; set; }
                public string msg { get; set; }
            }

        }

    }
    internal class  T6004
    {
        public class Input
        {
            public string[] orderlds { get; set; }
        }
        public class Outdata
        {

        }
    }
}
