namespace OnlineBusHos9_GJYB
{
    public class DataHelper
    {
        /// <summary>
        /// 查询HOS_ID是否存在
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <returns></returns>
        public static bool ExistsHospital(string HOS_ID)
        {
            bool flag = new BLL.hospital().Exists(HOS_ID);
            return flag;
        }

        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <returns></returns>
        public static Model.hospital GetHospital(string HOS_ID)
        {
            Model.hospital hospital = new BLL.hospital().GetModel(HOS_ID);
            return hospital;
        }
    }
}
