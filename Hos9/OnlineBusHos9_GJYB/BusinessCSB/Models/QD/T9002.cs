namespace OnlineBusHos9_GJYB.Models
{
    public class T9002
    {
        /// <summary>
        /// 输入（节点标识：signIn）
        /// </summary>
        public class SignOut
        {
            public string sign_no { get; set; }// 签到编号
            public string opter_no { get; set; }//操作员编号

        }

        public class Root
        {
            public SignOut signOut { get; set; }
        }
    }


    public class RT9002
    {
        /// <summary>
        /// 输出（节点标识：signinoutb）
        /// </summary>
        public class Signoutoutb
        {
            public string sign_time { get; set; }//签退时间
        }

        public class Root
        {
            public Signoutoutb signoutoutb { get; set; }
        }
    }
}
