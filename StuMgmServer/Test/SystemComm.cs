using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace StuMgmClient
{
    class SystemComm
    {
        #region 基本函数
        const int bufSize = 1024 * 1024;
        static IPEndPoint m_ipEndpoint;
        static Socket m_socket;
        static byte[] m_buf;

        internal static void Init(string ip, int port)
        {
            IPAddress ipAdress = IPAddress.Parse(ip);
            m_ipEndpoint = new IPEndPoint(ipAdress, port);
            m_buf = new byte[bufSize];
        }
        static bool Connect()
        {
            try
            {
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_socket.Connect(m_ipEndpoint);
                return m_socket.Connected;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false; 
            }
        }
        static bool Send(byte[] data)
        {
            try
            {
                m_socket.Send(data);
                return true;
            }
            catch(Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }
        static bool Receive()
        {
            try
            {
                Array.Clear(m_buf, 0, m_buf.Length);
                m_socket.Receive(m_buf);
                return true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }
        static void DisConnect()
        {
            m_socket.Close();
        }
        #endregion

        internal static ErrCode GetData(ClientRequest req,out object o)
        {
            o = null;

            if (!Connect())
                return ErrCode.FailConnect;

            byte[] sendBuf;
            if (!Utility.BinSerialize(req, out sendBuf))
                return ErrCode.FailSerial;

            if (!Send(sendBuf))
                return ErrCode.FailSend;

            if (!Receive())
                return ErrCode.FailReceive;

            DisConnect();

            if (!Utility.BinDeserialize(m_buf, out o))
                return ErrCode.FailDeserial;
            return ErrCode.Success;
        }
    }
}

