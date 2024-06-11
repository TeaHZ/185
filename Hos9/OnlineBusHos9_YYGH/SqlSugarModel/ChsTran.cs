using SqlSugar;
using System;


namespace SqlSugarModel
{
    [SugarTable("chs_tran")]
    public class ChsTran
    {
        /// <summary>
        /// TRAN_ID
        /// </summary>
        [SugarColumn(ColumnName = "TRAN_ID", IsPrimaryKey = true)]
        public string TRAN_ID { get; set; }

        /// <summary>
        /// HOS_ID
        /// </summary>
        [SugarColumn(ColumnName = "HOS_ID")]
        public string HOS_ID { get; set; }

        /// <summary>
        /// BIZ_TYPE
        /// </summary>
        [SugarColumn(ColumnName = "BIZ_TYPE")]
        public string BIZ_TYPE { get; set; }

        /// <summary>
        /// BIZ_NO
        /// </summary>
        [SugarColumn(ColumnName = "BIZ_NO")]
        public string BIZ_NO { get; set; }

        /// <summary>
        /// psn_no
        /// </summary>
        [SugarColumn(ColumnName = "psn_no")]
        public string psn_no { get; set; }

        /// <summary>
        /// insuplc_admdvs
        /// </summary>
        [SugarColumn(ColumnName = "insuplc_admdvs")]
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// mdtrt_id
        /// </summary>
        [SugarColumn(ColumnName = "mdtrt_id")]
        public string mdtrt_id { get; set; }

        /// <summary>
        /// setl_id
        /// </summary>
        [SugarColumn(ColumnName = "setl_id")]
        public string setl_id { get; set; }

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
        /// chsInput2201
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2201")]
        public string chsInput2201 { get; set; }

        /// <summary>
        /// chsOutput2201
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput2201")]
        public string chsOutput2201 { get; set; }

        /// <summary>
        /// chsInput2203
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2203")]
        public string chsInput2203 { get; set; }

        /// <summary>
        /// chsInput2204
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2204")]
        public string chsInput2204 { get; set; }

        /// <summary>
        /// chsOutput2204
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput2204")]
        public string chsOutput2204 { get; set; }

        /// <summary>
        /// chsInput2206
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2206")]
        public string chsInput2206 { get; set; }

        /// <summary>
        /// chsOutput2206
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput2206")]
        public string chsOutput2206 { get; set; }

        /// <summary>
        /// chsInput2207
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2207")]
        public string chsInput2207 { get; set; }
        /// <summary>
        /// chsOutput2207
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput2207")]
        public string chsOutput2207 { get; set; }
        /// <summary>
        /// chsInput2208
        /// </summary>
        [SugarColumn(ColumnName = "chsInput2208")]
        public string chsInput2208 { get; set; }
        /// <summary>
        /// chsOutput2208
        /// </summary>
        [SugarColumn(ColumnName = "chsOutput2208")]
        public string chsOutput2208 { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime? create_time { get; set; }

    }
}

