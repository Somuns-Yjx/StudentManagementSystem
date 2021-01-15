using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using StuMgmLib.MyNameSpace;

namespace Test
{
    public partial class Form1 : Form
    {
        IPEndPoint IPP = null;
        Socket socket = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Info.ClientSend cs = new Info.ClientSend();
            cs.account = 01941;
            cs.password = "980505";
            cs.sqlStr = null;
            byte[] send = BinaryED.Serialize<Info.ClientSend>(cs);
            socket.Send(send);
            byte[] recv = new byte[65535];
            socket.Receive(recv);
            Info.ServerSend ss = BinaryED.Deserialize<Info.ServerSend>(recv);
            short per = ss.permission;
            DataSet ds = ss.ds;
            DataTable dt1 = ds.Tables["course_info"];
            DataTable dt2 = ds.Tables["user_info"];
            DataTable dt3 = ds.Tables["user"];

            dataGridView1.DataSource = dt1;
            dataGridView2.DataSource = dt2;
            dataGridView3.DataSource = dt3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = "127.0.0.1";
                int port = 502;
                IPP = new IPEndPoint(IPAddress.Parse(ip), port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPP);
            }
            catch (Exception econnect)
            {
                MessageBox.Show(econnect.Message);
            }
        }

    }
}
