using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace OnlineBusHos185_GJYB.DAL
{
    //psn_idetinfo
    public partial class psn_idetinfo
    {

        public bool Exists(string psn_no, string psn_idet_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from psn_idetinfo");
            strSql.Append(" where ");
            strSql.Append(" psn_no = @psn_no and  ");
            strSql.Append(" psn_idet_type = @psn_idet_type  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@psn_idet_type", MySqlDbType.VarChar,3)         };
            parameters[0].Value = psn_no;
            parameters[1].Value = psn_idet_type;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_idetinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_idetinfo(");
            strSql.Append("psn_no,psn_idet_type,psn_type_lv,memo,begntime,endtime");
            strSql.Append(") values (");
            strSql.Append("@psn_no,@psn_idet_type,@psn_type_lv,@memo,@begntime,@endtime");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@psn_idet_type", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@psn_type_lv", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@memo", MySqlDbType.VarChar,500) ,
                        new MySqlParameter("@begntime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@endtime", MySqlDbType.DateTime)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.psn_idet_type;
            parameters[2].Value = model.psn_type_lv;
            parameters[3].Value = model.memo;
            parameters[4].Value = model.begntime;
            parameters[5].Value = model.endtime;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_idetinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_idetinfo set ");

            strSql.Append(" psn_no = @psn_no , ");
            strSql.Append(" psn_idet_type = @psn_idet_type , ");
            strSql.Append(" psn_type_lv = @psn_type_lv , ");
            strSql.Append(" memo = @memo , ");
            strSql.Append(" begntime = @begntime , ");
            strSql.Append(" endtime = @endtime  ");
            strSql.Append(" where psn_no=@psn_no and psn_idet_type=@psn_idet_type  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@psn_idet_type", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@psn_type_lv", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@memo", MySqlDbType.VarChar,500) ,
                        new MySqlParameter("@begntime", MySqlDbType.DateTime) ,
                        new MySqlParameter("@endtime", MySqlDbType.DateTime)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.psn_idet_type;
            parameters[2].Value = model.psn_type_lv;
            parameters[3].Value = model.memo;
            parameters[4].Value = model.begntime;
            parameters[5].Value = model.endtime;
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
        public bool Delete(string psn_no, string psn_idet_type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_idetinfo ");
            strSql.Append(" where psn_no=@psn_no and psn_idet_type=@psn_idet_type ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@psn_idet_type", MySqlDbType.VarChar,3)         };
            parameters[0].Value = psn_no;
            parameters[1].Value = psn_idet_type;


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
        public Model.psn_idetinfo GetModel(string psn_no, string psn_idet_type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psn_no, psn_idet_type, psn_type_lv, memo, begntime, endtime  ");
            strSql.Append("  from psn_idetinfo ");
            strSql.Append(" where psn_no=@psn_no and psn_idet_type=@psn_idet_type ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@psn_idet_type", MySqlDbType.VarChar,3)         };
            parameters[0].Value = psn_no;
            parameters[1].Value = psn_idet_type;


            Model.psn_idetinfo model = new Model.psn_idetinfo();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.psn_no = ds.Tables[0].Rows[0]["psn_no"].ToString();
                model.psn_idet_type = ds.Tables[0].Rows[0]["psn_idet_type"].ToString();
                model.psn_type_lv = ds.Tables[0].Rows[0]["psn_type_lv"].ToString();
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["begntime"].ToString() != "")
                {
                    model.begntime = DateTime.Parse(ds.Tables[0].Rows[0]["begntime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endtime"].ToString() != "")
                {
                    model.endtime = DateTime.Parse(ds.Tables[0].Rows[0]["endtime"].ToString());
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
            strSql.Append(" FROM psn_idetinfo ");
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
            strSql.Append(" FROM psn_idetinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

