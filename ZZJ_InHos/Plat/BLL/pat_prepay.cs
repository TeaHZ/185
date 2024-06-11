using System.Collections.Generic;
using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// pat_prepay
    /// </summary>
    public partial class pat_prepay
    {
        private readonly Ipat_prepay dal = DataAccess.Createpat_prepay();

        public pat_prepay()
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
        public bool Exists(int PAY_ID)
        {
            return dal.Exists(PAY_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Plat.Model.pat_prepay model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Plat.Model.pat_prepay model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PAY_ID)
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
        public Plat.Model.pat_prepay GetModel(int PAY_ID)
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
        public List<Plat.Model.pat_prepay> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Plat.Model.pat_prepay> DataTableToList(DataTable dt)
        {
            List<Plat.Model.pat_prepay> modelList = new List<Plat.Model.pat_prepay>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Plat.Model.pat_prepay model;
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

        public bool AddByTran(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb)
        {
            return dal.AddByTran(model, info, zfb, wc, bank, upcap, ccb);
        }

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 住院预交金保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="info"></param>
        /// <param name="zfb"></param>
        /// <param name="wc"></param>
        /// <param name="bank"></param>
        /// <param name="upcap"></param>
        /// <param name="ccb"></param>
        /// <returns></returns>
        public bool AddByTran_ZZJ(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb)
        {
            return dal.AddByTran_ZZJ(model, info, zfb, wc, bank, upcap, ccb);
        }

        #endregion ExtensionMethod
    }
}