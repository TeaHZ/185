using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace OnlineBusHos185_GJYB.DAL
{
    //psn_insuinfo
    public partial class psn_insuinfo
    {

        public bool Exists(string psn_no, string insutype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from psn_insuinfo");
            strSql.Append(" where ");
            strSql.Append(" psn_no = @psn_no and  ");
            strSql.Append(" insutype = @insutype  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@insutype", MySqlDbType.VarChar,6)          };
            parameters[0].Value = psn_no;
            parameters[1].Value = insutype;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_insuinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_insuinfo(");
            strSql.Append("psn_no,insutype,balc,psn_type,psn_insu_stas,psn_insu_date,paus_insu_date,cvlserv_flag,insuplc_admdvs,emp_name,status");
            strSql.Append(") values (");
            strSql.Append("@psn_no,@insutype,@balc,@psn_type,@psn_insu_stas,@psn_insu_date,@paus_insu_date,@cvlserv_flag,@insuplc_admdvs,@emp_name,@status");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@insutype", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@balc", MySqlDbType.Decimal,16) ,
                        new MySqlParameter("@psn_type", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@psn_insu_stas", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@psn_insu_date", MySqlDbType.Date) ,
                        new MySqlParameter("@paus_insu_date", MySqlDbType.Date) ,
                        new MySqlParameter("@cvlserv_flag", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@insuplc_admdvs", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@emp_name", MySqlDbType.VarChar,200) ,
                        new MySqlParameter("@status", MySqlDbType.Bit)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.insutype;
            parameters[2].Value = model.balc;
            parameters[3].Value = model.psn_type;
            parameters[4].Value = model.psn_insu_stas;
            parameters[5].Value = model.psn_insu_date;
            parameters[6].Value = model.paus_insu_date;
            parameters[7].Value = model.cvlserv_flag;
            parameters[8].Value = model.insuplc_admdvs;
            parameters[9].Value = model.emp_name;
            parameters[10].Value = model.status;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_insuinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_insuinfo set ");

            strSql.Append(" psn_no = @psn_no , ");
            strSql.Append(" insutype = @insutype , ");
            strSql.Append(" balc = @balc , ");
            strSql.Append(" psn_type = @psn_type , ");
            strSql.Append(" psn_insu_stas = @psn_insu_stas , ");
            strSql.Append(" psn_insu_date = @psn_insu_date , ");
            strSql.Append(" paus_insu_date = @paus_insu_date , ");
            strSql.Append(" cvlserv_flag = @cvlserv_flag , ");
            strSql.Append(" insuplc_admdvs = @insuplc_admdvs , ");
            strSql.Append(" emp_name = @emp_name , ");
            strSql.Append(" status = @status  ");
            strSql.Append(" where psn_no=@psn_no and insutype=@insutype  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@insutype", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@balc", MySqlDbType.Decimal,16) ,
                        new MySqlParameter("@psn_type", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@psn_insu_stas", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@psn_insu_date", MySqlDbType.Date) ,
                        new MySqlParameter("@paus_insu_date", MySqlDbType.Date) ,
                        new MySqlParameter("@cvlserv_flag", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@insuplc_admdvs", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@emp_name", MySqlDbType.VarChar,200) ,
                        new MySqlParameter("@status", MySqlDbType.Bit)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.insutype;
            parameters[2].Value = model.balc;
            parameters[3].Value = model.psn_type;
            parameters[4].Value = model.psn_insu_stas;
            parameters[5].Value = model.psn_insu_date;
            parameters[6].Value = model.paus_insu_date;
            parameters[7].Value = model.cvlserv_flag;
            parameters[8].Value = model.insuplc_admdvs;
            parameters[9].Value = model.emp_name;
            parameters[10].Value = model.status;
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
        public bool Delete(string psn_no, string insutype)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_insuinfo ");
            strSql.Append(" where psn_no=@psn_no and insutype=@insutype ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@insutype", MySqlDbType.VarChar,6)          };
            parameters[0].Value = psn_no;
            parameters[1].Value = insutype;


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
        public Model.psn_insuinfo GetModel(string psn_no, string insutype)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psn_no, insutype, balc, psn_type, psn_insu_stas, psn_insu_date, paus_insu_date, cvlserv_flag, insuplc_admdvs, emp_name, status  ");
            strSql.Append("  from psn_insuinfo ");
            strSql.Append(" where psn_no=@psn_no and insutype=@insutype ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30),
                    new MySqlParameter("@insutype", MySqlDbType.VarChar,6)          };
            parameters[0].Value = psn_no;
            parameters[1].Value = insutype;


            Model.psn_insuinfo model = new Model.psn_insuinfo();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.psn_no = ds.Tables[0].Rows[0]["psn_no"].ToString();
                model.insutype = ds.Tables[0].Rows[0]["insutype"].ToString();
                if (ds.Tables[0].Rows[0]["balc"].ToString() != "")
                {
                    model.balc = decimal.Parse(ds.Tables[0].Rows[0]["balc"].ToString());
                }
                model.psn_type = ds.Tables[0].Rows[0]["psn_type"].ToString();
                model.psn_insu_stas = ds.Tables[0].Rows[0]["psn_insu_stas"].ToString();
                if (ds.Tables[0].Rows[0]["psn_insu_date"].ToString() != "")
                {
                    model.psn_insu_date = DateTime.Parse(ds.Tables[0].Rows[0]["psn_insu_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["paus_insu_date"].ToString() != "")
                {
                    model.paus_insu_date = DateTime.Parse(ds.Tables[0].Rows[0]["paus_insu_date"].ToString());
                }
                model.cvlserv_flag = ds.Tables[0].Rows[0]["cvlserv_flag"].ToString();
                model.insuplc_admdvs = ds.Tables[0].Rows[0]["insuplc_admdvs"].ToString();
                model.emp_name = ds.Tables[0].Rows[0]["emp_name"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["status"].ToString() == "1") || (ds.Tables[0].Rows[0]["status"].ToString().ToLower() == "true"))
                    {
                        model.status = true;
                    }
                    else
                    {
                        model.status = false;
                    }
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据psn_no取参保信息
        /// </summary>
        public Model.psn_insuinfo GetInsuinfo(string psn_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psn_no, insutype, balc, psn_type, psn_insu_stas, psn_insu_date, paus_insu_date, cvlserv_flag, insuplc_admdvs, emp_name, status  ");
            strSql.Append("  from psn_insuinfo ");
            strSql.Append(" where psn_no=@psn_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30)          };
            parameters[0].Value = psn_no;

            Model.psn_insuinfo model = new Model.psn_insuinfo();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.psn_no = ds.Tables[0].Rows[0]["psn_no"].ToString();
                model.insutype = ds.Tables[0].Rows[0]["insutype"].ToString();
                if (ds.Tables[0].Rows[0]["balc"].ToString() != "")
                {
                    model.balc = decimal.Parse(ds.Tables[0].Rows[0]["balc"].ToString());
                }
                model.psn_type = ds.Tables[0].Rows[0]["psn_type"].ToString();
                model.psn_insu_stas = ds.Tables[0].Rows[0]["psn_insu_stas"].ToString();
                if (ds.Tables[0].Rows[0]["psn_insu_date"].ToString() != "")
                {
                    model.psn_insu_date = DateTime.Parse(ds.Tables[0].Rows[0]["psn_insu_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["paus_insu_date"].ToString() != "")
                {
                    model.paus_insu_date = DateTime.Parse(ds.Tables[0].Rows[0]["paus_insu_date"].ToString());
                }
                model.cvlserv_flag = ds.Tables[0].Rows[0]["cvlserv_flag"].ToString();
                model.insuplc_admdvs = ds.Tables[0].Rows[0]["insuplc_admdvs"].ToString();
                model.emp_name = ds.Tables[0].Rows[0]["emp_name"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["status"].ToString() == "1") || (ds.Tables[0].Rows[0]["status"].ToString().ToLower() == "true"))
                    {
                        model.status = true;
                    }
                    else
                    {
                        model.status = false;
                    }
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
            strSql.Append(" FROM psn_insuinfo ");
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
            strSql.Append(" FROM psn_insuinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

