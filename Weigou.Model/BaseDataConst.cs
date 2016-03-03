using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public static class BaseDataConst
    {
        /// <summary>
        ///变速箱T_BaseData->ParentID=1
        /// </summary>
        public static int Car_Transmission = 1;
        /// <summary>
        /// 座位数T_BaseData->ParentID=4
        /// </summary>
        public static int Car_Seats = 4;
        /// <summary>
        /// 排量T_BaseData->ParentID=11
        /// </summary>
        public static int Car_Cap = 11;
        /// <summary>
        /// 车级别T_BaseData->ParentID=16
        /// </summary>
        public static int Car_Level = 16;
        /// <summary>
        /// 教育程度T_BaseData->ParentID=24
        /// </summary>
        public static int Education = 24;
        /// <summary>
        /// 职位T_BaseData->ParentID=32
        /// </summary>
        public static int Occupation = 32;
        /// <summary>
        /// 行驶里程T_BaseData->ParentID=47
        /// </summary>
        public static int Mileage = 47;
        /// <summary>
        /// 广告Banner图片类型T_BaseData->ParentID=56
        /// </summary>
        public static int BannerType = 56;
        /// <summary>
        /// 准驾车型T_BaseData->ParentID=58
        /// </summary>
        public static int Car_Class = 58;
        /// <summary>
        /// 车辆类型T_BaseData->ParentID=76
        /// </summary>
        public static int Car_Type = 76;
    }
}
