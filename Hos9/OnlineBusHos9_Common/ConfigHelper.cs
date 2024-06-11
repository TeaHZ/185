using System.Configuration;
using System;

namespace OnlineBusHos9_Common
{
    public class ConfigHelper
    {
        public static int GetConfigInt(string key)
        {
            //string value = ConfigurationManager.AppSettings[key];

            string value = GetConfiguration(key);

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];


        }
    }
}