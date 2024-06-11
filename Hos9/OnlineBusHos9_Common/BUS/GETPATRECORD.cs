using Newtonsoft.Json;
using OnlineBusHos9_Common.HISModels;
using PasS.Base.Lib;
using System;
using CommonModel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Configuration;


namespace OnlineBusHos9_Common.BUS
{
    internal class GETPATRECORD
    {
        static string HerenHIS = ConfigurationManager.AppSettings["HerenHIS"];
        public static string B_GETPATRECORD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Model.GETPATRECORD_M.GETPATRECORD_IN _in = JsonConvert.DeserializeObject<Model.GETPATRECORD_M.GETPATRECORD_IN>(json_in);
                Model.GETPATRECORD_M.GETPATRECORD_OUT _out = new Model.GETPATRECORD_M.GETPATRECORD_OUT();
                string HOS_ID = _in.HOS_ID;
                string SFZ_NO = _in.SFZ_NO;
                string PAT_NAME = _in.PAT_NAME;
                string SEX = _in.SEX;
                string AGE = _in.AGE;
                string ADDRESS = _in.ADDRESS;
                string BIRTHDAY = _in.BIR_DATE;
                string GUARDIAN_NAME = _in.GUARDIAN_NAME;
                string GUARDIAN_SFZ_NO = _in.GUARDIAN_SFZ_NO;
                string MOBILE_NO = _in.MOBILE_NO;
                string YLCARD_TYPE = _in.YLCARD_TYPE;
                string YLCARD_NO = _in.YLCARD_NO;
                string USER_ID = _in.USER_ID;
                string PAT_CARD_OUT = _in.PAT_CARD_OUT;
                string BIR_DATE = _in.BIR_DATE;
                string TYPE = _in.TYPE;


                if (TYPE == "1")
                {

                    string HOSPATID = GETHOSPATIDBYSFZNO(_in.SFZ_NO);
                    JObject j5202 = new JObject
                        {
                            { "patientId",HOSPATID },
                            { "mphoneNumber",  _in.MOBILE_NO},
                            { "operatorName",  _in.LTERMINAL_SN},
                            { "operatorId", _in.LTERMINAL_SN }
                        };
                    Hashtable hs = new Hashtable
                            {
                                { "DataType", "JSON" },
                                { "TradeCode", "5202-QHZZJ"},
                                { "pInput", JsonConvert.SerializeObject(j5202) }
                            };

                    string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "pushService", hs).InnerText;

                    DateTime otime = DateTime.Now;
                    T5202 t5202 = JsonConvert.DeserializeObject<T5202>(rtnstr);
                    //PushServiceResult<List<T5202.Data>> result5202 = HerenHelper<List<T5202.Data>>.pushService("5202-QHZZJ", JsonConvert.SerializeObject(j5202));

                    if (t5202.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = t5202.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }
                
                    _out.HOSPATID = HOSPATID;

                        dataReturn.Code = 0;
                        dataReturn.Msg = "SUCCESS";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                   
                }
                else
                {
                    try
                    {
                        string idCardType = "01";

                        switch (YLCARD_TYPE)
                        {
                            case "4":
                                idCardType = "01";
                                break;

                            default:
                                break;
                        }

                        string sex = SEX == "男" ? "1" : "0";

                        HISModels.T5201.input input = new HISModels.T5201.input()
                        {
                            bingRenXM = _in.PAT_NAME,//   患者姓名
                            xingBie = sex,//  性别      0   女                       1   男
                            zhengJianHM = _in.SFZ_NO,//   证件号码未成年人建档，则为空，其他情况非空。
                            zhengJianLX = "1",

                            //   证件类型未成年人建档，则为空，其他情况非空。
                            //
                            //1   居民身份证
                            //2   居民户口簿
                            //3   护照
                            //4   军官证(士兵证)
                            //5   驾驶执照
                            //6   港奥居民来往内地通行证
                            //7   台湾居民来往内地通行证
                            //99  其他

                            chuShengRQ = _in.BIR_DATE,//    出生日期
                            minZu = "",//     民族
                            shengFen = "",//     省份
                            chengShi = "",//     城市
                            diQu = "",//     地区
                            diZhi = _in.ADDRESS,//    详细地址
                            jianHuRGX = "",//   监护人关系
                            jianHuRXM = "",//   监护人姓名
                            jianHuRZJH = "",//   监护人证件号 未成年人建档时必填。
                            jianHuRZJLX = "",//   监护人证件类型
                            shouJiHao = _in.MOBILE_NO,//     手机号码
                            hunYin = "0",//    婚姻
                                         //    0   不详
                                         //1   未婚
                                         //2   已婚
                                         //3   离异
                                         //4   丧偶
                            bingRenLX = YLCARD_TYPE == "2" ? "2" : "1",//    病人类型        1   自费   2   医保
                            duKaFS = "",//    读卡方式        1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5-电子医 保凭证
                            yiBaoXX = "",//   读医保卡返回的原始信息
                            yiBaoBH = "",//  医保编号
                            yeWuLX = "5201",//   业务类型        5201：自助建档
                        };

                        PushServiceResult<T5201.data> result = HerenHelper<T5201.data>.pushService("5201-QHZZJ", JsonConvert.SerializeObject(input));

                        if (result.code != 1)
                        {
                            dataReturn.Code = 5;
                            dataReturn.Msg = "[提示]建档失败";
                            dataReturn.Param = result.msg;
                            goto EndPoint;
                        }

                        _out.HOSPATID = result.data.PatientId;

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

                        #endregion 平台建档

                        dataReturn.Code = 0;
                        dataReturn.Msg = "SUCCESS";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                    }
                    catch (Exception ex)
                    {
                        dataReturn.Code = 5;
                        dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                    }
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
        private static string GETHOSPATIDBYSFZNO(string SFZNO)
        {
            T1001.Input t1001 = new T1001.Input()
            {
                zhengJianHM = SFZNO,// 证件号码
                yeWuLX = "1001",// 业务类型        1001:患者信息查询
                hospitalId = "320282466455146",// 医院ID
                yiBaoBH = "",//医保编号 医保卡必传
                yiBaoData = "",//医保信息        医保卡必传
                duKaFS = "1",// 读卡方式 默认1
                jiuZhenKH = "",// 就诊卡号
                yiBaoXX = "",// 医保信息        医保卡必传
                shouJiHao = "",//   手机号码
            };

            PushServiceResult<List<T1001.data>> result = HerenHelper<List<T1001.data>>.pushService("1001-QHZZJ", JsonConvert.SerializeObject(t1001));

            if (result.code != 1)
            {
                return SFZNO;
            }
            T1001.data data = result.data.FirstOrDefault();
            return data.jianDangId;

        }
        public class T5202
        {
            public int code { get; set; }
            public Data data { get; set; }
            public string msg { get; set; }
        }

        public class Data
        {
            public string mphoneNumber { get; set; }
            public string openPatRelationShipVOList { get; set; }
            public string operatorId { get; set; }
            public string operatorName { get; set; }
            public string patientId { get; set; }
        }


    }
}