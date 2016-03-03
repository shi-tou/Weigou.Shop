using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
    /// <summary>
    ///  基本配置类
    /// </summary>
    public class WeixinConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static WeixinConfigInfo GetConfig()
        {
            return WeixinConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(WeixinConfigInfo baseconfiginfo)
        {
            WeixinConfigFileManager ecfm = new WeixinConfigFileManager();
            WeixinConfigFileManager.ConfigInfo = baseconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
