using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("schedule_period")]
	public class SchedulePeriod
	{
		/// <summary>
		/// HOS_ID
		/// </summary>
		[SugarColumn(ColumnName ="HOS_ID",IsPrimaryKey=true)]
		public string HOS_ID { get; set; }

		/// <summary>
		/// DEPT_CODE
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_CODE")]
		public string DEPT_CODE { get; set; }

		/// <summary>
		/// DOC_NO
		/// </summary>
		[SugarColumn(ColumnName ="DOC_NO")]
		public string DOC_NO { get; set; }

		/// <summary>
		/// SCH_DATE
		/// </summary>
		[SugarColumn(ColumnName ="SCH_DATE")]
		public string SCH_DATE { get; set; }

		/// <summary>
		/// SCH_TIME
		/// </summary>
		[SugarColumn(ColumnName ="SCH_TIME")]
		public string SCH_TIME { get; set; }

		/// <summary>
		/// PERIOD_START
		/// </summary>
		[SugarColumn(ColumnName ="PERIOD_START")]
		public string PERIOD_START { get; set; }

		/// <summary>
		/// PERIOD_END
		/// </summary>
		[SugarColumn(ColumnName ="PERIOD_END")]
		public string PERIOD_END { get; set; }

		/// <summary>
		/// COUNT_ALL
		/// </summary>
		[SugarColumn(ColumnName ="COUNT_ALL")]
		public int COUNT_ALL { get; set; }

		/// <summary>
		/// COUNT_YET
		/// </summary>
		[SugarColumn(ColumnName ="COUNT_YET")]
		public int COUNT_YET { get; set; }

	}
}

