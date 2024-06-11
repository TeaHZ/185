using CSRedis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;

namespace OnlineBusHos9_Report
{
    public class RedisHelperSentinels
    {
        /// <summary>
        /// Redis 密码
        /// </summary>
        private static string Password = "";

        /// <summary>
        /// 连接池数量 推荐不超过5
        /// </summary>
        private static int poolsize = 1;

        /// <summary>
        /// 字符串是否加密
        /// </summary>
        public static bool ConStringEncrypt
        {
            get
            {
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];

                if (string.Compare(ConStringEncrypt, "true", true) == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 静态构造方法，获取主服务地址
        /// </summary>
        private static CSRedisClient Master(int defaultDatabase)
        {
            CSRedisClient redisClient = GetSentinel(defaultDatabase);
            if (redisClient != null)//获取哨兵配置
            {
                return redisClient;
            }
            else//如果哨兵配置不正确 则使用普通单机连接
            {
                Password = ConfigurationManager.AppSettings["RedisSLB.Password"].Trim();
                if (ConStringEncrypt)
                {
                    try
                    {
                        Password = PasS.Base.Lib.DESEncrypt.Decrypt(Password);
                    }
                    catch { Password = ConfigurationManager.AppSettings["RedisSLB.Password"].Trim(); }
                }
                string IP = ConfigurationManager.AppSettings["RedisSLB.IP"].Trim();
                int Port = int.Parse(ConfigurationManager.AppSettings["RedisSLB.Port"].Trim());
                CSRedisClient rds = new CSRedisClient($"{IP}:{Port},password={Password},defaultDatabase={defaultDatabase},poolsize={poolsize},tryit=1");//单机连接
                return rds;
            }
        }

        /// <summary>
        ///  静态构造方法，初始哨兵连接池
        /// </summary>
        /// <returns></returns>
        private static CSRedisClient GetSentinel(int defaultDatabase)
        {
            try
            {
                Password = ConfigurationManager.AppSettings["RedisPasswordS"].Trim();
                if (ConStringEncrypt)
                {
                    try
                    {
                        Password = PasS.Base.Lib.DESEncrypt.Decrypt(Password);
                    }
                    catch { Password = ConfigurationManager.AppSettings["RedisPasswordS"].Trim(); }
                }
                string[] ServerList = ConfigurationManager.AppSettings["RedisSentinelsAddress"].Trim().Split('|');
                string MasterName = ConfigurationManager.AppSettings["RedisSentinelsMaster"].Trim();

                if (ServerList.Length > 1)
                {
                    CSRedisClient rds = new CSRedis.CSRedisClient($"{MasterName},password={Password},defaultDatabase={defaultDatabase},poolsize={poolsize},tryit=1", ServerList);//哨兵模式
                    return rds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Object thisLock = new Object();

        public static CSRedisClient GetNewClient(int dbNum)
        {
            return Master(dbNum);
        }

        public static void Close()
        {
            foreach (CSRedisClient rc in sdClient.Values)
            {
                rc.Dispose();
            }
            sdClient.Clear();
        }

        public RedisHelperSentinels()
        {
            defdbNum = 0;
        }

        /// <summary>
        /// 指定Redis数据库(Redis默认有0-15共16个数据库)
        /// </summary>
        /// <param name="def_dbNum"></param>
        public RedisHelperSentinels(int def_dbNum)
        {
            defdbNum = def_dbNum;
        }

        private int defdbNum = 0;
        private CSRedisClient _client;

        /// <summary>
        /// 保存各连接的静态列表
        /// </summary>
        private static SortedDictionary<int, CSRedisClient> sdClient = new SortedDictionary<int, CSRedisClient>();

        /// <summary>
        /// 关闭并清除所有连接
        /// </summary>
        public void ClearStatic()
        {
            Clear();
        }

        /// <summary>
        /// 关闭并清除所有连接
        /// </summary>
        public static void Clear()
        {
            lock (thisLock)
            {
                if (sdClient != null && sdClient.Count > 0)
                {
                    foreach (int key in sdClient.Keys)
                    {
                        try
                        {
                            sdClient[key].Dispose();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    sdClient.Clear();
                }
            }
        }

        /// <summary>
        /// Redis当前指定的默认数据库
        /// </summary>
        /// <returns></returns>
        public CSRedisClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = ClientDb(defdbNum);
                }
                return _client;
            }
        }

        /// <summary>
        ///指定Redis数据库(Redis默认有0-15共16个数据库)
        /// </summary>
        /// <param name="dbNum"></param>
        /// <returns></returns>
        public static CSRedisClient ClientDb(int dbNum)
        {
            if (!sdClient.ContainsKey(dbNum))
            {
                lock (thisLock)
                {
                    if (!sdClient.ContainsKey(dbNum))
                    {
                        CSRedisClient nclient = GetNewClient(dbNum);
                        sdClient.Add(dbNum, nclient);
                    }
                }
            }
            return sdClient[dbNum];
        }

        public bool SetDataTable(string Key, DataTable dt)
        {
            string strJson = JsonConvert.SerializeObject(dt);
            return Client.Set(Key, strJson);
        }

        public bool SetDataTable(string Key, DataTable dt, DateTime expiresAt)
        {
            string strJson = JsonConvert.SerializeObject(dt);
            return Client.Set(Key, strJson, (expiresAt - DateTime.Now).Seconds);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="dt">DataTable</param>
        /// <param name="dbNum">数据库编号</param>
        /// <returns></returns>
        public bool SetDataTable(string Key, DataTable dt, int dbNum)
        {
            string strJson = JsonConvert.SerializeObject(dt);
            return ClientDb(dbNum).Set(Key, strJson);
        }

        public DataTable GetDataTable(string Key)
        {
            string strJson = Client.Get<string>(Key);
            if (strJson == null)
                return null;
            return JsonConvert.DeserializeObject<DataTable>(strJson);
        }

        public DataTable GetDataTable(string Key, int dbNum)
        {
            string strJson = ClientDb(dbNum).Get<string>(Key);
            if (strJson == null)
                return null;
            return JsonConvert.DeserializeObject<DataTable>(strJson);
        }

        private bool DoException(Exception ex)
        {
            WriteLog("Error", "DoException", ex.Message);
            if (ex.Message == "READONLY You can't write against a read only slave."
                    || ex.Message.Contains("Exceeded timeout of")
                    || ex.Message.Contains("Unknown reply on integer response: 43OK")
                    || ex.Message.Contains("Unexpected reply"))
            {
                ClearStatic();
                return true;
            }
            return false;
        }

        /**
      * 实际的写日志操作
      * @param type 日志记录类型
      * @param className 类名
      * @param content 写入内容
      */

        protected static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "RedisHelperSentinelsLog");
            }
            catch (Exception ex)
            {
            }

            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ": " + content;
                mySw.WriteLine(time + " " + type);
                mySw.WriteLine(write_content);
                mySw.WriteLine("");
                //关闭日志文件
                mySw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public bool Set(string Key, string value)
        {
            try
            {
                return Client.Set(Key, value);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Set(Key, value);
                }
                return false;
            }
        }

        public bool Set(string Key, byte[] value)
        {
            try
            {
                return Client.Set(Key, value);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Set(Key, value);
                }
                return false;
            }
        }

        public bool Set(string Key, string value, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Set(Key, value);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return ClientDb(dbNum).Set(Key, value);
                }
                return false;
            }
        }

