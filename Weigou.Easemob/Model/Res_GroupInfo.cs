using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 环信群组
    /// </summary>
    public class Res_GroupInfo : Res_BaseInfo
    {
        public List<Res_GroupDataInfo> data;
        public List<Res_EntitiesInfo> entities;
    }
    /// <summary>
    /// 环信群组-添加
    /// </summary>
    public class Res_GroupInfo_Add : Res_BaseInfo
    {
        public Res_GroupDataInfo data;
        public List<Res_EntitiesInfo> entities;
    }
    /// <summary>
    /// 环信群组-删除
    /// </summary>
    public class Res_GroupInfo_Delete : Res_BaseInfo
    {
        public Res_GroupDataInfo_Delete data;
        public List<Res_EntitiesInfo> entities;
    }

    /// <summary>
    /// 环信群组-添加
    /// </summary>
    public class Res_GroupInfo_Update : Res_BaseInfo
    {
        public Res_GroupDataInfo_Update data;
        public List<Res_EntitiesInfo> entities;
    }
    /// <summary>
    /// 环信群组
    /// </summary>
    public class Res_GroupDataInfo
    {
        /// <summary>
        /// 群组ID, 群组唯一标识符 ， 由环信服务器生成 。
        /// </summary>
        public string id;
        /// <summary>
        /// 群组ID, 和 id 一样，群组唯一标识符， 由环信服务器生成 。
        /// </summary>
        public string groupid;
        /// <summary>
        /// 群组名称 ， 任意字符串
        /// </summary>
        public string name;
        /// <summary>
        /// 群组名称 ， 任意字符串
        /// </summary>
        public string groupname;
        /// <summary>
        /// 群组名称 ， 任意字符串
        /// </summary>
        public string description;
        /// <summary>
        /// 群组类型： true 公开群，false 私有群
        /// </summary>
        public bool Public;
        /// <summary>
        /// 是否只有群成员可以进来发言，true 是 ， false 否
        /// </summary>
        public bool membersonly;
        /// <summary>
        /// 是否允许群成员邀请别人加入此群。 true 允许群成员邀请人加入此群， false 只有群主才可以往群里加人
        /// </summary>
        public bool allowinvites;
        /// <summary>
        /// 群成员上限，创建群组的时候设置，可修改
        /// </summary>
        public int maxusers;
        /// <summary>
        /// 现有成员总数
        /// </summary>
        public int affiliations_count;
        public List<Dictionary<string,object>> affiliations ;
        /// <summary>
        /// 群主的username， 例如：{“owner”: “13800138001”}
        /// </summary>
        public string owner;
        /// <summary>
        /// 群成员的username ， 例如： {“member”:”xc6xrnbzci”}
        /// </summary>
        public List<string> member;
    }

    /// <summary>
    /// 环信群组
    /// </summary>
    public class Res_GroupDataInfo_Delete
    {
        public bool success;
        public string groupid;
    }

    /// <summary>
    /// 环信群组
    /// </summary>
    public class Res_GroupDataInfo_Update
    {
        public string maxusers;
        public string groupname;
        public string description;
    }
}