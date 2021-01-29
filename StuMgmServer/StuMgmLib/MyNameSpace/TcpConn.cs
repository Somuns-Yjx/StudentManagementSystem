/* Describtion : Class for Tcp Network Connection
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/18 
 */
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace StuMgmLib.MyNameSpace
{
    public class TcpConn
    {
        public EndPoint Ep;
        public Socket socketClient;

        private IPEndPoint ipp;
        private Socket socket;

        private bool my_SocketExist = false;
        /// <summary>
        /// 判断服务器开关
        /// </summary>
        public bool SocketExist
        {
            get { return my_SocketExist; }
            private set { my_SocketExist = value; }
        }

        public void GetIPAddress(ComboBox cb)
        {
            cb.Items.Clear();
            foreach (IPAddress ipAddr in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddr.AddressFamily.ToString() == "InterNetwork")
                {
                    cb.Items.Add(ipAddr.ToString());
                }
            }
            if (cb.FindString("127.0.0.1") == -1)
                cb.Items.Add("127.0.0.1");
        }

        /// <summary>
        ///  开服务器
        /// </summary>
        public void OpenServer(string ipAddr, int port)
        {
            ipp = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipp);
            socket.Listen(0);
            SocketExist = true;
        }

        /// <summary>
        /// 关服务器
        /// </summary>
        public void CloseServer()
        {
            SocketExist = false;
            if (socketClient != null)
                socketClient.Close();
            if (socket != null)
                socket.Close();
        }

        /// <summary>
        /// 接收客户端连接
        /// </summary>
        public void AcceptConn()
        {
            try
            {
                socketClient = socket.Accept();         // 阻塞等待客户端连接
                Ep = socketClient.RemoteEndPoint;
            }
            catch (SocketException se)
            {
                Debug.Print(DateTime.Now + " : " + se.Message);
            }
        }

        private int recvTimeOut = 2000;
        public int RecvTimeOut // 设置接收超时时间 
        {
            get { return recvTimeOut; }
            set { recvTimeOut = value; }
        }
        const int recvLength = 65535;
        /// <summary>
        ///  接收数据
        /// </summary>
        public byte[] AcceptMsg()
        {
            byte[] clientSend = new byte[recvLength];
            try
            {
                socketClient.ReceiveTimeout = RecvTimeOut;
                socketClient.Receive(clientSend);
                return clientSend;
            }
            catch (Exception e)                                // 客户端断开连接
            {
                Debug.Print(DateTime.Now + " : " + e.Message);
                if (socketClient != null)
                    socketClient.Close();
                return null;
            }
        }

    }
}
