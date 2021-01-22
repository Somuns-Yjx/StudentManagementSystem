using System;
using System.Threading;
using System.Windows.Forms;
using StuMgmLib.MyNameSpace;

namespace StuMgmServer
{
    public partial class Server : Form
    {
        TcpConn tcpConn = new TcpConn();
        Thread tUpdateUi = null;
        private delegate void SetTextCallback(string text);

        public Server()
        {
            InitializeComponent();
        }
        private void 刷新IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcpConn.GetIPAddress(cbxIPAddr);
        }
        /// <summary>
        ///  委托：更新界面方法
        /// </summary>
        private void setText(string text)
        {
            if (rtxHistory.InvokeRequired)
            {
                SetTextCallback method = new SetTextCallback(setText);
                Invoke(method, new object[] { text });
            }
            else
            {
                rtxHistory.Text = text + rtxHistory.Text;
            }
        }

        /// <summary>
        ///  btn开关点击事件：开启、关闭服务器
        /// </summary>
        private void btnSerSwitch_Click(object sender, EventArgs e)
        {
            bool sFlag = tcpConn.SocketExist;
            try
            {
                if (sFlag == true)
                {
                    tcpConn.CloseServer();
                    btnSerSwitch.Text = "开启服务器";
                }
                else if (sFlag != true)
                {
                    int port = Convert.ToInt16(txtPort.Text);
                    tcpConn.OpenServer(cbxIPAddr.Text, port);
                    tUpdateUi = new Thread(updateHistory);
                    tUpdateUi.Start();
                    btnSerSwitch.Text = "关闭服务器";
                }
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
        }

        /// <summary>
        ///  线程：接收客户端连接，接收数据，数据处理；更新历史界面
        /// </summary>
        private void updateHistory()
        {
            while (tcpConn.SocketExist)
            {
                tcpConn.AcceptConn();
                setText(DateTime.Now.ToLongTimeString() + " : " + tcpConn.Ep.ToString() + "  建立连接 \n");
                tcpConn.AcpMsg();
                setText(DateTime.Now.ToLongTimeString() + " : " + tcpConn.Ep.ToString() + "  断开连接 \n");
            }
        }


        private void Server_Load(object sender, EventArgs e)
        {
            tcpConn.GetIPAddress(cbxIPAddr);
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult dr = MessageBox.Show("确认退出程序？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr != DialogResult.Yes)
            System.Environment.Exit(0);

        }





    }
}
