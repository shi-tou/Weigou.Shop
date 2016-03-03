using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using System;
using System.Collections.Generic;
using System.Text;
using Weigou.Common;

namespace Weigou.Common
{

    /// <summary>
    /// 极光推送
    /// </summary>
    public class JPushHelper
    {
        public static string app_key = Utils.GetConfig("AppKey");
        public static string master_secret = Utils.GetConfig("MasterSecret");        
        /// <summary>
        /// 极光推送
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static MessageResult Push(PushPayload payload)
        {
            try
            {
                JPushClient client = new JPushClient(app_key, master_secret);
                return client.SendPush(payload);
            }
            catch (Exception ex)
            {
                Utils.SaveLog("JPush Message", ex.Message);
                return null;
            }
        }


        #region -------所有设备------------
        /// <summary>
        /// 获取推送载体：所有设备平台/所有目标/alert通知内容
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_All_Alert(string alter, string title)
        {
            return PhshObject(Platform.all(), Audience.all(), alter, title);
        }
        /// <summary>
        /// 获取推送载体：所有设备平台/指定别名【alias】的目标对象/alert通知内容
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="alias">别名（多个别名请用","分隔）</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_Alias_Alert(string alter, string title, string alias)
        {
            return PhshObject(Platform.android(), Audience.s_alias(StringToArray(alias)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：所有设备平台/指定标签【tags】的目标对象/alert通知内容
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">标签（多个标签请用","分隔）</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_Tags_Alert(string alter, string title, string tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/指定【tags】及附加【and_tags】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_AndTags_Alert(string alter, string title, string tags, string and_tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)).tag(StringToArray(and_tags)), alter, title);
        }
        #endregion

        #region -------Android设备---------
        /// <summary>
        /// 获取推送载体：Android设备平台/所有目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_All_Alert(string alter, string title)
        {
            return PhshObject(Platform.android(), Audience.all(), alter, title);
        }
        /// <summary>
        /// 获取推送载体：Android设备平台/指定【alias】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_Alias_Alert(string alter, string title, string alias)
        {
            return PhshObject(Platform.android(), Audience.s_alias(StringToArray(alias)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：Android设备平台/指定【tags】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_Tags_Alert(string alter, string title, string tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：Android设备平台/指定【tags】及附加【and_tags】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_Alias_Alert(string alter, string title, string tags, string and_tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)).tag(StringToArray(and_tags)), alter, title);
        }
        #endregion

        #region -------IOS设备-------------
        /// <summary>
        /// 获取推送载体：IOS设备平台/所有目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <returns></returns>
        public static PushPayload PushObject_IOS_All_Alert(string alter, string title)
        {
            return PhshObject(Platform.android(), Audience.all(), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/指定【alias】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_IOS_Alias_Alert(string alter, string title, string alias)
        {
            return PhshObject(Platform.android(), Audience.s_alias(StringToArray(alias)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/指定【tags】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_IOS_Tags_Alert(string alter, string title, string tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/指定【tags】及附加【and_tags】目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">多个别名请用","分隔</param>
        /// <returns></returns>
        public static PushPayload PushObject_IOS_AndTags_Alert(string alter, string title, string tags, string and_tags)
        {
            return PhshObject(Platform.android(), Audience.s_tag(StringToArray(tags)).tag(StringToArray(and_tags)), alter, title);
        }
        #endregion

        #region -------Android_IOS设备-----
        /// <summary>
        /// 获取推送载体：IOS设备平台/所有目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_IOS_All_Alert(string alter, string title)
        {
            return PhshObject(Platform.android_ios(), Audience.all(), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/所有目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="alias">别名（多个别名请用","分隔）</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_IOS_Alias_Alert(string alter, string title, string alias)
        {
            return PhshObject(Platform.android_ios(), Audience.s_alias(StringToArray(alias)), alter, title);
        }
        /// <summary>
        /// 获取推送载体：IOS设备平台/所有目标对象/alter通知内容/
        /// </summary>
        /// <param name="alter">通知内容</param>
        /// <param name="title">标题（为空则不设置）</param>
        /// <param name="tags">标签（多个标签请用","分隔）</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_IOS_Tags_Alert(string alter, string title, string alias)
        {
            return PhshObject(Platform.android_ios(), Audience.s_alias(StringToArray(alias)), alter, title);
        }
        #endregion

        /// <summary>
        /// 构造推送载体信息
        /// </summary>
        /// <param name="platform">设备平台</param>
        /// <param name="audience">推送目标</param>
        /// <param name="alert">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <returns></returns>
        public static PushPayload PhshObject(Platform platform, Audience audience, string alter, string title)
        {
            PushPayload pushPayload = new PushPayload();
            //设备平台
            pushPayload.platform = platform;
            //推送目标
            pushPayload.audience = audience;
            //通知内容
            Notification notification = new Notification().setAlert(alter);
            //标题
            if (!string.IsNullOrEmpty(title))
            {
                if (platform == Platform.all() || platform == Platform.android_ios())
                {
                    notification.AndroidNotification = new AndroidNotification().setTitle(title);
                    notification.IosNotification = new IosNotification().incrBadge(1);
                }
                else if (platform == Platform.android())
                {
                    notification.AndroidNotification = new AndroidNotification().setTitle(title);
                }
                else if (platform == Platform.ios())
                {
                    notification.IosNotification = new IosNotification().incrBadge(1);
                }
            }
            pushPayload.notification = notification.Check();

            return pushPayload;
        }
        /// <summary>
        /// 将带","分隔的字条转换为string[]类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] StringToArray(string str)
        {
            List<string> list = new List<string>();
            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list.ToArray();
        }
    }
}
