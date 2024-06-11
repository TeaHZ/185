using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessFactory
{
   public  class TestBusParamHelper
    {
        /// <summary>
        /// 将入参记录下来
        /// </summary>
        /// <param name="className"></param>
        /// <param name="BusParam"></param>
        public static void WriteParam(string className, string BusParam)
        {
            string path = "";
            try
            {
                string str = System.Environment.CurrentDirectory;
                path = System.Environment.CurrentDirectory + "\\TestBusParam";
            }

            catch (Exception ex)
            {
              //  path = HttpContext.Current.Server.MapPath("TestBusParam");
            }
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            try
            { 
                string filename = path + "/" + className  + ".txt";//用日期对日志文件命名

                File.WriteAllText (filename, BusParam, Encoding.UTF8);
            }
            catch (Exception ex)
            {
            }
        }
        public static string  GetParam(string className    )
        {
           string  BusParam = "";
            string path = "";
            try
            {
                string str = System.Environment.CurrentDirectory;
                path = System.Environment.CurrentDirectory + "\\TestBusParam";
            }

            catch (Exception ex)
            {
                //path = HttpContext.Current.Server.MapPath("TestBusParam");
            }
          
            try
            {
                string filename = path + "/" + className + ".txt";
                BusParam = File.ReadAllText(filename, Encoding.UTF8);
            }
            catch (Exception ex)
            {
            }
            return BusParam;
        }
    }
}
