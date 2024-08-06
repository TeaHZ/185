using CommonModel;
using Hos185_His.Models;
using Hos185_His;

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace OnlineBusHos185_Common.BUS
{
    class GETPATRECORD
    {
        public static string B_GETPATRECORD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Model.GETPATRECORD_M.GETPATRECORD_IN _in = JsonConvert.DeserializeObject<Model.GETPATRECORD_M.GETPATRECORD_IN>(json_in);
                Model.GETPATRECORD_M.GETPATRECORD_OUT _out = new Model.GETPATRECORD_M.GETPATRECORD_OUT();
                string HOS_ID = FormatHelper.GetStr(_in.HOS_ID);
                string SFZ_NO = FormatHelper.GetStr(_in.SFZ_NO);
                if (SFZ_NO.Length == 15)
                {
                    SFZ_NO = PubFunc.IDCard15To18(SFZ_NO);
                }
                string PAT_NAME = FormatHelper.GetStr(_in.PAT_NAME);
                string SEX = FormatHelper.GetStr(_in.SEX);
                string AGE = FormatHelper.GetStr(_in.AGE);
                string ADDRESS = FormatHelper.GetStr(_in.ADDRESS);
                string BIRTHDAY = FormatHelper.GetStr(_in.BIR_DATE);
                //从身份证 截取
                BIRTHDAY = SFZ_NO.Substring(6, 4) + "-" + SFZ_NO.Substring(10, 2) + "-" + SFZ_NO.Substring(12, 2);
                string GUARDIAN_NAME = FormatHelper.GetStr(_in.GUARDIAN_NAME);
                string GUARDIAN_SFZ_NO = FormatHelper.GetStr(_in.GUARDIAN_SFZ_NO);
                string MOBILE_NO = FormatHelper.GetStr(_in.MOBILE_NO);
                string YLCARD_TYPE = FormatHelper.GetStr(_in.YLCARD_TYPE);
                string YLCARD_NO = FormatHelper.GetStr(_in.YLCARD_NO);
                string USER_ID = FormatHelper.GetStr(_in.USER_ID);
                string PAT_CARD_OUT = FormatHelper.GetStr(_in.PAT_CARD_OUT);
                string BIR_DATE = FormatHelper.GetStr(_in.BIR_DATE);


                try
                {

                    string idCardType = "01";

                    switch (YLCARD_TYPE)
                    {
                        case "4":
                            idCardType = "01";
                            
                        bool containsLetter = Regex.IsMatch(YLCARD_NO.Substring(0, 1), "[a-zA-Z]");
                            if (YLCARD_NO.Substring(0, 1) == "9" || containsLetter)
                            {
                                idCardType = "97";
                            }
                            break;
                        default:
                            break;
                    }

                    string sex = SEX == "男" ? "1" : "2";


                    Hos185_His.Models.SENDCARDINFO sendcardinfo = new Hos185_His.Models.SENDCARDINFO()
                    {
                        area = "", //现住地：区code
                        birthDay = BIRTHDAY, //出⽣⽇期 yyyy-MM-dd
                        city = "", //现住地：市code
                        detailAddress = ADDRESS, //现住地：详细信
                        guardianIdCardNo = GUARDIAN_SFZ_NO, //监护⼈证件号
                        home = "", //⼾⼝或家庭地址
                        homeTel = MOBILE_NO, //联系电话
                        idCardNo = SFZ_NO, //证件号
                        idCardType = idCardType, //证件类型 01:⾝份证 06:护照 08:港澳台居⺠来往内地通⾏证
                        linkManAddress = ADDRESS, //联系⼈地址
                        linkManName = PAT_NAME, //联系⼈姓名
                        linkManTel = MOBILE_NO, //联系⼈电话
                        mcardNo = SFZ_NO, //绑定的医疗证号
                        mcardNoType = "4", //绑定的医疗证类型 4:⾝份证/港澳台通⾏证 5:护照
                        name = PAT_NAME, //姓名
                        nationCode = "", //⺠族code
                        operCode = "", //操作⼈
                        pactCode = "", //合同编号
                        province = "", //现住地：省code
                        relaCode = "", //联系⼈和患者关系编码
                        road = ADDRESS, //现住地：街道code

                        sexCode = sex, //性别编码 M:男, F:⼥
                        sourceCode = "", //客⼾来源
                        sourceFlag = "MZ", //建档来源 MZ ⻔诊 JZ 急诊 TJ 体检
                        ternimalNo = "" //ternimalNo
                    };
                    TKHelper main = new TKHelper();

                    string inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(sendcardinfo);

                    Hos185_His.Models.Output<List<Hos185_His.Models.SENDCARDINFODATA>>
                        output = main.CallServiceAPI<List<Hos185_His.Models.SENDCARDINFODATA>>("/hispatientinfo/compatient/saveComPatientInfo", inputjson);

                    //StreamReader stream = new StreamReader("F:\\测试参数\\record.txt");
                    //string aab = stream.ReadToEnd();

                    //Hos185_His.Models.Output<List<Hos185_His.Models.SENDCARDINFODATA>> output = JsonConvert.DeserializeObject<Hos185_His.Models.Output<List<Hos185_His.Models.SENDCARDINFODATA>>>(aab);

                    if (output.code == 0)
                    {

                        Hos185_His.Models.SENDCARDINFODATA data = output.data.FirstOrDefault();

                        if (data!=null)
                        {


                            if (data.name.Trim()!=_in.PAT_NAME)
                            {
                                dataReturn.Code = 5;
                                dataReturn.Msg = "[提示]建档失败，请至人工窗口处理";
                                dataReturn.Param = "";
                                goto EndPoint;
                            }
                            _out.HOSPATID = data.cardNo;


                            #region 平台建档
                            var db = new DbMySQLZZJ().Client;
                            SqlSugarModel.PatInfo patInfo = new SqlSugarModel.PatInfo();
                            SqlSugarModel.PatCard patCard = new SqlSugarModel.PatCard();
                            SqlSugarModel.PatCardBind patCardBind = new SqlSugarModel.PatCardBind();
                            //如果身份证不为空，先用身份证查询
                            if (!string.IsNullOrEmpty(_in.SFZ_NO))
                            {
                                patInfo = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.SFZ_NO == _in.SFZ_NO).First();

                                if (patInfo != null)
                                {
                                    patCard = db.Queryable<SqlSugarModel.PatCard>().Where(t => t.PAT_ID == patInfo.PAT_ID && t.YLCARD_TYPE == FormatHelper.GetInt(_in.YLCARD_TYPE) && t.YLCARD_NO == _in.YLCARD_NO).First();
                                    //如果不同的卡对应不同的HOSPTAID，需要加上卡号去查
                                    patCardBind = db.Queryable<SqlSugarModel.PatCardBind>().Where(t => t.HOS_ID == _in.HOS_ID && t.PAT_ID == patInfo.PAT_ID).First();

                                }
                            }
                            else   //通过卡获取
                            {
                                patCard = db.Queryable<SqlSugarModel.PatCard>().Where(t => t.YLCARD_TYPE == FormatHelper.GetInt(_in.YLCARD_TYPE) && t.YLCARD_NO == _in.YLCARD_NO).First();

                                if (patCard != null)
                                {
                                    patInfo = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.PAT_ID == patCard.PAT_ID).First();
                                    //如果不同的卡对应不同的HOSPTAID，需要加上卡号去查
                                    patCardBind = db.Queryable<SqlSugarModel.PatCardBind>().Where(t => t.HOS_ID == _in.HOS_ID && t.PAT_ID == patInfo.PAT_ID).First();

                                }
                            }
                            if (patInfo == null)
                            {

                                int pat_id = 0;
                                if (!PubFunc.GetSysID("pat_info", out pat_id))
                                {
                                    dataReturn.Code = 5;
                                    dataReturn.Msg = "[提示]建档失败，请联系医院处理";
                                    dataReturn.Param = "获取pat_info的sysid失败";
                                    goto EndPoint;
                                }

                                patInfo = new SqlSugarModel.PatInfo();
                                patInfo.PAT_ID = pat_id;
                                patInfo.SFZ_NO = SFZ_NO;
                                patInfo.PAT_NAME = PAT_NAME;
                                patInfo.SEX = SEX;
                                patInfo.BIRTHDAY = BIRTHDAY;
                                patInfo.ADDRESS = ADDRESS;
                                patInfo.MOBILE_NO = MOBILE_NO;
                                patInfo.GUARDIAN_NAME = GUARDIAN_NAME;
                                patInfo.GUARDIAN_SFZ_NO = GUARDIAN_SFZ_NO;
                                patInfo.CREATE_TIME = DateTime.Now;
                                patInfo.MARK_DEL = false;
                                patInfo.OPER_TIME = DateTime.Now;
                                patInfo.NOTE = _in.LTERMINAL_SN;
                                db.Insertable(patInfo).ExecuteCommand();
                            }
                            if (patCard == null)
                            {
                                patCard = new SqlSugarModel.PatCard();
                                patCard.PAT_ID = patInfo.PAT_ID;
                                patCard.YLCARD_TYPE = FormatHelper.GetInt(_in.YLCARD_TYPE);
                                patCard.YLCARD_NO = _in.YLCARD_NO;
                                patCard.CREATE_TIME = DateTime.Now;
                                patCard.MARK_DEL = "0";
                                db.Insertable(patCard).ExecuteCommand();
                            }
                            if (patCardBind == null)
                            {
                                patCardBind = new SqlSugarModel.PatCardBind();
                                patCardBind.HOS_ID = _in.HOS_ID;
                                patCardBind.PAT_ID = patInfo.PAT_ID;
                                patCardBind.YLCARD_TYPE = FormatHelper.GetInt(_in.YLCARD_TYPE);
                                patCardBind.YLCARD_NO = _in.YLCARD_NO;
                                patCardBind.HOSPATID = _out.HOSPATID;
                                patCardBind.MARK_BIND = 1;
                                patCardBind.BAND_TIME = DateTime.Now;
                                db.Insertable(patCardBind).ExecuteCommand();
                            }

                            #endregion

                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param = JsonConvert.SerializeObject(_out);

                        }
                        else
                        {
                            dataReturn.Code = output.code;
                            dataReturn.Msg = output.message;

                        }

                    }
                    else
                    {
                        dataReturn.Code = output.code;
                        dataReturn.Msg = output.message;
                    }

                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确"+ex.Message;
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

    }
}
