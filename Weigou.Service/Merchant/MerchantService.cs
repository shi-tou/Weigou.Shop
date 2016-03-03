using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using Spring.Transaction.Interceptor;
using Weigou.Dao;
using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Service
{
    public class MerchantService : BaseService, IMerchantService
    {
        private IMerchantDao merchantDao;
        public IMerchantDao MerchantDao
        {
            set
            {
                merchantDao = value;
            }
        }
        /// <summary>
        /// 商户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantList(Pager p, Hashtable hs)
        {
            return merchantDao.GetMerchantList(p, hs);
        }
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int SetPictureTop(int ID, string merchantID, int Type)
        {
            return merchantDao.SetPictureTop(ID, merchantID, Type);
        }
        /// <summary>
        ///  图片
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPictureList(string code, int type)
        {
            return merchantDao.GetPictureList(code, type);
        }
        /// <summary>
        /// 回访列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantVisitList(Pager p, Hashtable hs)
        {
            return merchantDao.GetMerchantVisitList(p, hs);
        }
        /// <summary>
        /// 获取商户经营类目集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantGoodsTypeList(string MerchantID)
        {
            return merchantDao.GetMerchantGoodsTypeList(MerchantID);

        }
        /// <summary>
        /// 商户入驻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantSettledList(Pager p, Hashtable hs)
        {
            return merchantDao.GetMerchantSettledList(p, hs);
        }
        /// <summary>
        /// 入驻申请审核通过分配管理员帐号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [Transaction]
        public int AuditMerchantSettled(MerchantSettledInfo info)
        {
            int MerchantID = 0;
            #region 更新商户入驻申请表
            DataTable dt = GetDataByKey("T_MerchantApply", "ID", info.ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Status"] = info.Status;
            dr["ApprovedBy"] = info.ApprovedBy;
            dr["ApprovedTime"] = info.ApprovedTime;
            dr["ApprovedRemark"] = info.ApprovedRemark;
            dr["BussineType"] = info.BussineType;
            int res = UpdateDataTable(dt);
            #endregion

            #region 插入数据至商户表
            if (res > 0)
            {
                DataTable merchantdt = GetDataByKey("T_Merchant", "ID", 0);
                DataRow merchantdr;
                if (merchantdt.Rows.Count == 0)
                {
                    merchantdr = merchantdt.NewRow();
                    merchantdt.Rows.Add(merchantdr);
                }
                else
                {
                    merchantdr = merchantdt.Rows[0];
                }
                merchantdr["Name"] = dr["CompanyName"];
                merchantdr["ContactName"] = dr["ContactName"];
                merchantdr["ContactPhone"] = dr["ContactPhone"];
                merchantdr["ContactAddress"] = dr["ContactAddress"];
                merchantdr["BankType"] = dr["BankType"];
                merchantdr["BankNo"] = dr["BankCode"];
                merchantdr["CreateTime"] = DateTime.Now;
                merchantdr["CreateBy"] = info.ApprovedBy;
                merchantdr["Status"] = EnumStatus.Normal;
                MerchantID = Insert(merchantdt);

                #region 插入经营类目ID至关联表
                DataTable me_goodstypedt = GetDataByKey("T_MerchantGoodsType", "ID", 0);
                DataRow me_goodstypedr;
                foreach (var list in Convert.ToString(dr["BussineType"]).Split(','))
                {
                    me_goodstypedr = me_goodstypedt.NewRow();
                    me_goodstypedr["MerchantID"] = MerchantID;
                    me_goodstypedr["GoodsTypeID"] = list;
                    me_goodstypedt.Rows.Add(me_goodstypedr);
                }
                UpdateDataTable(me_goodstypedt);
                #endregion

            }
            #endregion

            #region 分配商户管理员帐号和角色
            DataTable userdt = GetDataByKey("T_User", "ID", 0);
            DataRow userdr;
            if (userdt.Rows.Count == 0)
            {
                userdr = userdt.NewRow();
                userdt.Rows.Add(userdr);
            }
            else
            {
                userdr = userdt.Rows[0];
            }
           // userdr["Type"] = EnumUserType.Merchant;
            userdr["MerchantID"] = MerchantID;
            userdr["Name"] = "商户管理员";
            userdr["UserName"] = info.ContactPhone;
            userdr["Password"] = "18CFAFE9ED8A29E9";
            userdr["RoleID"] = 10;
            userdr["Status"] = 1;
            userdr["CreateBy"] = info.ApprovedBy;
            userdr["CreateTime"] = DateTime.Now;
            res = UpdateDataTable(userdt);
            #endregion

            return res;
        }
        /// <summary>
        /// 添加店铺
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Transaction]
        public int AddMerchant(MerchantInfo info)
        {
            int res = 0;
            DataTable dt = GetDataByKey("T_Merchant", "ID", info.ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = info.CreateBy;
                dr["Status"] = Convert.ToInt16(EnumStatus.Normal);
            }
            //添加数据至商户表
            dr["Name"] = info.Name;
            dr["ContactName"] = info.ContactName;
            dr["ContactTel"] = info.ContactTel;
            dr["ContactPhone"] = info.ContactPhone;
            dr["ContactAddress"] = info.ContactAddress;
            dr["BankType"] = info.BankType;
            dr["BankNo"] = info.BankNo;
            dr["Email"] = info.Email;
            dr["SiteUrl"] = info.SiteUrl;
            dr["ProvinceID"] = info.ProvinceID;
            dr["CityID"] = info.CityID;
            dr["DistrictID"] = info.DistrictID;
            dr["Longitude"] = info.Longitude;
            dr["Latitude"] = info.Latitude;
            dr["SimpleDesc"] = info.SimpleDesc;
            dr["Description"] = info.Description;
            dr["ServiceDesc"] = info.ServiceDesc;
            dr["SpecialNote"] = info.SpecialNote;
            dr["Preferential"] = info.Preferential;
            dr["IsInternational"] = info.IsInternational;

            if (info.ID != "0")
            {
                res = UpdateDataTable(dt);
            }
            else
            {
                int merchantid = Insert(dt);

                //添加数据至店铺类目表
                DataTable me_goodstypedt = GetDataByKey("T_MerchantGoodsType", "ID", 0);
                DataRow me_goodstypedr;
                foreach (var item in info.GoodsTypeID.Split(','))
                {
                    me_goodstypedr = me_goodstypedt.NewRow();
                    me_goodstypedr["MerchantID"] = merchantid;
                    me_goodstypedr["GoodsTypeID"] = item;
                    me_goodstypedt.Rows.Add(me_goodstypedr);
                }
                UpdateDataTable(me_goodstypedt);

                //分配管理员帐号和密码
                DataTable userdt = GetDataByKey("T_User", "ID", 0);
                DataRow userdr;
                if (userdt.Rows.Count == 0)
                {
                    userdr = userdt.NewRow();
                    userdt.Rows.Add(userdr);
                }
                else
                {
                    userdr = userdt.Rows[0];
                }
               // userdr["Type"] = EnumUserType.Merchant;
                userdr["MerchantID"] = merchantid;
                userdr["Name"] = "商户管理员";
                userdr["UserName"] = info.Name;
                userdr["Password"] = "18CFAFE9ED8A29E9";
                userdr["RoleID"] = 10;
                userdr["Status"] = 1;
                userdr["CreateBy"] = info.CreateBy;
                userdr["CreateTime"] = DateTime.Now;
                res = UpdateDataTable(userdt);
            }
            return res;
        }
        /// <summary>
        /// 获取商户运费模版
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantLogisticsList(Pager p, Hashtable hs)
        {
            merchantDao.GetMerchantLogisticsList(p, hs);
            DataTable dt = p.DataSource;
            //增加省份列，将相关省份追加
            dt.Columns.Add("ProvinceName");
            dt.Columns.Add("LogisticsPrice");

            foreach (DataRow item in dt.Rows)
            {
                hs.Remove("MerchantLogisticsID");
                hs.Add("MerchantLogisticsID", item["ID"].ToString());
                string strLogisticsPrice = "";

                DataTable dtProvince = GetProvince(hs);
                foreach (DataRow row in dtProvince.Rows)
                {
                    if (item["ID"].ToString() == row["MerchantLogisticsID"].ToString())
                    {
                        //运费模版相同则追加
                        item["ProvinceName"] += row["ProvinceName"] + ",";
                    }
                    strLogisticsPrice = row["Price"].ToString();
                }
                string strProvinceNames = item["ProvinceName"].ToString();
                if (strProvinceNames != "")
                {
                    item["ProvinceName"] = strProvinceNames.Substring(0, strProvinceNames.Length - 1);
                }
                else
                {
                    item["ProvinceName"] = "全国";
                }
                item["LogisticsPrice"] = strLogisticsPrice;
            }
            p.DataSource = dt;
            return 0;
        }
        /// <summary>
        /// 获取运费相关省份
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetProvince(Hashtable hs)
        {
            return merchantDao.GetProvince(hs);
        }
        /// <summary>
        /// 获取组合经营类目名称
        /// </summary>
        /// <returns></returns>
        public string GetBussineName(string MerchantID)
        {
            string result = "";
            DataTable dt = GetMerchantGoodsTypeList(MerchantID);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count != 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == dt.Rows.Count - 1)
                        {
                            result += Convert.ToString(dt.Rows[i]["GoodsTypeName"]);
                        }
                        else
                        {
                            result += Convert.ToString(dt.Rows[i]["GoodsTypeName"]) + ",";
                        }
                    }
                }
                else
                {
                    result = Convert.ToString(dt.Rows[0]["GoodsTypeName"]);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取专场封面图片
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetSpecialPicture(Hashtable hs)
        {
            return merchantDao.GetSpecialPicture(hs);
        }
        /// <summary>
        /// 获取专场的店铺列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSpecialMerchantList(Pager p, Hashtable hs)
        {
            return merchantDao.GetSpecialMerchantList(p, hs);
        }
        /// <summary>
        /// 获取店铺设计列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetShopSignList(Hashtable hs)
        {
            return merchantDao.GetShopSignList(hs);
        }
        /// <summary>
        /// 查询公益基金和娱乐积分的返送比例
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable GetScale(string MerchantID)
        {
            return merchantDao.GetScale(MerchantID);
        }
        /// <summary>
        /// 检查该店铺是否为丝阁国际
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public int CheckIsInternational(string MerchantID)
        {
            return merchantDao.CheckIsInternational(MerchantID);
        }
        /// <summary>
        /// 检查该店铺是否支持试衣间功能
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable CheckMerchantSupportFitting(string MerchantID)
        {
            return merchantDao.CheckMerchantSupportFitting(MerchantID);
        }
        /// <summary>
        /// 获取店铺评分
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public decimal GetMerchantStar(string MerchantID)
        {
            return merchantDao.GetMerchantStar(MerchantID);
        }

    }
}
