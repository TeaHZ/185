using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Common.HISModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnlineBusHos9_Common.BUS
{
    internal class GETPATINFO
    {
        public static string B_GETPATINFO(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
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

                string idCardType = "";
                string mcardNoType = "";

                string duKaFS = "1";
                if (_in.MDTRT_CERT_TYPE == "01")
                {
                    duKaFS = "5";
                }

                if (_in.YLCARD_NO == "TEST666666")
                {//心跳？
                    dataReturn.Code = 0;
                    dataReturn.Msg = "咚咚";
                    dataReturn.Param = "";

                    return JsonConvert.SerializeObject(dataReturn);
                }

                T1001.Input t1001 = new T1001.Input()
                {
                    zhengJianHM = _in.SFZ_NO,// 证件号码
                    yeWuLX = "1001",// 业务类型        1001:患者信息查询
                    hospitalId = "320282466455146",// 医院ID
                    yiBaoBH = _in.YLCARD_TYPE == "2" ? _in.YLCARD_NO : "",//医保编号 医保卡必传
                    yiBaoData = _in.BUS_CARD_INFO,//医保信息        医保卡必传
                    duKaFS = duKaFS,// 读卡方式 默认1
                    jiuZhenKH = _in.YLCARD_TYPE == "1" ? _in.YLCARD_NO : "",// 就诊卡号
                    yiBaoXX = _in.CARD_INFO,// 医保信息        医保卡必传
                    shouJiHao = "",//   手机号码
                };

                PushServiceResult<List<T1001.data>> result = HerenHelper<List<T1001.data>>.pushService("1001-QHZZJ", JsonConvert.SerializeObject(t1001));

                if (result.code != 1)
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
                    dataReturn.Code = result.code;
                    dataReturn.Msg = result.msg; ;
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                    return JsonConvert.SerializeObject(dataReturn);
                }

                T1001.data data = result.data.FirstOrDefault();

                //Regex regex = new Regex("^1[3456789]\\d{9}$");

                //if (!regex.IsMatch(data.shouJiHao))
                //{
                //    dataReturn.Code = 1;
                //    dataReturn.Msg = "建档手机号码无效，请至窗口更新手机号码";
                //    dataReturn.Param = "";

                //    return JsonConvert.SerializeObject(dataReturn);
                //}

                _out.IS_EXIST = "1";
                _out.PAT_NAME = data.bingRenXM;
                _out.SEX = data.xingBie == 0 ? "女" : "男";
                _out.AGE = "";
                _out.MOBILE_NO = data.shouJiHao;
                _out.ADDRESS = data.jiaTingDZ;
                _out.SFZ_NO = data.zhengJianHM;
                _out.HOSPATID = data.jianDangId;
                _out.BIR_DATE = DateTime.Parse(data.chuShengRQ).ToString("yyyy-MM-dd");
                _out.GUARDIAN_NAME = data.lianXiRenXM;

                var db = new DbMySQLZZJ().Client;
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
                        return JsonConvert.SerializeObject(dataReturn);
                    }
                }
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
                    if (YLCARD_TYPE == "1")
                    {
                        patCardBind = db.Queryable<SqlSugarModel.PatCardBind>().Where(t => t.HOS_ID == _in.HOS_ID && t.HOSPATID == _in.YLCARD_NO).First();
                        if (patCardBind != null)
                        {
                            patCard = db.Queryable<SqlSugarModel.PatCard>().Where(t => t.PAT_ID == patCardBind.PAT_ID).First();//暂且这样，不同卡类型院内病人信息唯一  2023 06 27
                            patInfo = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.PAT_ID == patCardBind.PAT_ID).First();
                        }
                    }
                    else
                    {
                        patCard = db.Queryable<SqlSugarModel.PatCard>().Where(t => t.YLCARD_TYPE == FormatHelper.GetInt(_in.YLCARD_TYPE) && t.YLCARD_NO == _in.YLCARD_NO).First();

                        if (patCard != null)
                        {
                            patInfo = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.PAT_ID == patCard.PAT_ID).First();
                            //如果不同的卡对应不同的HOSPTAID，需要加上卡号去查
                            patCardBind = db.Queryable<SqlSugarModel.PatCardBind>().Where(t => t.HOS_ID == _in.HOS_ID && t.PAT_ID == patInfo.PAT_ID).First();
                        }
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
                                return JsonConvert.SerializeObject(dataReturn);
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
                        patCardBind.HOSPATID = _out.HOSPATID;

                        db.Updateable<SqlSugarModel.PatCardBind>(patCardBind)
                            .UpdateColumns(t => new { t.HOSPATID })
                            .Where(t => t.PAT_ID == patCardBind.PAT_ID && t.HOS_ID == patCardBind.HOS_ID).ExecuteCommand();
                    }

                    patInfo.SFZ_NO = _out.SFZ_NO;
                    patInfo.PAT_NAME = _out.PAT_NAME;
                    patInfo.BIRTHDAY = _out.BIR_DATE;
                    patInfo.SEX = _out.SEX;
                    patInfo.ADDRESS = _out.ADDRESS;
                    patInfo.MOBILE_NO = _out.MOBILE_NO;
                    db.Updateable(patInfo).UpdateColumns(t => new { t.PAT_NAME, t.SFZ_NO, t.BIRTHDAY, t.SEX, t.ADDRESS }).ExecuteCommand();

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
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.Message;
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}