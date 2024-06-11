using DB.Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnlineBusHos185_GJYB.DAL
{
    //psn_baseinfo
    public partial class psn_baseinfo
    {

        public bool Exists(string psn_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from psn_baseinfo");
            strSql.Append(" where ");
            strSql.Append(" psn_no = @psn_no  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30)           };
            parameters[0].Value = psn_no;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }

        public void SavePsnInfo(Model.psn_baseinfo psn_baseinfo, List<Model.psn_idetinfo> psn_idetinfos, List<Model.psn_insuinfo> psn_insuinfos)
        {
            Hashtable hashtable = new Hashtable();
            {
                Model.psn_baseinfo model = psn_baseinfo;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into psn_baseinfo(");
                strSql.Append("psn_no,psn_cert_type,certno,psn_name,gend,naty,brdy,expContent,Sync_flag");
                strSql.Append(") values (");
                strSql.Append("@psn_no,@psn_cert_type,@certno,@psn_name,@gend,@naty,@brdy,@expContent,@Sync_flag");
                strSql.Append(") ");
                strSql.Append("ON DUPLICATE KEY UPDATE ");
                strSql.Append(" psn_no = @psn_no , ");
                strSql.Append(" psn_cert_type = @psn_cert_type , ");
                strSql.Append(" certno = @certno , ");
                strSql.Append(" psn_name = @psn_name , ");
                strSql.Append(" gend = @gend , ");
                strSql.Append(" naty = @naty , ");
                strSql.Append(" brdy = @brdy , ");
                strSql.Append(" expContent = @expContent , ");
                strSql.Append(" Sync_flag = @Sync_flag  ");

                MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@psn_cert_type", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@certno", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@psn_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@gend", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@naty", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@brdy", MySqlDbType.Date) ,
                        new MySqlParameter("@expContent", MySqlDbType.VarChar,4000) ,
                        new MySqlParameter("@Sync_flag", MySqlDbType.Bit)

            };

                parameters[0].Value = model.psn_no;
                parameters[1].Value = model.psn_cert_type;
                parameters[2].Value = model.certno;
                parameters[3].Value = model.psn_name;
                parameters[4].Value = model.gend;
                parameters[5].Value = model.naty;
                parameters[6].Value = model.brdy;
                parameters[7].Value = model.expContent;
                parameters[8].Value = model.Sync_flag;
                hashtable.Add(strSql.ToString(), parameters);
            }
            for (int i = 0; i < psn_idetinfos.Count; i++)
            {
                Model.psn_idetinfo model = psn_idetinfos[i];
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into psn_idetinfo(");
                strSql.Append("psn_no,psn_idet_type,psn_type_lv,memo,begntime,endtime");
                strSql.Append(") values (");
                strSql.Append("@psn_no,@psn_idet_type,@psn_type_lv,@memo,@begntime,@endtime");
                strSql.Append(") ");
                strSql.Append("ON DUPLICATE KEY UPDATE ");
                strSql.Append(" psn_no = @psn_no , ");
                strSql.Append(" psn_idet_type = @psn_idet_type , ");
                strSql.Append(" psn_type_lv = @psn_type_lv , ");
                strSql.Append(" memo = @memo , ");
                strSql.Append(" begntime = @begntime , ");
                strSql.Append(" endtime = @endtime  ");
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
                for (int k = 0; k < i; k++)
                {
                    strSql.Append(" ");
                }

                hashtable.Add(strSql.ToString(), parameters);
            }
            for (int i = 0; i < psn_insuinfos.Count; i++)
            {
                Model.psn_insuinfo model = psn_insuinfos[i];
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into psn_insuinfo(");
                strSql.Append("psn_no,insutype,balc,psn_type,psn_insu_stas,psn_insu_date,paus_insu_date,cvlserv_flag,insuplc_admdvs,emp_name,status");
                strSql.Append(") values (");
                strSql.Append("@psn_no,@insutype,@balc,@psn_type,@psn_insu_stas,@psn_insu_date,@paus_insu_date,@cvlserv_flag,@insuplc_admdvs,@emp_name,@status");
                strSql.Append(") ");
                strSql.Append("ON DUPLICATE KEY UPDATE ");
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
                for (int k = 0; k < i; k++)
                {
                    strSql.Append(" ");
                }

                hashtable.Add(strSql.ToString(), parameters);
            }
            DbHelperMySQLInsur.ExecuteSqlTran(hashtable);


        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_baseinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_baseinfo(");
            strSql.Append("psn_no,psn_cert_type,certno,psn_name,gend,naty,brdy,expContent,Sync_flag");
            strSql.Append(") values (");
            strSql.Append("@psn_no,@psn_cert_type,@certno,@psn_name,@gend,@naty,@brdy,@expContent,@Sync_flag");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@psn_cert_type", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@certno", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@psn_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@gend", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@naty", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@brdy", MySqlDbType.Date) ,
                        new MySqlParameter("@expContent", MySqlDbType.VarChar,4000) ,
                        new MySqlParameter("@Sync_flag", MySqlDbType.Bit)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.psn_cert_type;
            parameters[2].Value = model.certno;
            parameters[3].Value = model.psn_name;
            parameters[4].Value = model.gend;
            parameters[5].Value = model.naty;
            parameters[6].Value = model.brdy;
            parameters[7].Value = model.expContent;
            parameters[8].Value = model.Sync_flag;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_baseinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_baseinfo set ");

            strSql.Append(" psn_no = @psn_no , ");
            strSql.Append(" psn_cert_type = @psn_cert_type , ");
            strSql.Append(" certno = @certno , ");
            strSql.Append(" psn_name = @psn_name , ");
            strSql.Append(" gend = @gend , ");
            strSql.Append(" naty = @naty , ");
            strSql.Append(" brdy = @brdy , ");
            strSql.Append(" expContent = @expContent , ");
            strSql.Append(" Sync_flag = @Sync_flag  ");
            strSql.Append(" where psn_no=@psn_no  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@psn_no", MySqlDbType.VarChar,30) ,
                        new MySqlParameter("@psn_cert_type", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@certno", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@psn_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@gend", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@naty", MySqlDbType.VarChar,3) ,
                        new MySqlParameter("@brdy", MySqlDbType.Date) ,
                        new MySqlParameter("@expContent", MySqlDbType.VarChar,4000) ,
                        new MySqlParameter("@Sync_flag", MySqlDbType.Bit)

            };

            parameters[0].Value = model.psn_no;
            parameters[1].Value = model.psn_cert_type;
            parameters[2].Value = model.certno;
            parameters[3].Value = model.psn_name;
            parameters[4].Value = model.gend;
            parameters[5].Value = model.naty;
            parameters[6].Value = model.brdy;
            parameters[7].Value = model.expContent;
            parameters[8].Value = model.Sync_flag;
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
        public bool Delete(string psn_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_baseinfo ");
            strSql.Append(" where psn_no=@psn_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30)           };
            parameters[0].Value = psn_no;


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
        public Model.psn_baseinfo GetModel(string psn_no)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psn_no, psn_cert_type, certno, psn_name, gend, naty, brdy, expContent, Sync_flag  ");
            strSql.Append("  from psn_baseinfo ");
            strSql.Append(" where psn_no=@psn_no ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@psn_no", MySqlDbType.VarChar,30)           };
            parameters[0].Value = psn_no;


            Model.psn_baseinfo model = new Model.psn_baseinfo();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.psn_no = ds.Tables[0].Rows[0]["psn_no"].ToString();
                model.psn_cert_type = ds.Tables[0].Rows[0]["psn_cert_type"].ToString();
                model.certno = ds.Tables[0].Rows[0]["certno"].ToString();
                model.psn_name = ds.Tables[0].Rows[0]["psn_name"].ToString();
                model.gend = ds.Tables[0].Rows[0]["gend"].ToString();
                model.naty = ds.Tables[0].Rows[0]["naty"].ToString();
                if (ds.Tables[0].Rows[0]["brdy"].ToString() != "")
                {
                    model.brdy = DateTime.Parse(ds.Tables[0].Rows[0]["brdy"].ToString());
                }
                model.expContent = ds.Tables[0].Rows[0]["expContent"].ToString();
                if (ds.Tables[0].Rows[0]["Sync_flag"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Sync_flag"].ToString() == "1") || (ds.Tables[0].Rows[0]["Sync_flag"].ToString().ToLower() == "true"))
                    {
                        model.Sync_flag = true;
                    }
                    else
                    {
                        model.Sync_flag = false;
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
            strSql.Append(" FROM psn_baseinfo ");
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
            strSql.Append(" FROM psn_baseinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

