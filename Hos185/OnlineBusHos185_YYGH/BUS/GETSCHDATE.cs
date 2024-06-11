using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static OnlineBusHos185_YYGH.Model.GETSCHDATE_M;

namespace OnlineBusHos185_YYGH.BUS
{
    class GETSCHDATE
    {
        public static string B_GETSCHDATE(string json_in)
        {



            DataReturn dataReturn = new DataReturn();

            try
            {
                Model.GETSCHDATE_M.GETSCHDATE_IN _in = JsonConvert.DeserializeObject<Model.GETSCHDATE_M.GETSCHDATE_IN>(json_in);
                Model.GETSCHDATE_M.GETSCHDATE_OUT _out = new Model.GETSCHDATE_M.GETSCHDATE_OUT();





                Hos185_His.Models.MZ.GETSCHINFO getschinfo = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = _in.DEPT_CODE, //科室编号
                    doctCode = _in.DOC_NO, //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = string.IsNullOrEmpty(_in.DOC_NO) ? "0" : "1", //排班类型 1专家 0普通
                    seeEndDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"), //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };




                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfo);

                Hos185_His.Models.Output<List<GETSCHINFODATA>> output
          = GlobalVar.CallAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstr);

                if (output.code == 0)
                {
                    var schdate = output.data.GroupBy(x => x.seeDate).Select(g => g.First()).ToList();


                    List<SCHLIST> SCHDEPTLIST = new List<SCHLIST>();
                    List<SCHLIST> SCHDOCLIST = new List<SCHLIST>();


                    foreach (var item in schdate)
                    {
                        SCHDEPTLIST.Add(new SCHLIST() { SCH_DATE = item.seeDate, WEEK_DAY = item.week });

                    }


                    _out.SCHDEPTLIST = SCHDEPTLIST;
                    _out.SCHDOCLIST = SCHDOCLIST;
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                }
                else
                {
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;
                    dataReturn.Param = "";
                }





            }
            catch (Exception ex)
            {
                dataReturn.Code = 7;
                dataReturn.Msg = ex.Message;
            }
            return JsonConvert.SerializeObject(dataReturn);


        }
    }
}
