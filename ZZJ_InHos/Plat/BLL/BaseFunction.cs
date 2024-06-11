using System.Data;
using ZZJ_InHos.Plat.DALFactory;
using ZZJ_InHos.Plat.IDAL;

namespace ZZJ_InHos.Plat.BLL
{
    /// <summary>
    /// BaseFunction
    /// </summary>
    public partial class BaseFunction
    {
        private readonly IBaseFunction dal = DataAccess.CreateBaseFunction();

        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        public bool GetSysIdBase(string SYSID_NAME, out int Sys_Id)
        {
            return dal.GetSysIdBase(SYSID_NAME, out Sys_Id);
        }

        /// <summary>
        /// 根据reg_id和pat_id获取身份证信息
        /// </summary>
        /// <param name="reg_id"></param>
        /// <param name="pat_id"></param>
        /// <returns></returns>
        public string GetSFZByByPatAndReg(int reg_id, int pat_id)
        {
            return dal.GetSFZByByPatAndReg(reg_id, pat_id);
        }

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string where, params string[] list)
        {
            return dal.GetList(tableName, where, list);
        }

        /// <summary>
        /// 根据表名，条件，请求参数获取数据列表
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="where">条件</param>
        /// <param name="list">返回字段</param>
        /// <returns></returns>
        public DataTable GetList_ZZJ(string tableName, string where, params string[] list)
        {
            return dal.GetList_ZZJ(tableName, where, list);
        }

        public bool ExecSql(string sqlcmd)
        {
            return dal.ExecSql(sqlcmd);
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        public bool UpdateList(string tableName, string condiction, string setWord)
        {
            return dal.UpdateList(tableName, condiction, setWord);
        }

        /// <summary>
        /// 更新列表(自助机)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condiction">条件</param>
        /// <param name="setWord">设置的值</param>
        /// <returns></returns>
        public bool UpdateList_ZZJ(string tableName, string condiction, string setWord)
        {
            return dal.UpdateList_ZZJ(tableName, condiction, setWord);
        }

        public DataTable Query(string sqlcmd)
        {
            return dal.Query(sqlcmd);
        }

        /// <summary>
        /// 获取指定基础表ID
        /// </summary>
        /// <param name="SYSID_NAME">表标识</param>
        /// <param name="Sys_Id">最新ID</param>
        /// <returns></returns>
        public bool GetSysIdBase_ZZJ(string SYSID_NAME, out int Sys_Id)
        {
            return dal.GetSysIdBase_ZZJ(SYSID_NAME, out Sys_Id);
        }

        public DataTable Query_ZZJ(string sqlcmd)
        {
            return dal.Query_ZZJ(sqlcmd);
        }

        public DataSet RunProcedureForQuery(string storedProcName, params IDataParameter[] parameteres)
        {
            return dal.RunProcedureForQuery(storedProcName, parameteres);
        }
    }
}