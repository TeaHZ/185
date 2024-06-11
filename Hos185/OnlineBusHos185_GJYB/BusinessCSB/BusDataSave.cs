using System;

namespace OnlineBusHos185_GJYB
{
    public class BusDataSave
    {
        public static bool T9001(string HOS_ID, bool Sync_flag, Models.InputRoot inputRoot, Models.T9001.Root inputdata, Models.RT9001.Root outputdata,string ybtype)
        {
            try
            {
                string sign_no = outputdata.signinoutb.sign_no;
                string opter_no = inputdata.signIn.opter_no;
                Model.hos_opter_sign hos_Opter_Sign = new Model.hos_opter_sign();
                hos_Opter_Sign.HOS_ID = HOS_ID;
                hos_Opter_Sign.opter_no = inputdata.signIn.opter_no;
                hos_Opter_Sign.mac = inputdata.signIn.mac;
                hos_Opter_Sign.ip = inputdata.signIn.ip;
                hos_Opter_Sign.sign_no = sign_no;
                hos_Opter_Sign.YB_TYPE = ybtype;
                try
                {
                    hos_Opter_Sign.sign_intime = Convert.ToDateTime(outputdata.signinoutb.sign_time);
                }
                catch
                {
                    hos_Opter_Sign.sign_intime = DateTime.Now;
                }
                hos_Opter_Sign.signout_flag = 0;
                new BLL.hos_opter_sign().Add(hos_Opter_Sign);//当天重复签到，返回的sign_no是相同的，进行更新

                BLL.hos_opter hos_Opter = new BLL.hos_opter();
                Model.hos_opter modelhos_Opter = hos_Opter.GetModel(HOS_ID, opter_no, ybtype);
 
                modelhos_Opter.sign_no = sign_no;
                modelhos_Opter.sign_date = DateTime.Now.ToString("yyyy-MM-dd");
                modelhos_Opter.mac = inputdata.signIn.mac;
                modelhos_Opter.ip = inputdata.signIn.ip;
                modelhos_Opter.YB_TYPE = ybtype;
                hos_Opter.Update(modelhos_Opter);

                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    sqlerrlog sqlerrlog = new sqlerrlog();
                    sqlerrlog.inTime = DateTime.Now;
                    sqlerrlog.TYPE = "数据保存失败";
                    sqlerrlog.ExceptionMessage = "9001：" + ex.Message;
                    LogHelper.SaveSqlerror(sqlerrlog);
                }
                catch { }
                return false;
            }

        }

        public static bool T9002(string HOS_ID, bool Sync_flag, Models.InputRoot inputRoot, Models.T9002.Root inputdata, Models.RT9002.Root outputdata,string ybtype)
        {
            try
            {
                string opter_no = inputdata.signOut.opter_no;
                BLL.hos_opter hos_Opter = new BLL.hos_opter();
                Model.hos_opter modelhos_Opter = hos_Opter.GetModel(HOS_ID, opter_no, ybtype);
                if (modelhos_Opter != null)
                {
                    modelhos_Opter.sign_no = "";
                    modelhos_Opter.sign_date = "";
                    hos_Opter.Update(modelhos_Opter);
                }


                string sign_no = inputdata.signOut.sign_no;
                Model.hos_opter_sign hos_Opter_Sign = new BLL.hos_opter_sign().GetModel(HOS_ID, opter_no, sign_no);
                if (hos_Opter_Sign != null)
                {
                    try
                    {
                        hos_Opter_Sign.sign_intime = Convert.ToDateTime(outputdata.signoutoutb.sign_time);
                    }
                    catch
                    {
                        hos_Opter_Sign.sign_intime = DateTime.Now;
                    }
                    hos_Opter_Sign.signout_flag = 1;
                    new BLL.hos_opter_sign().Update(hos_Opter_Sign);
                }
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    sqlerrlog sqlerrlog = new sqlerrlog();
                    sqlerrlog.inTime = DateTime.Now;
                    sqlerrlog.TYPE = "数据保存失败";
                    sqlerrlog.ExceptionMessage = "9002：" + ex.Message;
                    LogHelper.SaveSqlerror(sqlerrlog);
                }
                catch { }
                return false;
            }

        }
    }
}
