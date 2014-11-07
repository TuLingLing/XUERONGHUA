using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ed.Web.Helpers
{
    public class MyControllerHelper
    {
        #region 根据角色id筛选对应的模块权限数据 14-10-23 By 唐有炜
        /// <summary>
        /// 根据角色id筛选对应的模块权限数据
        /// </summary>
        /// <param name="tempMoudlsPowers"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static  List<Dictionary<string, object>> FilterModulePowers(IEnumerable<Dictionary<string, object>> tempMoudlsPowers, int roleId)
        {
            List<Dictionary<string, object>> mps = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> moudlsPower in tempMoudlsPowers)
            {
                object rid = 0;
                moudlsPower.TryGetValue("RoleId", out rid);
                if ((int)rid == roleId)
                {
                    mps.Add(moudlsPower);
                }
            }
            return mps;
        }
        #endregion
    }
}