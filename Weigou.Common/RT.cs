using System;
using System.Collections.Generic;
using System.Text;

namespace Weigou.Common
{
    /// <summary>
    /// 状态码
    /// </summary>
    public class RT
    {
        /// <summary>
        /// 失败
        /// </summary>
        public static int FAILED = -1;
        /// <summary>
        /// 成功
        /// </summary>
        public static int SUCCESS = 0;
        /// <summary>
        /// 已存在/重复
        /// </summary>
        public static int RESULT_EXIST = 101;
        /// <summary>
        /// 不存在
        /// </summary>
        public static int RESULT_NOT_EXIST = 102;
        /// <summary>
        /// 错误密码
        /// </summary>
        public static int RESULT_ERROR_PASSWORD = 103;
        /// <summary>
        /// 锁定
        /// </summary>
        public static int RESULT_LOCK = 104;
        /// <summary>
        /// 邮箱已存在
        /// </summary>
        public static int RESULT_EMAIL_EXIST = 105;
        /// <summary>
        /// 名称已存在/重复
        /// </summary>
        public static int RESULT_NAME_EXIST = 106;
        /// <summary>
        /// 手机已存在/重复
        /// </summary>
        public static int RESULT_MOBILENO_EXIST = 107;
        /// <summary>
        /// 编码已存在/重复
        /// </summary>
        public static int RESULT_CODE_EXIST = 108;
        /// <summary>
        /// 金额不足
        /// </summary>
        public static int RESULT_ACCOUNT_NOT_ENOUGH = 109;
        /// <summary>
        /// 积分不足
        /// </summary>
        public static int RESULT_SCORE_NOT_ENOUGH = 110;
        /// <summary>
        /// 商品已报名
        /// </summary>
        public static int RESULT_SIGNED = 111;
        /// <summary>
        /// 商品报名受时间限制（展出时间结束30天后才可报名）
        /// </summary>
        public static int RESULT_LIMIT_DAY = 112;
        /// <summary>
        /// 验证码不正确或已过期
        /// </summary>
        public static int RESULT_ERROR_VERIFYCODE = 113;

        /// <summary>
        /// 用户未登录（未授权访问）
        /// </summary>
        public static int RESULT_NOT_AUTHORIZED = 10004;
        /// <summary>
        /// 参数错误
        /// </summary>
        public static int RESULT_ERROR_PARAMS = 10003;
        /// <summary>
        /// 非法方法名
        /// </summary>
        public static int RESULT_ERROR_METHOD = 10002;
        /// <summary>
        /// 接口异常
        /// </summary>
        public static int RESULT_API_ERROR = 10001;

        /// <summary>
        /// 商品库存不足
        /// </summary>
        public static int RESULT_STOCK_INSUFFICIENT = 114;
         
        /// <summary>
        /// 短信:发送成功 100
        /// </summary>
        public static int RESULT_SMS_OK = 100;
        /// <summary>
        /// 短信:短信不足
        /// </summary>
        public static int RESULT_SMS_NOTENOUGH = 102;
        /// <summary>
        /// 短信:非法字符
        /// </summary>
        public static int RESULT_SMS_ILLEGALCHARACTER = 104;
        /// <summary>
        /// 短信:内容过多
        /// </summary>
        public static int RESULT_SMS_CONTENTTOOLENGTH = 105;
    }
}
