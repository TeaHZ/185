using System;

namespace ZZJ_InHos.Plat.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = "ZZJ_InHos.Plat.MySQLDAL";

        public DataAccess()
        { }

        /// <summary>
        /// 创建pat_prepay数据层接口。
        /// </summary>
        public static Plat.IDAL.Ipat_prepay Createpat_prepay()
        {
            string ClassNamespace = AssemblyPath + ".pat_prepay";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ipat_prepay)objType;
        }

        /// <summary>
        /// 创建BaseFunction数据层接口。
        /// </summary>
        public static Plat.IDAL.IBaseFunction CreateBaseFunction()
        {
            string ClassNamespace = AssemblyPath + ".BaseFunction";
            object objType = CreateObjectNoCache(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.IBaseFunction)objType;
        }

        /// <summary>
        /// 创建pay_info数据层接口。
        /// </summary>
        public static Plat.IDAL.Ipay_info Createpay_info()
        {
            string ClassNamespace = AssemblyPath + ".pay_info";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ipay_info)objType;
        }

        /// <summary>
        /// 创建pat_card数据层接口。
        /// </summary>
        public static Plat.IDAL.Ipat_card Createpat_card()
        {
            string ClassNamespace = AssemblyPath + ".pat_card";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ipat_card)objType;
        }

        /// <summary>
        /// 创建pat_card_bind数据层接口。
        /// </summary>
        public static Plat.IDAL.Ipat_card_bind Createpat_card_bind()
        {
            string ClassNamespace = AssemblyPath + ".pat_card_bind";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ipat_card_bind)objType;
        }

        /// <summary>
        /// 创建pat_info数据层接口。
        /// </summary>
        public static Plat.IDAL.Ipat_info Createpat_info()
        {
            string ClassNamespace = AssemblyPath + ".pat_info";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Plat.IDAL.Ipat_info)objType;
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

        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Activator.CreateInstance(System.Type.GetType(classNamespace));
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }
        }
    }
}