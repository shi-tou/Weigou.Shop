using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Model;
using Weigou.Dao;
using Weigou.Common;
using System.Collections;
using Spring.Transaction.Interceptor;
using Weigou.Model.Enum;
using Newtonsoft.Json;
using Weigou.Easemob;

namespace Weigou.Service
{
    public class MemberService : BaseService, IMemberService
    {
        private IMemberDao memberDao;
        public IMemberDao MemberDao
        {
            set
            {
                memberDao = value;
            }
        }

        #region 会员相关
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int Register(MemberInfo info, string verifyCode)
        {
            if (!CheckMobile(0, info.MobileNo))
            {
                return RT.RESULT_MOBILENO_EXIST;
            }
            DataTable dt = GetDataByKey("T_Member", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["MobileNo"] = info.MobileNo;
            dr["Password"] = DESEncrypt.Encrypt(info.Password);
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            int memberID = Insert(dt);
            if (memberID > 0)
            {
                return RT.SUCCESS;
            }
            return RT.FAILED; ;
        }
        /// <summary>
        /// 会员注册获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetVerifyCodeForReg(string mobileNo)
        {
            if (!CheckMobile(0, mobileNo))
            {
                return RT.RESULT_MOBILENO_EXIST;
            }
            string verifyCode = Utils.CreateVerityCode(6);
            if (SaveVerifyCode(mobileNo, verifyCode) == RT.SUCCESS)
            {
                return SendSms(mobileNo, SmsTemplateConst.Sms_VerifyCodeForReg, new string[] { verifyCode });
            }
            else
            {
               return RT.FAILED;
            }
        }

        /// <summary>
        /// 修改密码获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetVerifyCodeForUpPwd(string mobileNo)
        {
            string verifyCode = Utils.CreateVerityCode(6);
           
            if (SaveVerifyCode(mobileNo, verifyCode) == RT.SUCCESS)
            {
                return SendSms(mobileNo, SmsTemplateConst.Sms_VerifyCodeForUpPwd, new string[] { mobileNo, verifyCode });
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        /// 修改身份证号获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetVerifyCodeForUpCardNo(string mobileNo)
        {
            string verifyCode = Utils.CreateVerityCode(6);           
            if (SaveVerifyCode(mobileNo, verifyCode) == RT.SUCCESS)
            {
                return SendSms(mobileNo, SmsTemplateConst.Sms_VerifyCodeForUpCardNo, new string[] { mobileNo, verifyCode });
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        /// 找回密码获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetVerifyCodeForFindPwd(string mobileNo)
        {
            if (CheckMobile(0, mobileNo))
            {
                return RT.RESULT_MOBILENO_EXIST;
            }
            string verifyCode = Utils.CreateVerityCode(6);
            if (SaveVerifyCode(mobileNo, verifyCode) == RT.SUCCESS)
            {
                return SendSms(mobileNo, SmsTemplateConst.Sms_VerifyCodeForFindPwd, new string[] { mobileNo, verifyCode });
            }
            else
            {
                return RT.FAILED;
            }
        }


        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int MemberLogin(string mobileOrEmail, string password, out MemberInfo info)
        {
            info = null;
            DataTable dt = GetMemberInfo(mobileOrEmail);
            if (dt.Rows.Count == 0)
            {
                return RT.RESULT_NOT_EXIST;//用户不存在
            }
            DataRow dr = dt.Rows[0];
            if (DESEncrypt.Encrypt(password) != dt.Rows[0]["Password"].ToString())
            {
                return RT.RESULT_ERROR_PASSWORD;//密码不正确
            }
            if (Convert.ToInt16(dr["Status"]) == (int)EnumMemStatus.Locked)
            {
                return RT.RESULT_LOCK;
            }
            info = ObjectHelper.CopyToObject<MemberInfo>(dr);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberList(Pager p, Hashtable hs)
        {
            return memberDao.GetMemberList(p, hs);
        }
        /// <summary>
        /// 根据id获取会员
        /// </summary>
        /// <returns></returns>
        public DataTable GetMemberInfo(int memberID)
        {
            return memberDao.GetMemberInfo(memberID);
        }
        /// <summary>
        /// 根据卡号或手机号获取会员
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetMemberInfo(string mobileOrEmail)
        {
            return memberDao.GetMemberInfo(mobileOrEmail);
        }
        /// <summary>
        /// 后台添加会员
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0-成功 1-用户名已注册 2-手机号已注册 </returns>
        [Transaction]
        public int AddMember(MemberInfo info)
        {
            if (CheckEmail(info.ID, info.Email) > 0)
            {
                return RT.RESULT_EMAIL_EXIST;
            }
            if (!CheckMobile(info.ID, info.MobileNo))
            {
                return RT.RESULT_MOBILENO_EXIST;
            }
            int memberID = 0;
            info.Password = DESEncrypt.Encrypt(info.Password);
            DataTable dt = ObjectHelper.CopyToDataTable<MemberInfo>(info, "T_Member");
            DataRow dr = dt.Rows[0];
            dr["ID"] = DBNull.Value;
            dr["CreateTime"] = DateTime.Now;
            dr["ApprovedTime"] = DBNull.Value;
            dr["ApprovedBy"] = DBNull.Value;
            memberID = Insert(dt);
            if (memberID > 0)
            {
                RegisterEasemobUser(memberID,info.Password);
                SaveSysLog(memberID.ToString(), EnumModule.MemberManage,
                    EnumOperation.Add, info.CreateBy,
                    string.Format("{0}了姓名为{1}的会员", "添加", info.Name));
                return RT.SUCCESS;
            }
            else
                return RT.FAILED;
        }
        /// <summary>
        /// 注册环信用户
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int RegisterEasemobUser(int memberID, string pwd)
        {
            EaseMobHelper easemob = new EaseMobHelper();
            Req_UserInfo info = new Req_UserInfo();
            info.username = "wl" + DateTime.Now.ToString("yyyyMMddHHmmss");
            info.password = pwd;
            Res_UserInfo res = easemob.AddUser(info);
            if (res != null && res.entities != null && res.entities.Count > 0)
            {
                DataTable dt = GetDataByKey("T_EasemobUser", "UserName", "0");
                DataRow dr = dt.NewRow();
                dr["UserName"] = info.username;
                dr["MemberID"] = memberID;
                dr["Status"] = 1;
                dr["CreataTime"] = DateTime.Now;
                dt.Rows.Add(dr);
                if (UpdateDataTable(dt) > 0)
                {
                    return RT.SUCCESS;
                }
                else
                {
                    return RT.FAILED;
                }
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0-成功 1-失败 </returns>
        public int UpdateMember(MemberInfo info)
        {
            DataTable dt = GetDataByKey("T_Member", "ID", info.ID);
            if (dt.Rows.Count == 0)
            {
                return RT.RESULT_NOT_EXIST;
            }
            DataRow dr = dt.Rows[0];
            if (!string.IsNullOrEmpty(info.Name))
            {
                dr["Name"] = info.Name;
            }
            if (info.LevelID > 0)
            {
                dr["LevelID"] = info.LevelID;
            }
            if (!string.IsNullOrEmpty(info.MobileNo))
            {
                dr["MobileNo"] = info.MobileNo;
            }
           
            if (!string.IsNullOrEmpty(info.Email))
            {
                dr["Email"] = info.Email;
            }
            if (info.Sex > 0)
            {
                dr["Sex"] = info.Sex;
            }
            if (!string.IsNullOrEmpty(info.Birthday))
            {
                dr["Birthday"] = info.Birthday;
            }
            if (info.ProvinceID > 0)
            {
                dr["ProvinceID"] = info.ProvinceID;
            }
            if (info.CityID > 0)
            {
                dr["CityID"] = info.CityID;
            }
            if (info.DistrictID > 0)
            {
                dr["DistrictID"] = info.DistrictID;
            }
            if (!string.IsNullOrEmpty(info.Photo))
            {
                dr["Photo"] = info.Photo;
            }
            if (info.Education > 0)
            {
                dr["Education"] = info.Education;
            }
            if (info.Occupation > 0)
            {
                dr["Occupation"] = info.Occupation;
            }
            if (!string.IsNullOrEmpty(info.Hobby))
            {
                dr["Hobby"] = info.Hobby;
            }
            if (!string.IsNullOrEmpty(info.IdCard))
            {
                dr["IdCard"] = info.IdCard;
            }
            dr["Status"] = info.Status;
            int res = UpdateDataTable(dt);
            if (res > 0)
            {
                SaveSysLog(info.ID.ToString(), EnumModule.MemberManage,
                    (info.ID == 0 ? EnumOperation.Add : EnumOperation.Edit),
                    info.CreateBy,
                    string.Format("修改了姓名为{0}的会员的信息", info.Name));
                return RT.SUCCESS;
            }
            else
                return RT.FAILED;
        }
        /// <summary>
        /// 获取准驾车型信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCarClassList()
        {
            DataTable dt = GetDataByKey("T_BaseData", "ParentID", BaseDataConst.Car_Class);
            return dt;
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobileNo"></param>
        /// <returns>0- 邮箱可用 1-邮箱已存在</returns>
        public int CheckEmail(int id, string email)
        {
            DataTable dt = GetDataByKey("T_Member", "Email", email);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            if (id.ToString() != dt.Rows[0]["ID"].ToString())
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobileNo"></param>
        /// <returns>0- 手机可用 1-手机已注册</returns>
        public bool CheckMobile(int id, string mobileNo)
        {
            DataTable dt = GetDataByKey("T_Member", "MobileNo", mobileNo);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            if (id.ToString() == dt.Rows[0]["ID"].ToString())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改/重置密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="type"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Transaction]
        public int ChangePassword(PasswordInfo info)
        {
            DataTable dt = GetDataByKey("T_Member", "ID", info.MemberID);
            if (dt.Rows.Count == 0)
                return RT.RESULT_NOT_EXIST;
            DataRow dr = dt.Rows[0];
            //如果是修改密码，则验证旧密码
            if (info.Type == 1)
            {
                if (DESEncrypt.Encrypt(info.OldPassword) != Convert.ToString(dr["Password"]))
                {
                    return RT.RESULT_ERROR_PASSWORD;
                }
            }
            dr["Password"] = DESEncrypt.Encrypt(info.NewPassword);
            int res = UpdateDataTable(dt);
            if (res > 0)
            {
                SaveSysLog(info.MemberID.ToString(), EnumModule.MemberManage, EnumOperation.Edit, info.UserID, "修改了卡号为" + dr["CardNo"].ToString() + "的会员密码");
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        ///  找回密码
        /// </summary>
        /// <param name="info"></param> 
        /// <returns></returns>
        public int FindPassword(PasswordInfo info, string verifyCode)
        {
            DataTable dt = GetDataByKey("T_Member", "MobileNo", info.MobileNo);
            if (dt.Rows.Count == 0)
                return RT.RESULT_NOT_EXIST;
            DataRow dr = dt.Rows[0];

            dr["Password"] = DESEncrypt.Encrypt(info.NewPassword);
            int res = UpdateDataTable(dt);
            if (res > 0)
            {
                SaveSysLog(info.MemberID.ToString(), EnumModule.MemberManage, EnumOperation.Edit, info.UserID, "找回并修改了手机号为" + dr["MobileNo"].ToString() + "的会员密码");

                UpdateVerifyCode(info.MobileNo, verifyCode);
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        ///  修改备用联系手机号
        /// </summary>
        /// <param name="info"></param> 
        /// <returns></returns>
        public int ReserveMemberNo(string memberID, string reserveMemberNo)
        {
            DataTable dt = GetDataByKey("T_Member", "ID", memberID);
            if (dt.Rows.Count == 0)
                return RT.RESULT_NOT_EXIST;
            DataRow dr = dt.Rows[0];

            dr["ReserveMemberNo"] = reserveMemberNo;
            int res = UpdateDataTable(dt);
            if (res > 0)
            {
                SaveSysLog(memberID.ToString(), EnumModule.MemberManage, EnumOperation.Edit, 0, string.Format("修改了手机号为{0}的会员的备用手机号", dr["MobileNo"].ToString()));
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }
        /// <summary>
        /// 校验会员密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int CheckMemberPwd(int memberID, string pwd)
        {
            DataTable dt = GetDataByKey("T_Member", "ID", memberID);
            if (DESEncrypt.Encrypt(pwd) != dt.Rows[0]["Password"].ToString())
            {
                return RT.RESULT_ERROR_PASSWORD;//密码不正确
            }
            return RT.SUCCESS;
        }
        /// <summary>
        /// 会员申请解冻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberUnlockList(Pager p, Hashtable hs)
        {
            return memberDao.GetMemberUnlockList(p, hs);
        }
        /// <summary>
        /// 审核申请解冻会员
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Transaction]
        public int AuditMemberUnlock(int memberID, int status, int approvedBy, string remark)
        {
            DataTable dt = GetDataByKey("T_MemberUnlock", "MemberID", memberID);
            if (dt.Rows.Count == 0)
                return RT.FAILED;
            dt.Rows[0]["Status"] = status;
            dt.Rows[0]["ApprovedBy"] = approvedBy;
            dt.Rows[0]["ApprovedTime"] = DateTime.Now;
            dt.Rows[0]["ApprovedRemark"] = remark;
            if (UpdateDataTable(dt) > 0)
            {
                DataTable dtM = GetDataByKey("T_MemberUnlock", "MemberID", memberID);
                dtM.Rows[0]["Status"] = EnumStatus.Normal;
                UpdateDataTable(dtM);
                return RT.SUCCESS;
            }
            return RT.FAILED;

        }
        #endregion

        #region 会员账户管理
        /// <summary>
        /// 会员账户余额
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roletype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public decimal GetAccountBalance(int memberID)
        {
            return memberDao.GetAccountBalance(memberID);
        }
        /// <summary>
        /// 获取会员账户明细
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetAccountList(Pager p, Hashtable hs)
        {
            return memberDao.GetAccountList(p, hs);
        }
        #endregion

        #region 会员收藏（商品/店铺）
        /// <summary>
        /// 添加商品/店铺收藏
        /// </summary>
        /// <param name="targetID"></param>
        /// <param name="type"></param>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int AddFavorite(string targetID, string type, int memberID)
        {
            int res = RT.FAILED;
            if (IsFavorite(memberID, EnumFavType.Car, targetID))
            {
                res = RT.RESULT_EXIST;
            }
            else
            {
                DataTable fadt = GetDataByKey("T_Favorite", "ID", 0);
                DataRow dr;
                if (fadt.Rows.Count == 0)
                {
                    dr = fadt.NewRow();
                    fadt.Rows.Add(dr);
                    dr["CreateTime"] = DateTime.Now;
                    dr["MemberID"] = memberID;
                    dr["Type"] = type;
                    dr["TargetID"] = targetID;
                    res = UpdateDataTable(fadt);
                }
            }
            return res;
        }
        /// <summary>
        /// 检验是否收藏
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="type"></param>
        /// <param name="targetID"></param>
        /// <returns>true-已收藏 false-未收藏</returns>
        public bool IsFavorite(int memberID, EnumFavType type, string targetID)
        {
            Hashtable hs = new Hashtable();
            hs.Add("MemberID", memberID);
            hs.Add("Type", (int)type);
            hs.Add("TargetID", targetID);
            DataTable dt = GetDataByWhere("T_Favorite", hs);
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 会员收藏列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int GetFavoriteList(Pager p, Hashtable hs)
        {
            return memberDao.GetFavoriteList(p, hs);
        }
        #endregion

        #region 会员评论车辆/商品
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        public int AddComment(CommentInfo info)
        {
            DataTable dt = GetDataByKey("T_Comment", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["Type"] = info.Type;
            dr["ParentID"] = info.ParentID;
            dr["OrderID"] = info.OrderID;
            dr["TargetID"] = info.TargetID;
            dr["MemberID"] = info.MemberID;
            dr["Star"] = info.Star;
            dr["Content"] = info.Content;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            int res = UpdateDataTable(dt);
            if (res > 0)
                return RT.SUCCESS;
            else
                return RT.FAILED;
        }
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetCommentsList(Pager p, Hashtable hs)
        {
            return memberDao.GetCommentsList(p, hs);
        }
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsCommentInfoByID(Pager p, Hashtable hs)
        {
            return memberDao.GetGoodsCommentInfoByID(p, hs);
        }
        #endregion

        #region 收货地址

        /// <summary>
        /// 获取收货地址列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetDeliverAddressList(Pager p, Hashtable hs)
        {
            return memberDao.GetDeliverAddressList(p, hs);
        }

        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetDeliverAddress(Hashtable hs)
        {
            return memberDao.GetDeliverAddress(hs);
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Transaction]
        public int SaveDeliverAddress(DeliverAddressInfo info)
        {
            if (info.IsDefault == 1)
            {
                DataTable dt = GetDataByKey("T_DeliverAddress", "MemberID", info.MemberID);
                foreach (DataRow dr in dt.Rows)
                {
                    dr["IsDefault"] = 0;
                }
                UpdateDataTable(dt);
            }
            DataTable dtAddress = GetDataByKey("T_DeliverAddress", "ID", info.ID);
            DataRow drA;
            if (dtAddress.Rows.Count > 0)
            {
                drA = dtAddress.Rows[0];
            }
            else
            {
                drA = dtAddress.NewRow();
                dtAddress.Rows.Add(drA);
            }
            drA["MemberID"] = info.MemberID;
            drA["ProvinceID"] = info.ProvinceID;
            drA["CityID"] = info.CityID;
            drA["DistrictID"] = info.DistrictID;
            drA["Address"] = info.Address;
            drA["ZipCode"] = info.ZipCode;
            drA["ConsigneeName"] = info.ConsigneeName;
            drA["ConsigneeMobileNo"] = info.ConsigneeMobileNo;
            drA["IsDefault"] = info.IsDefault;
            drA["Status"] = 1;
            if (UpdateDataTable(dtAddress) > 0)
            {
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }

        /// 设置默认收货地址
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int SetDefaultAddress(int id, int memberID)
        {
            DataTable dt = memberService.GetDataByKey("T_DeliverAddress", "MemberID", memberID);
            foreach (DataRow dr in dt.Rows)
            {
                dr["IsDefault"] = 0;
            }
            UpdateDataTable(dt);
            DataTable dtAddress = GetDataByKey("T_DeliverAddress", "ID", id);
            DataRow drA;
            if (dtAddress.Rows.Count > 0)
            {
                drA = dtAddress.Rows[0];
            }
            else
            {
                drA = dtAddress.NewRow();
                dtAddress.Rows.Add(drA);
            }
            drA["IsDefault"] = 1;
            if (UpdateDataTable(dtAddress) > 0)
            {
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int DeleteDeliverAddress(int id)
        {
            DataTable dt = GetDataByKey("T_DeliverAddress", "ID", id);
            DataRow dr = dt.Rows[0];
            dr["Status"] = EnumStatus.Delete;
            UpdateDataTable(dt);
            return RT.SUCCESS;
        }

        #endregion

        #region 购物车相关
        /// <summary>
        /// 添加购物车入口
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="memberID">会员ID</param>
        /// <param name="count">购买数量</param>
        /// <returns></returns>
        [Transaction]
        public int AddShoppingCart(int goodsID, int memberID, int count)
        {
            Hashtable hs = new Hashtable();
            hs.Add("GoodsID", goodsID);
            hs.Add("MemberID", memberID);
            DataTable dt = GetDataByWhere("T_ShoppingCart", hs);
            DataRow dr;
            //已存在则更新数量
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                dr["Count"] = Convert.ToInt32(dr["Count"]) + count;
            }
            else//不存在则新增
            {
                dr = dt.NewRow();
                dr["Count"] = count;
                dr["MemberID"] = memberID;
                dr["GoodsID"] = goodsID;
                dt.Rows.Add(dr);
            }
            if (UpdateDataTable(dt) > 0)
            {
                return RT.SUCCESS;
            }
            return RT.FAILED;
        }
        /// <summary>
        /// 修改购物车数量
        /// </summary>
        /// <param name="cartID">购物车ID</param>
        /// <param name="count">更改数据</param>
        /// <returns></returns>
        [Transaction]
        public int UpdateShoppingCart(int cartID, int count)
        {
            DataTable dt = GetDataByKey("T_ShoppingCart", "ID", cartID);
            if (dt.Rows.Count == 0)
                return RT.FAILED;
            DataRow dr = dt.Rows[0];
            dr["Count"] = Convert.ToInt32(dr["Count"]) + count;
            if (UpdateDataTable(dt) > 0)
            {
                return RT.SUCCESS;
            }
            return RT.FAILED;
        }
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="strMemberID">会员ID</param>
        /// <returns></returns>
        public DataTable GetShoppingCartList(int memberID)
        {
            return memberDao.GetShoppingCartList(memberID);
        }
        #endregion

        #region 会员级别
        /// <summary>
        /// 会员级别列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        public int GetMemberLevelList(Pager p, Hashtable hs)
        {
            return memberDao.GetMemberLevelList(p, hs);
        }
         
        public string GetLevelName(string LevelID)
        {
           DataTable dt = memberDao.GetDataByKey("T_MemberLevel", "ID", LevelID);
           if (dt != null && dt.Rows.Count > 0)
               return dt.Rows[0]["Name"].ToString();
           else
               return "无数据";
        }
        #endregion

        #region 环信IM管理
        /// <summary>
        /// 环信用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetEasemobUserList(Pager p, Hashtable hs)
        {
            return memberDao.GetEasemobUserList(p, hs);
        }
        /// <summary>
        /// 获取环信群组
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetEasemobGroupList(Pager p, Hashtable hs)
        {
            return memberDao.GetEasemobGroupList(p, hs);
        }
        #endregion
    }
}
