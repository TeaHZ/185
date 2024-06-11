using System.Data;

namespace ZZJ_InHos.Plat.IDAL
{
    /// <summary>
    /// 接口层基础公用业务
    /// </summary>
    public interface IBaseFunction
    {
        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        bool GetSysIdBase(string SYSID_NAME, out int Sys_Id);

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        bool UpdateList(string tableName, string condiction, string setWord);

        /// <summary>
        /// 根据reg_id和pat_id获取身份证信息
        /// </summary>
        /// <param name="reg_id"></param>
        /// <param name="pat_id"></param>
        /// <returns></returns>
        string GetSFZByByPatAndReg(int reg_id, int pat_id);

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        DataTable GetList(string tableName, string where, params string[] list);

        /// <summary>
        /// 执行一条SQL数据，一把为insert或delete
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        bool ExecSql(string sqlcmd);

        /// <summary>
        /// 执行一条sql语句，返回查询数据
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        DataTable Query(string sqlcmd);

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        DataTable GetList_ZZJ(string tableName, string where, params string[] list);

        /// <summary>
        /// 更新列表(自助机)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        bool UpdateList_ZZJ(string tableName, string condiction, string setWord);

        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        bool GetSysIdBase_ZZJ(string SYSID_NAME, out int Sys_Id);

        DataTable Query_ZZJ(string sqlcmd);

        DataSet RunProcedureForQuery(string storedProcName, IDataParameter[] parameteres);
    }
}