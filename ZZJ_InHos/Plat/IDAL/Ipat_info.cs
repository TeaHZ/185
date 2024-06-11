using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层pat_info
    /// </summary>
    public interface Ipat_info
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int PAT_ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.pat_info model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.pat_info model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int PAT_ID);

        bool DeleteList(string PAT_IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.pat_info GetModel(int PAT_ID);

        Plat.Model.pat_info DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        int GetRecordCount(string strWhere);

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion 成员方法

        /// <summary>
        ///根据身份证和注册ID获取持卡人信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        DataTable GetBySfzAndRegID(string SFZ_NO, int REGPAT_ID);

        /// <summary>
        /// 根据注册人ID判断持卡人是否存在
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        bool Exists(int PAT_ID, int REGPAT_ID);

        /// <summary>
        /// 根据注册人ID和终端标示获取持卡人列表信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        DataTable GetBySnAndReg(string lTERMINAL_SN, int REGPAT_ID);

        /// <summary>
        /// 根据注册人ID获取持卡人列表信息
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="REGPAT_ID"></param>
        /// <returns></returns>
        DataTable GetByRegID(int REGPAT_ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat);

        bool DeleteByTran(Plat.Model.pat_info model, Plat.Model.regtopat regtopat, Plat.Model.pat_card card);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddByTran_ZZJ(Plat.Model.pat_info model, Plat.Model.pat_card card, Plat.Model.pat_card_bind bind);
    }
}