using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层pay_info
    /// </summary>
    public interface Ipay_info
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PAY_ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.pay_info model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.pay_info model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string PAY_ID);

        bool DeleteList(string PAY_IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.pay_info GetModel(string PAY_ID);

        Plat.Model.pay_info DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion 成员方法
    }
}