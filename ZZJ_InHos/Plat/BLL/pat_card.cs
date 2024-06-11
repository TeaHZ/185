using System.Collections.Generic;
using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// pat_card
    /// </summary>
    public partial class pat_card
    {
        private readonly Ipat_card dal = DataAccess.Createpat_card();

        public pat_card()
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
        public bool Exists(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.Exists(PAT_ID, YLCARTD_TYPE, YLCARD_NO);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_card model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.pat_card model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.Delete(PAT_ID, YLCARTD_TYPE, YLCARD_NO);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Plat.Model.pat_card GetModel(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO)
        {
            return dal.GetModel(PAT_ID, YLCARTD_TYPE, YLCARD_NO);
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
        public List<Plat.Model.pat_card> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pat_card> DataTableToList(DataTable dt)
        {
            List<Plat.Model.pat_card> modelList = new List<Plat.Model.pat_card>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.pat_card model;
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

        /// <summary>
        /// 判断医疗卡是否存在
        /// </summary>
        /// <param name="pat_id">用户代码</param>
        /// <param name="card_no">卡号</param>
        /// <param name="card_type">卡类别</param>
        /// <param name="hsp_id">医院代码</hsp_id>
        /// <returns></returns>
        public bool Exists(string pat_id, string card_no, int card_type, string hsp_id)
        {
            return dal.Exists(pat_id, card_no, card_type, hsp_id);
        }

        public DataTable GetListBydPatID(int PAT_ID)
        {
            return dal.GetListBydPatID(PAT_ID);
        }

        /// <summary>
        /// 获取注册人所有对应卡号
        /// </summary>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        public DataTable GetListBydREGPATID(int REGPAT_ID)
        {
            return dal.GetListBydREGPATID(REGPAT_ID);
        }

        public DataTable GetListBydREGPATID(int REGPAT_ID, string HOS_ID)
        {
            return dal.GetListBydREGPATID(REGPAT_ID, HOS_ID);
        }
    }
}