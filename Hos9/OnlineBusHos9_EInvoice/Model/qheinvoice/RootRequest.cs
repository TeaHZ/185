using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_EInvoice.Model.qheinvoice
{
    public class RootRequest<T>
    {

        public RootRequest(string type) { 
        
            HEADER heder= new HEADER();
            heder.TYPE = type;

            ROOT=new ROOT_IN() { HEADER=heder};
        }
        /// <summary>
        /// 
        /// </summary>
        public ROOT_IN ROOT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T BODY { get; set; }

        public class ROOT_IN
        {
            /// <summary>
            /// 
            /// </summary>
            public HEADER HEADER { get; set; }
        }
    }



 
}
