using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("pat_card_bind")]
	public class PatCardBind
	{
		/// <summary>
		/// HOS_ID
		/// </summary>
		[SugarColumn(ColumnName ="HOS_ID",IsPrimaryKey=true)]
		public string HOS_ID { get; set; }

		/// <summary>
		/// PAT_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAT_ID")]
		public int PAT_ID { get; set; }

		/// <summary>
		/// YLCARD_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="YLCARD_TYPE")]
		public int YLCARD_TYPE { get; set; }

		/// <summary>
		/// YLCARD_NO
		/// </summary>
		[SugarColumn(ColumnName ="YLCARD_NO")]
		public string YLCARD_NO { get; set; }

		/// <summary>
		/// HOSPATID
		/// </summary>
		[SugarColumn(ColumnName ="HOSPATID")]
		public string HOSPATID { get; set; }

		/// <summary>
		/// MARK_BIND
		/// </summary>
		[SugarColumn(ColumnName ="MARK_BIND")]
		public int MARK_BIND { get; set; }

		/// <summary>
		/// BAND_TIME
		/// </summary>
		[SugarColumn(ColumnName ="BAND_TIME")]
		public DateTime? BAND_TIME { get; set; }

	}
}

