using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace StuMgmServer
{
    class TcpConn
    {
        private IPEndPoint IPP = null;
        private Socket socket = null;
        private Socket socketClient = null;
        //private Thread tAccept = null;

        #region 连接状态字段
        private bool my_connect = false;
        public bool Connect
        {
            get { return my_connect; }
            set { my_connect = value; }
        }
        #endregion

        private bool my_SocketExist = false;
        /// <summary>
        /// 判断服务器开关
        /// </summary>
        public bool SocketExist
        {
            get { return my_SocketExist; }
            private set { my_SocketExist = value; }
        }

        #region  开启服务器
        public void OpenServer(int port)
        {
            IPP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IPP);
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
        public EndPoint acceptConnection()
        {
            try
            {
                socketClient = socket.Accept();         // 阻塞等待客户端连接
                return socketClient.RemoteEndPoint;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 接收数据
        public string acpMsg()
        {
            byte[] arrDataRecv = new byte[1024];                    // 定义接收数组
            string reEdPoint = socketClient.RemoteEndPoint.ToString();
            try
            {
                int len = socketClient.Receive(arrDataRecv);
                List<byte> listDataRecv = new List<byte> { };      // 定义截取列表
                return reEdPoint + " " + len.ToString();
            }
            catch                                // 客户端断开连接
            {
                return reEdPoint;
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
