using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("opt_pay_mx")]
	public class OptPayMx
	{
		/// <summary>
		/// PAY_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAY_ID",IsPrimaryKey=true)]
		public string PAY_ID { get; set; }

		/// <summary>
		/// ITEM_NO
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_NO")]
		public int ITEM_NO { get; set; }

		/// <summary>
		/// DAID
		/// </summary>
		[SugarColumn(ColumnName ="DAID")]
		public string DAID { get; set; }

		/// <summary>
		/// ITEM_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_TYPE")]
		public string ITEM_TYPE { get; set; }

		/// <summary>
		/// ITEM_CODE
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_CODE")]
		public string ITEM_CODE { get; set; }

		/// <summary>
		/// ITEM_NAME
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_NAME")]
		public string ITEM_NAME { get; set; }

		/// <summary>
		/// ITEM_SPEC
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_SPEC")]
		public string ITEM_SPEC { get; set; }

		/// <summary>
		/// ITEM_UNITS
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_UNITS")]
		public string ITEM_UNITS { get; set; }

		/// <summary>
		/// ITEM_PRICE
		/// </summary>
		[SugarColumn(ColumnName ="ITEM_PRICE")]
		public decimal ITEM_PRICE { get; set; }

		/// <summary>
		/// AMOUNT
		/// </summary>
		[SugarColumn(ColumnName ="AMOUNT")]
		public decimal AMOUNT { get; set; }

		/// <summary>
		/// COSTS
		/// </summary>
		[SugarColumn(ColumnName ="COSTS")]
		public decimal COSTS { get; set; }

		/// <summary>
		/// CHARGES
		/// </summary>
		[SugarColumn(ColumnName ="CHARGES")]
		public decimal CHARGES { get; set; }

		/// <summary>
		/// ZFBL
		/// </summary>
		[SugarColumn(ColumnName ="ZFBL")]
		public decimal? ZFBL { get; set; }

	}
}

