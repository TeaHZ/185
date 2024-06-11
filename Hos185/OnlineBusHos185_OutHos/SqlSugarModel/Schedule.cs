using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("schedule")]
	public class Schedule
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
		/// SCH_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="SCH_TYPE")]
		public int SCH_TYPE { get; set; }

		/// <summary>
		/// COUNT_ALL
		/// </summary>
		[SugarColumn(ColumnName ="COUNT_ALL")]
		public int COUNT_ALL { get; set; }

		/// <summary>
		/// COUNT_REM
		/// </summary>
		[SugarColumn(ColumnName ="COUNT_REM")]
		public int COUNT_REM { get; set; }

		/// <summary>
		/// GH_FEE
		/// </summary>
		[SugarColumn(ColumnName ="GH_FEE")]
		public decimal GH_FEE { get; set; }

		/// <summary>
		/// ZL_FEE
		/// </summary>
		[SugarColumn(ColumnName ="ZL_FEE")]
		public decimal ZL_FEE { get; set; }

		/// <summary>
		/// START_TIME
		/// </summary>
		[SugarColumn(ColumnName ="START_TIME")]
		public string START_TIME { get; set; }

		/// <summary>
		/// END_TIME
		/// </summary>
		[SugarColumn(ColumnName ="END_TIME")]
		public string END_TIME { get; set; }

		/// <summary>
		/// PRO_TITLE
		/// </summary>
		[SugarColumn(ColumnName ="PRO_TITLE")]
		public string PRO_TITLE { get; set; }

		/// <summary>
		/// REGISTER_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="REGISTER_TYPE")]
		public string REGISTER_TYPE { get; set; }

	}
}

