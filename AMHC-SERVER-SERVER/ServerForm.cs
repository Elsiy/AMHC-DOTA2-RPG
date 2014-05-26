using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
namespace AMHC_SERVER_SERVER
{
    public partial class ServerForm : Form
    {
        private Socket sSocket;//定义Socket对象
        private Thread th;//客户端连接服务器的线程 
        public Socket cSocket;//单个客户端连接的Socket对象 
        public NetworkStream ns;//网络流 
        public StreamReader sr;//流读取 
        public StreamWriter sw;//流写入

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
        }
        //public string readline;
        //接下来是设计客户端连接的管理，主要包括服务器与客户端之间的连接和收发数据问题，把它们放在一个函数

        public void Communication()
        {
            while (true)
            {
                try
                {
                    cSocket = sSocket.Accept(); //用cSocket来代表该客户端连接
                    if (cSocket.Connected)//测试是否连接成功
                    {

                        ns = new NetworkStream(cSocket);  //建立网络流，便于数据的读取
                        sr = new StreamReader(ns);         //实例化流读取对象
                        sw = new StreamWriter(ns);         //实例化写入流对象
                        //readedline = sr.ReadLine();
                        test();                               //从流中读取

                        sw.WriteLine(update_serverDialog("111")); //向流中写入数据
                        sw.Flush();                           //清理缓冲区
                    }
                    else
                    {
                        //MessageBox.Show("连接失败");
                    }
                }
                catch (SocketException ex)
                {
                    //MessageBox.Show(ex.Message);//捕获Socket异常
                    //serverInfo.Items.Add(ex.Message);
                    throw ex;
                }
                catch (Exception es)
                {
                    //MessageBox.Show("其他异常" + es.Message);//捕获其他异常
                    throw es;

                }
            }
        }

