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
        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认退出程序？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
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
                    tcpConn.CloseServer();
                else if (sFlag != true)
                {
                    int port = Convert.ToInt16(txtPort.Text);
                    tcpConn.OpenServer(port);
                    tUpdateUi = new Thread(updateHistory);
                    tUpdateUi.Start();
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
                setText(tcpConn.AcceptConn());
                setText(tcpConn.AcpMsg());
            }
        }

        /// <summary>
        ///  定时器更新btn开关服务器
        /// </summary>
        private void tmr_Tick(object sender, EventArgs e)
        {
            if (tcpConn.SocketExist)
                btnSerSwitch.Text = "关闭服务器";
            else
                btnSerSwitch.Text = "开启服务器";
        }


    }
}
