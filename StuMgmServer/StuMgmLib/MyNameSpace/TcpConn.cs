/* Describtion : Class for Tcp Network Connection
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/18 
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace StuMgmLib.MyNameSpace
{
    // 还有一种验证连接方式: Token
    public class TcpConn
    {
        private IPEndPoint IPP = null;
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
            cb.Items.Add("127.0.0.1");
        }

        #region  开启服务器
        public void OpenServer(string ipAddr, int port)
        {
            IPP = new IPEndPoint(IPAddress.Parse(ipAddr), port);
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
        /// <summary>
        /// 接收客户端连接
        /// </summary>
        public string AcceptConn()
        {
            try
            {
                socketClient = socket.Accept();         // 阻塞等待客户端连接
                return socketClient.RemoteEndPoint.ToString() + "  已连接 \n";
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        const int recvTimeOut = 3000;                                   // 设置接收超时时间
        const int recvLength = 65535;
        #region 接收数据
        /// <summary>
        ///  接收数据
        /// </summary>
        public string AcpMsg()
        {
            byte[] dataRecv = new byte[recvLength];                    // 定义接收数组
            string reEdPoint = "";
            try
            {
                reEdPoint = socketClient.RemoteEndPoint.ToString();
                socketClient.ReceiveTimeout = recvTimeOut;
                socketClient.Receive(dataRecv);

                var cs = BinaryED.Deserialize<Info.ClientSend>(dataRecv);
                Info.ServerSend ss = DataAnalyze.ClientSendAnalyze(cs);
                byte[] dataSend = BinaryED.Serialize<Info.ServerSend>(ss);
                socketClient.Send(dataSend);

                return reEdPoint + "  断开连接 \n";
            }
            catch                                // 客户端断开连接
            {
                if (socketClient != null)
                    return reEdPoint + "  断开连接 \n";
                else
                    return null;
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
