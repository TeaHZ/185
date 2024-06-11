using DB.Core;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace OnlineBusHos185_GJYB.DAL
{
    //hospital
    public partial class hospital
    {

        public bool Exists(string HOS_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hospital");
            strSql.Append(" where ");
            strSql.Append(" HOS_ID = @HOS_ID  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20)           };
            parameters[0].Value = HOS_ID;

            return DbHelperMySQLInsur.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hospital model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hospital(");
            strSql.Append("HOS_ID,HOS_NAME,fixmedins_code,fixmedins_name,fixmedins_soft_fcty,mdtrtarea_admvs,recer_sys_code,infver");
            strSql.Append(") values (");
            strSql.Append("@HOS_ID,@HOS_NAME,@fixmedins_code,@fixmedins_name,@fixmedins_soft_fcty,@mdtrtarea_admvs,@recer_sys_code,@infver");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20) ,
                        new MySqlParameter("@HOS_NAME", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@fixmedins_code", MySqlDbType.VarChar,12) ,
                        new MySqlParameter("@fixmedins_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@fixmedins_soft_fcty", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@mdtrtarea_admvs", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@recer_sys_code", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@infver", MySqlDbType.VarChar,6)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.HOS_NAME;
            parameters[2].Value = model.fixmedins_code;
            parameters[3].Value = model.fixmedins_name;
            parameters[4].Value = model.fixmedins_soft_fcty;
            parameters[5].Value = model.mdtrtarea_admvs;
            parameters[6].Value = model.recer_sys_code;
            parameters[7].Value = model.infver;
            DbHelperMySQLInsur.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hospital model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hospital set ");

            strSql.Append(" HOS_ID = @HOS_ID , ");
            strSql.Append(" HOS_NAME = @HOS_NAME , ");
            strSql.Append(" fixmedins_code = @fixmedins_code , ");
            strSql.Append(" fixmedins_name = @fixmedins_name , ");
            strSql.Append(" fixmedins_soft_fcty = @fixmedins_soft_fcty , ");
            strSql.Append(" mdtrtarea_admvs = @mdtrtarea_admvs , ");
            strSql.Append(" recer_sys_code = @recer_sys_code , ");
            strSql.Append(" infver = @infver  ");
            strSql.Append(" where HOS_ID=@HOS_ID  ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20) ,
                        new MySqlParameter("@HOS_NAME", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@fixmedins_code", MySqlDbType.VarChar,12) ,
                        new MySqlParameter("@fixmedins_name", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@fixmedins_soft_fcty", MySqlDbType.VarChar,50) ,
                        new MySqlParameter("@mdtrtarea_admvs", MySqlDbType.VarChar,6) ,
                        new MySqlParameter("@recer_sys_code", MySqlDbType.VarChar,10) ,
                        new MySqlParameter("@infver", MySqlDbType.VarChar,6)

            };

            parameters[0].Value = model.HOS_ID;
            parameters[1].Value = model.HOS_NAME;
            parameters[2].Value = model.fixmedins_code;
            parameters[3].Value = model.fixmedins_name;
            parameters[4].Value = model.fixmedins_soft_fcty;
            parameters[5].Value = model.mdtrtarea_admvs;
            parameters[6].Value = model.recer_sys_code;
            parameters[7].Value = model.infver;
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
        public bool Delete(string HOS_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from hospital ");
            strSql.Append(" where HOS_ID=@HOS_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20)           };
            parameters[0].Value = HOS_ID;


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


        public Model.hospital GetModel(string HOS_ID, string fixmedins_code,string yb_type)
        {

 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HOS_ID, HOS_NAME, fixmedins_code, fixmedins_name, fixmedins_soft_fcty, mdtrtarea_admvs, recer_sys_code, infver,api_uri,api_access_key,api_secretKey,api_name,api_version ,dev_no ,dev_safe_info, cainfo");
            strSql.Append("  from hospital ");
            strSql.Append(" where (HOS_ID=@HOS_ID or fixmedins_code=@fixmedins_code) and yb_type=@yb_type");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20) ,
                    new MySqlParameter("@fixmedins_code", MySqlDbType.VarChar,20),
                    new MySqlParameter("@yb_type", MySqlDbType.VarChar,20) };
            parameters[0].Value = HOS_ID;
            parameters[1].Value = fixmedins_code;
            parameters[2].Value = yb_type;

            Model.hospital model = new Model.hospital();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HOS_ID = ds.Tables[0].Rows[0]["HOS_ID"].ToString();
                model.HOS_NAME = ds.Tables[0].Rows[0]["HOS_NAME"].ToString();
                model.fixmedins_code = ds.Tables[0].Rows[0]["fixmedins_code"].ToString();
                model.fixmedins_name = ds.Tables[0].Rows[0]["fixmedins_name"].ToString();
                model.fixmedins_soft_fcty = ds.Tables[0].Rows[0]["fixmedins_soft_fcty"].ToString();
                model.mdtrtarea_admvs = ds.Tables[0].Rows[0]["mdtrtarea_admvs"].ToString();
                model.recer_sys_code = ds.Tables[0].Rows[0]["recer_sys_code"].ToString();
                model.infver = ds.Tables[0].Rows[0]["infver"].ToString();
                model.api_uri = ds.Tables[0].Rows[0]["api_uri"].ToString();
                model.api_access_key = ds.Tables[0].Rows[0]["api_access_key"].ToString();
                model.api_secretKey = ds.Tables[0].Rows[0]["api_secretKey"].ToString();
                model.api_name = ds.Tables[0].Rows[0]["api_name"].ToString();
                model.api_version = ds.Tables[0].Rows[0]["api_version"].ToString();
                model.dev_no = ds.Tables[0].Rows[0]["dev_no"].ToString();
                model.dev_safe_info = ds.Tables[0].Rows[0]["dev_safe_info"].ToString();
                model.cainfo = ds.Tables[0].Rows[0]["cainfo"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.hospital GetModel(string HOS_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HOS_ID, HOS_NAME, fixmedins_code, fixmedins_name, fixmedins_soft_fcty, mdtrtarea_admvs, recer_sys_code, infver,api_uri,api_access_key,api_secretKey,api_name,api_version ,dev_no ,dev_safe_info, cainfo  ");
            strSql.Append("  from hospital ");
            strSql.Append(" where HOS_ID=@HOS_ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20)           };
            parameters[0].Value = HOS_ID;


            Model.hospital model = new Model.hospital();
            DataSet ds = DbHelperMySQLInsur.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HOS_ID = ds.Tables[0].Rows[0]["HOS_ID"].ToString();
                model.HOS_NAME = ds.Tables[0].Rows[0]["HOS_NAME"].ToString();
                model.fixmedins_code = ds.Tables[0].Rows[0]["fixmedins_code"].ToString();
                model.fixmedins_name = ds.Tables[0].Rows[0]["fixmedins_name"].ToString();
                model.fixmedins_soft_fcty = ds.Tables[0].Rows[0]["fixmedins_soft_fcty"].ToString();
                model.mdtrtarea_admvs = ds.Tables[0].Rows[0]["mdtrtarea_admvs"].ToString();
                model.recer_sys_code = ds.Tables[0].Rows[0]["recer_sys_code"].ToString();
                model.infver = ds.Tables[0].Rows[0]["infver"].ToString();
                model.api_uri = ds.Tables[0].Rows[0]["api_uri"].ToString();
                model.api_access_key = ds.Tables[0].Rows[0]["api_access_key"].ToString();
                model.api_secretKey = ds.Tables[0].Rows[0]["api_secretKey"].ToString();
                model.api_name = ds.Tables[0].Rows[0]["api_name"].ToString();
                model.api_version = ds.Tables[0].Rows[0]["api_version"].ToString();
                model.dev_no = ds.Tables[0].Rows[0]["dev_no"].ToString();
                model.dev_safe_info = ds.Tables[0].Rows[0]["dev_safe_info"].ToString();
                model.cainfo = ds.Tables[0].Rows[0]["cainfo"].ToString();
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
            strSql.Append(" FROM hospital ");
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
            strSql.Append(" FROM hospital ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQLInsur.Query(strSql.ToString());
        }


    }
}

