using System.Collections.Generic;
using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// pat_card_bind
    /// </summary>
    public partial class pat_card_bind
    {
        private readonly Ipat_card_bind dal = DataAccess.Createpat_card_bind();

        public pat_card_bind()
        { }

        #region BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.Exists(HOS_ID, PAT_ID, YLCARTD_TYPE, YLCARD_NO);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_card_bind model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.pat_card_bind model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.Delete(HOS_ID, PAT_ID, YLCARTD_TYPE, YLCARD_NO);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Plat.Model.pat_card_bind GetModel(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.GetModel(HOS_ID, PAT_ID, YLCARTD_TYPE, YLCARD_NO);
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
        public List<Plat.Model.pat_card_bind> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pat_card_bind> DataTableToList(DataTable dt)
        {
            List<Plat.Model.pat_card_bind> modelList = new List<Plat.Model.pat_card_bind>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.pat_card_bind model;
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

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        //}
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion BasicMethod

        #region ExtensionMethod

        public bool AddByTran(Plat.Model.pat_card_bind bind, Plat.Model.pat_card card, Plat.Model.pat_card old)
        {
            return dal.AddByTran(bind, card, old);
        }

        #endregion ExtensionMethod
    }
}