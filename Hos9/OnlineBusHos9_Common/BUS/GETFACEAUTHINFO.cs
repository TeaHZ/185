using CommonModel;
using DB.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Common.HISModels;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using static OnlineBusHos9_Common.HISModels.rlsb45;

namespace OnlineBusHos9_Common.BUS
{
    internal class GETFACEAUTHINFO
    {
        public static string B_GETFACEAUTHINFO(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string appCode = "d250c4cd-29de-4080-87d9-0fc91bdace791021";//zn
            string json_out = "";
            Model.GETFACEAUTHINFO_M.GETFACEAUTHINFO_IN _in = JsonConvert.DeserializeObject<Model.GETFACEAUTHINFO_M.GETFACEAUTHINFO_IN>(json_in);
            Model.GETFACEAUTHINFO_M.GETFACEAUTHINFO_OUT _out = new Model.GETFACEAUTHINFO_M.GETFACEAUTHINFO_OUT();

            if (_in.BUS_TYPE == "01")//活体检测
            {
                //string apiUrl = "https://api.shuzike.com/lifeservice/check/45";
                string apiUrl = "http://192.167.219.105:8801/face_alive/";

                JObject j45 = new JObject();
                j45.Add("image_base64", _in.IMG_BASE64);
                string responseJson = GetPostresult(apiUrl, appCode, j45);

                rusult45 result = JsonConvert.DeserializeObject<rusult45>(responseJson);

                dataReturn.Code = result.code == 200 ? 0 : result.code;      //启航通信状态赋值
                dataReturn.Msg = result.msg;
                if (result.code != 200)
                {
                    _out.RESULT = "1";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    return JsonConvert.SerializeObject(dataReturn);
                }
                bool alive = result.data.result.alive;// 提取alive的值

                _out.RESULT = alive ? "0" : "1";      //启航业务状态赋值
                dataReturn.Param = JsonConvert.SerializeObject(_out);
                return JsonConvert.SerializeObject(dataReturn);
            }
            else if (_in.BUS_TYPE == "02")//人脸搜索
            {
                //string apiUrl = "https://api.shuzike.com/lifeservice/check/81";
                string apiUrl = "http://192.167.219.105:8801/face_search/";

                JObject j81 = new JObject();
                j81.Add("face_set_name", "qhfacecs2");//测试
                j81.Add("image_base64", _in.IMG_BASE64);

                DateTime itime = DateTime.Now;

                //string responseJson = "{\"msg\": \"成功\",\"code\": 200,\"data\": {\"result\": [{\"bounding_box\": {\"width\": 124,\"top_left_y\": 67,\"top_left_x\": 212,\"height\": 149},\"similarity\": 0.3,\"external_image_id\": \"fnwhont6\",\"face_id\": \"PBBBdDzY\"},\r\n{\"bounding_box\": {\"width\": 124,\"top_left_y\": 67,\"top_left_x\": 212,\"height\": 149},\"similarity\": 0.9,\"external_image_id\": \"fnwhont6\",\"face_id\": \"PBBBdDzY\"}]},\"reqno\": \"202309111807319748\"}";
                string responseJson = GetPostresult(apiUrl, appCode, j81);

                DateTime otime = DateTime.Now;
                //WriteLogdb("人脸搜索", j81.ToString(), itime, responseJson, otime);//日志

                rlsb81.result81 result81 = JsonConvert.DeserializeObject<rlsb81.result81>(responseJson);

                dataReturn.Code = result81.code == 200 ? 0 : result81.code;      //启航通信状态赋值
                dataReturn.Msg = result81.msg;
                if (result81.code != 200)
                {
                    _out.RESULT = "1";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    return JsonConvert.SerializeObject(dataReturn);
                }
                if (result81.data.result.Length != 0)
                {
                    bool similarityFound = false;

                    foreach (var resultsimilarity in result81.data.result)
                    {
                        if (resultsimilarity.similarity >= 0.85)
                        {
                            similarityFound = true;
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("select PAT_NAME,SFZ_NO from pat_info_face where face_id=@face_id");
                            //MySqlParameter[] parameters = {
                            //new MySqlParameter("@face_id", MySqlDbType.VarChar, 30)};
                            MySqlParameter[] parameters = {
                    new MySqlParameter("@face_id",result81.data.result[0].face_id) };
                            //parameters[0].Value = result81.data.result[0].face_id;

                            DataSet ds = DbHelperMySQLZZJ.Query(strSql.ToString(), parameters);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                _out.PAT_NAME = (string)ds.Tables[0].Rows[0].ItemArray[0];
                                _out.SFZ_NO = (string)ds.Tables[0].Rows[0].ItemArray[1];
                                _out.FACE_ID = result81.data.result[0].face_id;
                                _out.RESULT = "0";
                                dataReturn.Code = 0;
                                dataReturn.Msg = "成功";
                                dataReturn.Param = JsonConvert.SerializeObject(_out);
                                json_out = JsonConvert.SerializeObject(dataReturn);
                                return json_out;
                            }
                            else
                            {
                                _out.RESULT = "1";
                                dataReturn.Code = 0;
                                dataReturn.Msg = "本地表搜索失败 未搜索到相似人脸";//需要认证对比，添加基本信息
                                dataReturn.Param = JsonConvert.SerializeObject(_out);
                                json_out = JsonConvert.SerializeObject(dataReturn);
                                return json_out;
                            }
                        }
                    }

                    if (!similarityFound)
                    {
                        _out.RESULT = "1";
                        dataReturn.Code = 0;
                        dataReturn.Msg = "人脸搜索失败 未搜索到相似人脸";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        json_out = JsonConvert.SerializeObject(dataReturn);
                        return json_out;
                    }
                }
                else
                {
                    _out.RESULT = "1";
                    dataReturn.Code = 0;
                    dataReturn.Msg = "人脸搜索失败 未搜索到相似人脸";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;
                }
            }//人脸搜索
            else if (_in.BUS_TYPE == "03")//人证比对 对比成功后添加人脸
            {
                //string apiUrl = "https://api.shuzike.com/lifeservice/check/11";//外网地址 待转发修改
                string apiUrl = "http://192.167.219.105:8801/face_contrast/";

                JObject j11 = new JObject();
                j11.Add("name", _in.PAT_NAME);
                j11.Add("idcard", _in.SFZ_NO);
                j11.Add("personimg", _in.IMG_BASE64);

                DateTime itime = DateTime.Now;
                string responseJson = GetPostresult(apiUrl, appCode, j11);

                DateTime otime = DateTime.Now;
                //WriteLogdb("人证比对", j11.ToString(), itime, responseJson, otime);//日志
                rlsb11.result11 result11 = JsonConvert.DeserializeObject<rlsb11.result11>(responseJson);
                if (result11.code == 200)
                {
                    if (result11.data.result == "1")
                    {
                        _out.RESULT = "0";

                        string addfaceresult = AddFace("qhfacecs2", _in.IMG_BASE64);//qh_face实例
                        rlsb83.result83 result83 = JsonConvert.DeserializeObject<rlsb83.result83>(addfaceresult);
                        if (result83.code == 200)
                        {
                            _out.FACE_ID = result83.data.result[0].face_id;
                            _out.PAT_NAME = _in.PAT_NAME;
                            _out.SFZ_NO = _in.SFZ_NO;
                            //todo 存自己表faceid

                            StringBuilder strSql = new StringBuilder();//示例
                            strSql.Append("insert into pat_info_face(");
                            strSql.Append("face_id,PAT_NAME,SFZ_NO)");
                            strSql.Append(" values (");
                            strSql.Append("@faceid,@patname,@sfzno)");
                            MySqlParameter[] parameters = {
                    new MySqlParameter("@faceid", MySqlDbType.VarChar,100),
                    new MySqlParameter("@patname", MySqlDbType.VarChar,10),
                    new MySqlParameter("@sfzno", MySqlDbType.VarChar,20)};
                            parameters[0].Value = _out.FACE_ID;
                            parameters[1].Value = _out.PAT_NAME;
                            parameters[2].Value = _out.SFZ_NO;

                            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
                            if (rows < 1)
                            {
                                _out.RESULT = "1";
                                dataReturn.Code = 6;
                                dataReturn.Msg = "FACEID添加失败 请联系工作人员" + _out.FACE_ID;
                                dataReturn.Param = JsonConvert.SerializeObject(_out);
                                json_out = JsonConvert.SerializeObject(dataReturn);
                            }
                            else
                            {
                                _out.RESULT = "0";
                                dataReturn.Code = 0;
                                dataReturn.Msg = "success";
                                dataReturn.Param = JsonConvert.SerializeObject(_out);
                                json_out = JsonConvert.SerializeObject(dataReturn);
                                return json_out;
                            }
                        }
                        else
                        {
                            dataReturn.Code = result83.code;
                            dataReturn.Msg = result83.msg;
                            dataReturn.Param = JsonConvert.SerializeObject(_out);
                            json_out = JsonConvert.SerializeObject(dataReturn);
                            return json_out;
                        }
                    }
                    else
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = "人证比对失败";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        json_out = JsonConvert.SerializeObject(dataReturn);
                        return json_out;
                    }
                }
                else
                {
                    _out.RESULT = "1";
                    dataReturn.Code = result11.code;
                    dataReturn.Msg = result11.msg;
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;
                }
            }//人证比对  对比成功后添加人脸
            else if (_in.BUS_TYPE == "99")//人脸库创建 只用调用一次
            {
                //string apiUrl = "https://api.shuzike.com/lifeservice/check/82";
                string apiUrl = "http://192.167.219.105:8801/face_create/";

                JObject j82 = new JObject();
                j82.Add("face_set_name", "qhfacecs2");//测试用
                string responseJson = GetPostresult(apiUrl, appCode, j82);
                rlsb82.result82 result82 = JsonConvert.DeserializeObject<rlsb82.result82>(responseJson);
                if (result82.code == 200)
                {
                    dataReturn.Code = 0;
                    dataReturn.Msg = responseJson;
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;
                }
                else
                {
                    _out.RESULT = "1";
                    dataReturn.Code = result82.code;
                    dataReturn.Msg = result82.msg;
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;
                }
            }//人脸库创建
            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        private static string GetPostresult(string apiUrl, string appCode, object jobject)
        {
            DataReturn dataReturn = new DataReturn();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("UserAuthKey", appCode);
            string requestBody = jobject.ToString();
            // 将请求参数写入请求体
            byte[] data = Encoding.UTF8.GetBytes(requestBody);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                // 发送请求并获取响应
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseJson = reader.ReadToEnd();

                            return responseJson;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string AddFace(string face_set_name, string image_base64)
        {
            //string apiUrl = "https://api.shuzike.com/lifeservice/check/83";
            string apiUrl = "http://192.167.219.105:8801/face_add/";
            //string appCode = "8b969e08-8e62-45bd-91c7-e9c208c7d2681017";
            string appCode = "d250c4cd-29de-4080-87d9-0fc91bdace791021";//zn
            JObject j83 = new JObject();
            j83.Add("face_set_name", face_set_name);
            j83.Add("image_base64", image_base64);
            string responseJson = GetPostresult(apiUrl, appCode, j83);
            return responseJson;
        }
    }
}