        public bool Set(string Key, byte[] value, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Set(Key, value);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return ClientDb(dbNum).Set(Key, value);
                }
                return false;
            }
        }

        public bool Set(string Key, string value, int expireSeconds, int dbNum)
        {
            try
            {
                return Client.Set(Key, value, expireSeconds);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Set(Key, value, expireSeconds);
                }
                return false;
            }
        }

        public bool Set(string Key, string value, TimeSpan expiresIn)
        {
            try
            {
                return Client.Set(Key, value, expiresIn);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public bool Set(string Key, byte[] value, TimeSpan expiresIn)
        {
            try
            {
                return Client.Set(Key, value, expiresIn);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Set(Key, value, expiresIn);
                }
                return false;
            }
        }

        public bool Set(string Key, string value, TimeSpan expiresIn, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Set(Key, value, expiresIn);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        /// <summary>
        /// 重新设定过期时间
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool ExpireEntryIn(string Key, TimeSpan expiresIn)
        {
            try
            {
                return Client.Expire(Key, (int)expiresIn.TotalSeconds);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        /// <summary>
        /// 重新设定过期时间
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool ExpireEntryIn(string Key, int Seconds, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Expire(Key, Seconds);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public string Get(string Key)
        {
            try
            {
                return Client.Get<string>(Key);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Get<string>(Key);
                }
                return null;
            }
        }

        public byte[] GetByte(string Key)
        {
            try
            {
                return Client.Get<byte[]>(Key);
            }
            catch (Exception ex)
            {
                if (DoException(ex))
                {
                    return Client.Get<byte[]>(Key);
                }
                return null;
            }
        }

        public string Get(string Key, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Get<string>(Key);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return ex.ToString();
            }
        }

        public bool Set<T>(string Key, T value)
        {
            try
            {
                return Client.Set(Key, value);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public bool Set<T>(string Key, T value, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Set(Key, value);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public bool Set<T>(string Key, T value, TimeSpan expiresIn)
        {
            try
            {
                return Client.Set(Key, value, expiresIn);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public bool Set<T>(string Key, T value, TimeSpan expiresIn, int dbNum)
        {
            try
            {
                return ClientDb(dbNum).Set(Key, value, expiresIn);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }

        public T Get<T>(string Key)

        {
            try
            {
                return Client.Get<T>(Key);
            }
            catch
            {
                return default(T);
            }
        }

        public T Get<T>(string Key, int dbNum)

        {
            try
            {
                return ClientDb(dbNum).Get<T>(Key);
            }
            catch
            {
                return default(T);
            }
        }

        public int Del(string Key)
        {
            try
            {
                return (int)Client.Del(Key);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return 0;
            }
        }

        public int Del(string Key, int dbNum)
        {
            try
            {
                return (int)ClientDb(dbNum).Del(Key);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return 0;
            }
        }

        public string HGet(string hashid, string Key)
        {
            return Client.HGet(hashid, Key);
        }

        public bool HSet(string hashid, string Key, string Value)
        {
            try
            {
                return (bool)Client.HSet(hashid, Key, Value);
            }
            catch (Exception ex)
            {
                DoException(ex);
                return false;
            }
        }
    }
}