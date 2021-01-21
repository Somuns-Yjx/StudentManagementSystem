/* Describtion : Class for Data Analyze
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/18 
 */
using MySql.Data.MySqlClient;
using System.Data;
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
            notFound = -1,
            error = -2,
            admin = 1,
            teacher = 2,
            student = 3,
        }
        private const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505;charset = utf8";

        /*  Recv:    ___________________________________________________________________
         *                |     Account  |  Password  |   (SqlOperate)                                                                                          |
         *                |___short_____string______string[]________________________________________|
         * Analyze:
         *                  Account    Permission  (SqlOperate)
         *                  
         * Send:     ____________________________________________________________________
         *                |  Permission    |    DataSet                                                                                                                          |
         *                |___short________DS___________________________________________________|
         * 
        */

        /// <summary>
        /// 解析ClientSend
        /// </summary>
        public static Info.ServerSend ClientSendAnalyze(Info.ClientSend cs)
        {
            Info.ServerSend ss = new Info.ServerSend();
            ss.Permission = loginVerify(cs.Account, cs.Password);   // 验证身份
            if (ss.Permission < 0) // 小于0，则权限有误
            {
                ss.Ds = null;
                return ss;
            }

            string[] tbName;
            bool stuFlag = false;
            switch (ss.Permission)
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

            ss.SqlSucceed = false;
            if (cs.SqlStr != null)  // sql语句为空，则表示仅登录验证；若不为空，则取数据库操作返回值，并返回SS；
            {
                ss.SqlSucceed = mySqlModify(tbName, cs.SqlStr);
                return ss;
            }

            ss.Ds = getDataSet(tbName, stuFlag, cs.Account);
            return ss;
        }

        /// <summary>
        ///  登录验证，若失败，则返回错误码；若身份验证成功，则返回用户权限；
        /// </summary>
        private static short loginVerify(short account, string psw)
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
            catch (MySqlException)
            {
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
        private static bool mySqlModify(string[] tbName, string[] sqlStr) // Need to change ......
        {
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                int len = sqlStr.Length;
                for (int index = 0; index < len; index++)
                {
                    MySqlCommand mCmd = new MySqlCommand(sqlStr[index], con);  // 优化：所操作数据表是否匹配权限
                    mCmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
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
                    string newStr = str + " " + tbName[index];
                    if ((stuFlag == true) && (tbName[index] == "user_info"))
                    {
                        newStr += " where job_id = " + account.ToString();  // 学员权限时，返回该学员数据项
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