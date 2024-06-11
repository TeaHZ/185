using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_YYGH.Models.MZ
{
    public class EMERGENCYDATA
    {

    public string breathe { get; set; }// 呼吸
        public string cardNo { get; set; }//  门诊患者病历号
        public string diastolicPressure { get; set; }// 舒张压
        public string emergencyLevelCode { get; set; }// 急诊分级级别
        public string emergencyLevelName { get; set; }// 急诊分级名称
        public string preCheckDeptCode { get; set; }// 预检科室代码
        public string preCheckDeptName { get; set; }// 预检科室名称
        /// <summary>
        /// 预检序号
        /// </summary>
        public string preCheckNo { get; set; }//  预检序号
        public string preCheckRegisterFla { get; set; }// 
    }

    public class EmergencyInfo
    {
        public string treatmentId { get; set; }
        public string fzTime { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string deptCode { get; set; }
        public string deptName { get; set; }
        public string symptom { get; set; }
        public string illnessLevel { get; set; }
    }
}
