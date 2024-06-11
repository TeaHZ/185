using System.Collections.Generic;
using System.Data;
using ZZJ_Tran.Plat.DALFactory;
using ZZJ_Tran.Plat.IDAL;

namespace ZZJ_Tran.Plat.BLL
{
    /// <summary>
    /// alipay_tran
    /// </summary>
    public partial class alipay_tran
    {
        private readonly Ialipay_tran dal = DataAccess.Createalipay_tran();

        public alipay_tran()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string COMM_SN)
        {
            return dal.Exists(COMM_SN);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.alipay_tran model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.alipay_tran model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string COMM_SN)
        {
            return dal.Delete(COMM_SN);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string COMM_SNlist)
        {
            return dal.DeleteList(COMM_SNlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Plat.Model.alipay_tran GetModel(string COMM_SN)
        {
            return dal.GetModel(COMM_SN);
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
        public List<Plat.Model.alipay_tran> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.alipay_tran> DataTableToList(DataTable dt)
        {
            List<Plat.Model.alipay_tran> modelList = new List<Plat.Model.alipay_tran>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.alipay_tran model;
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

        #region ExtensionMethod

        public bool AddOrUpdate(Plat.Model.alipay_tran model)
        {
            return dal.AddOrUpdate(model);
        }

        #endregion ExtensionMethod
    }
}