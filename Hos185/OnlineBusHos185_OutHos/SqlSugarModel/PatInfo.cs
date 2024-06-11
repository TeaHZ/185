using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("pat_info")]
	public class PatInfo
	{
		/// <summary>
		/// PAT_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAT_ID",IsPrimaryKey=true)]
		public int PAT_ID { get; set; }

		/// <summary>
		/// SFZ_NO
		/// </summary>
		[SugarColumn(ColumnName ="SFZ_NO")]
		public string SFZ_NO { get; set; }

		/// <summary>
		/// PAT_NAME
		/// </summary>
		[SugarColumn(ColumnName ="PAT_NAME")]
		public string PAT_NAME { get; set; }

		/// <summary>
		/// SEX
		/// </summary>
		[SugarColumn(ColumnName ="SEX")]
		public string SEX { get; set; }

		/// <summary>
		/// BIRTHDAY
		/// </summary>
		[SugarColumn(ColumnName ="BIRTHDAY")]
		public string BIRTHDAY { get; set; }

		/// <summary>
		/// ADDRESS
		/// </summary>
		[SugarColumn(ColumnName ="ADDRESS")]
		public string ADDRESS { get; set; }

		/// <summary>
		/// MOBILE_NO
		/// </summary>
		[SugarColumn(ColumnName ="MOBILE_NO")]
		public string MOBILE_NO { get; set; }

		/// <summary>
		/// GUARDIAN_NAME
		/// </summary>
		[SugarColumn(ColumnName ="GUARDIAN_NAME")]
		public string GUARDIAN_NAME { get; set; }

		/// <summary>
		/// GUARDIAN_SFZ_NO
		/// </summary>
		[SugarColumn(ColumnName ="GUARDIAN_SFZ_NO")]
		public string GUARDIAN_SFZ_NO { get; set; }

		/// <summary>
		/// CREATE_TIME
		/// </summary>
		[SugarColumn(ColumnName ="CREATE_TIME")]
		public DateTime CREATE_TIME { get; set; }

		/// <summary>
		/// MARK_DEL
		/// </summary>
		[SugarColumn(ColumnName ="MARK_DEL")]
		public bool MARK_DEL { get; set; }

		/// <summary>
		/// DEL_TIME
		/// </summary>
		[SugarColumn(ColumnName ="DEL_TIME")]
		public DateTime? DEL_TIME { get; set; }

		/// <summary>
		/// OPER_TIME
		/// </summary>
		[SugarColumn(ColumnName ="OPER_TIME")]
		public DateTime OPER_TIME { get; set; }

		/// <summary>
		/// NOTE
		/// </summary>
		[SugarColumn(ColumnName ="NOTE")]
		public string NOTE { get; set; }

	}
}

