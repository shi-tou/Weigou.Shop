using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
    /// <summary>
    ///  基本配置类
    /// </summary>
    public class AlipayConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static AlipayConfigInfo GetConfig()
        {
            return AlipayConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(AlipayConfigInfo baseconfiginfo)
        {
            AlipayConfigFileManager ecfm = new AlipayConfigFileManager();
            AlipayConfigFileManager.ConfigInfo = baseconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
