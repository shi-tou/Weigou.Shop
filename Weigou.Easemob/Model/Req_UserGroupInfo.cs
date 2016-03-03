using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 请求的用户信息
    /// </summary>
    public class Req_UserGroupInfo:Res_BaseInfo
    {
        public List<Res_EntitiesInfo> entities;
        public List<Req_UserGroupData> data;
    }

    public class Req_UserGroupData
    {
        public string groupid;
        public string groupname;
    }
}