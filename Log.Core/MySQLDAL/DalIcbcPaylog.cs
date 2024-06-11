using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Core.Model;
namespace Log.Core.MySQLDAL
{
    /// <summary>
 /// 数据访问类:alipaylog
 /// </summary>
    public partial class DalIcbcPaylog
    {
       
            public DalIcbcPaylog()
            { }
            #region  Method
            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(string ORDERID, string MERID, decimal AMOUNT, string Btype, DateTime NOW, string DataSend, string DataRe)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from icbcpaylog");
                strSql.Append(" where ORDERID=@ORDERID and MERID=@MERID and AMOUNT=@AMOUNT and Btype=@Btype and NOW=@NOW and DataSend=@DataSend and DataRe=@DataRe ");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@AMOUNT", MySqlDbType.Decimal,10),
                    new MySqlParameter("@Btype", MySqlDbType.VarChar,20),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DataSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DataRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = ORDERID;
                parameters[1].Value = MERID;
                parameters[2].Value = AMOUNT;
                parameters[3].Value = Btype;
                parameters[4].Value = NOW;
                parameters[5].Value = DataSend;
                parameters[6].Value = DataRe;

                return DbHelperMySQL.Exists(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 增加一条数据
            /// </summary>
            public bool Add(icbcpaylog model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into icbcpaylog(");
                strSql.Append("ORDERID,MERID,AMOUNT,Btype,NOW,DataSend,DataRe)");
                strSql.Append(" values (");
                strSql.Append("@ORDERID,@MERID,@AMOUNT,@Btype,@NOW,@DataSend,@DataRe)");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@AMOUNT", MySqlDbType.Decimal,10),
                    new MySqlParameter("@Btype", MySqlDbType.VarChar,20),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DataSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DataRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = model.ORDERID;
                parameters[1].Value = model.MERID;
                parameters[2].Value = model.AMOUNT;
                parameters[3].Value = model.Btype;
                parameters[4].Value = model.NOW;
                parameters[5].Value = model.DataSend;
                parameters[6].Value = model.DataRe;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            /// <summary>
            /// 更新一条数据
            /// </summary>
            public bool Update(icbcpaylog model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update icbcpaylog set ");
                strSql.Append("ORDERID=@ORDERID,");
                strSql.Append("MERID=@MERID,");
                strSql.Append("AMOUNT=@AMOUNT,");
                strSql.Append("Btype=@Btype,");
                strSql.Append("NOW=@NOW,");
                strSql.Append("DataSend=@DataSend,");
                strSql.Append("DataRe=@DataRe");
                strSql.Append(" where ORDERID=@ORDERID and MERID=@MERID and AMOUNT=@AMOUNT and Btype=@Btype and NOW=@NOW and DataSend=@DataSend and DataRe=@DataRe ");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@AMOUNT", MySqlDbType.Decimal,10),
                    new MySqlParameter("@Btype", MySqlDbType.VarChar,20),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DataSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DataRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = model.ORDERID;
                parameters[1].Value = model.MERID;
                parameters[2].Value = model.AMOUNT;
                parameters[3].Value = model.Btype;
                parameters[4].Value = model.NOW;
                parameters[5].Value = model.DataSend;
                parameters[6].Value = model.DataRe;

                int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public bool Delete(string ORDERID, string MERID, decimal AMOUNT, string Btype, DateTime NOW, string DataSend, string DataRe)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from icbcpaylog ");
                strSql.Append(" where ORDERID=@ORDERID and MERID=@MERID and AMOUNT=@AMOUNT and Btype=@Btype and NOW=@NOW and DataSend=@DataSend and DataRe=@DataRe ");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@ORDERID", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MERID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@AMOUNT", MySqlDbType.Decimal,10),
                    new MySqlParameter("@Btype", MySqlDbType.VarChar,20),
                    new MySqlParameter("@NOW", MySqlDbType.DateTime),
                    new MySqlParameter("@DataSend", MySqlDbType.VarChar,1000),
                    new MySqlParameter("@DataRe", MySqlDbType.VarChar,1000)};
                parameters[0].Value = ORDERID;
                parameters[1].Value = MERID;
                parameters[2].Value = AMOUNT;
                parameters[3].Value = Btype;
                parameters[4].Value = NOW;
                parameters[5].Value = DataSend;
                parameters[6].Value = DataRe;

                int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            #endregion  Method

        }
    }
