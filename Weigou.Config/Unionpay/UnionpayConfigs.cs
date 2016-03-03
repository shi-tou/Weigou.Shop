using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
    /// <summary>
    ///  银联支付基本配置类
    /// </summary>
    public class UnionpayConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static UnionpayConfigInfo GetConfig()
        {
            return UnionpayConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(UnionpayConfigInfo baseconfiginfo)
        {
            UnionpayConfigFileManager ecfm = new UnionpayConfigFileManager();
            UnionpayConfigFileManager.ConfigInfo = baseconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
