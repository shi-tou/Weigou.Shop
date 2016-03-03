using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 请求的用户信息
    /// </summary>
    public class Req_GroupsInfo
    {
        public string groupid;
        /// <summary>
        /// 群组名称, 此属性为必须的
        /// </summary>
        public string groupname;
        /// <summary>
        /// 群组描述, 此属性为必须的
        /// </summary>
        public string desc;
        /// <summary>
        /// 是否是公开群, 此属性为必须的,为false时为私有群
        /// </summary>
        public bool Public;
        /// <summary>
        /// 群组成员最大数(包括群主), 值为数值类型,默认值200,此属性为可选的
        /// </summary>
        public int maxusers;
        /// <summary>
        /// 加入公开群是否需要批准, 默认值是false（加群不需要群主批准）, 此属性为可选的,只作用于公开群
        /// </summary>
        public bool approval;
        /// <summary>
        /// //群组的管理员, 此属性为必须的
        /// </summary>
        public string owner;
        /// <summary>
        /// //群组成员,此属性为可选的,但是如果加了此项,数组元素至少一个（注：群主jma1不需要写入到members里面）,["jma2","jma3"] 
        /// </summary>
        //public List<string> members;
    }
}