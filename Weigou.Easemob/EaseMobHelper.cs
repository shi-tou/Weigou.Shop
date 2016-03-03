using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using Weigou.Easemob;
using Weigou.Common;

namespace Weigou.Easemob
{
    public class EaseMobHelper
    {
        public static string reqUrlFormat = "https://a1.easemob.com/{0}/{1}/";
        public static string clientID = "YXA6LN3gwK6qEeWNs7lrynoYDg";
        public static string clientSecret = "YXA68M1ChIbjww63din8yUaaI-bzP8g";
        public static string appName = "wlchat";
        public static string orgName = "wlchat";
        public static string easeMobUrl { get { return string.Format(reqUrlFormat, orgName, appName); } }
        public string token { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientID">client_id</param>
        /// <param name="clientSecret">client_secret</param>
        /// <param name="appName">应用标识之应用名称</param>
        /// <param name="orgName">应用标识之登录账号</param>
        public EaseMobHelper()
        {
            this.token = QueryToken();
        }
        /// <summary>
        /// 使用app的client_id 和 client_secret登陆并获取授权token
        /// </summary>
        /// <returns></returns>
        string QueryToken()
        {
            if (string.IsNullOrEmpty(clientID) || string.IsNullOrEmpty(clientSecret))
            {
                return string.Empty;
            }
            string cacheKey = clientID + clientSecret;
            if (System.Web.HttpRuntime.Cache.Get(cacheKey) != null &&
                System.Web.HttpRuntime.Cache.Get(cacheKey).ToString().Length > 0)
            {
                return System.Web.HttpRuntime.Cache.Get(cacheKey).ToString();
            }

            string postUrl = easeMobUrl + "token";
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"grant_type\": \"client_credentials\",\"client_id\": \"{0}\",\"client_secret\": \"{1}\"", clientID, clientSecret);
            _build.Append("}");

            string resulst = HttpUtils.ReqUrl(postUrl, "POST", _build.ToString(), string.Empty);
            string token = string.Empty;
            int expireSeconds = 0;
            try
            {
                Hashtable hs = JsonConvert.DeserializeObject<Hashtable>(resulst);
                token = Convert.ToString(hs["access_token"]);
                int.TryParse(Convert.ToString(hs["expires_in"]), out expireSeconds);
                //设置缓存
                if (!string.IsNullOrEmpty(token) && token.Length > 0 && expireSeconds > 0)
                {
                    System.Web.HttpRuntime.Cache.Insert(cacheKey, token, null, DateTime.Now.AddSeconds(expireSeconds), System.TimeSpan.Zero);
                }
            }
            catch { return resulst; }
            return token;
        }

