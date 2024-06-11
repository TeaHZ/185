using System;
using System.Collections.Generic;
using System.Text;

public class NewIdHelper
{
    /// <summary>
    ///20位 yyyyMMddHHmmssfff+3位随机数
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId20
    {
        get
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + (rnd.Next(999)).ToString("D3");
        }
    }
    /// <summary>
    ///20位 yyyyMMdd+8位当日毫秒+4位随机数
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId20_2
    {
        get
        {
            DateTime dtnow = DateTime.Now;
            System.DateTime startTime = new System.DateTime(dtnow.Year, dtnow.Month, dtnow.Day, 0, 0, 0);
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            string ORDERID = (rnd.Next(9999)).ToString().PadLeft(4, '0');

            string tem = ((int)(dtnow - startTime).TotalMilliseconds).ToString("d8");
            return dtnow.ToString("yyyyMMdd") + tem + ORDERID;
        }
    }

    /// <summary>
    /// 20位  13位unixtime毫秒+7位随机数    时间最长到2286/11/21 0:00:00
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId20_3
    {
        get
        {
            DateTime dtnow = DateTime.Now;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            string tem = ((long)(dtnow - startTime).TotalMilliseconds).ToString("D13");
            return tem + (rnd.Next(9999977)).ToString("D7"); ;
        }
    }


    /// <summary>
    ///18位 yyyyMMddHHmmssfff+1位随机数
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId18
    {
        get
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + (rnd.Next(9)).ToString("D1");
        }
    }

    /// <summary>
    ///18位 yyMMddHHmmssfff+3位随机数
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId18_2
    {
        get
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return DateTime.Now.ToString("yyMMddHHmmssfff") + (rnd.Next(999)).ToString("D3");
        }
    }

    /// <summary>
    /// 18位  13位unixtime毫秒+5位随机数    时间最长到2286/11/21 0:00:00
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId18_3()
    {
        DateTime dtnow = DateTime.Now;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        string tem = ((long)(dtnow - startTime).TotalMilliseconds).ToString("D13");
        return tem + (rnd.Next(99999)).ToString("D5"); ;
    }

    /// <summary>
    /// 15位  13位unixtime毫秒+2位随机数    时间最长到2286/11/21 0:00:00
    /// </summary>
    /// <returns></returns>
    public static string NewOrderId15()
    {
        DateTime dtnow = DateTime.Now;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        string tem = ((long)(dtnow - startTime).TotalMilliseconds).ToString("D13");
        return tem + (rnd.Next(99)).ToString("D2"); ;
    }


}
