using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace StuMgmLib.MyNameSpace
{
    class SystemCtrl
    {
        /// <summary>
        ///  序列化
        /// </summary>
        static byte[] Serialize<T>(T c)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter iFormatter = new BinaryFormatter();
            iFormatter.Serialize(ms, c);
            byte[] buf = ms.GetBuffer();
            return buf;
        }

        /// <summary>
        ///  反序列化
        /// </summary>
        static T Deserialize<T>(byte[] buf)
        {
            MemoryStream ms = new MemoryStream(buf);
            BinaryFormatter iFormatter = new BinaryFormatter();
            var obj = (T)iFormatter.Deserialize(ms);
            return obj;
        }

        /// <summary>
        ///  获取返回数据
        /// </summary>
        public static byte[] GetServerResponse(byte[] clientRequset)
        {
            try
            {
                var cs = Deserialize<ClientRequest>(clientRequset);

                ServerResponse sr = new ServerResponse(null);

                switch (cs.Func)
                {
                    case ClientFunc.VerifyLogin:
                        UserInfo ui = (UserInfo)cs.Object;

                        if (verifyLogin(ui, out sr.Lev))
                            sr.Token = addToken(ui.Account, ref Info.myToken);
                        break;

                    case ClientFunc.GetCourseInfo:
                        sr.Object = getCourseInfo();
                        Debug.Print(sr.Object.GetType().ToString());
                        break;

                    case ClientFunc.GetUserCourseInfo:
                        short job_id;
                        UserCourseInfo uc = new UserCourseInfo();
                        if (verifyToken((Dictionary<short, int>)cs.Object, out job_id))
                            if (getUserCourseStatus(job_id, out uc.Status, out uc.Details))
                                sr.Object = uc;
                        break;

                }

                return Serialize<ServerResponse>(sr);
            }
            catch
            {
                return null;
            }
        }

        const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505;charset = utf8";

        static bool verifyLogin(UserInfo userInfo, out LvErr level)
        {
            level = LvErr.Error;
            string qStu = "select * from user where account = " + userInfo.Account + " and password = '" + userInfo.Password + "'";
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                if (mReader.HasRows)
                {
                    mReader.Read();
                    level = (LvErr)mReader.GetInt16("level");
                    return true;
                }
                else
                {
                    level = LvErr.NotFound;
                    return false;
                }
            }
            catch (MySqlException)
            {
                level = LvErr.Error;
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        static int addToken(short job_id, ref Dictionary<short, int> myToken)
        {
            if (myToken != null)
            {
                if (myToken.Count > Info.tokenMaxCount)  // 清除缓存
                    myToken.Clear();
                if (myToken.ContainsKey(job_id))
                {
                    myToken.Remove(job_id);
                }
            }
            Random r = new Random();    // 伪随机
            int token = r.Next();
            myToken.Add(job_id, token);
            return token;
        }

        static bool verifyToken(Dictionary<short, int> dic, out short account)
        {
            account = 0;
            if (dic.Count != 1)
                return false;

            foreach (var item in dic)
            {
                if (!Info.myToken.ContainsKey(item.Key))
                    return false;
                if (Info.myToken[item.Key] == item.Value)
                {
                    account = item.Key;
                    return true;
                }
            }
            return false;
        }

        const int nameColumn = 1;
        const int contentColumn = 3;
        static List<CourseInfo> getCourseInfo()
        {
            List<CourseInfo> listCI = new List<CourseInfo>();
            string str = "select * from course_info";
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(str, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                while (mReader.Read())
                {
                    CourseInfo ci = new CourseInfo();
                    ci.Id = mReader.GetInt16("id");
                    ci.Pid = mReader.GetInt16("pid");
                    ci.Time = mReader.GetInt16("time");
                    ci.Name = (mReader.IsDBNull(nameColumn)) ? "无" : mReader.GetString("name");
                    ci.Content = (mReader.IsDBNull(contentColumn)) ? "无" : mReader.GetString("content");
                    listCI.Add(ci);
                }
                return listCI;
            }
            catch (MySqlException mySqlEx)
            {
                Debug.Print(DateTime.Now + " : " + mySqlEx.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        static bool getUserCourseStatus(short jobId, out string status, out string details)
        {
            status = "";
            details = "";
            string qStu = "select * from usercouse_info where job_id = " + jobId;
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                mReader.Read();
                status = (mReader.IsDBNull(nameColumn)) ? " " : mReader.GetString("status");
                details = (mReader.IsDBNull(nameColumn)) ? " " : mReader.GetString("details");
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

    }


}
