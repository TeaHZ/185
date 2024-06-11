using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //psn_insuinfo
    public partial class psn_insuinfo
    {

        private readonly DAL.psn_insuinfo dal = new DAL.psn_insuinfo();
        public psn_insuinfo()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string psn_no, string insutype)
        {
            return dal.Exists(psn_no, insutype);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_insuinfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_insuinfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string psn_no, string insutype)
        {

            return dal.Delete(psn_no, insutype);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.psn_insuinfo GetModel(string psn_no, string insutype)
        {

            return dal.GetModel(psn_no, insutype);
        }

        public Model.psn_insuinfo GetInsuinfo(string psn_no)
        {

            return dal.GetInsuinfo(psn_no);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.psn_insuinfo GetModelByCache(string psn_no, string insutype)
        {

            string CacheKey = "psn_insuinfoModel-" + psn_no + insutype;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(psn_no, insutype);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.psn_insuinfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.psn_insuinfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.psn_insuinfo> DataTableToList(DataTable dt)
        {
            List<Model.psn_insuinfo> modelList = new List<Model.psn_insuinfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.psn_insuinfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.psn_insuinfo();
                    model.psn_no = dt.Rows[n]["psn_no"].ToString();
                    model.insutype = dt.Rows[n]["insutype"].ToString();
                    if (dt.Rows[n]["balc"].ToString() != "")
                    {
                        model.balc = decimal.Parse(dt.Rows[n]["balc"].ToString());
                    }
                    model.psn_type = dt.Rows[n]["psn_type"].ToString();
                    model.psn_insu_stas = dt.Rows[n]["psn_insu_stas"].ToString();
                    if (dt.Rows[n]["psn_insu_date"].ToString() != "")
                    {
                        model.psn_insu_date = DateTime.Parse(dt.Rows[n]["psn_insu_date"].ToString());
                    }
                    if (dt.Rows[n]["paus_insu_date"].ToString() != "")
                    {
                        model.paus_insu_date = DateTime.Parse(dt.Rows[n]["paus_insu_date"].ToString());
                    }
                    model.cvlserv_flag = dt.Rows[n]["cvlserv_flag"].ToString();
                    model.insuplc_admdvs = dt.Rows[n]["insuplc_admdvs"].ToString();
                    model.emp_name = dt.Rows[n]["emp_name"].ToString();
                    if (dt.Rows[n]["status"].ToString() != "")
                    {
                        if ((dt.Rows[n]["status"].ToString() == "1") || (dt.Rows[n]["status"].ToString().ToLower() == "true"))
                        {
                            model.status = true;
                        }
                        else
                        {
                            model.status = false;
                        }
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

    }
}