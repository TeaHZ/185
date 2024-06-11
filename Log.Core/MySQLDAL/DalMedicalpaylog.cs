using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Log.Core.Model;
using System.Configuration;
namespace Log.Core.MySQLDAL
{
	/// <summary>
    /// 数据访问类:ModMedicalpaylog
	/// </summary>
	public partial class DalMedicalpaylog
	{
		public DalMedicalpaylog()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(ModMedicalpaylog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into medicalpaylog(");
			strSql.Append("HOS_ID,tranNum,cardNum,retCode,NOW,PLAT_LSH,JE,SENDDATA,RECEIVEDATA,SENDYBDATA,RECEIVEYBDATA)");
			strSql.Append(" values (");
			strSql.Append("@HOS_ID,@tranNum,@cardNum,@retCode,@NOW,@PLAT_LSH,@JE,@SENDDATA,@RECEIVEDATA,@SENDYBDATA,@RECEIVEYBDATA)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
					new MySqlParameter("@tranNum", MySqlDbType.VarChar,10),
					new MySqlParameter("@cardNum", MySqlDbType.VarChar,20),
					new MySqlParameter("@retCode", MySqlDbType.VarChar,10),
					new MySqlParameter("@NOW", MySqlDbType.DateTime),
					new MySqlParameter("@PLAT_LSH", MySqlDbType.VarChar,30),
					new MySqlParameter("@JE", MySqlDbType.Decimal,10),
					new MySqlParameter("@SENDDATA", MySqlDbType.Text  ),
					new MySqlParameter("@RECEIVEDATA", MySqlDbType.Text),
					new MySqlParameter("@SENDYBDATA", MySqlDbType.Text),
					new MySqlParameter("@RECEIVEYBDATA", MySqlDbType.Text)};
			parameters[0].Value = model.HOS_ID;
			parameters[1].Value = model.tranNum;
			parameters[2].Value = model.cardNum;
			parameters[3].Value = model.retCode;
			parameters[4].Value = model.NOW;
			parameters[5].Value = model.PLAT_LSH;
			parameters[6].Value = model.JE;
			parameters[7].Value = model.SENDDATA;
			parameters[8].Value = model.RECEIVEDATA;
			parameters[9].Value = model.SENDYBDATA;
			parameters[10].Value = model.RECEIVEYBDATA;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

