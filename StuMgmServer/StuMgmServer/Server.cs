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

        private delegate void SetTextCallbakc();
        public Server()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        //具体要调用的方面 
        private void callbakc()
        {
            richTextBox1.Text += tcpConn.acceptConnection() + "  Connected \n";
            richTextBox1.Text += tcpConn.acpMsg() + "  Disconnected \n";
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
                    tcpConn.OpenServer(Convert.ToInt16(txtPort.Text));

                    //test();
                    tAccept = new Thread(newTest);
                    tAccept.Start();

                }
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
        }
        private void newTest()
        {
            SetTextCallbakc stcb = new SetTextCallbakc(callbakc);
            while (true)
            {
                Invoke(stcb);
                //callbakc();
            }
        }




    }
}
