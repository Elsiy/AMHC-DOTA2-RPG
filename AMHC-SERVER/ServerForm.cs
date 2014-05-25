using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace AMHC_SERVER
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                mythread = new Thread(new ThreadStart(BeginListen));
                mythread.Start();
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "完成", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        Thread mythread;
        Socket socket;
        // 清理所有正在使用的资源。
        protected override void Dispose( bool disposing )
        {
            try
　          {
　　              socket.Close();//释放资源
　　              mythread.Abort ( ) ;//中止线程
　          }
　          catch{ }
            if( disposing )
              {
                   if (components != null)
                   {
                       components.Dispose();
                   }
              }
              base.Dispose( disposing );
         }
         private void BeginListen()
         {
                string myAddr = "218.244.130.187";
                IPAddress ServerIP = IPAddress.Parse(myAddr);
                IPEndPoint iep = new IPEndPoint(ServerIP, 27000);
                socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                byte[] byteMessage=new byte[100]; 
                this.label1.Text=iep.ToString();
                socket.Bind(iep); 
//              do
                while(true)
                {
                   try
                   {
                       socket.Listen(5);
                       Socket newSocket=socket.Accept();
                       newSocket.Receive(byteMessage);
                       string sTime = DateTime.Now.ToShortTimeString ( ) ;
                       string msg=sTime+":"+"Message from:";
                        msg+=newSocket.RemoteEndPoint.ToString()+Encoding.Default.GetString(byteMessage);
                       this.listBox1.Items.Add(msg);
                   }
                   catch(SocketException ex)
                   {
                       this.label1.Text+=ex.ToString();
                   }
              }
//            while(byteMessage!=null);
         }
    }
}
