using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;
using System;
using System.Collections.Generic;
using System.IO;
using static OnlineBusHos9_InHos.Model.GETPATZYQD_M.GETPATZYQD_OUT;

namespace OnlineBusHos9_InHos.BUS
{
    internal class GETPATZYQD
    {
        public static string B_GETPATZYQD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.GETPATZYQD_M.GETPATZYQD_IN _in = JsonConvert.DeserializeObject<Model.GETPATZYQD_M.GETPATZYQD_IN>(json_in);
            Model.GETPATZYQD_M.GETPATZYQD_OUT _out = new Model.GETPATZYQD_M.GETPATZYQD_OUT();

            var settlenoListJson = _in.SETTLENOLIST.ToString();
            var settlenoList = JsonConvert.DeserializeObject<List<string>>(settlenoListJson);

            List<Qdinfo> qdlist = new List<Qdinfo>();

            foreach (var settleno in settlenoList)
            {
                HISModels.T5303.Input input = new HISModels.T5303.Input()
                {
                    patientId = _in.HOSPATID,
                    visitNo = _in.HOS_NO,
                    settleNo = settleno
                };

                PushServiceResult<T5303.Data> result = HerenHelper<T5303.Data>.pushService("5303-QHZZJ", JsonConvert.SerializeObject(input));

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;

                    return JsonConvert.SerializeObject(dataReturn);
                }


                Qdinfo ftledata = new Qdinfo()
                {
                    FILE_TYPE = "1",////1 base64 2 pdf地址 4 共享文件夹地址
                    //FILE_DATA = "http://192.168.31.144:9099/QD/" + settleno + ".pdf"
                    //FILE_DATA = "\\\\192.168.31.157\\PDFdownload\\QD\\" + settleno + ".pdf"
                    FILE_DATA = result.data.base64
                };
                qdlist.Add(ftledata);
            }

            _out.HOS_ID = _in.HOS_ID;
            _out.QDLIST = qdlist;
            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        private static void ConvertBase64ToPdf(string base64String, string filePath)
        {
            byte[] pdfBytes = Convert.FromBase64String(base64String);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            }
        }
        private static void SaveBase64AsPdf(string base64String, string destinationFolderPath, string fileName)
        {
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

            // 将 Base64 字符串转换为字节数组
            byte[] fileBytes = Convert.FromBase64String(base64String);

            // 将字节数组保存为 PDF 文件
            File.WriteAllBytes(destinationFilePath, fileBytes);
        }
    }
}

//string base64String = result.data.base64;
//string filePath = @"\\192.168.31.144\D:\PDFdownload\QD\" + settleno + ".pdf";
//ConvertBase64ToPdf(base64String, filePath);
//string filePath = "D:\\PDFdownload\\QD\\" + settleno + ".pdf";
////TODO 
//string destinationFolderPath = "\\\\192.168.31.157\\PDFdownload\\QD"; // 共享文件夹的路径
//string fileName = settleno + ".pdf"; // 文件名

//try
//{
//    SaveBase64AsPdf(base64String, destinationFolderPath, fileName);

//}
//catch (Exception ex)
//{
//    dataReturn.Code = 6;
//    dataReturn.Msg = "程序处理异常";
//    dataReturn.Param = ex.Message;
//}
//TODO