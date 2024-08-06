using BusinessInterface;
using PasS.Base.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineBusHos185
{
    /// <summary>
    /// 满意度评价接口
    /// </summary>
    public class PBusHos185 : ProcessingBusinessAsyncResult
    {

        /// <summary>
        /// 医院数据库链接的标识
        /// </summary>
        internal static ThreadLocal<string> AUID = new ThreadLocal<string>();
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);

            AUID.Value = InBusinessInfo.AUID;
            try
            {
                string context = InBusinessInfo.BusData;
                OutBusinessInfo.BusData = Entrance.BusinessHos(context);
                OutBusinessInfo.ReslutCode = 1;//返回结果标识 1成功
                OutBusinessInfo.ResultMessage = "Success";//返回结果说明
                return true;
            }
            catch (Exception ex)
            {
                OutBusinessInfo.ReslutCode = 0;//返回结果标识 1成功
                OutBusinessInfo.ResultMessage = string.Format("Process [{0}] fail:{1}", InBusinessInfo.BusID, ex.Message);//返回结果说明
                return true;
            }
        }
        public override byte[] DefErrotReturn(int Code, string ErrorMsage)
        {
            return new Byte[0];
        }

    }
}
