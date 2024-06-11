
using BusinessInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BusinessFactory
{
    public class LoadBusiness
    {
        /// <summary>
        /// 绑定本地目录文件信息
        /// </summary>
        public DataTable LoadBusinessDll(string Fullpth)
        {

            DataTable dtBusDll = new DataTable();
            dtBusDll.TableName = Fullpth;
            dtBusDll.Columns.Add("FilePath", typeof(String));
            dtBusDll.Columns.Add("Filename", typeof(String));
            dtBusDll.Columns.Add("FullName", typeof(String));
            dtBusDll.Columns.Add("Name", typeof(String));
            dtBusDll.Columns.Add("BusID", typeof(String));
            dtBusDll.Columns.Add("Version", typeof(String));
            dtBusDll.Columns.Add("FileSize", typeof(String));
            dtBusDll.Columns.Add("cTime", typeof(DateTime));
            dtBusDll.Columns.Add("MTime", typeof(DateTime));
            dtBusDll.Columns.Add("Extension", typeof(String));
            dtBusDll.Columns.Add("FileDescription", typeof(String));
            dtBusDll.Columns.Add("CompanyName", typeof(String));
            dtBusDll.Columns.Add("Local_pref", typeof(Boolean));
            dtBusDll.Columns.Add("isdirect", typeof(Boolean));
            dtBusDll.Columns.Add("Note", typeof(string));
            InitList2("", Fullpth, Fullpth, dtBusDll);

            return dtBusDll;
        }

        /// <summary>
        /// 获取DLL中的所有业务类的名称Name和全名FullName
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public DataTable GetBusName(string FileName)
        {
            DataTable dtBusType = new DataTable();

            dtBusType.Columns.Add("Name", typeof(String));
            dtBusType.Columns.Add("FullName", typeof(String));

            Dictionary<string, Type> dic = GetBusTypeName(FileName);
            foreach (Type t in dic.Values)
            {
                DataRow drnew = dtBusType.NewRow();
                drnew["Name"] = t.Name;
                drnew["FullName"] = t.FullName;
                dtBusType.Rows.Add(drnew);
            }
            return dtBusType;
        }

        /// <summary>
        /// 获取DLL中的所有业务类的 全名FullName 和FullName
        /// FullName Type
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public Dictionary<string, Type> GetBusTypeName(string FileName)
        {
            Dictionary<string, Type> dic = new Dictionary<string, Type>();
            try
            {
                Assembly assembly = Assembly.LoadFile(FileName);
                IZisdirect zisdirect = (IZisdirect)assembly.CreateInstance("BusinessInterface.Zisdirect");
                if (zisdirect != null)
                {
                    try
                    {
                        dic = zisdirect.GetTypes();

                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dic;
        }


        protected virtual void InitList2(string pathName, string strName, string jkml, DataTable tabletemp)
        {
            //获得当前目录下的所有文件 
            DirectoryInfo curDir = new DirectoryInfo(strName);//创建目录对象。
            FileInfo[] dirFiles;
            try
            {
                dirFiles = curDir.GetFiles();
            }
            catch { return; }

            string[] arrSubItem = new string[8];
            //文件的cTime和访问时间。

            foreach (FileInfo fileInfo in dirFiles)
            {
                string strFileName = fileInfo.Name;
                //如果不是文件pagefile.sys
                if (!strFileName.Equals("pagefile.sys") && wjlxpd(pathName, fileInfo.Extension))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(Path.Combine(strName, strFileName));
                        AssemblyName assemblyName = assembly.GetName();
                        Version version = assemblyName.Version;
                        // temprow[6] = version.ToString();
                        if (assembly.GetName().Name.Contains("968"))
                        {

                        }
                        IZisdirect zisdirect = (IZisdirect)assembly.CreateInstance("BusinessInterface.Zisdirect");
                        if (zisdirect != null)
                        {
                            try
                            {
                                Dictionary<string, Type> dic = zisdirect.GetTypes();
                                foreach (Type tp in dic.Values)
                                {
                                    DataRow temprow = tabletemp.NewRow();
                                    temprow["Local_pref"] = false;

                                    temprow["FullName"] = tp.FullName;
                                    temprow["BusID"] = tp.FullName;
                                    temprow["Name"] = tp.Name;
                                    temprow["FilePath"] = strName.Substring(jkml.Length) + "\\";// + strFileName;
                                                                                                // temprow["FilePath"] = Path.Combine(strName, strFileName);
                                    temprow["Filename"] = strFileName;

                                    FileVersionInfo myFVI = FileVersionInfo.GetVersionInfo(Path.Combine(strName, strFileName));
                                    temprow["Version"] = myFVI.FileVersion;
                                    temprow["FileSize"] = jsFiledx(fileInfo.Length);
                                    temprow["cTime"] = fileInfo.CreationTime;
                                    temprow["MTime"] = fileInfo.LastWriteTime;
                                    temprow["Extension"] = fileInfo.Extension;
                                    temprow["isdirect"] = true;
                                    temprow["Note"] = zisdirect.Note();
                                    //  
                                    temprow["FileDescription"] = myFVI.FileDescription;
                                    temprow["CompanyName"] = myFVI.CompanyName;
                                    tabletemp.Rows.Add(temprow);
                                }

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {; }

            }
            //以下是向列表框中插入目录，不是文件。获得当前目录下的各个子目录。
            int iItem = 0;//调用listView1.Items.Insert(iItem,LiItem);时用。不能使用iconIndex。
            DirectoryInfo Dir = new DirectoryInfo(strName);//创建目录对象。
            foreach (DirectoryInfo di in Dir.GetDirectories())
            {
                InitList2(pathName, di.FullName, jkml, tabletemp);
            }
        }

        private string jsFiledx(long Filelenss)
        {
            decimal Filelen = Convert.ToDecimal(Filelenss);
            decimal long1024 = 1024.00M;
            if (Filelen < long1024)
            {
                return Filelen + " 字节";
            }
            else if (Filelen < long1024 * long1024)
            {
                return Convert.ToDecimal(Filelen / long1024).ToString("0.000") + "K";
            }
            else if (Filelen < long1024 * long1024 * long1024)
            {
                return Convert.ToDecimal(Filelen / (long1024 * long1024)).ToString("0.000") + "M";
            }
            else if (Filelen < long1024 * long1024 * long1024 * long1024)
            {
                return Convert.ToDecimal(Filelen / (long1024 * long1024 * long1024)).ToString("0.000") + "G";
            }
            else
            {
                return Convert.ToDecimal(Filelen / (long1024 * long1024 * long1024 * long1024)).ToString("0.000") + "T";
            }
        }
        /// <summary>
        /// 判断文件Extension是否符合设定要求,符合返回True,不符合返回False
        /// </summary>
        /// <param name="kzm">文件Extension</param>
        /// <returns></returns>
        private bool wjlxpd(string pthName, string kzm)
        {
            return kzm.ToLower() == ".dll";
        }

    }
}
