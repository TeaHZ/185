using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //psn_baseinfo
    public partial class psn_baseinfo
    {

        private readonly DAL.psn_baseinfo dal = new DAL.psn_baseinfo();
        public psn_baseinfo()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string psn_no)
        {
            return dal.Exists(psn_no);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void SavePsnInfo(Model.psn_baseinfo psn_baseinfo, List<Model.psn_idetinfo> psn_idetinfos, List<Model.psn_insuinfo> psn_insuinfos)
        {
            dal.SavePsnInfo(psn_baseinfo, psn_idetinfos, psn_insuinfos);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_baseinfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_baseinfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string psn_no)
        {

            return dal.Delete(psn_no);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.psn_baseinfo GetModel(string psn_no)
        {

            return dal.GetModel(psn_no);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.psn_baseinfo GetModelByCache(string psn_no)
        {

            string CacheKey = "psn_baseinfoModel-" + psn_no;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(psn_no);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.psn_baseinfo)objModel;
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
        public List<Model.psn_baseinfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.psn_baseinfo> DataTableToList(DataTable dt)
        {
            List<Model.psn_baseinfo> modelList = new List<Model.psn_baseinfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.psn_baseinfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.psn_baseinfo();
                    model.psn_no = dt.Rows[n]["psn_no"].ToString();
                    model.psn_cert_type = dt.Rows[n]["psn_cert_type"].ToString();
                    model.certno = dt.Rows[n]["certno"].ToString();
                    model.psn_name = dt.Rows[n]["psn_name"].ToString();
                    model.gend = dt.Rows[n]["gend"].ToString();
                    model.naty = dt.Rows[n]["naty"].ToString();
                    if (dt.Rows[n]["brdy"].ToString() != "")
                    {
                        model.brdy = DateTime.Parse(dt.Rows[n]["brdy"].ToString());
                    }
                    model.expContent = dt.Rows[n]["expContent"].ToString();
                    if (dt.Rows[n]["Sync_flag"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Sync_flag"].ToString() == "1") || (dt.Rows[n]["Sync_flag"].ToString().ToLower() == "true"))
                        {
                            model.Sync_flag = true;
                        }
                        else
                        {
                            model.Sync_flag = false;
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