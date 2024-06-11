using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("pat_card")]
	public class PatCard
	{
		/// <summary>
		/// PAT_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAT_ID",IsPrimaryKey=true)]
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
		/// CREATE_TIME
		/// </summary>
		[SugarColumn(ColumnName ="CREATE_TIME")]
		public DateTime CREATE_TIME { get; set; }

		/// <summary>
		/// MARK_DEL
		/// </summary>
		[SugarColumn(ColumnName ="MARK_DEL")]
		public string MARK_DEL { get; set; }

		/// <summary>
		/// DEL_TIME
		/// </summary>
		[SugarColumn(ColumnName ="DEL_TIME")]
		public DateTime? DEL_TIME { get; set; }

	}
}

