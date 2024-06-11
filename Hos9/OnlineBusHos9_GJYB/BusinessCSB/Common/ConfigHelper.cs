using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace OnlineBusHos9_GJYB
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
