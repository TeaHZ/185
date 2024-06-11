using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("dept_info")]
	public class DeptInfo
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
		/// DEPT_CODE_ESB
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_CODE_ESB")]
		public string DEPT_CODE_ESB { get; set; }

		/// <summary>
		/// DEPT_NAME
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_NAME")]
		public string DEPT_NAME { get; set; }

		/// <summary>
		/// DEPT_INTRO
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_INTRO")]
		public string DEPT_INTRO { get; set; }

		/// <summary>
		/// DEPT_URL
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_URL")]
		public string DEPT_URL { get; set; }

		/// <summary>
		/// DEPT_TEL
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_TEL")]
		public string DEPT_TEL { get; set; }

		/// <summary>
		/// DEPT_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_TYPE")]
		public string DEPT_TYPE { get; set; }

		/// <summary>
		/// DEPT_ORDER
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_ORDER")]
		public string DEPT_ORDER { get; set; }

		/// <summary>
		/// DEPT_ADDRESS
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_ADDRESS")]
		public string DEPT_ADDRESS { get; set; }

		/// <summary>
		/// DEPT_USE
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_USE")]
		public string DEPT_USE { get; set; }

		/// <summary>
		/// ADD_DATE
		/// </summary>
		[SugarColumn(ColumnName ="ADD_DATE")]
		public DateTime? ADD_DATE { get; set; }

		/// <summary>
		/// STOP_DATE
		/// </summary>
		[SugarColumn(ColumnName ="STOP_DATE")]
		public DateTime? STOP_DATE { get; set; }

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

		/// <summary>
		/// ZL_FEE
		/// </summary>
		[SugarColumn(ColumnName ="ZL_FEE")]
		public decimal? ZL_FEE { get; set; }

		/// <summary>
		/// GH_FEE
		/// </summary>
		[SugarColumn(ColumnName ="GH_FEE")]
		public decimal? GH_FEE { get; set; }

		/// <summary>
		/// DEPT_SUP_CODE
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_SUP_CODE")]
		public string DEPT_SUP_CODE { get; set; }

		/// <summary>
		/// DEPT_SUP_NAME
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_SUP_NAME")]
		public string DEPT_SUP_NAME { get; set; }

		/// <summary>
		/// REGFLAG
		/// </summary>
		[SugarColumn(ColumnName ="REGFLAG")]
		public string REGFLAG { get; set; }

		/// <summary>
		/// SEX_LIMIT
		/// </summary>
		[SugarColumn(ColumnName ="SEX_LIMIT")]
		public string SEX_LIMIT { get; set; }

		/// <summary>
		/// AGE_LIMIT
		/// </summary>
		[SugarColumn(ColumnName ="AGE_LIMIT")]
		public string AGE_LIMIT { get; set; }

	}
}

