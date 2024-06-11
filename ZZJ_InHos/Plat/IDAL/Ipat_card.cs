using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层pat_card
    /// </summary>
    public interface Ipat_card
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.pat_card model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.pat_card model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.pat_card GetModel(int PAT_ID, int YLCARTD_TYPE, string YLCARD_NO);

        Plat.Model.pat_card DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        ///// <summary>
        ///// 根据分页获得数据列表
        ///// </summary>
        ////DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion 成员方法

        /// <summary>
        /// 判断医疗卡是否存在
        /// </summary>
        /// <param name="pat_id">用户代码</param>
        /// <param name="card_no">卡号</param>
        /// <param name="card_type">卡类别</param>
        /// <param name="hsp_id">医院代码</hsp_id>
        /// <returns></returns>
        bool Exists(string pat_id, string card_no, int card_type, string hsp_id);

        DataTable GetListBydPatID(int PAT_ID);

        /// <summary>
        /// 获取注册人所有对应卡号
        /// </summary>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        DataTable GetListBydREGPATID(int REGPAT_ID);

        DataTable GetListBydREGPATID(int REGPAT_ID, string HOS_ID);
    }
}