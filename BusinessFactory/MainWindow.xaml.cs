using BusinessInterface;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BusinessFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string projectID = "default";
        private DataTable dtbus;
        private DataTable dtbusName;
        private BusServiceAdapter busServiceAdapter = null;

        public MainWindow()
        {
            InitializeComponent();
            Paragraph graph = new Paragraph();
            inlines = graph.Inlines;
            txt_log.Document.Blocks.Add(graph);
            LagLoading();
        }

        private async void LagLoading()
        {
            Lag loading = new Lag();

            GridMain.Children.Add(loading);
            loading.Margin = new Thickness(5);

            Grid.SetColumn(loading, 0);
            Grid.SetRow(loading, 0);
            Grid.SetColumnSpan(loading, 2);
            await Task.Run(() =>
            {
                LoadBusiness();
            });

            GridMain.Children.Remove(loading);
        }

        private void LoadBusiness()
        {
            try
            {
                //MultipleDB.InitDBInfo();
                //MultipleDBBus.InitDBInfo();

                dtbus = new LoadBusiness().LoadBusinessDll(System.AppDomain.CurrentDomain.BaseDirectory);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    dgvbus.AutoGenerateColumns = false;
                    dgvbus.ItemsSource = dtbus.DefaultView;
                }));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("拒绝访问"))
                {
                    ShowLog("如果要启动http服务，请先以管理员身份运行Visual Studio或者程序。");
                }
                else
                    ShowLog(ex.ToString());
            }
            finally
            {
            }
        }

        /// <summary>
        /// 加载开发模式的SlbBusServer
        /// </summary>
        private void StartSLBBusServerDevelop()
        {
            ShowLog("StartSLBBusServerDevelop");
            // SlbBusServer.DatasReciveBusness_EventHandler += TcpPerformTaskServer_DatasReciveBusness_EventHandler;
            //请勿修改StartDevelop_test！！！！

            if ((bool)chbLocal_pref.IsChecked)
            {
                AddLocalBusToSLBBusServerDevelop();
            }
        }

        /// <summary>
        /// 将本地的Bus加载到SlbBusServer中
        /// </summary>
        private void AddLocalBusToSLBBusServerDevelop()
        {
            if (busServiceAdapter != null && dtbus != null && dtbus.Rows.Count > 0)
            {
                ShowLog("AddLocalBusToSLBBusServerDevelop");
                LoadBusiness loadBusines = new LoadBusiness();
                foreach (DataRow dr in dtbus.Rows)
                {
                    //   string fpat1 = dr["FilePath"].ToString() + dr["FileName"].ToString();
                    //  string stratpath = Application.StartupPath;
                    //  string Filename = Application.StartupPath+dr["FilePath"].ToString() + dr["FileName"].ToString();
                    //   Dictionary<string, Type> dic= loadBusines.GetBusTypeName(Filename);
                    string Fullname = dr["FullName"].ToString();
                    string Name = dr["Name"].ToString();
                    string DllName = dr["FileName"].ToString();
                    string FilePath = dr["FilePath"].ToString();
                    string BusID;
                    if (AddLocalBusToSLBBusServerDevelop(Fullname, Name, DllName, FilePath, out BusID))
                    {
                        dr["Local_pref"] = true;
                        if (!string.IsNullOrWhiteSpace(BusID))
                            dr["BusID"] = BusID;
                    }
                }
            }
        }

        private bool AddLocalBusToSLBBusServerDevelop(string Fullname, string Name, string DllName, string FilePath, out string BusID)
        {
            BusID = "";
            if (busServiceAdapter == null)
            {
                return false;
            }
            DataTable dtbusdb = SpringAPI.BusinessInfoTableGet(string.Format("projectID ='{0}' and DllName ='{1}' and Namespace_Class='{2}' ", projectID, DllName, Fullname));
            BusinessInfo businessInfo = new BusinessInfo();
            businessInfo.BusID = Fullname;
            businessInfo.BusName = Name;
            businessInfo.BMODID = "";
            businessInfo.busDDLBS = BusDDLBS.NoPolicy;
            businessInfo.busType = BusType.AsyncResult;
            businessInfo.DllName = DllName;
            businessInfo.DllPath = FilePath;
            businessInfo.NamespaceClass = Fullname;
            businessInfo.ProjectID = projectID;
            businessInfo.version = "1.0.0.0";
            if (dtbusdb.Rows.Count > 0)
            {
                businessInfo.BusID = dtbusdb.Rows[0]["BusID"].ToString();
                businessInfo.BusName = dtbusdb.Rows[0]["BusName"].ToString();
                businessInfo.busType = (BusType)int.Parse(dtbusdb.Rows[0]["BusType"].ToString());
                BusID = dtbusdb.Rows[0]["BusID"].ToString();
            }

            // AddOrUpdateBusinessDevelop_test(businessInfo);
            return true;
        }

        /// <summary>
        /// 将本地的Bus从SlbBusServer中移除
        /// </summary>
        private void RemoveLocalBusToSLBBusServerDevelop()
        {
            if (busServiceAdapter != null)
            {
                RemoveBusinessDevelop_test();
                ShowLog("RemoveLocalBusToSLBBusServerDevelop");
            }
        }

        /// <summary>
        /// 移除开发本地DLL业务，仅限于开发测试调用
        /// </summary>
        /// <param name="busnessInfo"></param>
        public void RemoveBusinessDevelop_test()
        {
            busServiceAdapter.RemoveBusiness();
        }

        /// <summary>
        /// 将本地的Bus从SlbBusServer中移除
        /// </summary>
        private void RemoveLocalBusToSLBBusServerDevelop(string BusID)
        {
            if (busServiceAdapter != null)
            {
                RemoveBusinessDevelop_test(BusID);
            }
        }

        ///// <summary>
        /////  加载开发本地DLL业务，仅限于开发测试调用
        ///// </summary>
        ///// <param name="busnessInfo"></param>
        //public void AddOrUpdateBusinessDevelop_test(BusinessInfo busnessInfo)
        //{
        //    BusinessInfoBusVersion busVersion = new BusinessInfoBusVersion(busnessInfo);
        //    busServiceAdapter.AddOrUpdateBusinessDevelop_test(busVersion);

        //}

        ///// <summary>
        /////  加载开发本地DLL业务，仅限于开发测试调用
        ///// </summary>
        ///// <param name="busnessInfo"></param>
        //public void AddOrUpdateBusinessDevelop_test(BusinessInfoBusVersion busnessInfo)
        //{
        //    busServiceAdapter.AddOrUpdateBusinessDevelop_test(busnessInfo);
        //}
        /// <summary>
        /// 移除开发本地DLL业务，仅限于开发测试调用
        /// </summary>
        /// <param name="BusID"></param>
        public void RemoveBusinessDevelop_test(string BusID)
        {
            busServiceAdapter.RemoveBusiness(BusID);
        }

        private void dgvbus_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView mys = (DataRowView)dgvbus.SelectedItem;
            bool Checkde = bool.Parse(mys["Local_pref"].ToString());
            if (!Checkde)
            {
                string Fullname = mys.Row["FullName"].ToString();
                string Name = mys.Row["Name"].ToString();
                string DllName = mys.Row["Filename"].ToString();
                string FilePath = mys.Row["FilePath"].ToString();
                string BusID;
                if (AddLocalBusToSLBBusServerDevelop(Fullname, Name, DllName, FilePath, out BusID))
                {
                    mys["CLocal_pref"] = true;
                    if (!string.IsNullOrWhiteSpace(BusID))
                        mys.Row["BusID"] = BusID;
                }
            }
            else
            {
                string BusID = mys.Row["BusID"].ToString();
                RemoveLocalBusToSLBBusServerDevelop(BusID);
                mys["CLocal_pref"] = false;
            }
        }

        private const int MaxCount = 1000;
        private int Count = 0;
        private InlineCollection inlines;

        /// <summary>
        /// 将日志显示在文本框里
        /// </summary>
        /// <param name="log"></param>
        private void ShowLog(string log)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                AppendText(Brushes.Green, DateTime.Now.ToString("HH:mm:ss.fff") + ":【" + txtFullName.Text + "】 " + log);
            }));
        }
        private void ShowLogError(string log)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                AppendText(Brushes.Red, DateTime.Now.ToString("HH:mm:ss.fff") + ":【" + txtFullName.Text + "】 " + log);
            }));
        }
        private void AppendText(Brush color, string format, params object[] args)
        {

            txt_log.BeginChange();
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            builder.Append(Count++);
            builder.Append("]");
            builder.Append(string.Format(format, (object[])args));
            builder.Append("\n");
            string str = builder.ToString();
            inlines.Add(new Run(str) { Foreground = color });
            if (inlines.Count > MaxCount)
            {
                inlines.Remove(inlines.FirstInline);
            }
            txt_log.ScrollToEnd();
            txt_log.EndChange();
        }
        /// <summary>
        /// 将日志显示在文本框里
        /// </summary>
        /// <param name="log"></param>
        private void ShowOut(string log)
        {


            log = ConvertJsonString(log);




            this.Dispatcher.Invoke(new Action(() =>
            {
                txt_TestOut.Text = log;
            }));
        }
        private string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private void dgvbus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView mys = (DataRowView)dgvbus.SelectedItem;
            txtFullName.Text = mys.Row["FullName"].ToString();
            txt_TestParam.Text = TestBusParamHelper.GetParam(txtFullName.Text);
        }

        //--------------------以下为测试内容------------------------------------------------------------------------------------

        private async void btn_test_Click(object sender, RoutedEventArgs e)
        {
            await Test();
        }

        private async Task<bool> Test()
        {
            DataRowView mys = (DataRowView)dgvbus.SelectedItem;


            if (mys == null)
            {
                ShowLogError("没有选择业务");


                return false;
            }


            string fullname = txtFullName.Text;
            string InData = txt_TestParam.Text;

            if (string.IsNullOrEmpty(InData))
            {
                ShowLogError("请求报文为空");


                return false;
            }
            string SubBusID = txt_SubBusID.Text;
            int dealtype = 1;
            if (RB_byte.IsChecked == true)
            { dealtype = 2; }
            if (RB_InputRoot.IsChecked == true)
            {
                dealtype = 3;
            }

            if (mys != null && !string.IsNullOrEmpty(fullname))
            {
                txt_TestOut.Text = "";
                var result = await Task.Run(() =>
                {
                    if (mys != null && !string.IsNullOrEmpty(fullname))
                    {
                        string Filename = System.AppDomain.CurrentDomain.BaseDirectory + mys["FilePath"].ToString() + mys["Filename"].ToString();
                        Assembly assembly = Assembly.LoadFile(Filename);
                        ProcessingBusinessBase Bus = (ProcessingBusinessBase)assembly.CreateInstance(fullname);

                        Type type = assembly.GetType(fullname);
                        object obj = Activator.CreateInstance(type);
                        MethodInfo method = type.GetMethod("ProcessingBusiness", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);

                        string key = "";

                        Stopwatch sw = Stopwatch.StartNew();

                        if (dealtype == 3)//InputRoot
                        {
                            InputRoot input = new InputRoot();
                            input.BusID = fullname;
                            input.msgid = NewIdHelper.NewOrderId18;
                            input.InData = InData;
                            OutputRoot output = Bus.Trans(input);

                            ShowOut(JsonConvert.SerializeObject(output));
                            ShowLog("耗时：" + sw.ElapsedMilliseconds.ToString());
                        }
                        else if (dealtype == 2)
                        {
                            int ccN = int.Parse(SubBusID);
                            byte[] bin = System.Text.Encoding.UTF8.GetBytes(InData);
                            byte[] bout;
                            if (Bus.ProcessingBusiness(ccN, bin, out bout))
                            {
                                ShowLog("执行成功。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");
                            }
                            else
                            {
                                ShowLogError("执行失败。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");
                            }
                        }
                        else if (dealtype == 1)
                        {
                            SLBBusinessInfo sLBBusOut;
                            SLBBusinessInfo sLBBusIn = new SLBBusinessInfo();
                            sLBBusIn.AUID = "wogk1hGg0X";//当前API用户

                            sLBBusIn.BusID = fullname;// ((DataRowView)dgvbus.SelectedItem).Row["BusID"].ToString();
                            sLBBusIn.TID = "HT:P" + NewIdHelper.NewOrderId20;
                            // sLBBusIn.CTag = txt_CTag.Text;
                            sLBBusIn.BusData = InData;
                            sLBBusIn.SubBusID = SubBusID;
                            if (Bus.ProcessingBusiness(sLBBusIn, out sLBBusOut))
                            {
                                if (sLBBusOut.ReslutCode == 1)
                                {
                                    ShowLog("执行成功。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");

                                    ShowOut(sLBBusOut.BusData);
                                }
                                else if (sLBBusOut.ReslutCode == 0)
                                {
                                    ShowLog("执行成功。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");

                                    ShowOut(sLBBusOut.BusData);
                                }
                                else
                                {
                                    ShowLogError("执行失败。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");

                                    ShowOut(JsonConvert.SerializeObject(sLBBusOut));
                                }
                                TestBusParamHelper.WriteParam(fullname, sLBBusIn.BusData);
                            }
                            else if (sLBBusOut != null)
                            {
                                ShowOut(JsonConvert.SerializeObject(sLBBusOut));
                            }
                            else
                            {
                                ShowLogError("执行失败。（耗时：" + sw.ElapsedMilliseconds.ToString() + "ms）");
                            }
                        }
                        else if (dealtype == 4)
                        {
                            SLBBusinessInfo sLBBusOut;
                            SLBBusinessInfo sLBBusIn = new SLBBusinessInfo();

                            sLBBusIn = Newtonsoft.Json.JsonConvert.DeserializeObject<SLBBusinessInfo>(InData);
                            sLBBusIn.TID = "HT:P" + NewIdHelper.NewOrderId20;
                            sLBBusIn.AUID = "JSQH";//当前API用户
                            if (Bus.ProcessingBusiness(sLBBusIn, out sLBBusOut))
                            {
                                if (sLBBusOut.ReslutCode == 1)
                                {
                                    ShowOut(sLBBusOut.BusData);
                                }
                                else
                                {
                                    ShowOut(JsonConvert.SerializeObject(sLBBusOut));
                                }
                                TestBusParamHelper.WriteParam(fullname, sLBBusIn.BusData);
                            }
                            else if (sLBBusOut != null)
                            {
                                ShowOut(JsonConvert.SerializeObject(sLBBusOut));
                            }
                            else
                            {
                            }
                        }
                    }
                    return true;
                });
                return result;
            }
            return true;
        }

        /// MD5 加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="encryptKey">加密密钥,最长32位</param>
        /// <returns></returns>
        private static string Encode(string encryptString, string encryptKey)
        {
            return (encryptString + encryptKey).Md5Hash();
        }

        public bool CallBusiness(SLBBusinessInfo sLBBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            DataRow[] drs = dtbus.Select("BusID ='" + sLBBusinessInfo.BusID + "'");
            if (drs.Length >= 0)//此服务器上有
            {
                string fullname = drs[0]["FullName"].ToString();
                string Filename = System.AppDomain.CurrentDomain.BaseDirectory + drs[0]["FilePath"].ToString() + drs[0]["Filename"].ToString();
                Assembly assembly = Assembly.LoadFile(Filename);
                ProcessingBusinessBase Bus = (ProcessingBusinessBase)assembly.CreateInstance(fullname);
                return Bus.ProcessingBusiness(sLBBusinessInfo, out OutBusinessInfo);
            }
            else//此服务器上无 需要转发其他服务器执行
            {
                return BusServiceAdapter.Ipb_CallOtherBusiness(sLBBusinessInfo, out OutBusinessInfo);
            }
        }
    }
}