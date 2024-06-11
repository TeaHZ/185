using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //psn_idetinfo
    public partial class psn_idetinfo
    {

        private readonly DAL.psn_idetinfo dal = new DAL.psn_idetinfo();
        public psn_idetinfo()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string psn_no, string psn_idet_type)
        {
            return dal.Exists(psn_no, psn_idet_type);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.psn_idetinfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.psn_idetinfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string psn_no, string psn_idet_type)
        {

            return dal.Delete(psn_no, psn_idet_type);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.psn_idetinfo GetModel(string psn_no, string psn_idet_type)
        {

            return dal.GetModel(psn_no, psn_idet_type);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.psn_idetinfo GetModelByCache(string psn_no, string psn_idet_type)
        {

            string CacheKey = "psn_idetinfoModel-" + psn_no + psn_idet_type;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(psn_no, psn_idet_type);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.psn_idetinfo)objModel;
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
        public List<Model.psn_idetinfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.psn_idetinfo> DataTableToList(DataTable dt)
        {
            List<Model.psn_idetinfo> modelList = new List<Model.psn_idetinfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.psn_idetinfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.psn_idetinfo();
                    model.psn_no = dt.Rows[n]["psn_no"].ToString();
                    model.psn_idet_type = dt.Rows[n]["psn_idet_type"].ToString();
                    model.psn_type_lv = dt.Rows[n]["psn_type_lv"].ToString();
                    model.memo = dt.Rows[n]["memo"].ToString();
                    if (dt.Rows[n]["begntime"].ToString() != "")
                    {
                        model.begntime = DateTime.Parse(dt.Rows[n]["begntime"].ToString());
                    }
                    if (dt.Rows[n]["endtime"].ToString() != "")
                    {
                        model.endtime = DateTime.Parse(dt.Rows[n]["endtime"].ToString());
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