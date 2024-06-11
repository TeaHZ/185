using System;

namespace ZZJ_Tran.Plat.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = "ZZJ_Tran.Plat.MySQLDAL";

        public DataAccess()
        { }

        /// <summary>
        /// 创建alipay_tran数据层接口。
        /// </summary>
        public static Plat.IDAL.Ialipay_tran Createalipay_tran()
        {
            string ClassNamespace = AssemblyPath + ".alipay_tran";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ialipay_tran)objType;
        }

        /// <summary>
        /// 创建wechat_tran数据层接口。
        /// </summary>
        public static Plat.IDAL.Iwechat_tran Createwechat_tran()
        {
            string ClassNamespace = AssemblyPath + ".wechat_tran";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Iwechat_tran)objType;
        }

        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Activator.CreateInstance(System.Type.GetType(classNamespace));
                    //objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch (Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
    }
}