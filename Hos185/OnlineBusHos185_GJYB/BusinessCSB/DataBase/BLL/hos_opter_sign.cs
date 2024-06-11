using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //hos_opter_sign
    public partial class hos_opter_sign
    {

        private readonly DAL.hos_opter_sign dal = new DAL.hos_opter_sign();
        public hos_opter_sign()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string HOS_ID, string opter_no, string sign_no)
        {
            return dal.Exists(HOS_ID, opter_no, sign_no);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hos_opter_sign model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hos_opter_sign model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string HOS_ID, string opter_no, string sign_no)
        {

            return dal.Delete(HOS_ID, opter_no, sign_no);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.hos_opter_sign GetModel(string HOS_ID, string opter_no, string sign_no)
        {

            return dal.GetModel(HOS_ID, opter_no, sign_no);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.hos_opter_sign GetModelByCache(string HOS_ID, string opter_no, string sign_no)
        {

            string CacheKey = "hos_opter_signModel-" + HOS_ID + opter_no + sign_no;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(HOS_ID, opter_no, sign_no);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.hos_opter_sign)objModel;
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
        public List<Model.hos_opter_sign> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.hos_opter_sign> DataTableToList(DataTable dt)
        {
            List<Model.hos_opter_sign> modelList = new List<Model.hos_opter_sign>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.hos_opter_sign model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.hos_opter_sign();
                    model.HOS_ID = dt.Rows[n]["HOS_ID"].ToString();
                    model.opter_no = dt.Rows[n]["opter_no"].ToString();
                    model.mac = dt.Rows[n]["mac"].ToString();
                    model.ip = dt.Rows[n]["ip"].ToString();
                    model.sign_no = dt.Rows[n]["sign_no"].ToString();
                    if (dt.Rows[n]["sign_intime"].ToString() != "")
                    {
                        model.sign_intime = DateTime.Parse(dt.Rows[n]["sign_intime"].ToString());
                    }
                    if (dt.Rows[n]["sign_outtime"].ToString() != "")
                    {
                        model.sign_outtime = DateTime.Parse(dt.Rows[n]["sign_outtime"].ToString());
                    }
                    if (dt.Rows[n]["signout_flag"].ToString() != "")
                    {
                        model.signout_flag = int.Parse(dt.Rows[n]["signout_flag"].ToString());
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