using System.Data;

namespace ZZJ_Tran.Plat.IDAL
{
    /// <summary>
    /// 接口层wechat_tran
    /// </summary>
    public interface Iwechat_tran
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string COMM_SN);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Plat.Model.wechat_tran model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Plat.Model.wechat_tran model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string COMM_SN);

        bool DeleteList(string COMM_SNlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Plat.Model.wechat_tran GetModel(string COMM_SN);

        Plat.Model.wechat_tran DataRowToModel(DataRow row);

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