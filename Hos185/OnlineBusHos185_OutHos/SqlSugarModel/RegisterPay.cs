using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("register_pay")]
	public class RegisterPay
	{
		/// <summary>
		/// PAY_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAY_ID",IsPrimaryKey=true)]
		public int PAY_ID { get; set; }

		/// <summary>
		/// REG_ID
		/// </summary>
		[SugarColumn(ColumnName ="REG_ID")]
		public int REG_ID { get; set; }

		/// <summary>
		/// HOS_ID
		/// </summary>
		[SugarColumn(ColumnName ="HOS_ID")]
		public string HOS_ID { get; set; }

		/// <summary>
		/// PAT_ID
		/// </summary>
		[SugarColumn(ColumnName ="PAT_ID")]
		public int PAT_ID { get; set; }

		/// <summary>
		/// CHARGE_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="CHARGE_TYPE")]
		public string CHARGE_TYPE { get; set; }

		/// <summary>
		/// QUERYID
		/// </summary>
		[SugarColumn(ColumnName ="QUERYID")]
		public string QUERYID { get; set; }

		/// <summary>
		/// DEAL_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="DEAL_TYPE")]
		public string DEAL_TYPE { get; set; }

		/// <summary>
		/// SUM_JE
		/// </summary>
		[SugarColumn(ColumnName ="SUM_JE")]
		public decimal SUM_JE { get; set; }

		/// <summary>
		/// CASH_JE
		/// </summary>
		[SugarColumn(ColumnName ="CASH_JE")]
		public decimal CASH_JE { get; set; }

		/// <summary>
		/// ACCT_JE
		/// </summary>
		[SugarColumn(ColumnName ="ACCT_JE")]
		public decimal? ACCT_JE { get; set; }

		/// <summary>
		/// FUND_JE
		/// </summary>
		[SugarColumn(ColumnName ="FUND_JE")]
		public decimal? FUND_JE { get; set; }

		/// <summary>
		/// OTHER_JE
		/// </summary>
		[SugarColumn(ColumnName ="OTHER_JE")]
		public decimal? OTHER_JE { get; set; }

		/// <summary>
		/// HOS_SN
		/// </summary>
		[SugarColumn(ColumnName ="HOS_SN")]
		public string HOS_SN { get; set; }

		/// <summary>
		/// OPT_SN
		/// </summary>
		[SugarColumn(ColumnName ="OPT_SN")]
		public string OPT_SN { get; set; }

		/// <summary>
		/// PRE_NO
		/// </summary>
		[SugarColumn(ColumnName ="PRE_NO")]
		public string PRE_NO { get; set; }

		/// <summary>
		/// RCPT_NO
		/// </summary>
		[SugarColumn(ColumnName ="RCPT_NO")]
		public string RCPT_NO { get; set; }

		/// <summary>
		/// DJ_DATE
		/// </summary>
		[SugarColumn(ColumnName ="DJ_DATE")]
		public DateTime DJ_DATE { get; set; }

		/// <summary>
		/// DJ_TIME
		/// </summary>
		[SugarColumn(ColumnName ="DJ_TIME")]
		public string DJ_TIME { get; set; }

		/// <summary>
		/// USER_ID
		/// </summary>
		[SugarColumn(ColumnName ="USER_ID")]
		public string USER_ID { get; set; }

		/// <summary>
		/// SOURCE
		/// </summary>
		[SugarColumn(ColumnName ="SOURCE")]
		public string SOURCE { get; set; }

		/// <summary>
		/// lTERMINAL_SN
		/// </summary>
		[SugarColumn(ColumnName ="lTERMINAL_SN")]
		public string lTERMINAL_SN { get; set; }

		/// <summary>
		/// IS_TH
		/// </summary>
		[SugarColumn(ColumnName ="IS_TH")]
		public bool IS_TH { get; set; }

		/// <summary>
		/// TH_DATE
		/// </summary>
		[SugarColumn(ColumnName ="TH_DATE")]
		public DateTime? TH_DATE { get; set; }

		/// <summary>
		/// TH_TIME
		/// </summary>
		[SugarColumn(ColumnName ="TH_TIME")]
		public string TH_TIME { get; set; }

		/// <summary>
		/// TH_USER_ID
		/// </summary>
		[SugarColumn(ColumnName ="TH_USER_ID")]
		public string TH_USER_ID { get; set; }

		/// <summary>
		/// TH_SOURCE
		/// </summary>
		[SugarColumn(ColumnName ="TH_SOURCE")]
		public string TH_SOURCE { get; set; }

		/// <summary>
		/// TH_lTERMINAL_SN
		/// </summary>
		[SugarColumn(ColumnName ="TH_lTERMINAL_SN")]
		public string TH_lTERMINAL_SN { get; set; }

	}
}

