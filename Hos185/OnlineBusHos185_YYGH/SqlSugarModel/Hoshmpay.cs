using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlSugarModel
{
    [SugarTable("hoshmpay")]
    public class Hoshmpay
    {



        public string COMM_MAIN { get; set; }

        public string TXN_TYPE { get; set; }
        public string HOS_ID { get; set; }
        public DateTime? gmt_create_time { get; set; }
        public string notify_time { get; set; }
        public decimal JE { get; set; }
        public string APPT_SN { get; set; }
        public string BIZ_TYPE { get; set; }
        public string ThirdPayType { get; set; }
        public string ThirdTradeNo { get; set; }
    }
}
