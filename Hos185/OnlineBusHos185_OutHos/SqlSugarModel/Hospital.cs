using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("hospital")]
	public class Hospital
	{
		/// <summary>
		/// HOS_ID
		/// </summary>
		[SugarColumn(ColumnName ="HOS_ID",IsPrimaryKey=true)]
		public string HOS_ID { get; set; }

		/// <summary>
		/// HOS_CODE
		/// </summary>
		[SugarColumn(ColumnName ="HOS_CODE")]
		public string HOS_CODE { get; set; }

		/// <summary>
		/// HOS_NAME
		/// </summary>
		[SugarColumn(ColumnName ="HOS_NAME")]
		public string HOS_NAME { get; set; }

		/// <summary>
		/// HOS_INTRO
		/// </summary>
		[SugarColumn(ColumnName ="HOS_INTRO")]
		public string HOS_INTRO { get; set; }

		/// <summary>
		/// STATUS
		/// </summary>
		[SugarColumn(ColumnName ="STATUS")]
		public int STATUS { get; set; }

	}
}

