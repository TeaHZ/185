using System.Collections.Generic;
using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// pat_info
    /// </summary>
    public partial class pat_info
    {
        private readonly Ipat_info dal = DataAccess.Createpat_info();

        public pat_info()
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
        public bool Exists(int PAT_ID)
        {
            return dal.Exists(PAT_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_info model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.pat_info model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PAT_ID)
        {
            return dal.Delete(PAT_ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PAT_IDlist)
        {
            return dal.DeleteList(PAT_IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Plat.Model.pat_info GetModel(int PAT_ID)
        {
            return dal.GetModel(PAT_ID);
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
        public List<Plat.Model.pat_info> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pat_info> DataTableToList(DataTable dt)
        {
            List<Plat.Model.pat_info> modelList = new List<Plat.Model.pat_info>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.pat_info model;
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion BasicMethod

        /// <summary>
        ///根据身份证和注册ID获取持卡人信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public DataTable GetBySfzAndRegID(string SFZ_NO, int REGPAT_ID)
        {
            return dal.GetBySfzAndRegID(SFZ_NO, REGPAT_ID);
        }

        /// <summary>
        /// 根据注册人ID判断持卡人是否存在
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public bool Exists(int PAT_ID, int REGPAT_ID)
        {
            return dal.Exists(PAT_ID, REGPAT_ID);
        }

        /// <summary>
        /// 根据注册人和终端唯一码获取持卡人信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetBySnAndReg(string lTERMINAL_SN, int REGPAT_ID)
        {
            return dal.GetBySnAndReg(lTERMINAL_SN, REGPAT_ID);
        }

        public bool AddByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat)
        {
            return dal.AddByTran(model, regtopat);
        }

        public bool DeleteByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat, Plat.Model.pat_card card)
        {
            return dal.DeleteByTran(model, regtopat, card);
        }

        /// <summary>
        /// 根据注册人ID获取持卡人列表信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetByRegID(int REGPAT_ID)
        {
            return dal.GetByRegID(REGPAT_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddByTran_ZZJ(Plat.Model.pat_info model, Plat.Model.pat_card card, Plat.Model.pat_card_bind bind)
        {
            return dal.AddByTran_ZZJ(model, card, bind);
        }
    }
}