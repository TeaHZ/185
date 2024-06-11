using SqlSugar;
using System;

namespace SqlSugarModel
{
    [SugarTable("ticketreprint")]
    public class Ticketreprint
    {
        /// <summary>
        /// SERIAL_NO
        /// </summary>
        [SugarColumn(ColumnName = "SERIAL_NO", IsPrimaryKey = true)]
        public int SERIAL_NO { get; set; }
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
        /// HOS_SN
        /// </summary>
        [SugarColumn(ColumnName = "HOS_SN")]
        public string HOS_SN { get; set; }

        /// <summary>
        /// TEXT
        /// </summary>
        [SugarColumn(ColumnName = "TEXT")]
        public string TEXT { get; set; }

        /// <summary>
        /// lTERMINAL_SN
        /// </summary>
        [SugarColumn(ColumnName = "lTERMINAL_SN")]
        public string lTERMINAL_SN { get; set; }

        /// <summary>
        /// NOW
        /// </summary>
        [SugarColumn(ColumnName = "NOW")]
        public DateTime? NOW { get; set; }

        /// <summary>
        /// SFZ_NO
        /// </summary>
        [SugarColumn(ColumnName = "SFZ_NO")]
        public string SFZ_NO { get; set; }

        /// <summary>
        /// print_times
        /// </summary>
        [SugarColumn(ColumnName = "print_times")]
        public int print_times { get; set; }

    }
}

