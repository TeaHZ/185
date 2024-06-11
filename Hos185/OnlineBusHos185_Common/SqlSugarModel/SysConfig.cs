using SqlSugar;

namespace SqlSugarModel
{
    [SugarTable("sys_config")]
    public class SysConfig
    {
        /// <summary>
        /// HOS_ID
        /// </summary>
        [SugarColumn(ColumnName = "HOS_ID")]
        public string HOS_ID { get; set; }

        /// <summary>
        /// config_key
        /// </summary>
        [SugarColumn(ColumnName = "config_key")]
        public string config_key { get; set; }

        /// <summary>
        /// config_value
        /// </summary>
        [SugarColumn(ColumnName = "config_value")]
        public string config_value { get; set; }

        /// <summary>
        /// config_note
        /// </summary>
        [SugarColumn(ColumnName = "config_note")]
        public string config_note { get; set; }

    }
}

