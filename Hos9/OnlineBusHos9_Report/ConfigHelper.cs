using Microsoft.Extensions.Configuration;
using System;


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
        //return ConfigurationManager.AppSettings[key];

        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        var builder = new ConfigurationBuilder()
        .SetBasePath(path)
        .AddJsonFile("appsettings.json");
        var config = builder.Build();
        // string value = config["Data"];//配置键
        string value = config.GetSection(key).Value;
        return value;

    }
}
