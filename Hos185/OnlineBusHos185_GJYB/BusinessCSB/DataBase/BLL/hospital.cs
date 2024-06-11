using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //hospital
    public partial class hospital
    {

        private readonly DAL.hospital dal = new DAL.hospital();
        public hospital()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string HOS_ID)
        {
            return dal.Exists(HOS_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hospital model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hospital model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string HOS_ID)
        {

            return dal.Delete(HOS_ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.hospital GetModel(string HOS_ID)
        {

            return dal.GetModel(HOS_ID);
        }
        public Model.hospital GetModel(string HOS_ID, string fixmedins_code,string ybtype)
        {

            return dal.GetModel(HOS_ID, fixmedins_code, ybtype);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.hospital GetModelByCache(string HOS_ID)
        {

            string CacheKey = "hospitalModel-" + HOS_ID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(HOS_ID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.hospital)objModel;
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
        public List<Model.hospital> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.hospital> DataTableToList(DataTable dt)
        {
            List<Model.hospital> modelList = new List<Model.hospital>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.hospital model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.hospital();
                    model.HOS_ID = dt.Rows[n]["HOS_ID"].ToString();
                    model.HOS_NAME = dt.Rows[n]["HOS_NAME"].ToString();
                    model.fixmedins_code = dt.Rows[n]["fixmedins_code"].ToString();
                    model.fixmedins_name = dt.Rows[n]["fixmedins_name"].ToString();
                    model.fixmedins_soft_fcty = dt.Rows[n]["fixmedins_soft_fcty"].ToString();
                    model.mdtrtarea_admvs = dt.Rows[n]["mdtrtarea_admvs"].ToString();
                    model.recer_sys_code = dt.Rows[n]["recer_sys_code"].ToString();
                    model.infver = dt.Rows[n]["infver"].ToString();


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