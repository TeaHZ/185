using CommonModel;
using Hos185_His;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos185_Common.BUS
{
    internal class GETPATINFO
    {
        public static string B_GETPATINFO(string json_in)
        {
            return Business(json_in);
        }

        public static string Business(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";;;
            try
            {
                Model.GETPATINFO_M.GETPATINFO_IN _in = JsonConvert.DeserializeObject<Model.GETPATINFO_M.GETPATINFO_IN>(json_in);
                Model.GETPATINFO_M.GETPATINFO_OUT _out = new Model.GETPATINFO_M.GETPATINFO_OUT();
                Dictionary<string, string> dic_filter = PubFunc.Get_Filter(FormatHelper.GetStr(_in.FILTER));

                string HOS_ID = FormatHelper.GetStr(_in.HOS_ID);
                string YLCARD_TYPE = FormatHelper.GetStr(_in.YLCARD_TYPE);//0 0无卡1院内卡2医保卡3 市民卡4身份证
                string YLCARD_NO = FormatHelper.GetStr(_in.YLCARD_NO);
                string SFZ_NO = FormatHelper.GetStr(_in.SFZ_NO);
                string LTERMINAL_SN = FormatHelper.GetStr(_in.LTERMINAL_SN);
                string USER_ID = FormatHelper.GetStr(_in.USER_ID);
                string PAT_CARD_OUT = dic_filter.ContainsKey("PAT_CARD_OUT") ? dic_filter["PAT_CARD_OUT"] : "";

                if (!string.IsNullOrEmpty(SFZ_NO)&&SFZ_NO.Length!=18)
                {
                    _out.IS_EXIST = "0";
                    dataReturn.Code = 2;
                    dataReturn.Msg = "当前身份证号不规范，请到人工窗口办理业务！";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;
                }

                string idCardType = "";
                string mcardNoType = "";
                if (_in.YLCARD_TYPE == "91")
                {
                    _in.SFZ_NO = _in.YLCARD_NO;
                }
                switch (_in.YLCARD_TYPE)
                {
                    case "1":
                        idCardType = "";
                        mcardNoType = "";
                        break;

                    case "2":
                    case "6":
                        idCardType = "01";
                        mcardNoType = "4";
                        break;

                    case "4":
                        idCardType = "01";
                        mcardNoType = "4";
                        break;
                    case "91":
                        idCardType = "01";
                        mcardNoType = "4";
                        break;
                    default:
                        break;
                }

                Hos185_His.Models.GETPATINFO getpatinfo = new Hos185_His.Models.GETPATINFO()
                {
                    businessType = "",
                    cardNo = _in.YLCARD_NO, //医院内部就诊卡号
                    idCardNo = _in.SFZ_NO, //证件号 和 cardNo不能同时为空
                    idCardType = idCardType, //证件类型 01:⾝份证 06:护照 08:港澳台居⺠来往内地通⾏证
                    mcardNo = "", //绑定的医疗证号
                    mcardNoType = "", //绑定的医疗证类型 4:⾝份证/港澳台通⾏证 5:护照
                    name = _in.PAT_NAME, //姓名（精确查找）
                    needWHBindHealthCardFlag = "", //是否获取武汉电⼦健康卡绑定信息
                    phoneNo = ""  //⼿机号
                };

                TKHelper main = new TKHelper();

                string inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(getpatinfo);

                Hos185_His.Models.Output<List<Hos185_His.Models.GETPATINFODATA>>
                    output = main.CallServiceAPI<List<Hos185_His.Models.GETPATINFODATA>>("/hispatientinfo/compatient/getComPatientInfo", inputjson);

                string age = "";

                if (output.code == 0)
                {
                    List<Hos185_His.Models.GETPATINFODATA> datas = output.data;

                    if (datas.Count == 0)
                    {
                        _out.IS_EXIST = "0";
                        dataReturn.Code = 0;
                        dataReturn.Msg = "没有查询到个人信息";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        json_out = JsonConvert.SerializeObject(dataReturn);
                        return json_out;
                    }
                    else
                    {
                        Hos185_His.Models.GETPATINFODATA data = datas.FirstOrDefault();

                        if (!string.IsNullOrEmpty(_in.PAT_NAME))
                        {
                            if (_in.PAT_NAME.Trim()!=data.name)
                            {
                                dataReturn.Code = 2;
                                dataReturn.Msg = "[提示]身份校验失败，请至人工窗口处理";
                                dataReturn.Param = JsonConvert.SerializeObject(_out);
                                return JsonConvert.SerializeObject(dataReturn);
                      
                            }
                        }

          
                        _out.IS_EXIST = "1";
                        _out.PAT_NAME = data.name;
                        _out.SEX = data.sexName;
                        _out.AGE = data.birthDay;
                        _out.MOBILE_NO = data.homeTel;
                        _out.ADDRESS = data.detailAddress;
                        _out.SFZ_NO = data.idCardNo;
                        _out.HOSPATID = data.cardNo;
                        _out.BIR_DATE = data.birthDay;
                        _out.GUARDIAN_NAME = data.linkManName;
                        dataReturn.Code = 0;
                        dataReturn.Msg = "SUCCESS";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);

                        //  return JsonConvert.SerializeObject  (dataReturn);
                    }

                    var db = new DbMySQLZZJ().Client;
                    #region 电子健康卡
                    bool IsEHealthCard = true;
                    EHealthCard.qrCodeAnalysisResponse response = new EHealthCard.qrCodeAnalysisResponse();
                    if (_in.YLCARD_TYPE == "8")//电子健康卡
                    {
                        response = EHealthCard.EHealthCardBus.GetHealthCardInfo(_in.YLCARD_NO);
                        if (response.code == 0)
                        {
                            //按身份证使用
                            _in.SFZ_NO = (response.data.idNumber).ToUpper();//身份证的x转成大写
                            _in.YLCARD_TYPE = "4";
                            _in.YLCARD_NO = _in.SFZ_NO;
                
                        }
                        else
                        {
                            dataReturn.Code = 999;
                            dataReturn.Msg = "电子健康卡信息查询失败：" + response.msg;
                            goto EndPoint;
                        }
                    }
                    #endregion

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
                    //没有绑过卡，调用医院接口
                    if (patInfo == null || patCardBind == null)
                    {
                        try
                        {
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
                                patInfo.SFZ_NO = _out.SFZ_NO;
                                patInfo.PAT_NAME = _out.PAT_NAME;
                                patInfo.SEX = _out.SEX;
                                patInfo.BIRTHDAY = _out.BIR_DATE;
                                patInfo.ADDRESS = _out.ADDRESS;
                                patInfo.MOBILE_NO = _out.MOBILE_NO;
                                patInfo.GUARDIAN_NAME = _out.GUARDIAN_NAME;
                                patInfo.GUARDIAN_SFZ_NO = _out.GUARDIAN_SFZ_NO;
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

                 
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param = JsonConvert.SerializeObject(_out);
                        }
                        catch (Exception ex)
                        {
                            dataReturn.Code = 5;
                            dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                            dataReturn.Param = ex.Message;
                        }
                    }
                    else
                    {
                        if (patCardBind.HOSPATID != _out.HOSPATID)
                        {

                            patCardBind.HOSPATID= _out.HOSPATID;    

                            db.Updateable<SqlSugarModel.PatCardBind>(patCardBind)
                                .UpdateColumns(t => new { t.HOSPATID })
                                .Where(t=>t.PAT_ID==patCardBind.PAT_ID &&t.HOS_ID==patCardBind.HOS_ID).ExecuteCommand();
                        }

                        patInfo.SFZ_NO= _out.SFZ_NO;
                        patInfo.PAT_NAME= _out.PAT_NAME;
                        patInfo.BIRTHDAY = _out.BIR_DATE;
                        patInfo.SEX = _out.SEX;
                        patInfo.ADDRESS=_out.ADDRESS;
                        db.Updateable(patInfo).UpdateColumns(t => new { t.PAT_NAME,t.SFZ_NO ,t.BIRTHDAY,t.SEX,t.ADDRESS}).ExecuteCommand();


                        _out.IS_EXIST = "1";
                        _out.PAT_NAME = patInfo.PAT_NAME;
                        _out.SEX = patInfo.SEX;
                        _out.AGE = "";
                        _out.MOBILE_NO = patInfo.MOBILE_NO;
                        _out.ADDRESS = patInfo.ADDRESS;
                        _out.SFZ_NO = patInfo.SFZ_NO;
                        _out.HOSPATID = _out.HOSPATID;
                        _out.BIR_DATE = patInfo.BIRTHDAY;
                        _out.GUARDIAN_NAME = patInfo.GUARDIAN_NAME;
                        dataReturn.Code = 0;
                        dataReturn.Msg = "SUCCESS";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                    }
                }
                else
                {
                    _out.IS_EXIST = "0";
                    _out.PAT_NAME = _in.PAT_NAME;
                    _out.SEX = "";
                    _out.AGE = "";
                    _out.MOBILE_NO = "";
                    _out.ADDRESS = "";
                    _out.SFZ_NO = "";
                    _out.HOSPATID = "";
                    _out.BIR_DATE = "";
                    _out.GUARDIAN_NAME = "";
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS"; ;
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.Message;
            }

        EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
