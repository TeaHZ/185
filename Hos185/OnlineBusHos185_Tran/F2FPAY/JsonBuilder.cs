using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.F2FPAY
{
    public abstract class JsonBuilder
    {

        // 验证bizContent对象
        public abstract bool Validate();

        // 将bizContent对象转换为json字符串
        public string BuildJson()
        {

            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }


    }
}
