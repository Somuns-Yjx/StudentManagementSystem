using System;
using System.Collections.Generic;
using System.Text;

namespace StuMgmLib.MyNameSpace
{
    class SystemData
    {
        static Dictionary<short, CourseInfo> allCourseInfo;
        static Dictionary<short,CourseStatusEnum> allCourseStatus;


        internal static CommErr InitSystemData()
        {
            CommErr err;
            err = InitCourseInfo();
            if (err != CommErr.Success)
                return err;
            return CommErr.Success;
        }
        static CommErr InitCourseInfo()
        {
            CommErr err;
            List<CourseInfo> courseInfo;
            err = SystemCtrl.GetCourseInfo(out courseInfo);
            if (err != CommErr.Success)
                return err;
            ...//将List<CourseInfo>转为Dictionary<short, CourseInfo>
            return CommErr.Success;
        }

        internal static CommErr RefreshUserCourseInfo(UserCourseInfo info)
        {
            ...//将UserCourseInfo转为Dictionary<short,CourseStatusEnum>
            return CommErr.Success;
        }


    }
}
