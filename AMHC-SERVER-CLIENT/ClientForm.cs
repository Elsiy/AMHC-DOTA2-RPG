﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace AMHC_SERVER_CLIENT
{

    public partial class ClientForm : Form
    {
        #region 全局安装
        //设置全局变量
        string dota2Path;
        string addonPath;
        string cfgPath;
        string downloadPath;
        string addonBakPath;

        Boolean download_success = false;

        public ClientForm()
        {
            InitializeComponent();
        }
       
        //窗体载入
        public void Form1_Load(object sender, EventArgs e)
        {
            ReadSettings();
            addonPath = dota2Path + "\\dota\\addons";
            if (!Directory.Exists(addonPath))
            {
                update_dialog("addons文件夹不存在，创建...");
                Directory.CreateDirectory(addonPath);
            }
            addonBakPath = dota2Path + "\\dota\\addons\\zAddonsBak";
            if (!Directory.Exists(addonBakPath))
            {
                Directory.CreateDirectory(addonBakPath);
                update_dialog("addons安装文件夹不存在，创建...");
            }
            cfgPath = dota2Path + "\\dota\\cfg";
            update_dialog("读取已安装RPG-成功");
            ReadAddons();
            update_dialog("读取活动RPG-成功");
            ReadCurrentAddon();
            installList.SelectedIndex = 0;

            //文件显示器相关
            /*string[] LogicDrives = System.IO.Directory.GetLogicalDrives();
            TreeNode[] cRoot = new TreeNode[LogicDrives.Length];
            for (int i = 0; i < LogicDrives.Length; i++)
            {
                TreeNode drivesNode = new TreeNode(LogicDrives[i]);
                treeView.Nodes.Add(drivesNode);
                if (LogicDrives[i] != "A:\\" && LogicDrives[i] != "B:\\")
                    getSubNode(drivesNode, true);
            }*/
            TreeNode aPath = new TreeNode(addonPath);
            //aPath.Text="ADDONS根目录";
            aPath.ToolTipText="ADDONS根目录";
            treeView.Nodes.Add(aPath);
            getSubNode (aPath,true);

        }
        #region 文件显示相关
        private void treeView_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    getSubNode(tn, true);
                }
            }
            catch { ;}
        }
        private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            listView1.Items.Clear();
            DirectoryInfo selDir =
            new DirectoryInfo(e.Node.FullPath);
            DirectoryInfo[] listDir = new DirectoryInfo[0];;
            FileInfo[] listFile = new FileInfo[0];
            try
            {
                listDir = selDir.GetDirectories();
                listFile = selDir.GetFiles();
            }
            catch
            {
                MessageBox.Show("该项目已经不存在");
            }
            foreach (DirectoryInfo cc in listDir) { 
                                
                //imageList1.Images.Clear();
                //Icon mIcon;
                //mIcon = Icon.ExtractAssociatedIcon(e.Node.FullPath+"\\"+cc.Name);

                //imageList1.Images.Add(mIcon);
                //listView1.SmallImageList = imageList1;
                listView1.Items.Add(cc.Name, 6);
            }
            foreach (FileInfo d in listFile) { 
                imageList1.Images.Clear();
                Icon mIcon;
                mIcon = Icon.ExtractAssociatedIcon(e.Node.FullPath+"\\"+d.Name);

                imageList1.Images.Add(mIcon);
                //MessageBox.Show(imageList1.Images.Count.ToString());
                listView1.SmallImageList = imageList1;
                listView1.Items.Add(d.Name, 1);
            }
        }
        private void getSubNode(TreeNode PathName, bool isEnd)
        {
            if (!isEnd)
                return; //exit this  
            TreeNode curNode;
            DirectoryInfo[] subDir;
            DirectoryInfo curDir = new DirectoryInfo(PathName.FullPath);
            try
            {
                subDir = curDir.GetDirectories();
            }
            catch
            {
                subDir = curDir.GetDirectories();
            }
            foreach (DirectoryInfo d in subDir)
            {
                curNode = new TreeNode(d.Name);
                PathName.Nodes.Add(curNode);
                getSubNode(curNode, false);
            }
        }
        #endregion
        //设置活动RPG按钮
        private void amhc_btn_set_addon_Click(object sender, EventArgs e)
        {
            DeleteCurrentAddon();
            CopyAddonFolder();
            ChangeAutoExecCfg();
        }
        //下载RPG按钮
        private void button1_Click_1(object sender, EventArgs e)
        {
            string addonName = "";
            string addonURL;
            int installListCount = addonList.Items.Count;
            Boolean installed = false;
            for (int i = 0; i < installListCount; i++)
            {
                addonList.SelectedIndex = i;
                if (addonList.SelectedItem.ToString() == installList.SelectedItem.ToString())
                {
                    installed = true;
                }
            }
            Boolean toinstall = false;
            if (installed)
            {
                toinstall = (MessageBox.Show("该RPG已经安装，是否删除并重新安装：" + addonName, "请确认", MessageBoxButtons.OKCancel) == DialogResult.OK);
                if (toinstall)
                {
                    DeleteFolder(addonBakPath + "\\" + installList.SelectedItem.ToString());
                    ReadAddons();
                }
            }
            if (!installed) { toinstall = true; }
            if (toinstall)
            {
                addonName = installList.SelectedItem.ToString();
                if (MessageBox.Show("是否下载Addon：" + addonName, "请确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Boolean download_success = false;
                    if (addonName == "Frota")
                    {
                        addonURL = "https://github.com/ash47/Frota/archive/master.zip";
                        update_dialog("开始下载:" + addonName);
                        download_success = startDownload(addonName, addonURL);
                    }
                    else if (addonName == "Reflex")
                    {
                        addonURL = "https://github.com/bmddota/reflexdota/archive/master.zip";
                        update_dialog("开始下载:" + addonName);
                        download_success = startDownload(addonName, addonURL);
                    }
                    else
                    {
                        update_dialog("该ADDON不提供自动下载，请手动下载安装");
                    }
                    Boolean unzip_success = false;
                    if (download_success)
                    {
                        //string SavePath,string unRarPath,string _addonName,string rarName
                        unzip_success = UnzipAddon(downloadPath + "\\unRar\\", downloadPath, "download.rar");
                        if (unzip_success)
                        {
                            copyAddonToAddonBak(addonName);
                            ReadAddons();
                        }
                        else
                        {
                            MessageBox.Show("解压缩失败，请手动安装");
                        }
                    }
                }
            }
        }
        //退出按钮
        private void amhc_btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region 读取写入设置
        private void ReadSettings()
        {
            string iniFile = AppDomain.CurrentDomain.BaseDirectory + "Settings.ini";
            if (!File.Exists(iniFile))
            {
                File.Create(iniFile);
            }
            else
            {
                ArrayList aa = new ArrayList();
                aa = Readini(iniFile);
                foreach (string bb in aa)
                {
                    if (bb.IndexOf("dota2Path") > -1)
                    {
                        dota2Path = bb.Substring(bb.IndexOf("-amhc-is-"), bb.Length - bb.IndexOf("-amhc-is-"));
                        dota2Path = dota2Path.Substring(9, dota2Path.Length - 9);
                        //MessageBox.Show(dota2Path );
                    }
                }
            }
            if (File.Exists(dota2Path + "\\dota.exe"))
            {
                update_dialog("从设置文件读取DOTA2目录成功");
            }
            while (!File.Exists(dota2Path + "\\dota.exe"))
            {
                update_dialog("未设置dota2路径，正在尝试自动检索");
                update_dialog("查找注册表中STEAM位置");
                string steamPath = GetRegistData("SteamPath");
                if (steamPath != "")
                {
                    update_dialog("注册表找到steam");
                    update_dialog(steamPath);
                }
                else
                {
                    update_dialog("未找到STEAM！");
                }
                dota2Path = steamPath + "\\steamapps\\common\\dota 2 beta";
                if (File.Exists(dota2Path + "\\dota.exe"))
                {
                    update_dialog("找到DOTA2，保存dota2路径");
                    update_dialog(dota2Path);

                }
                else
                {
                    update_dialog("自动设置dota2路径失败");
                    OpenFileDialog d2dlg = new OpenFileDialog();
                    d2dlg.Title = "请手动指定您的dota.exe位置";
                    d2dlg.Filter = "DOTA2主程序(dota.exe)|dota.exe";
                    if (d2dlg.ShowDialog() == DialogResult.OK)
                    {
                        dota2Path = d2dlg.FileName;
                        dota2Path = dota2Path.Substring(0, dota2Path.Length - 9);
                        update_dialog("手动设置dota2路径成功");
                    }
                }
            };
            //ArrayList dd = new ArrayList();
            //dd.Add( );
            Writeini(iniFile, "dota2Path-amhc-is-" + dota2Path);
        }
        
        #endregion 读取写入设置
        #region 读取Addons文件夹
        private void ReadAddons()
        {
            addonList.Items.Clear();
            DirectoryInfo addonFolderInfo = new DirectoryInfo(addonBakPath);
            //获取指定目录下的所有子目录及文件类型
            FileSystemInfo[] addonFolders = addonFolderInfo.GetFileSystemInfos();
            foreach (FileSystemInfo addonFolder in addonFolders)
            {
                if (addonFolder is DirectoryInfo)    //判断是否文件夹
                {
                    DirectoryInfo aFolder = new DirectoryInfo(addonFolder.FullName);
                    if (aFolder.Name != "unRar")
                    {
                        addonList.Items.Add(aFolder.Name);//文件夹的名称
                    }
                }
            }
        }
        #endregion 读取Addons文件夹
        #region 读取当前活动Addon

        private void ReadCurrentAddon()
        {
            DirectoryInfo addonFolderInfo = new DirectoryInfo(addonPath);
            //获取指定目录下的所有子目录及文件类型
            FileSystemInfo[] addonFolders = addonFolderInfo.GetFileSystemInfos();
            foreach (FileSystemInfo addonFolder in addonFolders)
            {
                if (addonFolder is DirectoryInfo)    //判断是否文件夹
                {
                    DirectoryInfo aFolder = new DirectoryInfo(addonFolder.FullName);
                    if (aFolder.Name != "zAddonsBak")
                    {
                        update_dialog(aFolder.Name);
                        textBox1.Text = aFolder.Name;
                    }
                }
            }
        }
        #endregion
        #region 设置活动Addon
        private void DeleteCurrentAddon()
        {
            DirectoryInfo addonFolderInfo = new DirectoryInfo(addonPath);
            //获取指定目录下的所有子目录及文件类型
            FileSystemInfo[] addonFolders = addonFolderInfo.GetFileSystemInfos();
            foreach (FileSystemInfo addonFolder in addonFolders)
            {
                if (addonFolder is DirectoryInfo)    //判断是否文件夹
                {
                    DirectoryInfo aFolder = new DirectoryInfo(addonFolder.FullName);
                    if (aFolder.Name != "zAddonsBak")
                    {
                        update_dialog("删除现有Addon:" + aFolder.Name);
                        DeleteFolder(addonPath + "\\" + aFolder.Name);
                    }
                }
            }
        }
        private void CopyAddonFolder()
        {
            update_dialog("复制新RPG文件夹");
            CopyDirectory(addonBakPath + "\\" + addonList.SelectedItem.ToString(), addonPath + "\\" + addonList.SelectedItem.ToString());

            update_dialog("刷新成功，现在的RPG为");
            update_commands(addonList.SelectedItem.ToString());
            ReadCurrentAddon();
        }
        #endregion
        #region 改写AutoExecCfg
        private Boolean ChangeAutoExecCfg()
        {
            return true;
        }
        #endregion
        #region 下载
        private Boolean startDownload(string _addonName, string _downloadURL)
        {
            btn_download.Enabled = false;
            download_success = false;
            btn_download.Text = "正在下载";

            string SavePath = addonBakPath;
            string rarName = "download.rar";
            downloadPath = "D:\\";
            if (!Directory.Exists(downloadPath))
            {
                MessageBox.Show("本电脑无D盘，将下载至C盘。");
                downloadPath = "C:\\";
            }
            progressBar1.Visible = true;
            update_dialog("下载" + _addonName + "到：");
            update_dialog(downloadPath);

            Boolean d = DownloadFile(_downloadURL, downloadPath + rarName, progressBar1);
            progressBar1.Visible = false;
            btn_download.Text = "下载RPG";
            btn_download.Enabled = true;
            return d;

        }
        #endregion
        #region 复制文件到AddonsBak
        private Boolean copyAddonToAddonBak(string _addonName)
        {
            string SourcePath;
            string Destination = addonBakPath;
            if (_addonName == "Frota")
            {
                SourcePath = downloadPath + "unRar\\Frota-master";
                Destination = addonBakPath + "\\Frota";
            }
            else if (_addonName == "Reflex")
            {
                SourcePath = downloadPath + "unRar\\reflexdota-master\\dota\\addons\\reflex";
                Destination = addonBakPath + "\\Reflex";
            }
            else
            {
                MessageBox.Show("不支持自动安装");
                return false;
            }
            DateTime installStartTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            int installedAddonsCount = addonList.Items.Count;
            for (int i = 0; i < installedAddonsCount; i++)
            {

            }
            Boolean d = CopyDirectory(SourcePath, Destination);
            do
            {
                currentTime = DateTime.Now;
                TimeSpan installTimePast = currentTime - installStartTime;
                if (installTimePast.Seconds > 30)
                {
                    if (MessageBox.Show("自动安装已经过去过久，自动安装可能失败了，是否继续等待？", "请确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                    }
                    else
                    {
                        MessageBox.Show("自动安装失败");
                        return false;
                    }
                }
            } while (!d);

            DeleteFolder(downloadPath + "unRar");
            File.Delete(downloadPath + "download.rar");
            update_dialog("安装成功");
            update_commands(_addonName);
            return true;
        }
        #endregion
        #region 安装RPG相关;
        private Boolean UnzipAddon(string unRarPath, string SavePath, string rarName)
        {
            update_dialog("开始解压缩");
            string t7ZipPath = (AppDomain.CurrentDomain.BaseDirectory + "7z.exe");
            //MessageBox.Show(t7ZipPath );
            AMHC_SERVER_CLIENT._7ZipHelper.ZipHelper(t7ZipPath);
            #region bak
            //MessageBox.Show(AMHC_SERVER_CLIENT._7ZipHelper);
            //解压缩开始
            /*if (!AMHC_SERVER_CLIENT.WINRAR.Exists())
            {
                //若无RAR软件，显示RAR路径，请求手动解压缩。
                MessageBox.Show("未找到WinRar，请手动安装");
                update_dialog("未找到解压缩软件！无法解压！");
                Process process = new Process();
                process.StartInfo.FileName = SavePath;
                process.StartInfo.Arguments = "";
                try
                {
                    process.Start();
                }
                catch (System.Exception)
                {
                    throw;
                }
                return false;
            }
            else
            {*/
            #endregion
            //Boolean c = AMHC_SERVER_CLIENT.WINRAR.unCompressRAR(unRarPath, SavePath, rarName);
            update_dialog("尝试使用7ZIP解压缩");
            Boolean c = AMHC_SERVER_CLIENT._7ZipHelper.DecompressFileToDestDirectory(SavePath + rarName, unRarPath);
            return c;
        }
        #endregion


        #region 相关进程
        public void DeleteFolder(string deleteDirectory)
        {
            if (Directory.Exists(deleteDirectory))
            {
                foreach (string deleteFile in Directory.GetFileSystemEntries(deleteDirectory))
                {
                    if (File.Exists(deleteFile))
                        File.Delete(deleteFile);
                    else
                        DeleteFolder(deleteFile);
                }
                Directory.Delete(deleteDirectory);
            }
        }
        public static Boolean CopyDirectory(String sourcePath, String destinationPath)
        {
            if (Directory.Exists(sourcePath))
            {
                DirectoryInfo info = new DirectoryInfo(sourcePath);
                Directory.CreateDirectory(destinationPath);
                foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
                {
                    String destName = Path.Combine(destinationPath, fsi.Name);

                    if (fsi is System.IO.FileInfo)
                    {
                        //如果是文件，复制文件
                        try
                        {
                            File.Copy(fsi.FullName, destName);
                        }
                        catch (System.Exception ex)
                        {

                            return false;
                            throw ex;
                        }
                    }
                    else
                    //如果是文件夹，新建文件夹，递归
                    {
                        try
                        {
                            Directory.CreateDirectory(destName);
                            CopyDirectory(fsi.FullName, destName);
                        }
                        catch (System.Exception ex)
                        {
                            return false;
                            throw ex;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GetRegistData(string name)
        {
            string registData;
            update_dialog("读取STEAM注册表值");
            RegistryKey hkml = Registry.CurrentUser;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey valve = software.OpenSubKey("VALVE", true);
            RegistryKey steam = valve.OpenSubKey("steam", true);
            registData = steam.GetValue(name).ToString();
            if (registData != null)
            {
                update_dialog("成功读取STEAM安装位置");
                update_dialog(registData);
            }
            else
            {
                update_dialog("注册表读取失败");
            }
            return registData;
        }
        private void update_commands(string _aName)
        {
            debug_dialog.Items.Clear();
            update_dialog("本RPG单机进入方法：");
            update_dialog("依次在控制台输入下列指令");
            update_dialog("dota_local_addon_enable 1");
            update_dialog("dota_local_addon_game " + _aName);
            update_dialog("dota_local_addon_map " + _aName);
            update_dialog("dota_force_gamemode 15");
            update_dialog("update_addon_paths");
            update_dialog("dota_wait_for_players_to_load 0");
            string mapName = "";
            if (_aName == "Reflex")
            {
                mapName = "reflex";
            }
            else if (_aName == "Frota")
            {
                update_dialog("括号内的内容不用输");
                mapName = "riverofsouls(钩肥地图)";
            }
            else
            {
                mapName = "地图名";
            }
            update_dialog("map " + mapName);
            update_dialog("广域网连接命令：");
            update_dialog("update_addon_paths");
            update_dialog("connect IP:端口");
        }
        public Boolean DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                update_dialog("正下载到：" + filename);
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    if (totalBytes < 10)
                    {
                        MessageBox.Show("无法取得文件大小，下载失败");
                        download_success = false;
                        return false;
                    }
                    prog.Maximum = (int)totalBytes;
                }

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                update_dialog("下载完成");
                download_success = true;
                prog.Visible = false;
                so.Close();
                st.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("下载出错");
                throw ex;
            }
            return download_success;
        }
        public void update_dialog(string msg)
        {
            debug_dialog.Items.Add(msg);
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            debug_dialog.Items.Clear();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string toDeleteAddonFolder = addonBakPath + "\\" + addonList.SelectedItem.ToString();
            if (Directory.Exists(toDeleteAddonFolder))
            {
                DeleteFolder(toDeleteAddonFolder);
                update_dialog("删除成功！");
            }
            else
            {
                update_dialog("您选中的RPG已经不存在。");
            }
            ReadAddons();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "steam://run/570";
            process.StartInfo.Arguments = "";
            try
            {

                process.Start();
                update_dialog("正启动DOTA2");
            }
            catch (System.Exception)
            {
                MessageBox.Show("启动DOTA2失败");
                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //AUTOEXECSETTINGS forms = new AUTOEXECSETTINGS();
            //forms.Show();
            //string _command = "";
            //_command = debug_dialog.SelectedItem.ToString();
            MessageBox.Show("连接前，如果改变过活动RPG，请手动在控制台输入一次update_addon_paths!!!请先输入完成后再来点击确定，否则游戏可能崩溃！！！");
            SteamCommand(steamcommandtext.Text);
            //if (_command != "")
            //{
            //    update_dialog(":复制成功:请到游戏CTRL+V粘贴:");
            //    Clipboard.SetData(System.Windows.Forms.DataFormats.Text, _command);
            //}
        }
        // 读操作
        private static ArrayList Readini(string readFile)
        {
            // 读取文件的源路径及其读取流
            string strReadFilePath = readFile;
            StreamReader srReadFile = new StreamReader(strReadFilePath);
            ArrayList aa = new System.Collections.ArrayList();
            // 读取流直至文件末尾结束
            while (!srReadFile.EndOfStream)
            {
                string strReadLine = srReadFile.ReadLine(); //读取每行数据
                aa.Add(strReadLine);
            }

            // 关闭读取流文件
            srReadFile.Close();
            return aa;
        }

        public static void Writeini(string writeFile, string data)
        {
            // 写入文件的源路径及其写入流
            StreamWriter sw = new StreamWriter(writeFile, false);
            sw.WriteLine(data);
            sw.Close();
        }
        static void SteamCommand(string command)
        {
            try
            {
                Process.Start("explorer.exe", "steam://" + command);
            }
            catch (System.Exception)
            {
                MessageBox.Show("执行失败，请确认DOTA2运行中");
            }
            
        }

        private void debug_dialog_SelectedIndexChanged(object sender, EventArgs e)
        {
            //steamcommandtext.Text = debug_dialog.SelectedItem.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            steamcommandtext.Text = "connect/play.dota2rpg.com:27060";
        }
        #endregion 全局安装
        #region 全局网络
        private Socket s; //定义Socket对象
        public NetworkStream ns;//网络流 
        public StreamReader sr;//流读取 
        public StreamWriter sw;//流写入

        private void button6_Click(object sender, EventArgs e)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");   //服务器IP 
            try 
            {     
                s.Connect(serverIP, 9902); //连接服务器，端口号用9900 
            } 
            catch (Exception ex) 
            {     
                MessageBox.Show(ex.Message); 
            } 
            try 
            {     
                ns = new NetworkStream(s); //实例化网络流
                sr = new StreamReader(ns); //实例化流读取对象
                sw = new StreamWriter(ns); //实例化写入流对象
                sw.WriteLine(serverText.Text);//将textBox1.Text的数据写入流
                sw.Flush(); //清理缓冲区
                serverResponse.Items.Add(sr.ReadLine());//将从流中读取的数据写入lbInfo 
                
            }
            catch (Exception ex)
            {     
                MessageBox.Show(ex.Message); //捕获异常
            }
        }

        private Boolean SendServerCommand(string _command)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");   //服务器IP 
            try 
            {     
                s.Connect(serverIP, 9900); //连接服务器，端口号用9900 
            } 
            catch (Exception ex) 
            {     
                MessageBox.Show(ex.Message); 
                return false;
            } 
            try 
            {     
                ns = new NetworkStream(s); //实例化网络流
                sr = new StreamReader(ns); //实例化流读取对象
                sw = new StreamWriter(ns); //实例化写入流对象
                sw.WriteLine(_command);//将textBox1.Text的数据写入流
                sw.Flush(); //清理缓冲区
                serverResponse.Items.Add(sr.ReadLine());//将从流中读取的数据写入lbInfo 
                return true;
            }
            catch (Exception ex)
            {     
                MessageBox.Show(ex.Message); //捕获异常
                return false;
            }
            
        }
        
        #endregion 全局网络

        private void button8_Click(object sender, EventArgs e)
        {
            //for (int i = 1;i<7;i++){
            //    SendServerCommand("RefreshServerStatu"+i.ToString());
            //}
            SendServerCommand("RefreshServerStatu1");
        }

        private void serverResponse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void startServer1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("将会在服务器再启动一个DOTA2 RPG房间，请确认输入的服务器启动密码正确，服务器开始启动后，本程序可能会假死，请耐心等待，服务器端口号将会在左边窗口显示");
            string server_command = "StartServergAMHCmeReflexeAMHCGMmAMHCmereflexeAMHCMN"+ "mAMHCpwd"+pwdtext.Text+"eAMHCpwd";
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress serverIP = IPAddress.Parse("218.244.130.187");   //服务器IP 
            try 
            {     
                s.Connect(serverIP, 9987); //连接服务器，端口号用9900 
            } 
            catch (Exception ex) 
            {     
                MessageBox.Show(ex.Message); 
            } 
            try 
            {     
                ns = new NetworkStream(s); //实例化网络流
                sr = new StreamReader(ns); //实例化流读取对象
                sw = new StreamWriter(ns); //实例化写入流对象
                sw.WriteLine(server_command);//将textBox1.Text的数据写入流
                sw.Flush(); //清理缓冲区
                serverResponse.Items.Add(sr.ReadLine());//将从流中读取的数据写入lbInfo 
                
            }
            catch (Exception ex)
            {     
                MessageBox.Show(ex.Message); //捕获异常
            }
        }

    }
}