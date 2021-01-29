/* Describtion : Class for Data Operation
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/28
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StuMgmLib.MyNameSpace
{
    public class SystemCtrl
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

                ServerResponse sr = new ServerResponse(null);

                switch (cr.Func)
                {
                    case ClientFunc.VerifyLogin:
                        #region 登陆验证
                        LoginResponse lr = new LoginResponse();
                        sr = new ServerResponse(lr);
                        UserInfoLogin login = (UserInfoLogin)cr.Object;

                        getPerFromDB(login, out sr.Final, out sr.ErrMessage, out lr.Level);
                        if (sr.Final)
                            lr.Token = addToken(login.Account, lr.Level, ref QT.quickTable);

                        break;
                        #endregion
                    case ClientFunc.GetCourseInfo:
                        #region 获取课程表
                        sr.Object = getCosInfo(out sr.Final, out sr.ErrMessage);
                        break;
                        #endregion
                    case ClientFunc.GetSelfUserCourseInfo:
                        #region 学员获取个人进度详情

                        UserCourseInfoReq ucir = (UserCourseInfoReq)cr.Object;
                        UserCourseInfo uc = new UserCourseInfo();
                        sr = new ServerResponse(uc);

                        vrTokenFrT(ucir.Job_Id, ucir.Token, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        getUsrCosStatus(ucir.Job_Id, out sr.Final, out sr.ErrMessage, out uc.Status, out uc.Details);
                        break;

                        #endregion
                    case ClientFunc.GetSomeoneUserCInfo:
                        #region 教师获取某人进度详情

                        UserCourseInfoOper uciO = (UserCourseInfoOper)cr.Object;

                        vrTokenFrT(uciO.Job_Id, uciO.Token, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        Lvl l = getPerFrT(uciO.Job_Id);
                        vrPer(l, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        sr.Object = getUsrCosStatus(uciO.Status, out sr.Final, out sr.ErrMessage);
                        break;

                        #endregion
                    case ClientFunc.SUpdateCourse:
                        #region 学生修改课程状态

                        UserCourseInfoOper suico = (UserCourseInfoOper)cr.Object;

                        vrTokenFrT(suico.Job_Id, suico.Token, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        sUpUsrCosInfo(suico.Job_Id, suico.Status, out  sr.Final, out sr.ErrMessage);
                        break;

                        #endregion
                    case ClientFunc.TUpdateCourse:
                        #region 教师修改课程详情

                        UserCourseInfoOper tucio = (UserCourseInfoOper)cr.Object;

                        vrTokenFrT(tucio.Job_Id, tucio.Token, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        Lvl lv = getPerFrT(tucio.Job_Id);
                        vrPer(lv, out sr.Final, out sr.ErrMessage);
                        if (!sr.Final)
                            break;

                        tUpUsrCosInfo(tucio.Status, out sr.Final, out sr.ErrMessage);

                        break;
                        #endregion
                }

                return Serialize<ServerResponse>(sr);
            }
            catch
            {
                return null;    // 非客户端连接：用调试助手连接服务器
            }
        }

        const string conStr = "data source=localhost; initial catalog=xinje; user id=root; pwd=980505;charset = utf8";

        #region Token、Permission

        static void getPerFromDB(UserInfoLogin o, out bool final, out string errMessage, out Lvl level)
        {
            final = false;
            level = Lvl.Error;
            errMessage = null;

            string qStu = "select * from user where account = ";
            qStu += o.Account + " and password = '" + o.Password + "'";

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
                    final = true;
                }
                else
                {
                    level = Lvl.NotFound;
                    final = false;
                }
            }
            catch (MySqlException e)
            {
                errMessage = e.Message;
                final = false;
                Debug.Print(DateTime.Now + " : " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        static int addToken(short job_id, Lvl level, ref Dictionary<short, QTInfo> myToken)
        {
            if (myToken != null)
            {
                if (myToken.Count > QT.tokenMaxCount)  // 清除缓存
                    myToken.Clear();
                if (myToken.ContainsKey(job_id))
                {
                    myToken.Remove(job_id);
                }
            }
            Random r = new Random();    // 伪随机
            int token = r.Next();

            QTInfo u = new QTInfo(token, level);

            myToken.Add(job_id, u);
            return token;
        }

        static void vrTokenFrT(short job_id, int token, out bool final, out string errMessage)
        {
            final = false;
            errMessage = null;
            if (QT.quickTable[job_id].Token != token)
            {
                errMessage = "Token Err";
                return;
            }
            final = true;
        }

        static Lvl getPerFrT(short job_id)
        {
            return QT.quickTable[job_id].Level;
        }

        static void vrPer(Lvl lv, out bool final, out string errMessage)
        {
            if (!(lv == Lvl.Teacher || lv == Lvl.Admin))
            {
                final = false;
                errMessage = "Permission denied";
            }
            final = true;
            errMessage = null;
        }

        #endregion

        #region GetInfo

        // Common
        #region CourseInfo
        const int nameColumn = 1;
        const int contentColumn = 3;
        static List<CourseInfo> getCosInfo(out bool final, out string errMessage)
        {
            final = false;
            errMessage = null;

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
                final = true;
                return listCI;
            }
            catch (MySqlException mySqlEx)
            {
                errMessage = mySqlEx.Message;
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
        /// 员工获取课程详情
        /// </summary>
        static void getUsrCosStatus(short jobId, out bool final, out string errMessage, out string status, out string details)
        {
            final = false;
            errMessage = null;
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
                final = true;
            }
            catch (MySqlException MySqlE)
            {
                errMessage = MySqlE.Message;
                Debug.Print(DateTime.Now + " : " + MySqlE.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        ///  教师获取课程详情
        /// </summary>
        static List<UserCourseInfo> getUsrCosStatus(string sqlStr, out bool final, out string errMessage)
        {
            List<UserCourseInfo> list = new List<UserCourseInfo>();
            final = false;
            errMessage = null;

            string qStu = "select * from usercouse_info where";
            qStu += sqlStr;
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand mCmd = new MySqlCommand(qStu, con);
                MySqlDataReader mReader = mCmd.ExecuteReader();
                UserCourseInfo u = new UserCourseInfo();
                while (mReader.Read())
                {
                    u.JobId = mReader.GetInt16("job_id");
                    u.Name = mReader.GetString("name");
                    u.Status = (mReader.IsDBNull(statusColumn)) ? " " : mReader.GetString("status");
                    u.Details = (mReader.IsDBNull(detailsColumn)) ? " " : mReader.GetString("details");
                    list.Add(u);
                }
                final = true;
                return list;
            }
            catch (MySqlException MySqlE)
            {
                errMessage = MySqlE.Message;
                Debug.Print(DateTime.Now + " : " + MySqlE.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #endregion

        #region Update

        #region Student

        /// <summary>
        ///  学员更改个人课程状态
        /// </summary>
        static void sUpUsrCosInfo(short job_id, string sqlStr, out bool final, out string eMessage)
        {
            final = false;
            eMessage = null;
            string str = "UPDATE usercouse_info " + " set status = '" + sqlStr + "' where job_id = " + job_id;
            MySqlConnection con = new MySqlConnection(conStr);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(str, con);
                if (cmd.ExecuteNonQuery() > 0)
                    final = true;
            }
            catch (MySqlException MySqlE)
            {
                eMessage = MySqlE.Message;
                Debug.Print(DateTime.Now + " : " + MySqlE.Message);
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region Teacher

        static void tUpUsrCosInfo(string sqlStr, out bool final, out string eMessage)
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
            catch (MySqlException MySqlE)
            {
                eMessage = MySqlE.Message;
                Debug.Print(DateTime.Now + " : " + MySqlE.Message);
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
