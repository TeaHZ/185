using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("doc_info")]
	public class DocInfo
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
		/// DOC_NAME
		/// </summary>
		[SugarColumn(ColumnName ="DOC_NAME")]
		public string DOC_NAME { get; set; }

		/// <summary>
		/// IS_EXPERT
		/// </summary>
		[SugarColumn(ColumnName ="IS_EXPERT")]
		public bool IS_EXPERT { get; set; }

		/// <summary>
		/// PRO_TITLE
		/// </summary>
		[SugarColumn(ColumnName ="PRO_TITLE")]
		public string PRO_TITLE { get; set; }

		/// <summary>
		/// DOC_INTRO
		/// </summary>
		[SugarColumn(ColumnName ="DOC_INTRO")]
		public string DOC_INTRO { get; set; }

		/// <summary>
		/// DOC_ORDER
		/// </summary>
		[SugarColumn(ColumnName ="DOC_ORDER")]
		public string DOC_ORDER { get; set; }

		/// <summary>
		/// ADD_TIME
		/// </summary>
		[SugarColumn(ColumnName ="ADD_TIME")]
		public DateTime ADD_TIME { get; set; }

		/// <summary>
		/// STOP_TIME
		/// </summary>
		[SugarColumn(ColumnName ="STOP_TIME")]
		public DateTime? STOP_TIME { get; set; }

		/// <summary>
		/// DOC_SKILLED
		/// </summary>
		[SugarColumn(ColumnName ="DOC_SKILLED")]
		public string DOC_SKILLED { get; set; }

		/// <summary>
		/// SPRECHSTUNDE
		/// </summary>
		[SugarColumn(ColumnName ="SPRECHSTUNDE")]
		public string SPRECHSTUNDE { get; set; }

		/// <summary>
		/// IMAGE_URL
		/// </summary>
		[SugarColumn(ColumnName ="IMAGE_URL")]
		public string IMAGE_URL { get; set; }

		/// <summary>
		/// WB
		/// </summary>
		[SugarColumn(ColumnName ="WB")]
		public string WB { get; set; }

		/// <summary>
		/// PY
		/// </summary>
		[SugarColumn(ColumnName ="PY")]
		public string PY { get; set; }

	}
}

