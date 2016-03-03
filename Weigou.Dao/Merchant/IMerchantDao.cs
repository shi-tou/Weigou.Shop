﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Weigou.Common;

namespace Weigou.Dao
{
    public interface IMerchantDao
    {
        /// <summary>
        /// 商户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMerchantList(Pager p, Hashtable hs);
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        int SetPictureTop(int ID, string merchantID, int Type);
        /// <summary>
        ///  图片列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable GetPictureList(string code, int type);
        /// <summary>
        /// 回访列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMerchantVisitList(Pager p, Hashtable hs);
        /// <summary>
        /// 商户入驻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMerchantSettledList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取商户运费模版
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMerchantLogisticsList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取运费相关省份
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetProvince(Hashtable hs);
        /// <summary>
        /// 获取商户经营类目集合
        /// </summary>
        /// <returns></returns>
        DataTable GetMerchantGoodsTypeList(string MerchantID);
        /// <summary>
        /// 获取专场封面图片
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetSpecialPicture(Hashtable hs);
        /// <summary>
        /// 获取专场的店铺列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetSpecialMerchantList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取店铺设计列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetShopSignList(Hashtable hs);
        /// <summary>
        /// 查询公益基金和娱乐积分的返送比例
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        DataTable GetScale(string MerchantID);
        /// <summary>
        /// 检查该店铺是否为丝阁国际
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        int CheckIsInternational(string MerchantID);
        /// <summary>
        /// 检查该店铺是否支持试衣间功能
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        DataTable CheckMerchantSupportFitting(string MerchantID);
        /// <summary>
        /// 获取店铺评分
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        decimal GetMerchantStar(string MerchantID);
    }
}
