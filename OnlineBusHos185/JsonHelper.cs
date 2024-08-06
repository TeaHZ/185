using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace OnlineBusHos185
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {

            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string json = JsonConvert.SerializeObject(o, jsonSetting);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }


        /// <summary>
        /// json格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertJsonString(string str)
        {

            //格式化json字符串

            JsonSerializer serializer = new JsonSerializer();

            TextReader tr = new StringReader(str);

            JsonTextReader jtr = new JsonTextReader(tr);

            object obj = serializer.Deserialize(jtr);

            if (obj != null)
            {

                StringWriter textWriter = new StringWriter();

                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)

                {

                    Formatting = Formatting.Indented,

                    Indentation = 4,

                    IndentChar = ' '

                };

                serializer.Serialize(jsonWriter, obj);

                return textWriter.ToString();

            }

            else
            {

                return str;

            }

        }




        public static DataTable toDataTable( object obj, string _tName = null)
        {
            Type t = obj.GetType();
            dynamic ts = obj;
            object tf = ts[0];

            PropertyInfo[] pi = tf.GetType().GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            DataTable DT = new DataTable();
            DT.TableName = _tName == null ? tf.GetType().ToString() : _tName;
            foreach (PropertyInfo p in pi)
            {
                DT.Columns.Add(p.Name, p.PropertyType);
            }

            DataRow dr = null;
            foreach (var v in ts)
            {
                dr = DT.NewRow();
                foreach (PropertyInfo p in pi)
                {
                    dr[p.Name] = p.GetValue(v, null);
                }
                DT.Rows.Add(dr);
            }
            return DT;
        }

    }



}