using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.HerenModels
{
    internal class Request
    {
        public string appId { get; set; }
        public string method { get; set; }
        public string sign { get; set; }
        public string notifyUrl { get; set; }
        public string optional { get; set; }
    }
}
