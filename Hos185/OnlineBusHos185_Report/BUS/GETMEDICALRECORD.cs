using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.Report;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Report.BUS
{
    internal class GETMEDICALRECORD
    {
        public static string B_GETMEDICALRECORD(string json_in)
        {
            GETMEDICALRECORD_M.GETMEDICALRECORD_IN _in = JsonConvert.DeserializeObject<GETMEDICALRECORD_M.GETMEDICALRECORD_IN>(json_in);
            GETMEDICALRECORD_M.GETMEDICALRECORD_OUT _out = new GETMEDICALRECORD_M.GETMEDICALRECORD_OUT();
            _out.MEDICALREPORT = new List<GETMEDICALRECORD_M.MEDICALREPORT>();

            var db = new DbMySQLZZJ().Client;
            int MEDICALRECORDMOUS = 3;
            SqlSugarModel.SysConfig model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "MEDICALRECORDMOUS").First();
            if (model != null)
            {
                MEDICALRECORDMOUS = FormatHelper.GetInt(model.config_value);
            }


            if (_in.YLCARD_TYPE=="1")//条码
            {
                _in.HOSPATID = _in.YLCARD_NO;
            }


            DataReturn dataReturn = new DataReturn();

            Hos185_His.Models.Report.outPatientEmrInfo outPatientEmrInfo = new Hos185_His.Models.Report.outPatientEmrInfo()
            {
                cardNo = _in.HOSPATID,//医院内部就诊卡号
                clinicCode = "",//挂号流水号
                days = FormatHelper.GetStr(MEDICALRECORDMOUS*30),//有效天数
                idCardNo = "",
                idCardType = "",
                mcardNo = "",//绑定的医疗证号
                mcardNoType = "",//绑定的医疗证类型
                recordType = "",//病历类型,多个以#分割
                signState = "1"//签名状态 1展示所有病历 空或2只展示签名病历
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(outPatientEmrInfo);

            Output<List<outPatientEmrInfoData>> output
      = GlobalVar.CallAPI<List<outPatientEmrInfoData>>("/hisemr/emr/outPatientEmrInfo", jsonstr);

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message.ToString();
            if (output.code != 0)
            {
                return JsonConvert.SerializeObject(dataReturn);
            }

            if (output.data==null ||output.data.Count==0)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "暂无可打印的病历资料，请咨询医生病历是否保存签署";

                return JsonConvert.SerializeObject(dataReturn);
            }

            try
            {

    
                _out.REPORT_ALL_NUM = output.data.Count.ToString();
                _out.REPORT_AUDIT_NUM = output.data.Count.ToString();
                _out.REPORT_PRINT_NUM = output.data.Count.ToString();
                foreach (var item in output.data)
                {
                    GETMEDICALRECORD_M.MEDICALREPORT mEDICALREPORT = new GETMEDICALRECORD_M.MEDICALREPORT
                    {
                        REPORT_SN = item.recordId,
                        REPORT_DEPT_NAME = item.deptName,
                        REPORT_DOC_NAME = item.seeDoctName,
                        REPORT_DATE = item.regDate.Trim().Substring(0,10),
                        PRINT_FLAG = item.printState,//测试用，0， item.isPrint,
                        DATA_TYPE = "1",
                        REPORT_TYPE=item.sourceType
                    };

                    _out.MEDICALREPORT.Add(mEDICALREPORT);
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);

                return JsonConvert.SerializeObject(dataReturn);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 8;
                dataReturn.Msg += ex.ToString();
                return JsonConvert.SerializeObject(dataReturn);
            }
        }

        public static string B_GETMEDICALRESULT(string json_in)
        {
            GETMEDICALRESULT_M.GETMEDICALRESULT_IN _in = JsonConvert.DeserializeObject<GETMEDICALRESULT_M.GETMEDICALRESULT_IN>(json_in);
            GETMEDICALRESULT_M.GETMEDICALRESULT_OUT _out = new GETMEDICALRESULT_M.GETMEDICALRESULT_OUT();

            Hos185_His.Models.Report.patientEmrPDFDetail detail = new patientEmrPDFDetail()
            {
                recordId = _in.REPORT_SN,
                sourceType = string.IsNullOrEmpty(_in.REPORT_TYPE)?"MZ": _in.REPORT_TYPE
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(detail);

            Output<patientEmrPDFDetailData> output
      = GlobalVar.CallAPI<patientEmrPDFDetailData>("/hisemr/emr/patientEmrPDFDetail", jsonstr);

            DataReturn dataReturn = new DataReturn();

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message;
            try
            {
                if (output.code != 0)
                {
                    return JsonConvert.SerializeObject(dataReturn);
                }

                _out.DATA_TYPE = "1";

                if (output.data.pdfStatus != "1")//获取PDF状态 0不存在 1成功 2转换失败
                {
                    dataReturn.Code = 1;
                    dataReturn.Msg = "获取pdf失败";
                    return JsonConvert.SerializeObject(dataReturn);
                }
                _out.REPORTDATA = output.data.pdfData;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);

                return JsonConvert.SerializeObject(dataReturn);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                return JsonConvert.SerializeObject(dataReturn);
            }
        }

        public static string B_ZZJMEDICALPRNBACK(string json_in)
        {
            ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_IN>(json_in);
            ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_OUT _out = new ZZJMEDICALPRNBACK_M.ZZJMEDICALPRNBACK_OUT();
            DataReturn dataReturn = new DataReturn();


            JObject jobj = new JObject();
            jobj.Add("inspectNo", _in.REPORT_SN);
            jobj.Add("inspectType", "EMR");

            Output<object> mzoutput
      = GlobalVar.CallAPI<object>("/hislispacs/tech/updatePrintStatus", jobj.ToString());

            dataReturn.Code = 0;
            dataReturn.Msg = "SUCCESS";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            #region 不管成功失败,记录打印,用于计数
            try
            {
                StringBuilder str_reportmx = new StringBuilder();
                str_reportmx.Append("insert into reportmx(HOS_ID,TYPE,HOS_SN,lTERMINAL_SN,USER_ID,NOW) values (");
                str_reportmx.Append("@HOS_ID,@TYPE,@HOS_SN,@lTERMINAL_SN,@USER_ID,@NOW);");
                MySqlParameter[] parameters =
                {
                    new MySqlParameter("@HOS_ID",MySqlDbType.VarChar,20),
                    new MySqlParameter("@TYPE",MySqlDbType.VarChar,30),
                    new MySqlParameter("@HOS_SN",MySqlDbType.VarChar,100),
                    new MySqlParameter("@lTERMINAL_SN",MySqlDbType.VarChar,30),
                    new MySqlParameter("@USER_ID",MySqlDbType.VarChar,30),
                    new MySqlParameter("@NOW",MySqlDbType.DateTime)
                    };
                parameters[0].Value = _in.HOS_ID;
                parameters[1].Value = "病历报告";
                parameters[2].Value = _in.REPORT_SN;
                parameters[3].Value = _in.LTERMINAL_SN;
                parameters[4].Value = _in.USER_ID;
                parameters[5].Value = DateTime.Now;
                DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
            }
            catch (Exception ex)
            {
                Log.Core.Model.ModSqlError logsql = new Log.Core.Model.ModSqlError();
                logsql.TYPE = "病历";
                logsql.EXCEPTION = ex.ToString();
                logsql.time = DateTime.Now;
                new Log.Core.MySQLDAL.DalSqlERRROR().Add(logsql);
            }
            #endregion

            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}