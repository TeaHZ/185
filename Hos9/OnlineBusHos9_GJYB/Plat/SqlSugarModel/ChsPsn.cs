using SqlSugar;
using System;

namespace OnlineBusHos9_GJYB.SqlSugarModel
{
    [SugarTable("chs_psn")]
    public class ChsPsn
    {
        /// <summary>
        /// psn_no
        /// </summary>
        [SugarColumn(ColumnName = "psn_no", IsPrimaryKey = true)]
        public string psn_no { get; set; }

        /// <summary>
        /// insuplc_admdvs
        /// </summary>
        [SugarColumn(ColumnName = "insuplc_admdvs")]
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// chsOutput1101
        /// </summary>
        [SugarColumn(ColumnName = "chsInput1101")]
        public string chsInput1101 { get; set; }
        /// <summary>
        /// chsOutput1101
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput1101")]
        public string chsOutput1101 { get; set; }

        /// <summary>
        /// mdtrt_cert_type
        /// </summary>
        [SugarColumn(ColumnName = "mdtrt_cert_type")]
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// mdtrt_cert_no
        /// </summary>
        [SugarColumn(ColumnName = "mdtrt_cert_no")]
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// card_sn
        /// </summary>
        [SugarColumn(ColumnName = "card_sn")]
        public string card_sn { get; set; }

        /// <summary>
        /// begntime
        /// </summary>
        [SugarColumn(ColumnName = "begntime")]
        public string begntime { get; set; }

        /// <summary>
        /// psn_cert_type
        /// </summary>
        [SugarColumn(ColumnName = "psn_cert_type")]
        public string psn_cert_type { get; set; }

        /// <summary>
        /// certno
        /// </summary>
        [SugarColumn(ColumnName = "certno")]
        public string certno { get; set; }

        /// <summary>
        /// psn_name
        /// </summary>
        [SugarColumn(ColumnName = "psn_name")]
        public string psn_name { get; set; }
        /// <summary>
        /// save_time
        /// </summary>
        [SugarColumn(ColumnName = "save_time")]
        public DateTime? save_time { get; set; }
    }
}

