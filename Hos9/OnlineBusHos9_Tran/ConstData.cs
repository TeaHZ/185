namespace ConstData
{
    public class CodeDefine
    {
        /// <summary>
        /// 0 表示全部成功
        /// </summary>
        public const int SUCCESS = 0;

        //***************************************************
        //1-999 错误码
        //***************************************************

        /// <summary>
        /// Json解析异常
        /// </summary>
        public const int Json_Error = 10;
        /// <summary>
        /// DLL调用异常
        /// </summary>
        public const int CallDllError = 11;
        /// <summary>
        /// 配置文件中未找到dll对应的方法
        /// </summary>
        public const int NotFindDllMethod = 12;
        /// <summary>
        /// websocket登录失败 
        /// </summary>
        public const int WebSocketLogin_Fail = 13;
        /// <summary>
        /// 科室预约 未找到科室数据
        /// </summary>
        public const int NotFindDeptInfo_KSYY = 14;
        /// <summary>
        /// 科室挂号 未找到科室数据
        /// </summary>
        public const int NotFindDeptInfo_KSGH = 15;

        /// <summary>
        /// 医生预约 未找到医生数据
        /// </summary>
        public const int NotFindDeptInfo_YSYY = 16;
        /// <summary>
        ///  医生挂号 未找到医生数据
        /// </summary>
        public const int NotFindDeptInfo_YSGH = 17;

        /// <summary>
        /// 科室急诊 未找到科室数据
        /// </summary>
        public const int NotFindDeptInfo_JZGH = 18;

        /// <summary>
        /// 入参数据在定义之外
        /// </summary>
        public const int Parameter_Define_Out = 19;

        /// <summary>
        /// 未查询到指定数据
        /// </summary>
        public const int Data_Not_Find = 20;


        /// <summary>
        /// 冲正
        /// </summary>
        public const int CodeWXZFBTF = 222;
        /// <summary>
        /// 未查询到指定数据
        /// </summary>
        public const int BusError = 21;

        /// <summary>
        /// sql保存失败
        /// </summary>
        public const int SqlSaveError = 22;

        /// <summary>
        /// 文件上传失败
        /// </summary>
        public const int FileUpLoadError = 23;

        //1000-1299 WebSocket交互码
        //1300-1600 常规定义
        /// <summary>
        /// UI消息类型 普通 
        /// </summary>
        public const int UIMessageType_Normal = 1;
        /// <summary>
        /// UI消息类型 警告 
        /// </summary>
        public const int UIMessageType_Warning = 2;
        /// <summary>
        /// UI消息类型 错误 
        /// </summary>
        public const int UIMessageType_Error = 3;

        /// <summary>
        /// UI消息类型 错误 
        /// </summary>
        public const int UIJump = 4;

    }
}