using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StuMgmLib.MyNameSpace
{
    public class DataAnalyze
    {
        const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505";

        public static void GetFunc(byte[] dataRecv)
        {
            short account = 01941;
            string psw = "980505";
            bool res = LoginVerify(account, psw);
            res = !res;


            // 对buf数据处理，判断身份验证还是数据库操作
            //switch ()
            //{
            //    case :break;
            //    case :break;
            //    case :break;
            //    default:break;
            //}
        }

        public static bool LoginVerify(short account, string psw)
        {
            string qStu = "select * from user where account = " + account + " and password = '" + psw + "'";
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                if (mReader.HasRows)
                    return true;
                else
                    return false;
                //DataTable dt = new DataTable();
                //dt.Load(mReader);
                //Random r = new Random();
            }
            catch (MySqlException mySqlEx)
            {
                MessageBox.Show(mySqlEx.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        private static DataTable mysqlUse()
        {
            // mysql Query
            string key = "";
            string para = "";
            string qStu = "select * from staffs where ";            // 验证登录信息
            switch (key)
            {
                case "Id":
                    qStu += " Id =" + Convert.ToString(para);
                    break;
                case "All":
                    qStu = "select * from staffs "; break;
                default:
                    qStu += key + "= '" + Convert.ToString(para) + "'";
                    break;
            }
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(mReader);
                return dt;
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