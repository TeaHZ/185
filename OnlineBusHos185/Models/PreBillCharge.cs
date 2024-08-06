using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBusHos185.Models
{
    public class PreBillCharge
    {

        [Required(ErrorMessage = "octoken 不能为空")]
        public string Octoken { get; set; }

        [Required(ErrorMessage = "payauthno 不能为空")]
        public string Payauthno { get; set; }

        [Required(ErrorMessage = "chsOutput1101 不能为空")]
        public string ChsOutput1101 { get; set; }

        [Required(ErrorMessage = "chsInput2201 不能为空")]
        public string ChsInput2201 { get; set; }

        [Required(ErrorMessage = "chsInput2203 不能为空")]
        public string ChsInput2203 { get; set; }

        [Required(ErrorMessage = "chsInput2204 不能为空")]
        public string ChsInput2204 { get; set; }

        [Required(ErrorMessage = "chsInput2206 不能为空")]
        public string ChsInput2206 { get; set; }
        public string hos_id { get; set; }
    }
    public class PreBillChargeResponse
    {

        public string resultCode { get; set; }// 结果代码    字符型	3		Y	0：成功 -1：失败 其他：平台会自动再次调用该接口
        public string resultMessage { get; set; }//  结果信息 字符型	2000			当交易结果代码不成功时，该字段必须返回
        public JObject uldFeeInfoStr { get; set; }//  费用明细上传返回信息json字符串 字符型	2000		Y json字符串
        public JObject payOrderStr { get; set; }//下单接口返回信息json字符串 字符型	2000		Y json字符串

        public string chsOutput2204 { get; set; }
    }
}
