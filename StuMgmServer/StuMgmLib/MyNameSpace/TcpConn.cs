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
    // 还有一种验证连接方式: Token
    public class TcpConn
    {
        public EndPoint Ep;
        private IPEndPoint ipp = null;
        private Socket socket = null;
        private Socket socketClient = null;

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

        #region  开启服务器
        public void OpenServer(string ipAddr, int port)
        {
            ipp = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipp);
            socket.Listen(0);
            SocketExist = true;
        }
        #endregion

        #region 关闭服务器
        public void CloseServer()
        {
            if (socketClient != null)
                socketClient.Close();
            if (socket != null)
                socket.Close();
            SocketExist = false;
        }
        #endregion

        #region 接收客户端连接
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
                Debug.Print(se.Message);
            }
        }
        #endregion

        const int recvTimeOut = -1;                                   // 设置接收超时时间
        const int recvLength = 65535;
        #region 接收数据
        /// <summary>
        ///  接收数据
        /// </summary>
        public void AcpMsg()
        {
            byte[] clientSend = new byte[recvLength];                    // 定义接收数组
            try
            {

                socketClient.ReceiveTimeout = recvTimeOut;
                socketClient.Receive(clientSend);

                byte[] serverSend = SystemCtrl.GetServerResponse(clientSend);

                if (serverSend != null)
                    socketClient.Send(serverSend);

            }
            catch (SocketException se)                                // 客户端断开连接
            {
                Debug.Print(se.Message);
            }
            finally
            {
                if (socketClient != null)
                    socketClient.Close();
            }
        }
        #endregion

    }
}
