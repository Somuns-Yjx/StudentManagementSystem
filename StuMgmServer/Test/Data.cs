using System;
using System.Collections.Generic;
using System.Data;



namespace StuMgmClient
{

    enum ErrCode
    {
        Success,
        FailSerial,
        FailDeserial,
        FailConnect,
        FailSend,
        FailReceive,
        ErrData,
    }

    public enum FuncCode
    {
        VerifLogin = 1,
        GetCourseInfo,
        GetUserCourseInfo,
    }
    public enum Roles
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
        public FuncCode Func;
        public object Object;
        public ClientRequest(FuncCode func, object obj)
        {
            Func = func;
            Object = obj;
        }
    }

    [Serializable]
    public class UserInfo
    {
        public short Account;       // Changed
        public string Password;
        public Int16 Token;
        public Roles UserRole;
        public UserInfo(short account, string password) // Changed
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
        lll,
        ...//
    }

    [Serializable]
    public class UserCourseInfo
    {
        public short JobId;//工号
        public string Name;//姓名
        ..public DateTime Entry;
        public string InfoDescribe;//信息描述
    }




    /*


    #region 学生题目信息一览
   
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

    */







    //[Serializable]
    //public class ServerSend
    //{
    //    public short permission { get; set; }
    //    public DataSet ds { get; set; }
    //}


}


namespace StuMgmClient
{

    //class Data
    //{
    //    //状态值字符串转字典
    //    public Dictionary<int, int> StateParsing(DataTable table)
    //    {
    //        Dictionary<int, int> myDictionary = new Dictionary<int, int>();
    //        ClientMysql cm = new ClientMysql();
    //        DataSet ds = cm.SelectState();
    //        DataRow dr = ds.Tables["user_info"].Rows[0];
    //        string state = dr["course_status"].ToString();
    //        int num = 0;
    //        int oldTem = 0;
    //        //切割字符串
    //        string[] sArray = state.Split(new char[2] { ':', ';' });
    //        foreach (string i in sArray)
    //        {
    //            if (i.Equals("")) { break; }
    //            int tem = Convert.ToInt32(i);
    //            num++;
    //            if (num % 2 != 0)
    //            {
    //                myDictionary.Add(tem, 0);
    //                oldTem = tem;
    //            }
    //            else
    //            {
    //                myDictionary[oldTem] = tem;
    //            }
    //        }
    //        return myDictionary;
    //    }
    //    //字典转字符串
    //    public string DicParsing(Dictionary<int, int> dic) 
    //    {
    //        string stateText = "";
    //        foreach (var item in dic)
    //      {
    //          stateText = stateText + item.Key + ":" + item.Value + ";";
    //      }
    //        return stateText;
    //    }
    //    //查询所有子节点
    //    public void GetAllNodes(string id, DataTable table, ref Dictionary<int, int> nodesDic)
    //    {

    //        //把父节点的数据筛选出来
    //        DataRow[] rows = table.Select("pid =" + id);//取根  
    //        if (rows.Length <= 0)
    //        {
    //            nodesDic.Add(Convert.ToInt32(id), 0);
    //            return;
    //        }
    //        foreach (DataRow dr in rows)
    //        {
    //            GetAllNodes(dr["id"].ToString(), table, ref nodesDic);
    //        }
           
    //    }
    //    //获取所有子节点Dictionary
    //    public Dictionary<int, int> GetNodesDic(DataTable table) 
    //    {
    //        Dictionary<int, int> nodesDic = new Dictionary<int, int>();
    //        GetAllNodes("0", table, ref nodesDic);
    //        return nodesDic;
    //    }
    //}
      
}
