using Soft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace OnlineBusHos185
{
    public class Entrance
    {
        public static string BusinessHos(string json_in)
        {
            EntranceModel model = JSONSerializer.Deserialize<EntranceModel>(json_in);
            string json_out = "";
            DataSet ds_result = null;
            bool b_result = false;
            DataTable dt_result = null;
            int query_type = -1;//1 DataSet 2 bool 3 DataTable
            switch (model.TYPE)
            {
                case "CHECKHOSCARD"://检测医疗卡是否在医院注册
                    //b_result = new ExternalHos().CHECKHOSCARD(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 2;
                    break;

                case "REGISTERAPPTSAVE"://预约挂号保存
                    dt_result = new ExternalHos4MidSevice().REGISTERAPPTSAVE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5],
                        model.PARAM[6], model.PARAM[7], int.Parse(model.PARAM[8]), model.PARAM[9], model.PARAM[10], model.PARAM[11], model.PARAM[12], model.PARAM[13], model.PARAM[14],
                        int.Parse(model.PARAM[15]), model.PARAM[16], model.PARAM[17], model.PARAM[18], model.PARAM[19], model.PARAM[20], model.external);
                    query_type = 3;
                    break;

                case "REGISTERPAYSAVE"://预约挂号支付保存
                    dt_result = new ExternalHos4MidSevice().REGISTERPAYSAVE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5],
                        model.PARAM[6], model.PARAM[7], int.Parse(model.PARAM[8]), model.PARAM[9], model.PARAM[10], model.PARAM[11], model.PARAM[12], model.PARAM[13], model.PARAM[14],
                        model.PARAM[15], model.PARAM[16], model.PARAM[17], model.PARAM[18], model.PARAM[19], decimal.Parse(model.PARAM[20]), model.PARAM[21], model.PARAM[22], model.PARAM[23], model.PARAM[24], model.PARAM[25], model.external);
                    query_type = 3;
                    break;

                case "REGISTERCANCELCHECK"://检查预约是否可以取消
                    //dt_result = new ExternalHos().REGISTERCANCELCHECK(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 3;
                    break;

                case "REGISTERPAYCANCEL"://预约取消(含支付)
                    if (model.PARAM.Length > 5)//支付
                    {
                        string CLJG = "";
                        bool result = new ExternalHos4MidSevice().REGISTERPAYCANCEL(model.PARAM[0], model.PARAM[1], model.PARAM[2], decimal.Parse(model.PARAM[3]), model.PARAM[4], DateTime.Parse(model.PARAM[5]), model.PARAM[6], model.PARAM[7], model.PARAM[8], out CLJG, model.external);
                        dt_result = new DataTable();
                        dt_result.Columns.Add("CLJG", typeof(string));
                        dt_result.Columns.Add("CLBZ", typeof(string));
                        DataRow dr_new = dt_result.NewRow();
                        dr_new["CLJG"] = CLJG;
                        dr_new["CLBZ"] = result ? "0" : "6";
                        dt_result.Rows.Add(dr_new);
                        query_type = 3;
                        break;
                    }
                    else//非支付
                    {
                        b_result = new ExternalHos4MidSevice().REGISTERPAYCANCEL(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                        query_type = 2;
                        break;
                    }
                case "GETOUTFEENOPAY"://未缴费诊间费用查询(汇总、按医嘱处方)
                    ds_result = new ExternalHos4MidSevice().GETOUTFEENOPAY(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 1;
                    break;

                case "GETOUTFEENOPAYMX"://诊间处方费用明细
                    ds_result = new ExternalHos4MidSevice().GETOUTFEENOPAYMX(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 1;
                    break;

                case "OUTFEEPAYLOCK"://诊间支付锁定
                    ds_result = new ExternalHos4MidSevice().OUTFEEPAYLOCK(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 1;
                    break;

                case "OUTFEEPAYUNLOCK"://诊间支付锁定解除
                    //dt_result = new ExternalHos().OUTFEEPAYUNLOCK(int.Parse(model.PARAM[0]), model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], decimal.Parse(model.PARAM[5]), decimal.Parse(model.PARAM[6]), model.PARAM[7], model.PARAM[8], model.external);
                    query_type = 3;
                    break;

                case "OUTFEERETLOCK"://诊间退费锁定
                    //dt_result = new ExternalHos().OUTFEERETLOCK(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 3;
                    break;

                case "OUTFEERETUNLOCK"://诊间退费锁定解除
                    //b_result = new ExternalHos().OUTFEERETUNLOCK(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 2;
                    break;

                case "OUTFEERETSAVE"://诊间退费保存
                    //dt_result = new ExternalHos().OUTFEERETSAVE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], Decimal.Parse(model.PARAM[4]),
                        //model.PARAM[5], decimal.Parse(model.PARAM[6]), model.PARAM[7], model.PARAM[8], model.PARAM[9], DateTime.Parse(model.PARAM[10]), model.PARAM[11], model.PARAM[12], model.external);
                    query_type = 3;
                    break;

                case "OUTFEEPAYSAVE"://诊间支付保存
                    dt_result = new ExternalHos4MidSevice().OUTFEEPAYSAVE(model.PARAM[0], model.PARAM[1], Decimal.Parse(model.PARAM[2]), model.PARAM[3], Decimal.Parse(model.PARAM[4]),
                        model.PARAM[5], model.PARAM[6], model.PARAM[7], model.PARAM[8], DateTime.Parse(model.PARAM[9]), model.PARAM[10], model.PARAM[11], model.external);
                    query_type = 3;
                    break;

                case "GETLISREPORT"://获取病人检验报告单列表
                    ds_result = new ExternalHos4MidSevice().GETLISREPORT(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], int.Parse(model.PARAM[4]), int.Parse(model.PARAM[5]), model.external);
                    query_type = 1;
                    break;

                case "GETLISRESULT"://获取检验报告结果明细
                    ds_result = new ExternalHos4MidSevice().GETLISRESULT(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 1;
                    break;

                case "GETRISREPORT"://获取病人检查报告
                    ds_result = new ExternalHos4MidSevice().GETRISREPORT(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], int.Parse(model.PARAM[4]), int.Parse(model.PARAM[5]), model.external);
                    query_type = 1;
                    break;

                case "GETOUTQUEUEMY"://实时叫号当前账户信息查询
                    //ds_result = new ExternalHos().GETOUTQUEUEMY(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 1;
                    break;

                case "GETOUTQUEUELNOW"://实时叫号信息查询
                    //dt_result = new ExternalHos().GETOUTQUEUELNOW(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 3;
                    break;

                case "GETPATINFBYHOSNO"://通过住院号获取病人信息
                    //dt_result = new ExternalHos().GETPATINFBYHOSNO(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 3;
                    break;

                case "GETPATHOSINFO"://获取住院病人信息详情
                    //ds_result = new ExternalHos().GETPATHOSINFO(int.Parse(model.PARAM[0]), int.Parse(model.PARAM[1]), model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.external);
                    query_type = 1;
                    break;

                case "SAVEINPATYJJ"://缴纳预交金保存
                    //dt_result = new ExternalHos().SAVEINPATYJJ(model.PARAM[0], model.PARAM[1], model.PARAM[2], decimal.Parse(model.PARAM[3]), model.PARAM[4], model.PARAM[5], model.PARAM[6], model.PARAM[7], model.PARAM[8], model.external);
                    query_type = 3;
                    break;

                case "CHECKYLCARD"://验证医疗卡是否已经绑定
                    //b_result = new ExternalHos().CHECKYLCARD(model.PARAM[0], model.PARAM[1], int.Parse(model.PARAM[2]), model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 2;
                    break;

                case "GETSCHDOC"://获取指定医院及科室专家排班列表(当日即实时)
                    ds_result = new ExternalHos4MidSevice().GETSCHDOC(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETSCHDEPT"://获取指定医院科室排班列表(当日即实时)
                    ds_result = new ExternalHos4MidSevice().GETSCHDEPT(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 1;
                    break;

                case "GETSCHPERIOD"://获取指定医院科室(专家)日期排班时间段
                    ds_result = new ExternalHos4MidSevice().GETSCHPERIOD(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 1;
                    break;

                case "GETGOODSCATE"://获取物价分类
                    //ds_result = new ExternalHos().GETGOODSCATE(model.PARAM[0], model.external);
                    query_type = 1;
                    break;

                case "GETGOODSMX"://获取物价明细列表
                    //ds_result = new ExternalHos().GETGOODSMX(model.PARAM[0], model.PARAM[1], model.external);
                    query_type = 1;
                    break;

                case "GETPATUOTCC"://获取指定挂号的主诉信息
                    //dt_result = new ExternalHos().GETPATUOTCC(model.PARAM[0], model.PARAM[1], model.external);
                    query_type = 3;
                    break;

                case "SENDCARDINFO"://病人建档
                    dt_result = new ExternalHos4MidSevice().SENDCARDINFO(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.PARAM[6],
                                                              model.PARAM[7], model.PARAM[8], model.PARAM[9], model.external);
                    query_type = 3;
                    break;

                case "YYGHUPLOADSAVE"://预约挂号数据同步
                    //ds_result = new ExternalHos().YYGHUPLOADSAVE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETCHECKLEDGERINFO"://获取 HIS 每天对账总计金额
                    //ds_result = new ExternalHos().GetCheckLedgerInfo(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETCHECLEDGERDETAIL"://获取 HIS 每天对账明细记录
                    //ds_result = new ExternalHos().GETCHECLEDGERDETAIL(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETCHECLEDGERDETAILTF"://获取 HIS 每天对账退费明细记录
                    //ds_result = new ExternalHos().GETCHECLEDGERDETAILTF(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETHISYBDETAILINFOBYDATE"://获取 HIS 每天医保对账明细
                    //ds_result = new ExternalHos().GETHISYBDETAILINFOBYDATE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 1;
                    break;

                case "GETNJYBDETAILINFOBYDATE"://获取 HIS 每天医保对账明细
                    //ds_result = new ExternalHos().GETNJYBDETAILINFOBYDATE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    query_type = 1;
                    break;

                case "GETHISOTHERMX"://对帐明细HIS端数据（不含APP）
                    //ds_result = new ExternalHos().GETHISOTHERMX(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    query_type = 1;
                    break;

                case "GETZFOTHERMX"://对帐明细支付端数据（不含APP） 20161122
                    //ds_result = new ExternalHos().GETZFOTHERMX(model.PARAM[0], model.PARAM[1], model.external);
                    query_type = 1;
                    break;

                case "GETPLATPOSMX"://获取宜兴POS数据
                    //ds_result = new ExternalHos().GETPLATPOSMX(model.PARAM[0], model.PARAM[1], model.external);
                    query_type = 1;
                    break;

                case "CHECKCANCELSTATE"://检查是否可以退费
                    if (model.PARAM.Length > 3)
                    {
                        dt_result = new ExternalHos4MidSevice().CHECKCANCELSTATE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.external);
                    }
                    else
                    {
                        dt_result = new ExternalHos4MidSevice().CHECKCANCELSTATE(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.external);
                    }
                    query_type = 3;
                    break;

                case "MZSTFUPLOAD"://门诊退费同步
                    dt_result = new ExternalHos4MidSevice().MZSTFUPLOAD(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 3;
                    break;

                case "YYGHMOBPAYYJS"://医保在线支付预结算
                    dt_result = new ExternalHos4MidSevice().YYGHMOBPAYYJS(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.PARAM[6], model.external);
                    query_type = 3;
                    break;

                case "YYGHMOBPAYJS"://医保在线支付结算
                    //dt_result = new ExternalHos().YYGHMOBPAYJS(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.PARAM[6], model.PARAM[7], model.external);
                    query_type = 3;
                    break;

                case "GETAPPTSIGNININFO"://APP支付前判断病人是否在院签到
                    dt_result = new ExternalHos4MidSevice().GETAPPTSIGNININFO(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.PARAM[6], model.external);
                    query_type = 3;
                    break;

                case "GETPERECORD"://获取病人体检记录
                    //ds_result = new ExternalHos().GETPERECORD(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.PARAM[5], model.external);
                    query_type = 1;
                    break;

                case "GETALLPEREPORT"://7.2获取体检报告包含明细指标（分科室）
                    //ds_result = new ExternalHos().GETALLPEREPORT(model.PARAM[0], model.PARAM[1], model.PARAM[2], model.PARAM[3], model.PARAM[4], model.external);
                    query_type = 1;
                    break;

                case "COMMONINTERFACE":
                    query_type = 1;
                    ds_result = new ExternalHos4MidSevice().COMMONINTERFACE(model.external);
                    break;

                case "SAVEHOSPREINFO"://互联网处方信息上传
                    query_type = 2;
                    b_result = new ExternalHos4MidSevice().SAVEHOSPREINFO();
                    break;
            }
            if (query_type == 1)
            {
                json_out = JSONSerializer.Serialize(ds_result);
            }
            else if (query_type == 2)
            {
                json_out = JSONSerializer.Serialize(b_result);
            }
            else if (query_type == 3)
            {
                json_out = JSONSerializer.Serialize(dt_result);
            }
            return json_out;
        }
    }

    public class EntranceModel
    {
        /// <summary>
        /// 参数
        /// </summary>
        public string[] PARAM { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        public Dictionary<string, string> external { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string TYPE { get; set; }
    }
}