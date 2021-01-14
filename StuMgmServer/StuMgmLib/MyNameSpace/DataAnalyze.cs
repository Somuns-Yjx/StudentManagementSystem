using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StuMgmLib.MyNameSpace
{
    /// <summary>
    /// 数据操作
    /// </summary>
    public class DataAnalyze
    {
       
        private enum verifyCode : short
        {
            error = -1,
            notFound = -2,
            admin = 1,
            teacher = 2,
            student = 3,
        }
        private const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505;charset = utf8";

        /*  Recv:    ___________________________________________________________________
         *                |     Account  |  Password  |   (SqlOperate)                                                                                          |
         *                |___short_____string______string________________________________________|
         * Analyze:
         *                  Account    Permission  (SqlOperate)
         *                  
         * Send:     ____________________________________________________________________
         *                |  Permission    |    DataSet                                                                                                                                                        |
         *                |___short________DS_______________________________________________________________|
        */

        /// <summary>
        /// 解析ClientSend
        /// </summary>
        public static Info.ServerSend ClientSendAnalyze(Info.ClientSend cs)
        {
            Info.ServerSend ss = new Info.ServerSend();
            ss.permission = LoginVerify(cs.account, cs.password);   // 验证身份
            if (ss.permission < 0) // 小于0，则权限有误
            {
                ss.ds = null;
                return ss;
            }
            // if(operationCode != 0)
            // 写数据表操作
            // To do sth here ........
            string[] tbName;
            bool stuFlag = false;
            switch (ss.permission)
            {
                case (short)verifyCode.admin:
                    tbName = new string[] { "user_info", "course_info", "user" };
                    break;
                case (short)verifyCode.teacher:
                    tbName = new string[] { "user_info", "course_info" };
                    break;
                case (short)verifyCode.student:
                    tbName = new string[] { "user_info", "course_info" };
                    stuFlag = true; break;
                default:
                    tbName = null;
                    break;
            }
            ss.ds = getDataSet(tbName, stuFlag, cs.account);
            return ss;
        }


        /// <summary>
        ///  登录验证，若失败，则返回错误码；若身份验证成功，则返回用户权限；
        /// </summary>
        private static short LoginVerify(short account, string psw)
        {
            short notFound = -1;
            short error = -2;
            string qStu = "select * from user where account = " + account + " and password = '" + psw + "'";
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                if (mReader.HasRows)
                {
                    mReader.Read();
                    return mReader.GetInt16("permission");
                }
                else
                    return notFound;
            }
            catch (MySqlException mySqlEx)
            {
                MessageBox.Show(mySqlEx.Message);
                return error;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        ///  改
        /// </summary>
        private static void mySqlModify()
        {

        }

        /// <summary>
        ///  查  将各表填入dataset
        /// </summary>
        private static DataSet getDataSet(string[] tbName, bool stuFlag, int account)
        {
            string str = "select * from ";
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                for (int index = 0; index < tbName.Length; index++)
                {
                    string newStr = str + tbName[index];
                    if ((stuFlag == true) && (tbName[index] == "user_info"))
                    {
                        newStr += "where job_id = " + account.ToString();
                    }
                    MySqlCommand mCmd = new MySqlCommand(newStr, con);
                    MySqlDataReader mReader = mCmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(mReader);
                    dt.TableName = tbName[index];
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (MySqlException mySqlEx)
            {
                MessageBox.Show(mySqlEx.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }



    }

}