/* Describtion : Class for Data Send From Client / Server
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/18 
 */
using System;
using System.Collections.Generic;

namespace StuMgmLib.MyNameSpace
{
    #region QuickTable

    internal class QTInfo
    {
        public int Token;
        public Lvl Level;
        public QTInfo(int token, Lvl lv)
        {
            Token = token;
            Level = lv;
        }
    }
    public class QT // quickTable
    {
        internal const Int16 tokenMaxCount = 32767;
        internal static Dictionary<short, QTInfo> quickTable = new Dictionary<short, QTInfo>();
    }

    #endregion

    #region ClientClass

    public enum ClientFunc
    {
        VerifyLogin = 1,
        GetCourseInfo = 2,
        GetSelfUserCourseInfo = 3,
        GetSomeoneUserCInfo = 6,
        SUpdateCourse = 4,
        TUpdateCourse = 5,

    }
    public enum Lvl
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
    public class UserInfoLogin
    {
        public short Account;
        public string Password;
        public UserInfoLogin(short account, string password) // Changed
        {
            Account = account;
            Password = password;
        }
    }

    [Serializable]
    public class UserCourseInfoReq
    {
        public short Job_Id;
        public int Token;
    }

    [Serializable]
    public class UserCourseInfoOper 
    {
        public short Job_Id;
        public int Token;
        public string Status;
    }


    #endregion

    #region ServerClass
    [Serializable]
    public class ServerResponse
    {
        public bool Final;
        public string ErrMessage;
        public object Object;
        public ServerResponse(object obj)
        {
            Object = obj;
        }

    }

    [Serializable]
    public class LoginResponse
    {
        public int Token;
        public Lvl Level;
    }

    [Serializable]
    public class CourseInfo
    {
        public short Id;
        public short Pid;
        public int Time;
        public string Name;
        public string Content;
    }

    [Serializable]
    public class UserCourseInfo
    {
        public short JobId;//工号
        public string Name;//姓名
        public string Status;
        //        ..public DateTime Entry;
        public string Details;//信息描述
    }


    #endregion

}