        private delegate void FlushClient();//代理
        public string update_serverDialog(string msg)
        {
            FlushClient stcb = new FlushClient(RequestUpdate);
            Invoke(stcb);
            //label3.Text = "服务器状态关闭";
            //MessageBox.Show(request);
            return label3.Text;
        }
        public void RequestUpdate()
        {
            string request = serverInfo.Items[serverInfo.Items.Count - 1].ToString();
            change_respon_text(request);
            do_request(request);
        }
        private void change_respon_text(string request)
        {
            if (request.IndexOf("StartServer") > -1)
            {
                string server_game = request.Substring(request.IndexOf("gAMHCme") + 7, request.IndexOf("eAMHCGM") - (request.IndexOf("gAMHCme")) - 7);
                //MessageBox.Show(server_game);
                string server_map = request.Substring(request.IndexOf("mAMHCme") + 7, request.IndexOf("eAMHCMN") - (request.IndexOf("mAMHCme")) - 7);
                //MessageBox.Show(server_map);
                string pwd = request.Substring(request.IndexOf("mAMHCpwd") + 8, request.IndexOf("eAMHCpwd") - (request.IndexOf("mAMHCpwd")) - 8);
                
                if (pwd == "AMHC27015")
                {
                string free_port = GetFreePort();
                
                    if (free_port != "")
                    {
                        label3.Text = "服务器启动，端口为" + free_port;
                        startServer(server_map, server_game, free_port);
                    }
                    else
                    {
                        label3.Text = "所有端口都被占用了，服务器启动失败";
                    }
                    //MessageBox.Show(free_port);
                }
                else
                {
                    label3.Text = "启动服务器密码错误！";
                }
            }
            else
            {
                label3.Text = "指令错误";
            }
        }
        private string GetFreePort()
        {
            string fPort = "";
            Random ran = new Random();
            int startPort = ran.Next(27015, 30000);
            while (fPort == "")
            {
                string sfPort = startPort.ToString();
                if (check_port(sfPort))
                {
                    fPort = sfPort;
                }
                startPort = ran.Next(27015, 30000);
            }
            return fPort;
        }
        private void do_request(string request)
        {

        }
        private Boolean check_port(string port)
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo("netstat", "-a");
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                string result = p.StandardOutput.ReadToEnd();
                if (result.IndexOf(":" + port) >= 0)
                    return false;
                //MessageBox.Show("4000端口被占用");
                else
                {
                    //MessageBox.Show("ok");
                    return true;
                }
            }
            catch (SystemException e)
            {

                //MessageBox.Show(e.ToString());
                return false;
                throw e;

            }
        }

        //以下代码的用法在第16章有关线程的用法时曾提到过，主要用于从当前线程操作主线程中的控件，这里就不在赘
        public void send()
        {
            if (serverInfo.Items.Count > 10)
            {
                serverInfo.Items.Clear();
            }
            serverInfo.Items.Add(sr.ReadLine() + "\n");
        }

        private delegate void SetTextCallback();//用于操作主线程控件
        public void test()
        {
            SetTextCallback stcb = new SetTextCallback(send);
            Invoke(stcb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建Socket对象
            IPAddress serverIP = IPAddress.Any;
            IPEndPoint server = new IPEndPoint(serverIP, 9987);//实例化服务器的IP和端口
            sSocket.Bind(server); sSocket.Listen(10);
            try
            {
                th = new Thread(new ThreadStart(Communication));//创建线程
                th.Start();
                //启动线程     
                label1.Text = "服务器启动成功！";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("服务器启动失败！" + ex.Message);
                button1.Enabled = true;
                throw ex;

            }
        }
        private void ServerFormClose(object sender, EventArgs e)
        {
            // Application.Exit();
            if (th != null)
                th.Abort();

            this.Dispose();
            this.Close();
        }
        private string GetServerStatu(int serverNo)
        {
            return "-AMHC-服务器关闭中";
        }
        public void serverResponse_itemsAdded(object sender, EventArgs e)
        {
        }

        string sv_file = "C:\\amhcserver\\sv.exe";
        string sv_path = "C:\\amhcserver";
        private void button2_Click(object sender, EventArgs e)
        {
            string mArg = "-console +hostname Dota2server +sv_lan 0 -game dota " +
                "+dota_local_addon_enable 1 " +
                "dota_local_addon_game " + comboBox1.Text + " +dota_local_addon_map " + comboBox1.Text +
                " +dota_force_gamemode 15 +update_addon_paths +dota_wait_for_players_to_load 0 +dota_wait_for_players_to_load_timeout 10" +
                " +maxplayers 12 +map " + comboBox3.Text + " -port " +
                comboBox2.Text;
            //MessageBox.Show(mArg);
            StartProcess(sv_file, sv_path, mArg, ProcessWindowStyle.Normal);
        }
        private void startServer(string mapName, string gameName, string port)
        {
            string mArg = "-console +hostname Dota2server +sv_lan 0 -game dota " +
            "+dota_local_addon_enable 1 " +
            "dota_local_addon_game " + gameName + " +dota_local_addon_map " + gameName +
            " +dota_force_gamemode 15 +update_addon_paths +dota_wait_for_players_to_load 0 +dota_wait_for_players_to_load_timeout 10" +
            " +maxplayers 12 +map " + mapName + " -port " +
            port;
            //MessageBox.Show(mArg);
            StartProcess(sv_file, sv_path, mArg, ProcessWindowStyle.Normal);
        }
        private Boolean eventHandled;
        public static bool StartProcess(string file, string workdirectory, string args, ProcessWindowStyle style)
        {
            try
            {
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(file, args);
                startInfo.WindowStyle = style;
                startInfo.WorkingDirectory = workdirectory;
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.EnableRaisingEvents = true;
                myprocess.Exited += new EventHandler(ProcessExited);
                myprocess.Start();
                return true;

            }
            catch (Exception e0)
            {
                //MessageBox.Show("启动应用程序时出错！原因：" + e0.Message);
                return false;
                throw e0;
            }
            //return false;
        }
        private static void ProcessExited(object sender, System.EventArgs e)
        {
            //MessageBox.Show("服务器异常退出");
        }

        private void serverInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void serverInfo_MessageIncome(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //sSocket.Close();
        }


    }
}
