using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{
    public class UserInfo
    {
        public UserInfo() { }

        private int _id;
        private string _username;
        private int _merchatnID;
        private string _password;
        private string _name;
        private bool _disabled;
        private int _createby;
        private DateTime _createtime;
        private DateTime _lastLoginTime;

        /// <summary>
        ///  ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        public int MerchantID
        {
            set { _merchatnID = value; }
            get { return _merchatnID; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled
        {
            set { _disabled = value; }
            get { return _disabled; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy
        {
            set { _createby = value; }
            get { return _createby; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastLoginTime = value; }
            get { return _lastLoginTime; }
        }
    }
}