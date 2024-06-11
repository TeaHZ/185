using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace OnlineBusHos185_GJYB.DAL
{
    //hos_opter_sign
    public partial class hos_opter_sign
    {

        public bool Exists(string HOS_ID, string opter_no, string sign_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hos_opter_sign");
            strSql.Append(" where ");
            strSql.Append(" HOS_ID = @HOS_ID and  ");
            strSql.Append(" opter_no = @opter_no and  ");
            strSql.Append(" sign_no = @sign_no  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@sign_no", MySqlDbType.VarChar,30)          };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;
            parameters[2].Value = sign_no;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hos_opter_sign model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hos_opter_sign(");
            strSql.Append("HOS_ID,opter_no,mac,ip,sign_no,sign_intime,sign_outtime,signout_flag,yb_type");
            strSql.Append(") values (");
            strSql.Append("@HOS_ID,@opter_no,@mac,@ip,@sign_no,@sign_intime,@sign_outtime,@signout_flag,@yb_type");
            strSql.Append(") ");
            strSql.Append("ON DUPLICATE KEY UPDATE ");
            strSql.Append(" mac = @mac , ");
            strSql.Append(" ip = @ip , ");
            strSql.Append(" sign_no = @sign_no , ");
            strSql.Append(" sign_intime = @sign_intime , ");
            strSql.Append(" sign_outtime = @sign_outtime , ");
            strSql.Append(" signout_flag = @signout_flag , ");
            strSql.Append(" yb_type = @yb_type  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@opter_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@mac", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@ip", MySqlDbType.VarChar,15) ,
                        new MySqlParameter("@sign_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@sign_intime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@sign_outtime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@signout_flag", MySqlDbType.Int32,11),
                        new MySqlParameter("@yb_type", MySqlDbType.VarChar,30)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.opter_no;
            parameters[2].Value = model.mac;
            parameters[3].Value = model.ip;
            parameters[4].Value = model.sign_no;
            parameters[5].Value = model.sign_intime;
            parameters[6].Value = model.sign_outtime;
            parameters[7].Value = model.signout_flag;
            parameters[8].Value = model.YB_TYPE;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hos_opter_sign model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hos_opter_sign set ");

            strSql.Append(" HOS_ID = @HOS_ID , ");
            strSql.Append(" opter_no = @opter_no , ");
            strSql.Append(" mac = @mac , ");
            strSql.Append(" ip = @ip , ");
            strSql.Append(" sign_no = @sign_no , ");
            strSql.Append(" sign_intime = @sign_intime , ");
            strSql.Append(" sign_outtime = @sign_outtime , ");
            strSql.Append(" signout_flag = @signout_flag  ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no and sign_no=@sign_no  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@opter_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@mac", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@ip", MySqlDbType.VarChar,15) ,
                        new MySqlParameter("@sign_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@sign_intime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@sign_outtime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@signout_flag", MySqlDbType.Int32,11)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.opter_no;
            parameters[2].Value = model.mac;
            parameters[3].Value = model.ip;
            parameters[4].Value = model.sign_no;
            parameters[5].Value = model.sign_intime;
            parameters[6].Value = model.sign_outtime;
            parameters[7].Value = model.signout_flag;
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
        public bool Delete(string HOS_ID, string opter_no, string sign_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from hos_opter_sign ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no and sign_no=@sign_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@sign_no", MySqlDbType.VarChar,30)          };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;
            parameters[2].Value = sign_no;


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
        public Model.hos_opter_sign GetModel(string HOS_ID, string opter_no, string sign_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HOS_ID, opter_no, mac, ip, sign_no, sign_intime, sign_outtime, signout_flag  ");
            strSql.Append("  from hos_opter_sign ");
            strSql.Append(" where HOS_ID=@HOS_ID and opter_no=@opter_no and sign_no=@sign_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@opter_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@sign_no", MySqlDbType.VarChar,30)          };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = opter_no;
            parameters[2].Value = sign_no;


            Model.hos_opter_sign model = new Model.hos_opter_sign();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HOS_ID = ds.Tables[0].Rows[0]["HOS_ID"].ToString();
                model.opter_no = ds.Tables[0].Rows[0]["opter_no"].ToString();
                model.mac = ds.Tables[0].Rows[0]["mac"].ToString();
                model.ip = ds.Tables[0].Rows[0]["ip"].ToString();
                model.sign_no = ds.Tables[0].Rows[0]["sign_no"].ToString();
                if (ds.Tables[0].Rows[0]["sign_intime"].ToString() != "")
                {
                    model.sign_intime = DateTime.Parse(ds.Tables[0].Rows[0]["sign_intime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sign_outtime"].ToString() != "")
                {
                    model.sign_outtime = DateTime.Parse(ds.Tables[0].Rows[0]["sign_outtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["signout_flag"].ToString() != "")
                {
                    model.signout_flag = int.Parse(ds.Tables[0].Rows[0]["signout_flag"].ToString());
                }

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
            strSql.Append(" FROM hos_opter_sign ");
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
            strSql.Append(" FROM hos_opter_sign ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

