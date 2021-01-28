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
        #region 流
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
        #endregion


        /// <summary>
        ///  获取返回数据
        /// </summary>
        public static byte[] CreateServerResponse(byte[] clientRequset)
        {
            try
            {
                var cr = Deserialize<ClientRequest>(clientRequset);

                ServerResponse sr = null;

                switch (cr.Func)
                {
                    case ClientFunc.VerifyLogin:
                        UserInfoLogin uil = (UserInfoLogin)cr.Object;
                        LoginResponse lr = new LoginResponse();

                        if (getPermission(uil, out lr.Level))
                            lr.Token = addToken(uil.Account, ref Info.myToken);

                        sr = new ServerResponse(lr);
                        break;

                    case ClientFunc.GetCourseInfo:

                        sr = new ServerResponse(getCourseInfo());
                        Debug.Print(sr.Object.GetType().ToString());
                        break;

                    case ClientFunc.GetUserCourseInfo:  // 获取学员个人课程信息，详情

                        UserCourseInfoReq ucir = (UserCourseInfoReq)cr.Object;
                        UserCourseInfo uc = new UserCourseInfo();

                        if (!verifyToken(ucir.Job_Id, ucir.Token))
                            break;

                        if (getUserCourseStatus(ucir.Job_Id, out uc.Status, out uc.Details))
                            sr = new ServerResponse(uc);
                        break;

                    case ClientFunc.SUpdateCourse: // 学生修改课程信息，仅有权修改自己的个别类型状态
                        /* Todo
                         verify (jobid token)
                        update */
                        UserCourseInfoOper suico = (UserCourseInfoOper)cr.Object;
                        UpdateRp urs = new UpdateRp();

                        if (!verifyToken(suico.Job_Id, suico.Token))
                            break;

                        sUpdateInfo(suico.Job_Id, suico.sqlStr, out  urs.Final, out urs.ErrMessge);

                        sr = new ServerResponse(urs);

                        break;
                    case ClientFunc.TUpdateCourse:
                        /*Todo
                        教师修改课程信息，有权更改学生课程状态
                        verify (jobid,token)
                        verify(permission)
                        update*/
                        UserCourseInfoOper tucio = (UserCourseInfoOper)cr.Object;
                        UpdateRp urt = new UpdateRp();

                        if (!verifyToken(tucio.Job_Id, tucio.Token))
                            break;

                        Lvl l = Lvl.Error;
                        if (!getPermission(tucio.Job_Id, out l))
                            break;

                        if (!(l == Lvl.Teacher || l == Lvl.Teacher))
                        {
                            urt.Final = false;
                            urt.ErrMessge = "permission err";
                            break;
                        }

                        tUpdateInfo(tucio.sqlStr, out urt.Final, out urt.ErrMessge);
                        sr = new ServerResponse(urt);

                        break;
                }
                if (null == sr)
                    return null;
                return Serialize<ServerResponse>(sr);
            }
            catch
            {
                return null;    // 非客户端连接：用调试助手连接服务器
            }
        }

        const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505;charset = utf8";

        #region Verify

        static bool getPermission(object o, out Lvl level)
        {
            level = Lvl.Error;

            string qStu = "select * from user where account = ";

            if (o is UserInfoLogin)
            {
                UserInfoLogin uil = (UserInfoLogin)o;
                qStu += uil.Account + " and password = '" + uil.Password + "'";
            } // 首次登陆验证
            else if (o is UserCourseInfoOper)
            {
                UserCourseInfoOper ucio = (UserCourseInfoOper)o;
                qStu += ucio.Job_Id + ucio.sqlStr;
            } // 数据库操作验证权限

            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                if (mReader.HasRows)
                {
                    mReader.Read();
                    level = (Lvl)mReader.GetInt16("level");
                    return true;
                }
                else
                {
                    level = Lvl.NotFound;
                    return false;
                }
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

        static bool verifyToken(short job_id, int token)
        {
            if (Info.myToken[job_id] == token)
                return true;
            return false;
        }

        #endregion

        #region CourseInfo
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
        #endregion

        #region UserCourseInfo
        const int statusColumn = 3;
        const int detailsColumn = 4;

        /// <summary>
        /// 员工获取课程信息
        /// </summary>
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
                status = (mReader.IsDBNull(statusColumn)) ? " " : mReader.GetString("status");
                details = (mReader.IsDBNull(detailsColumn)) ? " " : mReader.GetString("details");
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

        #endregion

        #region Update

        #region Student

        /// <summary>
        ///  学员更改个人课程状态
        /// </summary>
        static void sUpdateInfo(short job_id, string sqlStr, out bool final, out string eMessage)
        {
            final = false;
            eMessage = null;
            string str = "select * from usercourse_info where account = " + job_id + sqlStr;
            MySqlConnection conn = new MySqlConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                if (cmd.ExecuteNonQuery() > 0)
                    final = true;
            }
            catch (MySqlException e)
            {
                eMessage = e.Message;
                Debug.Print(e.Message);    // 可以去掉
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region Teacher

        static void tUpdateInfo(string sqlStr, out bool final, out string eMessage)
        {
            final = false;
            eMessage = null;
            string str = "select * from usercourse_info where " + sqlStr;
            MySqlConnection conn = new MySqlConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                if (cmd.ExecuteNonQuery() > 0)
                    final = true;
            }
            catch (MySqlException e)
            {
                eMessage = e.Message;
                Debug.Print(e.Message);    // 可以去掉
            }
            finally
            {
                conn.Close();
            }
        }



        #endregion

        #endregion



    }


}
