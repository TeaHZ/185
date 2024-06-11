using System;
 
namespace Log.Core.Model
{
	/// <summary>
	/// unionpaylog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ModUnionpaylog
	{
        public ModUnionpaylog()
		{}
		#region Model
		private string _orderid;
		private string _merid;
		private string _txntime;
		private string _respcode;
		private string _queryid;
		private DateTime _now;
		private string _btype;
		private string _datere;
		private string _datesend;
		/// <summary>
		/// 
		/// </summary>
		public string orderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string txnTime
		{
			set{ _txntime=value;}
			get{return _txntime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string respCode
		{
			set{ _respcode=value;}
			get{return _respcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string queryId
		{
			set{ _queryid=value;}
			get{return _queryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime now
		{
			set{ _now=value;}
			get{return _now;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BType
		{
			set{ _btype=value;}
			get{return _btype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DateRe
		{
			set{ _datere=value;}
			get{return _datere;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DateSend
		{
			set{ _datesend=value;}
			get{return _datesend;}
		}
		#endregion Model

	}
}

