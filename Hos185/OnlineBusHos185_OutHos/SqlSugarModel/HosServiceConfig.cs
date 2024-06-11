using System;
using SqlSugar;

namespace SqlSugarModel
{
	[SugarTable("hos_service_config")]
	public class HosServiceConfig
	{
		/// <summary>
		/// HOS_ID
		/// </summary>
		[SugarColumn(ColumnName ="HOS_ID",IsPrimaryKey=true)]
		public string HOS_ID { get; set; }

		/// <summary>
		/// callmode
		/// </summary>
		[SugarColumn(ColumnName ="callmode")]
		public string callmode { get; set; }

		/// <summary>
		/// Service_URL
		/// </summary>
		[SugarColumn(ColumnName ="Service_URL")]
		public string Service_URL { get; set; }

		/// <summary>
		/// MethodName
		/// </summary>
		[SugarColumn(ColumnName ="MethodName")]
		public string MethodName { get; set; }

		/// <summary>
		/// use_encryption
		/// </summary>
		[SugarColumn(ColumnName ="use_encryption")]
		public string use_encryption { get; set; }

		/// <summary>
		/// params
		/// </summary>
		[SugarColumn(ColumnName ="params")]
		public string Params { get; set; }

		/// <summary>
		/// xmlpath
		/// </summary>
		[SugarColumn(ColumnName ="xmlpath")]
		public string xmlpath { get; set; }

	}
}

