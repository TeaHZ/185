namespace OnlineBusHos185_GJYB.Models
{
    public class T9001
    {
        /// <summary>
        /// 输入（节点标识：signIn）
        /// </summary>
        public class SignIn
        {
            public string opter_no { get; set; }// 操作员编号
            public string mac { get; set; }//签到MAC地址
            public string ip { get; set; }//签到IP地址

        }

        public class Root
        {
            public SignIn signIn { get; set; }

        }
    }

    public class RT9001
    {
        /// <summary>
        /// 输出（节点标识：signinoutb）
        /// </summary>
        public class Signinoutb
        {
            public string sign_time { get; set; }//签到时间
            public string sign_no { get; set; }//签到编号

        }

        public class Root
        {
            public Signinoutb signinoutb { get; set; }
        }
    }
}
