using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_GJYB.Model;
using OnlineBusHos185_GJYB.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos185_GJYB.BUS
{
    /// <summary>
    /// B方案，HIS提供接口返回医保交易参数，启航调用医保
    /// </summary>
    public class BBusiness : IBusiness
    {
        public virtual DataReturn PSNQUERY(string injson)
        {
            DataReturn dataReturn = new DataReturn();
            GJYB_PSNQUERY_M.GJYB_PSNQUERY_IN _in = JsonConvert.DeserializeObject<GJYB_PSNQUERY_M.GJYB_PSNQUERY_IN>(injson);
            GJYB_PSNQUERY_M.GJYB_PSNQUERY_OUT _out = new GJYB_PSNQUERY_M.GJYB_PSNQUERY_OUT();
            string HOS_ID = _in.HOS_ID;
            string opter_no = _in.USER_ID;
      
            string ybtype = _in.YLCARD_TYPE == "6" ? "JSSYB" : "CHSYB";

            Models.T1101.Root t1101 = new Models.T1101.Root();
            t1101.data = new Models.T1101.Data();
            t1101.data.mdtrt_cert_type = _in.MDTRT_CERT_TYPE;
            t1101.data.mdtrt_cert_no = _in.MDTRT_CERT_NO;
            t1101.data.card_sn = _in.CARD_SN;
            t1101.data.begntime = _in.BEGNTIME;
            t1101.data.psn_cert_type = _in.PSN_CERT_TYPE;
            t1101.data.certno = _in.CERTNO;
            t1101.data.psn_name = _in.PSN_NAME;

            string insuplc_admdvs = ""; //_in.INSUPLC_ADMDVS

            string msg = "";
            string infno = "";
            bool flag = false;
            infno = "1101";
            Models.InputRoot inputRoot1101 = new Models.InputRoot();
            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, t1101,ybtype, ref inputRoot1101, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot1101 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot1101);
            if (outputRoot1101.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot1101.err_msg;
                return dataReturn;
            }

            Models.RT1101.Root rt1101 = outputRoot1101.GetOutput<Models.RT1101.Root>();
            //保存记录
            SqlSugarModel.ChsPsn chsPsn = new SqlSugarModel.ChsPsn();
            chsPsn.psn_no = rt1101.baseinfo.psn_no;
            if (rt1101.insuinfo != null)
            {
                chsPsn.insuplc_admdvs = rt1101.insuinfo[0].insuplc_admdvs;
            }
            chsPsn.chsInput1101 = JsonConvert.SerializeObject(t1101);
            chsPsn.chsOutput1101 = JsonConvert.SerializeObject(rt1101);
            chsPsn.mdtrt_cert_type = t1101.data.mdtrt_cert_type;
            chsPsn.mdtrt_cert_no = t1101.data.mdtrt_cert_no;
            chsPsn.card_sn = t1101.data.card_sn;
            chsPsn.begntime = t1101.data.begntime;
            chsPsn.psn_cert_type = t1101.data.psn_cert_type;
            chsPsn.certno = t1101.data.certno;
            chsPsn.psn_name = t1101.data.psn_name;
            chsPsn.save_time = DateTime.Now;
            var db = new DbContext().Client;
            db.Saveable(chsPsn).ExecuteCommand();

            if (rt1101.insuinfo == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "您的医保卡无参保信息";
                return dataReturn;
            }
            //取psn_insu_date为空或者大于当前日期
            var insurinfo = rt1101.insuinfo.Find(t => string.IsNullOrEmpty(t.paus_insu_date) || t.paus_insu_date.CompareTo(DateTime.Now.ToString("yyyy-MM-dd")) > 0);

            if (insurinfo == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "您的医保卡已暂停参保";
                return dataReturn;
            }
            _out.PSN_NAME = rt1101.baseinfo.psn_name;
            _out.PSN_NO = rt1101.baseinfo.psn_no;
            _out.SEX = rt1101.baseinfo.gend == "1" ? "男" : "女";
            _out.NATION = rt1101.baseinfo.naty;
            _out.BIRTHDAY = rt1101.baseinfo.brdy;
            _out.AGE = rt1101.baseinfo.age;
            _out.SFZ_NO = rt1101.baseinfo.certno;
            string balc = FormatHelper.GetStr(insurinfo.balc);
            string insutype = FormatHelper.GetStr(insurinfo.insutype);
            _out.BALANCE = balc;
            _out.INSUTYPE = insutype;
            _out.PAT_CARD_OUT = JsonConvert.SerializeObject(rt1101);
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            return dataReturn;
        }

        public virtual DataReturn REGTRY(string injson)
        {



            DataReturn dataReturn = new DataReturn();
            GJYB_REGTRY_M.GJYB_REGTRY_IN _in = JsonConvert.DeserializeObject<GJYB_REGTRY_M.GJYB_REGTRY_IN>(injson);


            string ybtype = _in.YLCARD_TYPE == "6" ? "JSSYB" : "CHSYB";



            GJYB_REGTRY_M.GJYB_REGTRY_OUT _out = new GJYB_REGTRY_M.GJYB_REGTRY_OUT();
            string HOS_SN = _in.HOS_SN;
            string psn_no = _in.PSN_NO;
            string HOS_ID = _in.HOS_ID;
            string opter_no = _in.USER_ID;
            var db = new DbContext().Client;
            SqlSugarModel.ChsPsn chspsn = db.Queryable<SqlSugarModel.ChsPsn>().Where(t => t.psn_no == psn_no).Single();
            if (chspsn == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "未取到个人信息";
                return dataReturn;
            }
            JObject chsOutput1101 = JObject.Parse(chspsn.chsOutput1101);
            JObject chsInput1101 = JObject.Parse(chspsn.chsInput1101);
            string insuplc_admdvs = chspsn.insuplc_admdvs;//参保地行政区划

            JObject jzzj = new JObject();
            JObject jybrc = new JObject();
            jybrc.Add("in1101", chsInput1101);
            jybrc.Add("out1101", chsOutput1101);

            jzzj.Add("zzj", jybrc);

            string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
            medicareParam = Base64Helper.Base64Encode(medicareParam);



            string preCheckNo = "";
            string deptCode = "";
            string registerType = "";
            string preid = "0";

            if (_in.IS_YY == "3")
            {
                preCheckNo = _in.HOS_SN;
                deptCode = _in.DEPT_CODE;
                registerType = "1";
                preid = "0";
            }
            else
            {
                preid = HOS_SN;
            }

    


            REGISTERFEE registerfee = new REGISTERFEE()
            {
                medicareParam = medicareParam,//        医保预留
                pactCode = "17",//   结算code      FALSE
                patientID = _in.HOSPATID,// 患者ID        FALSE
                scheduleId = _in.SCH_ID,//         FALSE
                vipCardNo = "",//        FALSE
                vipCardType = "",//            FALSE
                preid = preid,
                preCheckNo = preCheckNo,
                deptCode = deptCode,
                registerType = registerType
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

            Output<REGISTERFEEDATA> outputregisterfee
      = GlobalVar.CallAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);

            string chsInput2201 = "";
            string chsInput2203 = "";
            string chsInput2204 = "";
            string chsInput2206 = "";
            string chsInput2207 = "";//新增2207入参
            if (outputregisterfee.code != 0)
            {
                dataReturn.Code = outputregisterfee.code;
                dataReturn.Msg = outputregisterfee.message;

                return dataReturn;
            }
            string sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06
            JObject jybcc = JObject.Parse(Base64Helper.Base64Decode(outputregisterfee.data.medicareParam));

            decimal totalfee = outputregisterfee.data.totalFee;
            chsInput2201 = jybcc["zzj"]["in2201"].ToString();
            chsInput2203 = jybcc["zzj"]["in2203"].ToString();
            chsInput2204 = jybcc["zzj"]["in2204"].ToString();
            chsInput2206 = jybcc["zzj"]["in2206"].ToString();
            chsInput2207 = jybcc["zzj"]["in2207"].ToString();

            string msg = "";

            //挂号登记2201
            string infno = "2201";
            Models.InputRoot inputRoot2201 = new Models.InputRoot();
            JObject jin2201 = JObject.Parse(chsInput2201);

            bool flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2201,ybtype, ref inputRoot2201, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2201 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2201);
            if (outputRoot2201.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2201.err_msg;
                return dataReturn;
            }
            //医保出参处理
            var jout2201 = JObject.FromObject(outputRoot2201.output);
            string mdtrt_id = jout2201["data"]["mdtrt_id"].ToString();

            //门诊就诊信息上传A【2203A】
            infno = "2203A";
            Models.InputRoot inputRoot2203 = new Models.InputRoot();
            JObject jin2203 = JObject.Parse(chsInput2203);
            jin2203["mdtrtinfo"]["mdtrt_id"] = mdtrt_id;
            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2203,ybtype, ref inputRoot2203, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2203 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2203);
            if (outputRoot2203.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2203.err_msg;
                return dataReturn;
            }
            //门诊费用明细信息撤销【2205】

            //门诊费用明细信息上传【2204】
            infno = "2204";
            Models.InputRoot inputRoot2204 = new Models.InputRoot();
            JObject jin2204 = JObject.Parse(chsInput2204);
            var listmx = jin2204["feedetail"];
            for (int i = 0; i < listmx.Count(); i++)
            {
                jin2204["feedetail"][i]["mdtrt_id"] = mdtrt_id;
            }

            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2204, ybtype, ref inputRoot2204, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2204 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2204);
            if (outputRoot2204.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2204.err_msg;
                return dataReturn;
            }

            //门诊预结算【2206】
            infno = "2206";
            Models.InputRoot inputRoot2206 = new Models.InputRoot();
            JObject jin2206 = JObject.Parse(chsInput2206);
            jin2206["data"]["mdtrt_id"] = mdtrt_id;
            jin2206["data"]["mdtrt_cert_no"] = jin2206["data"]["mdtrt_cert_no"].ToString().Split('|')[0];

            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2206, ybtype, ref inputRoot2206, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2206 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2206);
            if (outputRoot2206.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2206.err_msg;
                return dataReturn;
            }

            //保存记录
            SqlSugarModel.ChsTran chsTran = new SqlSugarModel.ChsTran();
            chsTran.HOS_ID = HOS_ID;
            chsTran.BIZ_TYPE = "01";
            chsTran.BIZ_NO = outputregisterfee.data.ghxh;
            chsTran.TRAN_ID = chsTran.HOS_ID + "_" + chsTran.BIZ_TYPE + "_" + chsTran.BIZ_NO;
            chsTran.psn_no = psn_no;
            chsTran.insuplc_admdvs = insuplc_admdvs;
            chsTran.mdtrt_id = mdtrt_id;
            chsTran.chsOutput1101 = chspsn.chsOutput1101;

            chsTran.chsInput2201 = JsonConvert.SerializeObject(inputRoot2201.input);
            chsTran.chsOutput2201 = JsonConvert.SerializeObject(outputRoot2201.output);
            chsTran.chsInput2203 = JsonConvert.SerializeObject(inputRoot2203.input);
            chsTran.chsInput2204 = JsonConvert.SerializeObject(inputRoot2204.input);
            chsTran.chsOutput2204 = JsonConvert.SerializeObject(outputRoot2204.output);
            chsTran.chsInput2206 = JsonConvert.SerializeObject(inputRoot2206.input);
            chsTran.chsOutput2206 = JsonConvert.SerializeObject(outputRoot2206.output);
            chsTran.chsInput2207 = (chsInput2207);
            chsTran.totalfee = totalfee;

            chsTran.create_time = DateTime.Now;
            db.Saveable(chsTran).ExecuteCommand();

            Models.RT2206.Root rt2206 = outputRoot2206.GetOutput<Models.RT2206.Root>();
            _out.MDTRT_ID = rt2206.setlinfo.mdtrt_id;
            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(rt2206.setlinfo.medfee_sumamt);
            _out.ACCT_PAY = FormatHelper.GetStr(rt2206.setlinfo.acct_pay);
            _out.PSN_CASH_PAY = FormatHelper.GetStr(totalfee - rt2206.setlinfo.medfee_sumamt + rt2206.setlinfo.psn_cash_pay);
            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(rt2206.setlinfo.fund_pay_sumamt);
            _out.OTH_PAY = FormatHelper.GetStr(rt2206.setlinfo.oth_pay);
            _out.BALC = FormatHelper.GetStr(rt2206.setlinfo.balc);

            _out.SJH = sjh;

            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            return dataReturn;
        }

        public virtual DataReturn OUTPTRY(string injson)
        {
            DataReturn dataReturn = new DataReturn();
            GJYB_OUTPTRY_M.GJYB_OUTPTRY_IN _in = JsonConvert.DeserializeObject<GJYB_OUTPTRY_M.GJYB_OUTPTRY_IN>(injson);
            string ybtype = _in.YLCARD_TYPE == "6" ? "JSSYB" : "CHSYB";

            GJYB_OUTPTRY_M.GJYB_OUTPTRY_OUT _out = new GJYB_OUTPTRY_M.GJYB_OUTPTRY_OUT();
            string HOS_SN = _in.HOS_SN;
            string psn_no = _in.PSN_NO;
            string HOS_ID = _in.HOS_ID;
            string opter_no = _in.USER_ID;
            var db = new DbContext().Client;
            SqlSugarModel.ChsPsn chspsn = db.Queryable<SqlSugarModel.ChsPsn>().Where(t => t.psn_no == psn_no).Single();
            if (chspsn == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "未取到个人信息";
                return dataReturn;
            }
            string chsOutput1101 = chspsn.chsOutput1101;
            string insuplc_admdvs = chspsn.insuplc_admdvs;//参保地行政区划
            JObject chsInput1101 = JObject.Parse(chspsn.chsInput1101);

            JObject jzzj = new JObject();
            JObject jybrc = new JObject();
            jybrc.Add("in1101", chsInput1101);
            jybrc.Add("out1101", JObject.Parse(chsOutput1101));

            jzzj.Add("zzj", jybrc);

            string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
            medicareParam = Base64Helper.Base64Encode(medicareParam);
            Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
            {
                hospitalcode = "",//医院代码
                lifeEquityCardNo = "",//权益卡卡号
                lifeEquityCardType = "",//权益卡类型
                medicareParam = medicareParam,//医保参数
                pactCode = "01",//合同单位
                recipeNos = _in.PRE_NO.Replace('#', ','),
                billType = _in.TKBILL_TYPE,
                regid = HOS_SN//挂号单号
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

            Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
   = GlobalVar.CallAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr);
            dataReturn.Code = outputpre.code;
            dataReturn.Msg = outputpre.message;

            if (outputpre.code != 0)
            {
                return dataReturn;
            }
            string sjh = outputpre.data.receiptNumber;

            JObject jybcc = JObject.Parse(Base64Helper.Base64Decode(outputpre.data.insuranceparameters));

            decimal totalfee = decimal.Parse(outputpre.data.totCost);
            string chsInput2201 = jybcc["zzj"]["in2201"].ToString();
            string chsInput2203 = jybcc["zzj"]["in2203"].ToString();
            string chsInput2204 = jybcc["zzj"]["in2204"].ToString();
            string chsInput2206 = jybcc["zzj"]["in2206"].ToString();
            string chsInput2207 = jybcc["zzj"]["in2207"].ToString();

            string msg = "";
            string infno = "";
            bool flag = false;
            bool reg = false;//是否需要重新登记
            string mdtrt_id = "";
            //挂号登记2201
            string chsOutput2201 = "";
            Models.InputRoot inputRoot2201 = new Models.InputRoot();
            if (!string.IsNullOrEmpty(chsInput2201))
            {
                reg = true;
                infno = "2201";

                JObject jin2201 = JObject.Parse(chsInput2201);
                flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2201, ybtype, ref inputRoot2201, ref msg);
                if (!flag)
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = msg;
                    return dataReturn;
                }
                //调用医保
                Models.OutputRoot outputRoot2201 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2201);
                if (outputRoot2201.infcode != "0")
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = outputRoot2201.err_msg;
                    return dataReturn;
                }
                //医保出参处理
                var jout2201 = JObject.FromObject(outputRoot2201.output);
                mdtrt_id = jout2201["data"]["mdtrt_id"].ToString();
                chsOutput2201 = JsonConvert.SerializeObject(outputRoot2201);
            }
            Models.InputRoot inputRoot2203 = new Models.InputRoot();
            if (!string.IsNullOrEmpty(chsInput2203))
            {
                //门诊就诊信息上传A【2203A】
                infno = "2203A";

                JObject jin2203 = JObject.Parse(chsInput2203);
                if (reg)
                {
                    jin2203["mdtrtinfo"]["mdtrt_id"] = mdtrt_id;
                }
                flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2203, ybtype, ref inputRoot2203, ref msg);
                if (!flag)
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = msg;
                    return dataReturn;
                }
                //调用医保
                Models.OutputRoot outputRoot2203 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2203);
                if (outputRoot2203.infcode != "0")
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = outputRoot2203.err_msg;
                    return dataReturn;
                }
            }
            //门诊费用明细信息撤销【2205】
            if (!reg)
            {
                //不是重新登记的，撤销明细
                infno = "2205";
                Models.InputRoot inputRoot2205 = new Models.InputRoot();
                JObject jin2205 = new JObject();
                JObject jin2205data = new JObject();
                jin2205data["mdtrt_id"] = mdtrt_id;
                jin2205data["chrg_bchno"] = "0000";
                jin2205data["psn_no"] = psn_no;
                jin2205["data"] = jin2205data;

                flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2205, ybtype, ref inputRoot2205, ref msg);
                if (!flag)
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = msg;
                    return dataReturn;
                }
                //调用医保
                Models.OutputRoot outputRoot2205 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2205);
                //if (outputRoot2205.infcode != "0")
                //{
                //    outputdata = QHXmlMode.GetRtnXml(TYPE, PATIENTTYPE, "8", outputRoot2205.err_msg).InnerXml;
                //    return;
                //}
            }
            //门诊费用明细信息上传【2204】
            infno = "2204";
            Models.InputRoot inputRoot2204 = new Models.InputRoot();
            JObject jin2204 = JObject.Parse(chsInput2204);
            if (reg)
            {
                var listmx = jin2204["feedetail"];
                for (int i = 0; i < listmx.Count(); i++)
                {
                    jin2204["feedetail"][i]["mdtrt_id"] = mdtrt_id;
                }
            }
            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2204, ybtype, ref inputRoot2204, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2204 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2204);
            if (outputRoot2204.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2204.err_msg;
                return dataReturn;
            }

            //门诊预结算【2206】
            infno = "2206";
            Models.InputRoot inputRoot2206 = new Models.InputRoot();
            JObject jin2206 = JObject.Parse(chsInput2206);

            jin2206["data"]["mdtrt_cert_no"] = jin2206["data"]["mdtrt_cert_no"].ToString().Split('|')[0];

            if (reg)
            {
                jin2206["data"]["mdtrt_id"] = mdtrt_id;
            }
            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2206, ybtype, ref inputRoot2206, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2206 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2206);
            if (outputRoot2206.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2206.err_msg;
                return dataReturn;
            }

            //保存记录

            #region

            SqlSugarModel.ChsTran chsTran = new SqlSugarModel.ChsTran();
            chsTran.HOS_ID = HOS_ID;
            chsTran.BIZ_TYPE = "02";//01挂号 02缴费
            chsTran.BIZ_NO = _in.PAY_ID;
            chsTran.TRAN_ID = chsTran.HOS_ID + "_" + chsTran.BIZ_TYPE + "_" + _in.PAY_ID;
            chsTran.psn_no = psn_no;
            chsTran.insuplc_admdvs = insuplc_admdvs;
            chsTran.mdtrt_id = mdtrt_id;
            chsTran.chsOutput1101 = chsOutput1101;
            chsTran.chsInput2201 = JsonConvert.SerializeObject(inputRoot2201.input);
            chsTran.chsOutput2201 = chsOutput2201;
            chsTran.chsInput2203 = JsonConvert.SerializeObject(inputRoot2203.input);
            chsTran.chsInput2204 = JsonConvert.SerializeObject(inputRoot2204.input);
            chsTran.chsOutput2204 = JsonConvert.SerializeObject(outputRoot2204.output);
            chsTran.chsInput2206 = JsonConvert.SerializeObject(inputRoot2206.input);
            chsTran.chsOutput2206 = JsonConvert.SerializeObject(outputRoot2206);
            chsTran.chsInput2207 = chsInput2207;
            chsTran.create_time = DateTime.Now;
            chsTran.totalfee = totalfee;
            db.Saveable(chsTran).ExecuteCommand();

            #endregion

            #region 自付比例

            try
            {
                Hos185_His.Models.MZ.GETOUTFEENOPAYMX nopaymx = new Hos185_His.Models.MZ.GETOUTFEENOPAYMX()
                {
                    clinicCode = _in.HOS_SN,  //挂号流水号
                    invoiceNo = "",  //发票号
                    invoiceSeq = "",  //发票流水号
                    lifeEquityCardNo = "",  //权益卡卡号
                    lifeEquityCardType = "",  //权益卡类型
                    pactCode = "01",  //合同编号
                    recipeNo = _in.PRE_NO,  //处方号,多个以#分割
                    sequenceNo = "0",  //处方流水号
                    ybPay = "1"  //是否能够医保支付 0 不可以 1可以
                };

                string jsonstrmx = Newtonsoft.Json.JsonConvert.SerializeObject(nopaymx);

                Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>> output
       = GlobalVar.CallAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>>("/hischargesinfo/outpatientfee/recipedetailinfo", jsonstrmx);

                if (output.code != 0)
                {
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;
                    return dataReturn;
                }

                var i2204 = jin2204["feedetail"];

                List<T2204.Feedetail> feedetails = JsonConvert.DeserializeObject<List<T2204.Feedetail>>(i2204.ToString());
                RT2204.Root r2204 = JsonConvert.DeserializeObject<RT2204.Root>(JsonConvert.SerializeObject(outputRoot2204.output));
                List<GJYB_OUTPTRY_M.ZlRateDetail> zlrates = new List<GJYB_OUTPTRY_M.ZlRateDetail>();
                foreach (var item in output.data)
                {
                    GJYB_OUTPTRY_M.ZlRateDetail detail = new GJYB_OUTPTRY_M.ZlRateDetail();

                    var inf = feedetails.Find(x => x.medins_list_codg == item.ybHisItemCode);
                    if (inf != null)
                    {
                        string feedetl_sn = inf.feedetl_sn;

                        string rate = r2204.result.Find(x => x.feedetl_sn == feedetl_sn).selfpay_prop.ToString();

                        detail.itemcode = item.itemCode;
                        detail.itemname = item.itemName;
                        detail.spec = item.specs;
                        detail.price = item.unitPrice.ToString();
                        detail.camt = item.qty.ToString();
                        detail.zlrate = rate;

                        zlrates.Add(detail);
                    }
                    else
                    {
                        detail.itemcode = item.itemCode;
                        detail.itemname = item.itemName;
                        detail.spec = item.specs;
                        detail.price = item.unitPrice.ToString();
                        detail.camt = item.qty.ToString();
                        detail.zlrate = "1";

                        zlrates.Add(detail);

                    }


                }

                _out.ZlRateDetail = zlrates;

            }
            catch (Exception ex) { }
            #endregion

            Models.RT2206.Root rt2206 = outputRoot2206.GetOutput<Models.RT2206.Root>();
            _out.MDTRT_ID = rt2206.setlinfo.mdtrt_id;
            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(rt2206.setlinfo.medfee_sumamt);
            _out.ACCT_PAY = FormatHelper.GetStr(rt2206.setlinfo.acct_pay);

            decimal? cashpay = totalfee - rt2206.setlinfo.medfee_sumamt;

            if (cashpay * 100 < 5)
            {
                cashpay = 0m;
            }
            _out.PSN_CASH_PAY = FormatHelper.GetStr(cashpay + rt2206.setlinfo.psn_cash_pay);
            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(rt2206.setlinfo.fund_pay_sumamt);
            _out.OTH_PAY = FormatHelper.GetStr(rt2206.setlinfo.oth_pay);
            _out.BALC = FormatHelper.GetStr(rt2206.setlinfo.balc);
            _out.SJH = sjh;
            _out.JEALL = totalfee.ToString();
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            return dataReturn;
        }

        public virtual DataReturn SETTLE(string injson)
        {
            DataReturn dataReturn = new DataReturn();
            GJYB_SETTLE_M.GJYB_SETTLE_IN _in = JsonConvert.DeserializeObject<GJYB_SETTLE_M.GJYB_SETTLE_IN>(injson);
            GJYB_SETTLE_M.GJYB_SETTLE_OUT _out = new GJYB_SETTLE_M.GJYB_SETTLE_OUT();
            string ybtype = _in.YLCARD_TYPE == "6" ? "JSSYB" : "CHSYB";

            string HOS_ID = _in.HOS_ID;
            string opter_no = _in.USER_ID;
            string HOS_SN = _in.HOS_SN;
            string ISGH = _in.ISGH;
            string PAY_ID = _in.PAY_ID;

            string clinicCode = "";
            string receiptNumber = "";

            if (string.IsNullOrEmpty(_in.SJH))
            {
                dataReturn.Code = 222;
                dataReturn.Msg = "未能获取到HIS预算收据号";
                return dataReturn;
            }

            if (ISGH == "1")
            {
                string[] hisneed = _in.SJH.Split('|');

                clinicCode = hisneed[1];
                receiptNumber = hisneed[0];
            }

            string TRAN_ID = HOS_ID + "_" + (ISGH == "1" ? "01" : "02") + "_" + (ISGH == "1" ? clinicCode : PAY_ID);

            var db = new DbContext().Client;
            SqlSugarModel.ChsTran chsTran = db.Queryable<SqlSugarModel.ChsTran>().Where(t => t.TRAN_ID == TRAN_ID).Single();
            if (chsTran == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "未取到预结算信息";
                return dataReturn;
            }

            string psn_no = chsTran.psn_no;
            SqlSugarModel.ChsPsn chspsn = db.Queryable<SqlSugarModel.ChsPsn>().Where(t => t.psn_no == psn_no).Single();
            if (chspsn == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "未取到个人信息";
                return dataReturn;
            }
            string insuplc_admdvs = chsTran.insuplc_admdvs;

            //调用HIS获取结算报文
            string chsInput2207 = chsTran.chsInput2207;

            string msg = "";
            string infno = "";
            bool flag = false;
            //门诊结算【2207】
            infno = "2207";
            Models.InputRoot inputRoot2207 = new Models.InputRoot();
            JObject jin2207 = JObject.Parse(chsInput2207);
            jin2207["data"]["mdtrt_id"] = chsTran.mdtrt_id;

            JObject jin2206 = JObject.Parse(chsTran.chsInput2206);
            jin2207["data"]["chrg_bchno"] = jin2206["data"]["chrg_bchno"];

            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2207, ybtype, ref inputRoot2207, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2207 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2207);
            if (outputRoot2207.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2207.err_msg;
                return dataReturn;
            }
            Models.RT2207.Root rt2207 = outputRoot2207.GetOutput<Models.RT2207.Root>();

            //保存记录
            chsTran.chsInput2207 = JsonConvert.SerializeObject(inputRoot2207);
            chsTran.chsOutput2207 = JsonConvert.SerializeObject(outputRoot2207.output);
            chsTran.setl_id = rt2207.setlinfo.setl_id;
            db.Updateable(chsTran).ExecuteCommand();

            _out.MDTRT_ID = rt2207.setlinfo.mdtrt_id;
            _out.SETL_ID = rt2207.setlinfo.setl_id;
            decimal totalfee = chsTran.totalfee;

            decimal? cashpay = totalfee - rt2207.setlinfo.medfee_sumamt;
            if (cashpay * 100 < 5)
            {
                cashpay = 0m;
            }
            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(rt2207.setlinfo.medfee_sumamt);
            _out.ACCT_PAY = FormatHelper.GetStr(rt2207.setlinfo.acct_pay);
            _out.PSN_CASH_PAY = FormatHelper.GetStr(cashpay + rt2207.setlinfo.psn_cash_pay); ;//FormatHelper.GetStr(rt2207.setlinfo.psn_cash_pay);
            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(rt2207.setlinfo.fund_pay_sumamt);
            _out.OTH_PAY = FormatHelper.GetStr(rt2207.setlinfo.oth_pay);
            _out.BALC = FormatHelper.GetStr(rt2207.setlinfo.balc);
            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            return dataReturn;
        }

        public virtual DataReturn REFUND(string injson)
        {
            DataReturn dataReturn = new DataReturn();
            GJYB_REFUND_M.GJYB_REFUND_IN _in = JsonConvert.DeserializeObject<GJYB_REFUND_M.GJYB_REFUND_IN>(injson);
            GJYB_REFUND_M.GJYB_REFUND_OUT _out = new GJYB_REFUND_M.GJYB_REFUND_OUT();
            string ybtype = _in.YLCARD_TYPE == "6" ? "JSSYB" : "CHSYB";

            string HOS_ID = _in.HOS_ID;
            string opter_no = _in.USER_ID;

            string mdtrt_id = _in.MDTRT_ID;
            string setl_id = _in.SETL_ID;

            var db = new DbContext().Client;
            SqlSugarModel.ChsTran chsTran = db.Queryable<SqlSugarModel.ChsTran>().Where(t => t.mdtrt_id == mdtrt_id && t.setl_id == setl_id).Single();
            if (chsTran == null)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = "未取到结算信息";
                return dataReturn;
            }
            string psn_no = chsTran.psn_no;
            string insuplc_admdvs = chsTran.insuplc_admdvs;

            string msg = "";
            string infno = "";
            bool flag = false;
            //门诊结算撤销【2208】
            infno = "2208";
            Models.InputRoot inputRoot2208 = new Models.InputRoot();
            JObject jin2208 = new JObject();
            JObject jin2208data = new JObject();
            jin2208data["psn_no"] = psn_no;
            jin2208data["mdtrt_id"] = mdtrt_id;
            jin2208data["setl_id"] = setl_id;
            jin2208["data"] = jin2208data;

            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2208, ybtype, ref inputRoot2208, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot2208 = GlobalVar.YBTrans(HOS_ID, ybtype, inputRoot2208);
            if (outputRoot2208.infcode != "0")
            {
                dataReturn.Code = 1;
                dataReturn.Msg = outputRoot2208.err_msg;
                return dataReturn;
            }

            //保存记录
            chsTran.chsInput2208 = JsonConvert.SerializeObject(inputRoot2208);
            chsTran.chsOutput2208 = JsonConvert.SerializeObject(outputRoot2208);
            db.Updateable(chsTran).ExecuteCommand();

            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            return dataReturn;
        }

        public virtual DataReturn COMMON(string json_in)
        {
            throw new NotImplementedException();
        }
    }
}