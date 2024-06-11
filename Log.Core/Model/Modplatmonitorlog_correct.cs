using System;

namespace Log.Core.Model
{
	/// <summary>
	/// platmonitorlog_correct:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Modplatmonitorlog_correct
    {
		public Modplatmonitorlog_correct()
		{}
		#region Model
		private int _id;
		private string _hos_id;
		private string _pat_name;
		private string _sfz_no;
		private string _m_type;
		private DateTime _acc_time;
		private DateTime _now;
		private string _deal_result;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HOS_ID
		{
			set{ _hos_id=value;}
			get{return _hos_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PAT_NAME
		{
			set{ _pat_name=value;}
			get{return _pat_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SFZ_NO
		{
			set{ _sfz_no=value;}
			get{return _sfz_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string M_TYPE
		{
			set{ _m_type=value;}
			get{return _m_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ACC_TIME
		{
			set{ _acc_time=value;}
			get{return _acc_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime NOW
		{
			set{ _now=value;}
			get{return _now;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEAL_RESULT
		{
			set{ _deal_result=value;}
			get{return _deal_result;}
		}
		#endregion Model

	}
}

