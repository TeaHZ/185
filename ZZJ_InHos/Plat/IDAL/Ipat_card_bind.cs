using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层pat_card_bind
    /// </summary>
    public interface Ipat_card_bind
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.pat_card_bind model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.pat_card_bind model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.pat_card_bind GetModel(string HOS_ID, int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        Plat.Model.pat_card_bind DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion 成员方法

        #region MethodEx

        bool AddByTran(Plat.Model.pat_card_bind bind, Plat.Model.pat_card card, Plat.Model.pat_card old);

        #endregion MethodEx
    }
}