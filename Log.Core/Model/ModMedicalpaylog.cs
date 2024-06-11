using System;
namespace Log.Core.Model
{
	/// <summary>
	/// medicalpaylog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ModMedicalpaylog
	{
        public ModMedicalpaylog()
		{}
		#region Model
		private string _hos_id;
		private string _trannum;
		private string _cardnum;
		private string _retcode;
		private DateTime? _now;
		private string _plat_lsh;
		private decimal? _je;
		private string _senddata;
		private string _receivedata;
		private string _sendybdata;
		private string _receiveybdata;
		/// <summary>
		/// 医院代码
		/// </summary>
		public string HOS_ID
		{
			set{ _hos_id=value;}
			get{return _hos_id;}
		}
		/// <summary>
		/// 交易编号
		/// </summary>
		public string tranNum
		{
			set{ _trannum=value;}
			get{return _trannum;}
		}
		/// <summary>
		/// 卡号
		/// </summary>
		public string cardNum
		{
			set{ _cardnum=value;}
			get{return _cardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string retCode
		{
			set{ _retcode=value;}
			get{return _retcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? NOW
		{
			set{ _now=value;}
			get{return _now;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLAT_LSH
		{
			set{ _plat_lsh=value;}
			get{return _plat_lsh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? JE
		{
			set{ _je=value;}
			get{return _je;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SENDDATA
		{
			set{ _senddata=value;}
			get{return _senddata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RECEIVEDATA
		{
			set{ _receivedata=value;}
			get{return _receivedata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SENDYBDATA
		{
			set{ _sendybdata=value;}
			get{return _sendybdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RECEIVEYBDATA
		{
			set{ _receiveybdata=value;}
			get{return _receiveybdata;}
		}
		#endregion Model

	}
}

