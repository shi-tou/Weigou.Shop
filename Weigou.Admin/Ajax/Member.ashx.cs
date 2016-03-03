using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weigou.Service;
using Weigou.Common;
using System.Text;
using System.Web.SessionState;
using Weigou.Model;
using Weigou.Model.Enum;
using System.Collections.Generic;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Member : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 会员管理
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="hc"></param>
        public void CheckMemName(HttpContext hc)
        {
            string name = GetRequest("Name", "");
            string id = GetRequest("ID", "");
            DataTable dt = memberService.GetDataByKey("T_Member", "Name", name);
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ID"].ToString() != id)
                {
                    res = 1;
                }
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 会员列表
        /// </summary>
        public void GetMemberList(HttpContext hc)
        {
            string userName = GetRequest("UserName", "");
            string name = GetRequest("Name", "");
            string mobileNo = GetRequest("MobileNo", "");
            string sex = GetRequest("Sex", "");
            string status = GetRequest("Status", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (userName != "")
            {
                hs.Add("UserName", userName);
            }
            if (name != "")
            {
                hs.Add("Name", name);
            }
            if (mobileNo != "")
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (sex != "")
            {
                hs.Add("Sex", sex);
            }
            if (status != "")
            {
                hs.Add("Status", status);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            memberService.GetMemberList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteMember(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            DataTable dt = memberService.GetDataByKey("T_Member", "ID", id);
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                res = memberService.UpdateDataTable(dt);
                if (res > 0)
                    goodsService.SaveSysLog(id.ToString(), EnumModule.MemberManage, EnumOperation.Delete, UserInfo.ID, "删除会员：【" + dt.Rows[0]["Name"] + "】");
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
       
        /// <summary>
        /// 修改/重置密码
        /// </summary>
        /// <param name="hc"></param>
        public void ChangePassword(HttpContext hc)
        {
            int type = GetRequest("Type",1);
            int memberID = GetRequest("MemberID",0);
            string oldPwd = GetRequest("OldPassword","");
            string newPwd = GetRequest("NewPassword", "");
            PasswordInfo info = new PasswordInfo();
            info.MemberID = memberID;
            info.Type = type;
            info.OldPassword = oldPwd;
            info.NewPassword = newPwd;
            info.UserID = UserInfo.ID;
            int res = memberService.ChangePassword(info);
            ResponseWrite(hc, res.ToString());
        }
        
        /// <summary>
        /// 会员申请解冻列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetMemberUnlockList(HttpContext hc)
        {
            string name = GetRequest("MemberName", "");
            string mobileNo = GetRequest("MobileNo", "");
            string status = GetRequest("Status", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (name != "")
            {
                hs.Add("MemberName", name);
            }
            if (mobileNo != "")
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (status != "")
            {
                hs.Add("Status", status);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            memberService.GetMemberUnlockList(p, hs);
            ResponseWrite(hc, p);
        }

        #endregion

        #region 会员级别
        /// <summary>
        /// 会员列表
        /// </summary>
        public void GetMemberLevelList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();           
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            memberService.GetMemberLevelList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteMemberLevel(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            DataTable dt = memberService.GetDataByKey("T_MemberLevel", "ID", id);
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                res = memberService.UpdateDataTable(dt);
                if (res > 0)
                    memberService.SaveSysLog(id.ToString(), EnumModule.MemberManage, EnumOperation.Delete, UserInfo.ID, "删除会员级别：【" + dt.Rows[0]["Name"] + "】");
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion
    }
}
