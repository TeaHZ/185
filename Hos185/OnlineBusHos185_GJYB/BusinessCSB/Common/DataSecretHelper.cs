using System.Text;

namespace OnlineBusHos185_GJYB
{
    class DataSecretHelper
    {
        private const string aesKey = "C51CCEB037B32E4DFB434D4E26E78190";

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptData(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return AESHelper.Encrypt(str, aesKey);
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecryptData(string str)
        {
            try
            {
                return AESHelper.Decrypt(str, aesKey);
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 加密手机号
        /// </summary>
        /// <param name="Mobile_no"></param>
        /// <returns></returns>
        public static string EncryptMobileNo(string Mobile_no)
        {
            if (string.IsNullOrEmpty(Mobile_no))
            {
                return "";
            }
            if (Mobile_no.Length < 11)
            {
                Mobile_no += "|";
                Mobile_no = Mobile_no.PadRight(11, '0');
            }
            Mobile_no = string.Concat(new string[]
            {
                Mobile_no,
                Mobile_no[0].ToString(),
                Mobile_no[3].ToString(),
                Mobile_no[6].ToString(),
                Mobile_no[7].ToString()
            });
            return AESHelper.Encrypt(Mobile_no, aesKey);
        }

        /// <summary>
        /// 隐藏手机号*
        /// </summary>
        /// <param name="Mobile_no"></param>
        /// <returns></returns>
        public static string HideMobileNo(string Mobile_no)
        {
            //17625896916 -176****6916
            //320***********6919
            if (string.IsNullOrEmpty(Mobile_no))
            {
                return "";
            }
            if (Mobile_no.Length == 11)
            {
                Mobile_no = string.Concat(Mobile_no.Substring(0, 3), "****", Mobile_no.Substring(7));
            }
            else
            {
                //不是11位的，显示前半部分
                int index = Mobile_no.Length / 2;
                int length = Mobile_no.Length;

                StringBuilder sb = new StringBuilder();
                sb.Append(Mobile_no.Substring(0, index));
                for (int i = 0; i < length - index; i++)
                {
                    sb.Append("*");
                }
                Mobile_no = sb.ToString();
            }
            return Mobile_no;
        }

        /// <summary>
        /// 解密手机号
        /// </summary>
        /// <param name="MobileSecret"></param>
        /// <returns></returns>
        public static string DecryptMobileNo(string MobileSecret)
        {
            if (string.IsNullOrEmpty(MobileSecret))
            {
                return "";
            }
            try
            {
                string Mobile_no = "";
                Mobile_no = AESHelper.Decrypt(MobileSecret, aesKey);

                if (Mobile_no.Contains("|"))
                {
                    Mobile_no = Mobile_no.Substring(0, Mobile_no.IndexOf('|'));
                }
                else
                {
                    Mobile_no = Mobile_no.Substring(0, Mobile_no.Length - 4);
                }

                return Mobile_no;
            }
            catch
            {
                return MobileSecret;
            }
        }

        /// <summary>
        /// 加密身份证号
        /// </summary>
        /// <param name="sfz_no"></param>
        /// <returns></returns>
        public static string EncryptSfzNo(string sfz_no)
        {
            if (string.IsNullOrEmpty(sfz_no))
            {
                return "";
            }
            if (sfz_no.Length < 18)
            {
                sfz_no += "|";
                sfz_no = sfz_no.PadRight(18, '0');
            }
            sfz_no = string.Concat(new string[]
            {
                sfz_no,
                sfz_no[1].ToString(),
                sfz_no[4].ToString(),
                sfz_no[5].ToString(),
                sfz_no[7].ToString(),
                sfz_no[8].ToString()
            });
            return AESHelper.Encrypt(sfz_no, aesKey);
        }

        /// <summary>
        /// 隐藏身份证*
        /// </summary>
        /// <param name="Mobile_no"></param>
        /// <returns></returns>
        public static string HideSfzNo(string sfz_no)
        {
            //17625896916 -176****6916
            //320***********6919
            //320722950718691
            //320*********691
            if (string.IsNullOrEmpty(sfz_no))
            {
                return "";
            }
            if (sfz_no.Length == 18)
            {
                sfz_no = string.Concat(sfz_no.Substring(0, 3), "***********", sfz_no.Substring(14));
            }
            else if (sfz_no.Length == 15)
            {
                sfz_no = string.Concat(sfz_no.Substring(0, 3), "*********", sfz_no.Substring(12));
            }
            else
            {
                //不是11位的，显示前半部分
                int index = sfz_no.Length / 2;
                int length = sfz_no.Length;

                StringBuilder sb = new StringBuilder();
                sb.Append(sfz_no.Substring(0, index));
                for (int i = 0; i < length - index; i++)
                {
                    sb.Append("*");
                }
                sfz_no = sb.ToString();
            }
            return sfz_no;
        }

        /// <summary>
        /// 解密身份证号
        /// </summary>
        /// <param name="SfzNoSecret"></param>
        /// <returns></returns>
        public static string DecryptSfzNo(string SfzNoSecret)
        {
            if (string.IsNullOrEmpty(SfzNoSecret))
            {
                return "";
            }
            try
            {
                string sfz_no = "";
                sfz_no = AESHelper.Decrypt(SfzNoSecret, aesKey);

                if (sfz_no.Contains("|"))
                {
                    sfz_no = sfz_no.Substring(0, sfz_no.IndexOf('|'));
                }
                else
                {
                    sfz_no = sfz_no.Substring(0, sfz_no.Length - 5);
                }

                return sfz_no;
            }
            catch
            {
                return SfzNoSecret;
            }
        }


        /// <summary>
        /// 隐藏邮箱
        /// </summary>
        /// <param name="Mobile_no"></param>
        /// <returns></returns>
        public static string HideEMail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "";
            }
            if (email.Contains("@"))
            {
                //显示@后面的
                int index = email.IndexOf('@');
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < index; i++)
                {
                    sb.Append("*");
                }
                sb.Append(email.Substring(index));
                email = sb.ToString();
            }

            return email;
        }

    }
}
