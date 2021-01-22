using System;
using System.Collections.Generic;
using System.Text;

namespace StuMgmClient
{
    class SystemCtrl
    {
        internal static ErrCode VerifLogin(string userName, string pawssword, out Roles role, out string token)
        {
            role = Roles.Error;
            token = string.Empty;

            UserInfo cs = new UserInfo(userName, pawssword);
            ClientRequest req = new ClientRequest(FuncCode.VerifLogin, cs);

            object o;
            ErrCode err = SystemComm.GetData(req, out o);
            if (err != ErrCode.Success)
                return err;

            if (!(o is UserInfo))
                return ErrCode.ErrData;

            role = ((UserInfo)o).UserRole;
            token = ((UserInfo)o).Token;
            return ErrCode.Success;
        }

        internal static ErrCode GetCourseInfo(out List<CourseInfo> courseInfo)
        {
            courseInfo = null;
            //******
            ClientRequest req = new ClientRequest(FuncCode.GetCourseInfo, null);

            object o;
            ErrCode err = SystemComm.GetData(req, out o);
            if (err != ErrCode.Success)
                return err;

            if (!(o is List<CourseInfo>))
                return ErrCode.ErrData;

            courseInfo = (List<CourseInfo>)o;
            return ErrCode.Success;
        }
        
        internal static ErrCode RefreshUserCourseInfo(string token)
        {
            UserCourseInfo info;
            ErrCode err;

            err = GetUserCourseInfo(token, out info);
            if (err != ErrCode.Success)
                return err;

            err = SystemData.RefreshUserCourseInfo(info);
            if (err != ErrCode.Success)
                return err;

            return ErrCode.Success;
        }
        
        static ErrCode GetUserCourseInfo(string token ,out UserCourseInfo info)
        {
            info = null;
            ClientRequest req = new ClientRequest(FuncCode.GetUserCourseInfo, token);

            object o;
            ErrCode err = SystemComm.GetData(req, out o);
            if (err != ErrCode.Success)
                return err;

            if (!(o is UserCourseInfo))
                return ErrCode.ErrData;

            info = (UserCourseInfo)o;
            return ErrCode.Success;
        }







    }
}
