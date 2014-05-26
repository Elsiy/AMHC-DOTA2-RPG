using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AMHC_SERVER_CLIENT
{
    class ClientProc
    {
        public Socket tempSocket;

        public ClientProc()
        {

        }

        public void CloseThread()
        {
            Thread th = Thread.CurrentThread;
            th.Abort();
        }
      
        public void ServiceClient()
        {
            IPEndPoint newclient = (IPEndPoint)this.tempSocket.RemoteEndPoint;

            NetworkStream ns = new NetworkStream(tempSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            //string welcome = "Welcome to tcp server!";
            //sw.WriteLine(welcome);
            //sw.Flush();
            sw.AutoFlush = true;

            char[] myReadBuffer = new char[1024];

            int count;
            while (true)
            {
                try
                {
                    count = sr.Read(myReadBuffer, 0, myReadBuffer.Length);//读取数据
                    if (count > 0)
                    {
                        sw.Write(myReadBuffer, 0, count);//将原返回数据
                        //sw.Flush();

                    }
                }
                catch
                {
                    break;
                }
            }
            sw.Close();
            sr.Close();
            ns.Close();
        }
    }
}
