using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 环信用户
    /// </summary>
    public class Res_UserInfo : Res_BaseInfo
    {
        public List<Res_EntitiesInfo> entities;
        public List<string> data;
    }
}