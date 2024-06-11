using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace OnlineBusHos9_Report
{
    public class FormatHelper
    {
        public FormatHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// /*****************************************************************************************
        ///* 类名：FormatHelper                                                  *
        ///* 功能：把金额数据从小写转换为大写                                    *
        /// * 限制条件：金额小于一万亿，且少于两位小数                         *                                       *
        ///* 用法：this.textBox2.Text = FormatHelper.ToUpper(d);                            *
        /// *****************************************************************************************/
        /// 小写金额转换为大写金额，其他条件：金额小于一万亿，最多两位小数
        /// </summary>
        /// <param name="d">源金额，je 《 1000000000000.00(一万亿)，且最多两位小数 </param>
        /// <returns>结果，大写金额</returns>
        static public string ToUpper(decimal InJe)
        {
            decimal d = Math.Abs(InJe);
            if (d == 0)
                return "零元整";

            string je = d.ToString("####.00");
            if (je.Length > 15)
                return "";
            je = new String('0', 15 - je.Length) + je;      //若小于15位长，前面补0

            string stry = je.Substring(0, 4);        //取得'亿'单元
            string strw = je.Substring(4, 4);        //取得'万'单元
            string strg = je.Substring(8, 4);        //取得'元'单元
            string strf = je.Substring(13, 2);        //取得小数部分

            string str1 = "", str2 = "", str3 = "";

            str1 = getupper(stry, "亿");        //亿单元的大写
            str2 = getupper(strw, "万");        //万单元的大写
            str3 = getupper(strg, "元");        //元单元的大写


            string str_y = "", str_w = "";
            if (je[3] == '0' || je[4] == '0')        //亿和万之间是否有0
                str_y = "零";
            if (je[7] == '0' || je[8] == '0')        //万和元之间是否有0
                str_w = "零";

            string ret = str1 + str_y + str2 + str_w + str3;    //亿，万，元的三个大写合并

            for (int i = 0; i < ret.Length; i++)        //去掉前面的"零"   
            {
                if (ret[i] != '零')
                {
                    ret = ret.Substring(i);
                    break;
                }

            }
            for (int i = ret.Length - 1; i > -1; i--)      //去掉最后的"零" 
            {
                if (ret[i] != '零')
                {
                    ret = ret.Substring(0, i + 1);
                    break;
                }
            }

            if (ret[ret.Length - 1] != '元')        //若最后不位不是'元'，则加一个'元'字
                ret = ret + "元";

            if (ret == "零零元")           //若为零元，则去掉"元数"，结果只要小数部分
                ret = "";

            if (strf == "00")            //下面是小数部分的转换
            {
                ret = ret + "整";
            }
            else
            {
                string tmp = "";
                tmp = getint(strf[0]);
                if (tmp == "零")
                    ret = ret + tmp;
                else
                    ret = ret + tmp + "角";

                tmp = getint(strf[1]);
                if (tmp == "零")
                    ret = ret + "整";
                else
                    ret = ret + tmp + "分";
            }

            if (ret[0] == '零')
            {
                ret = ret.Substring(1);          //防止0.03转为"零叁分"，而直接转为"叁分"
            }
            if (InJe < 0)//add by wl 2013.09.22
            {
                ret = "负" + ret;
            }
            return ret;             //完成，返回


        }


        /// <summary>                               
        ///* 功能：把金额数据从小写转换为大写套打 既 打印的纸张上有      万   仟    佰    拾  元  角    分   
        /// * 限制条件：金额小于一万亿，且少于两位小数                
        ///* 用法：this.textBox2.Text = FormatHelper.ToUpper(d);   
        /// 小写金额转换为大写金额，其他条件：金额小于一万亿，最多两位小数
        /// </summary>
        /// <param name="d">源金额，je 《 1000000000000.00(一万亿)，且最多两位小数 </param>
        /// <returns>结果，大写金额</returns>
        static public string ToUpperTD(decimal InJe)
        {
            string strKge = "  ";//用来代替 万   仟    佰    拾  元  角    分的空格
            decimal d = Math.Abs(InJe);
            if (d == 0)
                return "零元整";

            string je = d.ToString("####.00");
            if (je.Length > 15)
                return "";
            je = new String('0', 15 - je.Length) + je;      //若小于15位长，前面补0

            string stry = je.Substring(0, 4);        //取得'亿'单元
            string strw = je.Substring(4, 4);        //取得'万'单元
            string strg = je.Substring(8, 4);        //取得'元'单元
            string strf = je.Substring(13, 2);        //取得小数部分

            string str1 = "", str2 = "", str3 = "", str4 = "";

            str1 = getupper(stry, "亿");        //亿单元的大写
            str2 = getupper(strw, strKge);        //万单元的大写



            string str_y = "";
            if (je[3] == '0' || je[4] == '0')        //亿和万之间是否有0
                str_y = "零";

            string ret = str1 + str_y + str2;   //亿，万两个大写合并

            for (int i = 0; i < ret.Length; i++)        //去掉前面的"零"   
            {
                if (ret[i] != '零')
                {
                    ret = ret.Substring(i);
                    break;
                }

            }

            if (ret == "" || ret == "零") //如果不足万元
                ret = "零" + strKge;


            string tmp1 = getint(strg[0]);
            string tmp2 = getint(strg[1]);
            string tmp3 = getint(strg[2]);
            string tmp4 = getint(strg[3]);

            str3 = tmp1 + strKge + tmp2 + strKge + tmp3 + strKge + tmp4 + strKge;   //元单元的大写

            string tmp5 = getint(strf[0]);
            string tmp6 = getint(strf[1]);
            str4 = tmp5 + strKge + tmp6; //角分 

            ret = ret + str3 + str4;

            if (InJe < 0)//add by wl 2013.09.22
            {
                ret = "负" + ret;
            }
            return ret;             //完成，返回


        }


        /// <summary>
        /// 把一个单元转为大写，如亿单元，万单元，个单元
        /// </summary>
        /// <param name="str">这个单元的小写数字（4位长，若不足，则前面补零）</param>
        /// <param name="strDW">亿，万，元</param>
        /// <returns>转换结果</returns>
        static private string getupper(string str, string strDW)
        {
            if (str == "0000")
                return "";

            string ret = "";
            string tmp1 = getint(str[0]);
            string tmp2 = getint(str[1]);
            string tmp3 = getint(str[2]);
            string tmp4 = getint(str[3]);
            if (tmp1 != "零")
            {
                ret = ret + tmp1 + "仟";
            }
            else
            {
                ret = ret + tmp1;
            }

            if (tmp2 != "零")
            {
                ret = ret + tmp2 + "佰";
            }
            else
            {
                if (tmp1 != "零")           //保证若有两个零'00'，结果只有一个零，下同
                    ret = ret + tmp2;
            }

            if (tmp3 != "零")
            {
                ret = ret + tmp3 + "拾";
            }
            else
            {
                if (tmp2 != "零")
                    ret = ret + tmp3;
            }

            if (tmp4 != "零")
            {
                ret = ret + tmp4;
            }

            if (ret[0] == '零')            //若第一个字符是'零'，则去掉
                ret = ret.Substring(1);
            if (ret[ret.Length - 1] == '零')        //若最后一个字符是'零'，则去掉
                ret = ret.Substring(0, ret.Length - 1);

            return ret + strDW;            //加上本单元的单位

        }
        /// <summary>
        /// 单个数字转为大写
        /// </summary>
        /// <param name="c">小写阿拉伯数字 0---9</param>
        /// <returns>大写数字</returns>
        static private string getint(char c)
        {
            string str = "";
            switch (c)
            {
                case '0':
                    str = "零";
                    break;
                case '1':
                    str = "壹";
                    break;
                case '2':
                    str = "贰";
                    break;
                case '3':
                    str = "叁";
                    break;
                case '4':
                    str = "肆";
                    break;
                case '5':
                    str = "伍";
                    break;
                case '6':
                    str = "陆";
                    break;
                case '7':
                    str = "柒";
                    break;
                case '8':
                    str = "捌";
                    break;
                case '9':
                    str = "玖";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>

        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        /// <summary>
        /// 是否为时间型字符串
        /// </summary>
        /// <param name="source">时间字符串(15:00:00)</param>
        /// <returns></returns>

        public static bool IsTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }
        #region 身份证格式验证,以及15.18位互转方法

        /// <summary>
        /// 验证18位身份证格式
        /// </summary>
        /// <param name="cid"></param>
        /// <returns>返回字符串,出错信息 正确返回空字符串</returns>
        public static string CheckCidInfo18(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|X|x)$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return "- 您的身份证号码格式有误!";
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return "- 您的身份证号码格式有误!";//非法生日
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

            }
            if (iSum % 11 != 1)
                return ("- 您的身份证号码格式有误!");//非法证号

            return "";

        }

        /// <summary>
        /// 验证15位身份证格式
        /// </summary>
        /// <param name="cid"></param>
        /// <returns>返回字符串,出错信息 正确返回空字符串</returns>
        public string CheckCidInfo15(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };

            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{15}$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return "- 您的身份证号码格式有误!";
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (int.Parse(cid.Substring(0, 2)) > aCity.Length)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2));
            }
            catch
            {
                return "- 您的身份证号码格式有误!";//非法生日
            }
            return "";
        }

        /// <summary>
        /// 15位转18位身份证号
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public string per15To18(string perIDSrc)
        {
            int iS = 0;
            //加权因子常数 
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数 
            string LastCode = "10X98765432";
            //新身份证号 
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字 
            perIDNew += "19";
            perIDNew += perIDSrc.Substring(6, 9);
            //进行加权求和 
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值 
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

        /// <summary>
        /// 18位转15位身份证号
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public string per18To15(string perIDSrc)
        {
            //前6位
            string str1 = perIDSrc.Substring(0, 6);
            //后9位
            string str2 = perIDSrc.Substring(8, 9);
            //新字符串
            string perIDNew = str1 + str2;
            return perIDNew;

        }
        #endregion


        /// <summary>
        /// 获取字符串中指定位置开始的指定长度的字符串，支持汉字英文混合 汉字为2字节计数
        /// </summary>
        /// <param name="strSub">输入中英混合字符串</param>
        /// <param name="start">开始截取的起始位置</param>
        /// <param name="length">要截取的字符串长度</param>
        /// <returns></returns>
        public static string GetSubString(string strSub, int start, int length)
        {
            string temp = strSub;
            int j = 0, k = 0, p = 0;

            CharEnumerator ce = temp.GetEnumerator();
            while (ce.MoveNext())
            {
                j += (ce.Current > 0 && ce.Current < 255) ? 1 : 2;

                if (j <= start)
                {
                    p++;
                }
                else
                {
                    if (j == GetStringLength(temp))
                    {
                        temp = temp.Substring(p, k + 1);
                        break;
                    }
                    if (j <= length + start)
                    {
                        k++;
                    }
                    else
                    {
                        temp = temp.Substring(p, k);
                        break;
                    }
                }
            }

            return temp;
        }



        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public static int GetStringLength(string str)
        {
            if (str.Equals(string.Empty))

                return 0;
            int strlen = 0;

            ASCIIEncoding strData = new ASCIIEncoding();

            byte[] strBytes = strData.GetBytes(str);

            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)

                    strlen++;

                strlen++;

            }
            return strlen;
        }

        /// <summary>
        ///返回 GUID 用于数据库操作，特定的时间代码可以提高检索效
        /// </summary>
        /// <returns>COMB (GUID 与时间混合型) 类型 GUID 数据</returns>
        public static Guid NewComb()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));

            // Convert to a byte array // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.33333333333));
            // Reverse the bytes to match SQL Servers ordering Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new System.Guid(guidArray);
        }
        /// <summary>
        /// 从 SQL SERVER 返回的 GUID 中生成时间信息 
        /// </summary>
        /// <param name="guid">包含时间信息的 COMB</param>
        /// <returns></returns>
        public static DateTime GetDateFromComb(System.Guid guid)
        {
            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4]; byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();
            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);
            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Convert the bytes to ints
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);
            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.33333333333);
            return date;
        }

        /// <summary>    
        /// 实现数据的四舍五入法　　 
        /// </summary>    
        /// <param name="v">要进行处理的数据</param>   
        /// <param name="x">保留的小数位数</param>    
        /// <returns>四舍五入后的结果</returns>    
        public static double Round(double v, int x)
        {
            bool isNegative = false;
            //如果是负数       
            if (v < 0)
            {
                isNegative = true;
                v = -v;
            }
            int IValue = 1;
            for (int i = 1; i <= x; i++)
            {
                IValue = IValue * 10;
            }
            double Int = Math.Round(v * IValue + 0.5, 0);
            v = Int / IValue;
            if (isNegative)
            { v = -v; }
            return v;
        }
        /// <summary>
        /// 判断某一数据是Null或由空字符串组成的
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(object dc)
        {
            if (dc == DBNull.Value)
                return true;
            return string.IsNullOrEmpty(dc.ToString().Trim());
        }
        /// <summary>
        /// 返回 某一数据转换成string 并去掉空格  是Null返回 ""
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static string GetStr(object dc)
        {
            if (dc == DBNull.Value)
                return "";
            try
            {
                return dc.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 判断某一int数据是Null或0
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static bool IntIsDBNullOrZero(object dc)
        {
            if (dc == DBNull.Value)
                return true;
            try
            {
                return int.Parse(dc.ToString()) == 0;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 判断某一Decimal数据是Null或0
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static bool DecimalIsDBNullOrZero(object dc)
        {

            if (dc == DBNull.Value)
                return true;
            try
            {
                return Decimal.Parse(dc.ToString()) == 0;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 返回某一Bool数据是Null返回false
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static bool Bool(object dc)
        {
            if (dc == DBNull.Value)
                return false;
            return bool.Parse(dc.ToString());

        }
        /// <summary>
        /// 返回某一Decimal数据是Null或0
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object dc)
        {

            if (dc == DBNull.Value)
                return 0;
            try
            {
                return Decimal.Parse(dc.ToString());
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 返回某一float数据是Null或0
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static float GetFloat(object dc)
        {

            if (dc == DBNull.Value)
                return 0;
            try
            {

                return float.Parse(dc.ToString());
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 返回某一Int数据是Null或0
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static int GetInt(object dc)
        {

            if (dc == DBNull.Value)
                return 0;
            try
            {
                return int.Parse(dc.ToString());
            }
            catch
            {
                return 0;
            }

        }

        /// <summary>
        /// /去除小数点后面多余的0 .000 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearDec0(string str)
        {
            if (!str.Contains('.'))
            {
                return str;
            }
            else
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str.Substring(i, 1) == "0")
                    {
                        str = str.Substring(0, i) + " ";
                    }
                    else if (str.Substring(i, 1) == ".")
                    {
                        str = str.Substring(0, i) + " ";
                        break;
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return str.Trim();
        }
    }
}