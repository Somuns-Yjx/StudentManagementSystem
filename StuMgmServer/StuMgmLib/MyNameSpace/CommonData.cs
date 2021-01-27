/* Describtion : Class for Data Send From Client / Server
 * Company : Wuxi Xinje
 * Author : Somuns
 * DateTime : 2021/1/18 
 */
using System;
using System.Collections.Generic;
using System.Data;

namespace StuMgmLib.MyNameSpace
{
    public class Info
    {
        internal const Int16 tokenMaxCount = 32767;
        internal static Dictionary<short, int> myToken = new Dictionary<short, int>();
    }

    #region ClientClass

    public enum ClientFunc
    {
        VerifyLogin = 1,
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
        public short Account;       
        public string Password;
        public Int16 Token;           
        public LvErr UserLev;
        public UserInfo(short account, string password) // Changed
        {
            Account = account;
            Password = password;
        }
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
        public string Status;
        //        ..public DateTime Entry;
        public string Details;//信息描述
    }
    #endregion

    #region ServerClass
    [Serializable]
    public class ServerResponse
    {
        public LvErr Lev;
        public int Token;
        //public string CourseStatus;
        public object Object;
        //public ServerResponse(object obj)
        //{
        //    Object = obj;
        //}
    }
    #endregion



}



