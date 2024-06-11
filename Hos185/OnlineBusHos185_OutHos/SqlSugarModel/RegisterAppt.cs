using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("register_appt")]
	public class RegisterAppt
	{
		/// <summary>
		/// REG_ID
		/// </summary>
		[SugarColumn(ColumnName ="REG_ID",IsPrimaryKey=true)]
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
		/// DEPT_CODE
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_CODE")]
		public string DEPT_CODE { get; set; }

		/// <summary>
		/// DEPT_NAME
		/// </summary>
		[SugarColumn(ColumnName ="DEPT_NAME")]
		public string DEPT_NAME { get; set; }

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
		public string SCH_TYPE { get; set; }

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
		/// TIME_POINT
		/// </summary>
		[SugarColumn(ColumnName ="TIME_POINT")]
		public string TIME_POINT { get; set; }

		/// <summary>
		/// REGISTER_TYPE
		/// </summary>
		[SugarColumn(ColumnName ="REGISTER_TYPE")]
		public string REGISTER_TYPE { get; set; }

		/// <summary>
		/// IS_FZ
		/// </summary>
		[SugarColumn(ColumnName ="IS_FZ")]
		public bool IS_FZ { get; set; }

		/// <summary>
		/// ZL_FEE
		/// </summary>
		[SugarColumn(ColumnName ="ZL_FEE")]
		public decimal ZL_FEE { get; set; }

		/// <summary>
		/// GH_FEE
		/// </summary>
		[SugarColumn(ColumnName ="GH_FEE")]
		public decimal GH_FEE { get; set; }

		/// <summary>
		/// APPT_PAY
		/// </summary>
		[SugarColumn(ColumnName ="APPT_PAY")]
		public decimal APPT_PAY { get; set; }

		/// <summary>
		/// APPT_DATE
		/// </summary>
		[SugarColumn(ColumnName ="APPT_DATE")]
		public DateTime APPT_DATE { get; set; }

		/// <summary>
		/// APPT_TIME
		/// </summary>
		[SugarColumn(ColumnName ="APPT_TIME")]
		public string APPT_TIME { get; set; }

		/// <summary>
		/// APPT_SN
		/// </summary>
		[SugarColumn(ColumnName ="APPT_SN")]
		public string APPT_SN { get; set; }

		/// <summary>
		/// HOS_SN
		/// </summary>
		[SugarColumn(ColumnName ="HOS_SN")]
		public string HOS_SN { get; set; }

		/// <summary>
		/// APPT_ORDER
		/// </summary>
		[SugarColumn(ColumnName ="APPT_ORDER")]
		public string APPT_ORDER { get; set; }

		/// <summary>
		/// ZS_NAME
		/// </summary>
		[SugarColumn(ColumnName ="ZS_NAME")]
		public string ZS_NAME { get; set; }

		/// <summary>
		/// 0未支付 1已支付  2已失效/过期 3已退费 4已就诊 5已取消（患者主动取消)
		/// </summary>
		[SugarColumn(ColumnName ="APPT_TYPE")]
		public string APPT_TYPE { get; set; }

		/// <summary>
		/// HOSPATID
		/// </summary>
		[SugarColumn(ColumnName ="HOSPATID")]
		public string HOSPATID { get; set; }

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
		/// CANCEL_DATE
		/// </summary>
		[SugarColumn(ColumnName = "CANCEL_DATE")]
		public DateTime CANCEL_DATE { get; set; }

		/// <summary>
		/// CANCEL_TIME
		/// </summary>
		[SugarColumn(ColumnName = "CANCEL_TIME")]
		public string CANCEL_TIME { get; set; }

	}
}

