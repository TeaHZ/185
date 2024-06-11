using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_GJYB.Model;

using System;

namespace OnlineBusHos9_GJYB.BUS
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
            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, t1101, ref inputRoot1101, ref msg);
            if (!flag)
            {
                dataReturn.Code = 1;
                dataReturn.Msg = msg;
                return dataReturn;
            }
            //调用医保
            Models.OutputRoot outputRoot1101 = GlobalVar.YBTrans(HOS_ID, inputRoot1101);
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
            throw new NotImplementedException();
        }

        public virtual DataReturn OUTPTRY(string injson)
        {
            throw new NotImplementedException();
        }

        public virtual DataReturn SETTLE(string injson)
        {
            throw new NotImplementedException();
        }

        public virtual DataReturn REFUND(string injson)
        {
            throw new NotImplementedException();
        }

        public virtual DataReturn COMMON(string json_in)
        {
            throw new NotImplementedException();
        }
    }
}