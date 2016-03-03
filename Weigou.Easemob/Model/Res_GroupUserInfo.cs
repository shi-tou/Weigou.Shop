using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 环信群成员
    /// </summary>
    public class Res_GroupUserInfo : Res_BaseInfo
    {
        public List<Res_EntitiesInfo> entities;
        public List<Dictionary<string, object>> data;
    }

    /// <summary>
    /// 添加环信群成员返回数据
    /// </summary>
    public class Res_GroupUserInfo_Add : Res_BaseInfo
    {
        public List<Res_EntitiesInfo> entities;
        public Res_GroupUserDataInfo_Add data;
    }
   
    public class Res_GroupUserDataInfo_Add
    {
        public string action;
        public bool result;
        public string groupid;
        public string user;
    }
    /// <summary>
    /// 添加环信群成员返回数据
    /// </summary>
    public class Res_GroupUserInfo_Add_Batch : Res_BaseInfo
    {
        public List<Res_EntitiesInfo> entities;
        public Res_GroupUserDataInfo_Add_Batch data;
    }
    public class Res_GroupUserDataInfo_Add_Batch
    {
        public string action;
        public string groupid;
        public List<string> newmembers;
    }
}