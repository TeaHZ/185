using DB.Core;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace OnlineBusHos9_GJYB.DAL
{
    //hos_opter
    public partial class hos_opter
    {

        public bool Exists(string HOS_ID, string opter_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hos_opter");
            strSql.Append(" where ");
            strSql.Append(" HOS_ID = @HOS_ID and  ");
            strSql.Append(" opter_no = @opter_no  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30)         };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hos_opter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hos_opter(");
            strSql.Append("HOS_ID,opter_type,opter_no,opter_name,sign_no,sign_date,mac,ip");
            strSql.Append(") values (");
            strSql.Append("@HOS_ID,@opter_type,@opter_no,@opter_name,@sign_no,@sign_date,@mac,@ip");
            strSql.Append(") ");
            strSql.Append(" ON DUPLICATE KEY UPDATE ");
            strSql.Append(" opter_type = @opter_type , ");
            strSql.Append(" opter_no = @opter_no , ");
            strSql.Append(" opter_name = @opter_name  ");
            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@opter_type", MySqlDbType.Int32,11) ,
                        new MySqlParameter("@opter_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@opter_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@sign_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@sign_date", MySqlDbType.VarChar,10),
                        new MySqlParameter("@mac", MySqlDbType.VarChar,50),
                        new MySqlParameter("@ip", MySqlDbType.VarChar,50)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.opter_type;
            parameters[2].Value = model.opter_no;
            parameters[3].Value = model.opter_name;
            parameters[4].Value = model.sign_no;
            parameters[5].Value = model.sign_date;
            parameters[6].Value = model.mac;
            parameters[7].Value = model.ip;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hos_opter model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hos_opter set ");

            strSql.Append(" HOS_ID = @HOS_ID , ");
            strSql.Append(" opter_type = @opter_type , ");
            strSql.Append(" opter_no = @opter_no , ");
            strSql.Append(" opter_name = @opter_name , ");
            strSql.Append(" sign_no = @sign_no , ");
            strSql.Append(" sign_date = @sign_date ,  ");
            strSql.Append(" mac = @mac ,  ");
            strSql.Append(" ip = @ip  ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@opter_type", MySqlDbType.Int32,11) ,
                        new MySqlParameter("@opter_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@opter_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@sign_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@sign_date", MySqlDbType.VarChar,10),
                        new MySqlParameter("@mac", MySqlDbType.VarChar,50),
                        new MySqlParameter("@ip", MySqlDbType.VarChar,50)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.opter_type;
            parameters[2].Value = model.opter_no;
            parameters[3].Value = model.opter_name;
            parameters[4].Value = model.sign_no;
            parameters[5].Value = model.sign_date;
            parameters[6].Value = model.mac;
            parameters[7].Value = model.ip;
            int rows = DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string HOS_ID, string opter_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from hos_opter ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30)         };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;


            int rows = DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.hos_opter GetModel(string HOS_ID, string opter_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HOS_ID, opter_type, opter_no, opter_name, sign_no, sign_date , mac,ip ");
            strSql.Append("  from hos_opter ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30)         };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;


            Model.hos_opter model = new Model.hos_opter();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HOS_ID = ds.Tables[0].Rows[0]["HOS_ID"].ToString();
                if (ds.Tables[0].Rows[0]["opter_type"].ToString() != "")
                {
                    model.opter_type = int.Parse(ds.Tables[0].Rows[0]["opter_type"].ToString());
                }
                model.opter_no = ds.Tables[0].Rows[0]["opter_no"].ToString();
                model.opter_name = ds.Tables[0].Rows[0]["opter_name"].ToString();
                model.sign_no = ds.Tables[0].Rows[0]["sign_no"].ToString();
                model.sign_date = ds.Tables[0].Rows[0]["sign_date"].ToString();
                model.mac = ds.Tables[0].Rows[0]["mac"].ToString();
                model.ip = ds.Tables[0].Rows[0]["ip"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM hos_opter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM hos_opter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

