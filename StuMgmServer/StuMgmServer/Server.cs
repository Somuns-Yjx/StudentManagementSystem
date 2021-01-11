using System;
using System.Threading;
using System.Windows.Forms;
using StuMgmLib.MyNameSpace;

namespace StuMgmServer
{
    public partial class Server : Form
    {
        TcpConn tcpConn = new TcpConn();
        Thread tAccept = null;
        private delegate void SetTextCallback(string text);

        public Server()
        {
            InitializeComponent();
        }
        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        /// <summary>
        ///  委托更新界面
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
                    tAccept = new Thread(updateHistory);
                    tAccept.Start();
                }
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
        }

        private void updateHistory()
        {
            while (tcpConn.SocketExist)
            {
                setText(tcpConn.acceptConnection());
                setText(tcpConn.acpMsg());
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (tcpConn.SocketExist)
                btnSerSwitch.Text = "关闭服务器";
            else
                btnSerSwitch.Text = "开启服务器";
        }





    }
}
