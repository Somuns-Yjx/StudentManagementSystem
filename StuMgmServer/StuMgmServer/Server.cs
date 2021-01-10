using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
        /// <summary>
        ///  委托更新界面
        /// </summary>
        private void setText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                SetTextCallback method = new SetTextCallback(setText);
                Invoke(method, new object[] { text });
            }
            else
            {
                richTextBox1.Text += text;
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
                btnSerSwitch.Text = "打开服务器";
        }



    }
}
