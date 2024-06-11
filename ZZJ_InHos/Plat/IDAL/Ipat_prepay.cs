using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层pat_prepay
    /// </summary>
    public interface Ipat_prepay
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int PAY_ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.pat_prepay model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.pat_prepay model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int PAY_ID);

        bool DeleteList(string PAY_IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.pat_prepay GetModel(int PAY_ID);

        Plat.Model.pat_prepay DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 预交金收费保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="info"></param>
        /// <param name="zfb"></param>
        /// <param name="wc"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        bool AddByTran(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb);

        #endregion 成员方法

        #region MethodEx

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
        bool AddByTran_ZZJ(Plat.Model.pat_prepay model, Plat.Model.pay_info info, Plat.Model.pay_info_zfb zfb, Plat.Model.pay_info_wc wc, Plat.Model.pay_info_bank bank, Plat.Model.pay_info_upcap upcap, Plat.Model.pay_info_ccb ccb);

        #endregion MethodEx
    }
}