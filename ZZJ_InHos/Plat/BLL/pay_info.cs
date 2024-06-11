using System.Collections.Generic;
using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// pay_info
    /// </summary>
    public partial class pay_info
    {
        private readonly Ipay_info dal = DataAccess.Createpay_info();

        public pay_info()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PAY_ID)
        {
            return dal.Exists(PAY_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pay_info model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.pay_info model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string PAY_ID)
        {
            return dal.Delete(PAY_ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PAY_IDlist)
        {
            return dal.DeleteList(PAY_IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Plat.Model.pay_info GetModel(string PAY_ID)
        {
            return dal.GetModel(PAY_ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pay_info> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pay_info> DataTableToList(DataTable dt)
        {
            List<Plat.Model.pay_info> modelList = new List<Plat.Model.pay_info>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.pay_info model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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

        #endregion BasicMethod
    }
}