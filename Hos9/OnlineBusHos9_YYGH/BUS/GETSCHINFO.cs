using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class GETSCHINFO
    {
        public static string B_GETSCHINFO(string json_in)
        {
            return DoBusiness(json_in);
        }

        public static string DoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHINFO_M.GETSCHINFO_IN _in = JsonConvert.DeserializeObject<GETSCHINFO_M.GETSCHINFO_IN>(json_in);
                GETSCHINFO_M.GETSCHINFO_OUT _out = new GETSCHINFO_M.GETSCHINFO_OUT();

  

                HISModels.T2001.input input = new T2001.input()
                {
                    hospitalId = "320282466455146",//  医院编码
                    keShiDM = _in.DEPT_CODE,//科室代码
                    yeWuLX = "2001",//业务类型
                    shiFouCXXL = "0",
                    //1:keShiDM必传,传1表示根据keShiDM查询其下属的科室小类
                    //0:若科室代码为空，则查询所有科室大类,不为空,则根据keShiDM查询医生排班
                    menZhenLX = "0",//  门诊类型 默认0
                };

        

                PushServiceResult<List<T2001.data>> result = HerenHelper<List<T2001.data>>.pushService("2001-QHZZJ", JsonConvert.SerializeObject(input));

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;

                    return JsonConvert.SerializeObject(dataReturn);
                }

                _out.DEPTLIST = new List<GETSCHINFO_M.DEPT>();

                _out.DOCLIST = new List<GETSCHINFO_M.DOC>();

                /*

                1	普通门诊
                2	急诊
                3	专科
                4	副高
                5	正高
                6	专家
                7	省级专家
                 */

                Dictionary<string, string> dicreglvl = new Dictionary<string, string>
                {
                    { "1", "普通门诊" },
                    { "2", "急诊" },
                    { "3", "专科" },
                    { "4", "副高" },
                    { "5", "正高" },
                    { "6", "专家" },
                    { "7", "省级专家" },
                    { "8", "发热门诊" },
                    { "35", "普通（药学）" }
                };

                foreach (var item in result.data)
                {
                    if (item.paiBanRQ != _in.SCH_DATE)
                    {
                        continue;
                    }

                    if (item.tingZhenBZ == 1)
                    {
                        continue;
                    }

                    if (item.shiFouZJ == 1)
                    {
                        //专家号
                        GETSCHINFO_M.DOC doc = new GETSCHINFO_M.DOC()
                        {
                            DOC_NO = item.yiShengDM,
                            DOC_NAME = item.yiShengXM,

                            GH_FEE = "0.00",
                            ZL_FEE = (item.zhenLiaoJSF + item.zhenLiaoFei).ToString(),
                            SCH_TYPE = "2",
                            SCH_DATE = item.paiBanRQ,
                            SCH_TIME = item.guaHaoBC,
                            PERIOD_START = "",
                            PERIOD_END = "",
                            CAN_WAIT = "1",
                            REGISTER_TYPE = item.guaHaoLB,
                            REGISTER_TYPE_NAME = dicreglvl[item.guaHaoLB],
                            STATUS = "",
                            COUNT_REM = item.haoYuanKYS == -2 ? "999" : item.haoYuanKYS.ToString(),
                            YB_CODE = "",
                            PRO_TITLE = item.titleName,
                            SCH_ID = item.dangTianPBID+"|"+item.yiZhouPBID,

                        };
                        _out.DOCLIST.Add(doc);
                        //_out.DOCLIST = _out.DOCLIST.OrderBy(t => t.DOC_NO).ToList();//升序
                    }
                    else
                    {
                        //普通号
                        GETSCHINFO_M.DEPT dept = new GETSCHINFO_M.DEPT()
                        {
                            DEPT_CODE = item.keShiDM,
                            DEPT_NAME = item.yiShengXM,

                            DOC_NO = item.yiShengDM,
                            DOC_NAME = "",

                            GH_FEE = "0.00",
                            ZL_FEE = (item.zhenLiaoJSF + item.zhenLiaoFei).ToString(),

                            SCH_TYPE = "1",
                            SCH_DATE = item.paiBanRQ,
                            SCH_TIME = item.guaHaoBC,
                            PERIOD_START = "",
                            PERIOD_END = "",
                            CAN_WAIT = "1",

                            REGISTER_TYPE = item.guaHaoLB,
                            REGISTER_TYPE_NAME = dicreglvl[item.guaHaoLB],
                            STATUS = "",
                            COUNT_REM = item.haoYuanKYS == -2 ? "999" : item.haoYuanKYS.ToString(),
                            YB_CODE = "",
                            PRO_TITLE = item.titleName,
                            SCH_ID = item.dangTianPBID + "|" + item.yiZhouPBID,

                        };

                        _out.DEPTLIST.Add(dept);
                    }
                }
                
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}