﻿using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
//using System.Threading.Tasks;

namespace ConsoleApplication1
{

    #region 题目信息
    public class CourseInfo
    {
        public short Id;
        public short Pid;
        public int Time;
        public string Name;
        public string Content;
    }
    #endregion

    #region 学生题目信息一览
    [Serializable]
    public enum CourseStatusEnum
    {
        undo,                  // 未开始
        doing,                 // 进行中
        waiting,              // 等待验收
        failed,                 // 验收失败
        preSharing,       // 准备分享
        pass                     // 验收通过
    }
    [Serializable]
    public class CourseStatus
    {
        public short CourseId;
        public CourseStatusEnum Status;
    }
    [Serializable]
    public class UserInfo
    {
        public short JobId;
        public string Name;
        public List<CourseStatus> CourseStatus;
    }
    #endregion

    #region 学生单题详细信息
    public class HistoryInfo
    {
        DateTime Time;
        string Describe;
    }
    public class DetailInfo
    {
        public short CourseId;
        public short JobId;
        List<HistoryInfo> Describes;
    }
    #endregion



    class Server
    {
        //List<CourseInfo> GetCourseInfo();
        //UserInfo GetUserInfo(short jobId);
        //DetailInfo GetDetailInfo(short jobId, short courseId);

        public byte[] GetUser(short jobId)
        {
            UserInfo info = new UserInfo();
            info.JobId = 111;
            info.Name = "aaaa";
            info.CourseStatus = new List<CourseStatus>();
            CourseStatus aa = new CourseStatus();
            aa.CourseId = 222;
            aa.Status = CourseStatusEnum.undo;      // 做题状态
            info.CourseStatus.Add(aa);



            MemoryStream ms = new MemoryStream();
            BinaryFormatter iFormatter = new BinaryFormatter();
            iFormatter.Serialize(ms, info);
            byte[] buff = ms.GetBuffer();
            return buff;
        }

        public UserInfo Prase(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(bt);
            BinaryFormatter iFormatter = new BinaryFormatter();
            UserInfo obj = (UserInfo)iFormatter.Deserialize(ms);
            return obj;
        }



    }


    class StudentInfo
    {
        //List<CourseInfo> GetCourseInfo();
        //UserInfo GetUserInfo(short jobId);
        //DetailInfo GetDetailInfo(short jobId, short courseId);
    }


    class Program
    {
        //static void Main(string[] args)
        //{
        //    Server s = new Server();
        //    byte[] buf = s.GetUser(0);
        //    UserInfo aa = s.Prase(buf);
        //}
    }
}
