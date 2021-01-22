using System;
using System.Collections.Generic;
using System.Data;

namespace StuMgmLib.MyNameSpace
{

    enum CommErr
    {
        Success,
        FailSerial,
        FailDeserial,
        FailConnect,
        FailSend,
        FailReceive,
        ErrData,
    }

    public enum ClientFunc
    {
        VerifLogin = 1,
        GetCourseInfo = 2,
        GetUserCourseInfo = 3,
    }
    public enum LvErr
    {
        NotFound = -1,
        Error = -2,
        Admin = 1,
        Teacher = 2,
        Student = 3,
    }

    [Serializable]
    public class ClientRequest
    {
        public ClientFunc Func;
        public object Object;
        public ClientRequest(ClientFunc func, object obj)
        {
            Func = func;
            Object = obj;
        }
    }

    [Serializable]
    public class UserInfo
    {
        public string Account;
        public string Password;
        public string Token;
        public LvErr UserRole;
        public UserInfo(string account, string password)
        {
            Account = account;
            Password = password;
        }
    }

    public class CourseInfo
    {
        public short Id;
        public short Pid;
        public int Time;
        public string Name;
        public string Content;
    }


    [Serializable]
    public enum CourseStatusEnum
    {
        //lll,
        //...//
    }

    [Serializable]
    public class UserCourseInfo
    {
        public short JobId;//工号
        public string Name;//姓名
//        ..public DateTime Entry;
        public string InfoDescribe;//信息描述
    }
}