        #region ====用户相关====
        /// <summary>
        /// 注册单个环信用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo AddUser(Req_UserInfo info)
        {
            try
            {
                string postUrl = easeMobUrl + "users";
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", JsonConvert.SerializeObject(info), this.token);
                //Utils.SaveLog("注册环信用户-AddUser()", resulst);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch (Exception ex)
            {
                Utils.SaveLog("注册环信用户-AddUser()", ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取单个环信用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo GetUser(string username)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username;
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                //Utils.SaveLog("获取单个环信用户-GetUser()", resulst);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch (Exception ex) { Utils.SaveLog("获取单个环信用户-GetUser()", ex.Message.ToString()); return null; }
        }
        /// <summary>
        /// 删除单个环信用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo DeleteUser(string username)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username;
                string resulst = HttpUtils.ReqUrl(postUrl, "DELETE", "", this.token);
                //Utils.SaveLog("删除单个环信用户-DeleteUser()", resulst);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch (Exception ex) { Utils.SaveLog("删除单个环信用户-DeleteUser()", ex.Message.ToString()); return null; }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo UpdateUserPwd(string username, string newPwd)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/password" ;
                string resulst = HttpUtils.ReqUrl(postUrl, "PUT", "{\"newpassword\" : \"" + newPwd + "\"}", this.token);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo UpdateUserNickname(string username, string nickname)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username;
                string resulst = HttpUtils.ReqUrl(postUrl, "PUT", "{\"nickname\" : \"" + nickname + "\"}", this.token);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加用户好友
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friendname"></param>
        /// <returns></returns>
        public Res_UserInfo AddUserFriend(string username, string friendname)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/contacts/users/" + friendname;
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", "", this.token);
                //Utils.SaveLog("添加用户好友-AddUserFriend()", resulst);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch (Exception ex) { Utils.SaveLog("添加用户好友-AddUserFriend()", ex.Message.ToString()); return null; }
        }
        /// <summary>
        /// 删除用户好友
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo DeleteUserFriend(string username, string friendname)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/contacts/users/" + friendname;
                string resulst = HttpUtils.ReqUrl(postUrl, "DELETE", "", this.token);
                //Utils.SaveLog("删除用户好友-DeleteUserFriend()", resulst);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch (Exception ex) { Utils.SaveLog("删除用户好友-DeleteUserFriend()", ex.Message.ToString()); return null; }
        }
        /// <summary>
        /// 获取用户好友
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Res_UserInfo GetUserFriends(string username)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/contacts/users/";
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取用户黑名单
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Res_UserInfo GetUserBlocks(string username)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/blocks/users/";
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 加入用户黑名单
        /// </summary>
        /// <param name="username"></param>
        /// <param name="usernames"></param>
        /// <returns></returns>
        public Res_UserInfo AddUserBlocks(string username, List<string> usernames)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/blocks/users/";
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", JsonConvert.SerializeObject(usernames), this.token);
                Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        #endregion

        #region ====群组相关====
        /// <summary>
        /// 获取app中所有的群组
        /// </summary>
        /// <returns></returns>
        public Res_GroupInfo GetChatGroups()
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups";
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                Res_GroupInfo resInfo = JsonConvert.DeserializeObject<Res_GroupInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取app中所有的群组
        /// </summary>
        /// <returns></returns>
        public Res_GroupInfo GetChatGroups(string[] group_id)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + string.Join(",", group_id);
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                resulst = resulst.Replace("public", "Public");
                Res_GroupInfo resInfo = JsonConvert.DeserializeObject<Res_GroupInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加群组
        /// </summary>
        /// <returns></returns>
        public Res_GroupInfo_Add AddChatGroups(Req_GroupsInfo info)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups";
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", JsonConvert.SerializeObject(info).Replace("Public", "public"), this.token);
                Utils.SaveLog("添加群组-AddChatGroups()", resulst);
                Res_GroupInfo_Add resInfo = JsonConvert.DeserializeObject<Res_GroupInfo_Add>(resulst);
                return resInfo;
            }
            catch( Exception ex) { Utils.SaveLog("添加群组-AddChatGroups()", ex.Message.ToString()); return null; }
        }
        /// <summary>
        /// 修改群组
        /// </summary>
        /// <returns></returns>
        public Res_GroupInfo_Update UpdateChatGroups(string groupid, Req_GroupsInfo info)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid;
                string resulst = HttpUtils.ReqUrl(postUrl, "PUT", JsonConvert.SerializeObject(info).Replace("desc", "description"), this.token);
                Res_GroupInfo_Update resInfo = JsonConvert.DeserializeObject<Res_GroupInfo_Update>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 删除群组
        /// </summary>
        /// <returns></returns>
        public Res_GroupInfo_Delete DeleteChatGroups(string groupid)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid;
                string resulst = HttpUtils.ReqUrl(postUrl, "DELETE", "", this.token);
                Res_GroupInfo_Delete resInfo = JsonConvert.DeserializeObject<Res_GroupInfo_Delete>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取群组成员信息
        /// </summary>
        /// <returns></returns>
        public Res_GroupUserInfo GetGroupsUser(string groupid)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid + "/users";
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                Res_GroupUserInfo resInfo = JsonConvert.DeserializeObject<Res_GroupUserInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加单个群组成员
        /// </summary>
        /// <returns></returns>
        public Res_GroupUserInfo_Add AddGroupsUser(string groupid, string username)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid + "/users/" + username;
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", "", this.token);
                Res_GroupUserInfo_Add resInfo = JsonConvert.DeserializeObject<Res_GroupUserInfo_Add>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 批量添加群组成员
        /// </summary>
        /// <returns></returns>
        public Res_GroupUserInfo_Add_Batch AddGroupsUsers(string groupid, List<string> username)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid + "/users/";
                string resulst = HttpUtils.ReqUrl(postUrl, "POST", "{\"usernames\":" + JsonConvert.SerializeObject(username) + "}", this.token);
                Res_GroupUserInfo_Add_Batch resInfo = JsonConvert.DeserializeObject<Res_GroupUserInfo_Add_Batch>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 删除单个群组成员
        /// </summary>
        /// <returns></returns>
        public Res_GroupUserInfo_Add DeleteGroupsUser(string groupid, string username)
        {
            try
            {
                string postUrl = easeMobUrl + "chatgroups/" + groupid + "/users/" + username;
                string resulst = HttpUtils.ReqUrl(postUrl, "DELETE", "", this.token);
                Res_GroupUserInfo_Add resInfo = JsonConvert.DeserializeObject<Res_GroupUserInfo_Add>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取成员参与的群组
        /// </summary>
        /// <returns></returns>
        public Req_UserGroupInfo GetGroupsByUser(string username)
        {
            try
            {
                string postUrl = easeMobUrl + "users/" + username + "/joined_chatgroups";
                string resulst = HttpUtils.ReqUrl(postUrl, "GET", "", this.token);
                Req_UserGroupInfo resInfo = JsonConvert.DeserializeObject<Req_UserGroupInfo>(resulst);
                return resInfo;
            }
            catch { return null; }
        }
        #endregion

        #region ====聊天记录====
        /// <summary>
        /// 环信用户聊天记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Res_UserInfo GetUserMessage()
        {
            try
            {
                return null;
            }
            //    string postUrl = easeMobUrl + "users";
            //    string resulst = HttpHelper.ReqUrl(postUrl, "POST", JsonConvert.SerializeObject(info), this.token);
            //    Utils.SaveLog("注册环信用户-AddUser()", resulst);
            //    Res_UserInfo resInfo = JsonConvert.DeserializeObject<Res_UserInfo>(resulst);
            //    return resInfo;
            //}
            catch (Exception ex)
            {
                Utils.SaveLog("注册环信用户-AddUser()", ex.Message.ToString());
                return null;
            }
        }
        #endregion
    }
}