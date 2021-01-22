using System;
using System.Collections.Generic;
using System.Text;

namespace StuMgmClient
{
    class SystemData
    {
        static Dictionary<short, CourseInfo> allCourseInfo;
        static Dictionary<short,CourseStatusEnum> allCourseStatus;


        internal static ErrCode InitSystemData()
        {
            ErrCode err;
            err = InitCourseInfo();
            if (err != ErrCode.Success)
                return err;
            return ErrCode.Success;
        }
        static ErrCode InitCourseInfo()
        {
            ErrCode err;
            List<CourseInfo> courseInfo;
            err = SystemCtrl.GetCourseInfo(out courseInfo);
            if (err != ErrCode.Success)
                return err;
            ...//将List<CourseInfo>转为Dictionary<short, CourseInfo>
            return ErrCode.Success;
        }

        internal static ErrCode RefreshUserCourseInfo(UserCourseInfo info)
        {
            ...//将UserCourseInfo转为Dictionary<short,CourseStatusEnum>
            return ErrCode.Success;
        }


    }
}
